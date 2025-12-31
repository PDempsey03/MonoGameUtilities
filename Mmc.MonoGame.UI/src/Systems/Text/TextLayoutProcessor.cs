using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Models.Text;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Mmc.MonoGame.UI.Systems.Text
{
    public static class TextLayoutProcessor
    {
        /// <summary>
        /// Parses and tokenizes rich text into measured words in an unwrapped manner.
        /// </summary>
        /// <param name="text">rich text to reformat.</param>
        /// <param name="fontFamily">font family used to format rich text.</param>
        /// <param name="defaultColor">default color for text.</param>
        /// <returns>list of unwrapped MeasuredWords.</returns>
        public static List<MeasuredWord> ProcessText(string text, FontFamily fontFamily, Color defaultColor)
        {
            return TokenizeTextRunSegments(ParseText(text, fontFamily, defaultColor));
        }

        /// <summary>
        /// Reformats raw rich text into list of text run segments.
        /// </summary>
        /// <param name="text">rich text to reformat.</param>
        /// <param name="fontFamily">font family used to format rich text.</param>
        /// <param name="defaultColor">default color for text.</param>
        /// <returns>list of TextRunSegments</returns>
        public static List<TextRunSegment> ParseText(string text, FontFamily fontFamily, Color defaultColor)
        {
            var runs = new List<TextRunSegment>();

            var currentText = new StringBuilder();

            // track state
            bool isBold = false;
            bool isItalic = false;
            bool isUnderlined = false;
            Color currentColor = defaultColor;

            for (int i = 0; i < text.Length; i++)
            {
                // handle escape character so you can do things like \[b] amd still have it display [b]
                if (text[i] == '\\' && i + 1 < text.Length)
                {
                    currentText.Append(text[i + 1]);
                    i++; // need to not double count the next character
                    continue;
                }

                // check for tag start
                if (text[i] == '[')
                {
                    int closingBracketIndex = text.IndexOf(']', i);
                    if (closingBracketIndex != -1)
                    {
                        // get the tag
                        string tag = text.Substring(i + 1, closingBracketIndex - i - 1).ToLower();

                        // submit previous text to a new run
                        if (currentText.Length > 0)
                        {
                            string displayText = currentText.ToString();
                            SpriteFont selectedFont = fontFamily.GetFont(isBold, isItalic);
                            runs.Add(new TextRunSegment()
                            {
                                Text = displayText,
                                Font = selectedFont,
                                Color = currentColor,
                                IsUnderlined = isUnderlined,
                                PositionOffset = Vector2.Zero, // placeholder until refactor
                                Size = selectedFont.MeasureString(displayText)
                            });

                            currentText.Clear();
                        }

                        // process the tag
                        switch (tag)
                        {
                            case "b": isBold = true; break;
                            case "/b": isBold = false; break;

                            case "i": isItalic = true; break;
                            case "/i": isItalic = false; break;

                            case "u": isUnderlined = true; break;
                            case "/u": isUnderlined = false; break;

                            case string colorData when colorData.StartsWith("c="):
                                if (!TryParseColor(colorData[2..], out currentColor))
                                    currentColor = defaultColor;
                                break;
                            case "/c": currentColor = defaultColor; break;
                        }

                        i = closingBracketIndex;
                        continue;
                    }
                }

                currentText.Append(text[i]);
            }

            if (currentText.Length > 0)
            {
                string displayText = currentText.ToString();
                SpriteFont selectedFont = fontFamily.GetFont(isBold, isItalic);
                runs.Add(new TextRunSegment()
                {
                    Text = displayText,
                    Font = selectedFont,
                    Color = currentColor,
                    IsUnderlined = isUnderlined,
                    PositionOffset = Vector2.Zero, // placeholder until refactor
                    Size = selectedFont.MeasureString(displayText)
                });
            }

            return runs;
        }

        /// <summary>
        /// Reformats list of text run segments into a list of measured words.
        /// </summary>
        /// <param name="segments">text run segments to reformat.</param>
        /// <returns>list of unwrapped MeasuredWords.</returns>
        public static List<MeasuredWord> TokenizeTextRunSegments(List<TextRunSegment> segments)
        {
            List<MeasuredWord> words = [];

            MeasuredWord currentWord = new MeasuredWord();

            foreach (var segment in segments)
            {
                string[] parts = segment.Text.Split(' ');

                for (int i = 0; i < parts.Length; i++)
                {
                    bool isLastPart = i == parts.Length - 1;

                    string currentText = isLastPart ? parts[i] : parts[i] + " ";

                    if (string.IsNullOrEmpty(currentText) && !isLastPart) currentText = " ";

                    var newSegment = new TextRunSegment()
                    {
                        Text = currentText,
                        Font = segment.Font,
                        Color = segment.Color,
                        IsUnderlined = segment.IsUnderlined,
                        PositionOffset = Vector2.Zero,
                        Size = segment.Font.MeasureString(currentText)
                    };

                    currentWord.Segments.Add(newSegment);

                    if (!isLastPart)
                    {
                        words.Add(currentWord);
                        currentWord = new MeasuredWord();
                    }
                }
            }

            // add final word
            if (currentWord.Segments.Count > 0) words.Add(currentWord);

            return words;
        }

        /// <summary>
        /// Wraps words by applying offsets to each segment inside of the measured words.
        /// </summary>
        /// <param name="words">list of measured words which need to be wrapped.</param>
        /// <param name="maxWidth">max width to display words on to.</param>
        /// <returns>size of the resulting wrap.</returns>
        public static Vector2 WrapWords(List<MeasuredWord> words, float maxWidth)
        {
            // cursor keeps track of where the next character should be placed
            Vector2 cursor = Vector2.Zero;
            float currentLineHeight = 0;
            float maxSeenWidth = 0;

            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];

                var wordWidth = word.Segments.Sum(s => s.Font.MeasureString(s.Text).X);

                // if the word cant fit, move the cursor down to the next line
                if (cursor.X + wordWidth > maxWidth && cursor.X > 0)
                {
                    maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);
                    cursor.X = 0;
                    cursor.Y += currentLineHeight;
                    currentLineHeight = 0;
                }

                var segments = word.Segments;

                // edge case if word is too large to fit on one line
                if (wordWidth > maxWidth)
                {
                    HandleWrapLargeWord(word, maxWidth, cursor, maxSeenWidth, out cursor, out maxSeenWidth);
                }
                else
                {
                    for (int j = 0; j < segments.Count; j++)
                    {
                        var segment = segments[j];
                        // update the offset of the segment to where the cursor currently is which is where the next character should go
                        segment.PositionOffset = cursor;

                        cursor.X += segment.Size.X;
                        currentLineHeight = Math.Max(currentLineHeight, segment.Size.Y);
                    }
                }
            }

            maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);

            // returns how much space was taken up
            return new Vector2(maxSeenWidth, cursor.Y + currentLineHeight);
        }

        /// <summary>
        /// Handles breaking up of large words which need wrapped.
        /// </summary>
        /// <param name="measuredWord">word to break up and wrap.</param>
        /// <param name="maxWidth">max width to take up.</param>
        /// <param name="cursor">where the cursor started.</param>
        /// <param name="maxSeenWidth">largest width seen so far.</param>
        /// <param name="newCursorPosition">cursor position at the end of the wrap.</param>
        /// <param name="newMaxSeenWidth">max seen width at the end of the wrap.</param>
        private static void HandleWrapLargeWord(MeasuredWord measuredWord, float maxWidth, Vector2 cursor, float maxSeenWidth, out Vector2 newCursorPosition, out float newMaxSeenWidth)
        {
            var segments = measuredWord.Segments;
            float currentLineHeight = 0;

            // loop through all segments to accumulate their characters
            for (int j = 0; j < segments.Count; j++)
            {
                var segment = segments[j];

                currentLineHeight = Math.Max(currentLineHeight, segment.Size.Y);

                for (int k = 0; k < segment.Text.Length; k++)
                {
                    // find width of the current character
                    char c = segment.Text[k];
                    float charWidth = segment.Font.MeasureString(c.ToString()).X;

                    // if this character makes it cross the max width, then everything before it in the segment can stay on the same line
                    // and everything after it must be moved down to the next line
                    if (cursor.X + charWidth > maxWidth && cursor.X > 0)
                    {
                        maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);
                        cursor.X = 0;

                        // text for the new segments
                        string firstHalfText = segment.Text.Substring(0, k);
                        string secondHalfText = segment.Text.Substring(k);

                        // update the existing segment to keep the first half
                        segment.PositionOffset = cursor;
                        segment.Text = firstHalfText;
                        segment.Size = segment.Font.MeasureString(segment.Text);

                        // update the cursor for the second half of the segment
                        cursor.Y += currentLineHeight;

                        // create new segment to reprsent the second half of the sement
                        SpriteFont newFont = segment.Font;
                        TextRunSegment secondHalfSplitSegment = new TextRunSegment()
                        {
                            Text = secondHalfText,
                            Font = segment.Font,
                            Color = segment.Color,
                            IsUnderlined = segment.IsUnderlined,
                            PositionOffset = cursor,
                            Size = newFont.MeasureString(secondHalfText)
                        };

                        // add the segment to the list
                        segments.Insert(j + 1, secondHalfSplitSegment);

                        // break out of the character loop, because it will then move on to the next segment (the second half segment) and perform the
                        // same operations on it, which is how multiple splits of the same word are handled
                        break;
                    }

                    // always move the cursor over one after a character doesnt cause a new line
                    cursor.X += charWidth;
                }
            }


            // update the outgoing variable values
            newMaxSeenWidth = maxSeenWidth;

            newCursorPosition = cursor;
        }

        /// <summary>
        /// Shifts segments to match the requirements of the text alignment.
        /// </summary>
        /// <param name="measuredWords">words to align.</param>
        /// <param name="contentBounds">bounds to align to.</param>
        /// <param name="textHorizontalAlignment">horizontal text alignment</param>
        /// <param name="textVerticalAlignment">vertical text alignment</param>
        public static void ApplyTextAlignment(List<MeasuredWord> measuredWords, Rectangle contentBounds,
            TextHorizontalAlignment textHorizontalAlignment, TextVerticalAlignment textVerticalAlignment)
        {
            var segments = measuredWords.SelectMany(w => w.Segments);

            ApplyHorizontalTextAlignment(segments, contentBounds.Width, textHorizontalAlignment);

            ApplyVerticalTextAlignment(segments, contentBounds.Height, textVerticalAlignment);
        }

        /// <summary>
        /// Shift segments horizontally to match the requirements of the horizontal text alignment.
        /// </summary>
        /// <param name="segments">segments to shift.</param>
        /// <param name="contentWidth">width of the content box.</param>
        /// <param name="textHorizontalAlignment">horizontal text alignment.</param>
        private static void ApplyHorizontalTextAlignment(IEnumerable<TextRunSegment> segments, float contentWidth, TextHorizontalAlignment textHorizontalAlignment)
        {
            if (textHorizontalAlignment == TextHorizontalAlignment.Center)
            {
                var lines = segments.GroupBy(s => s.PositionOffset.Y);

                foreach (var line in lines)
                {
                    float lineWidth = line.Sum(l => l.Size.X);
                    float xOffset = (contentWidth - lineWidth) / 2;
                    foreach (var segment in line)
                    {
                        segment.PositionOffset.X += xOffset;
                    }
                }
            }
            else if (textHorizontalAlignment == TextHorizontalAlignment.Right)
            {
                var lines = segments.GroupBy(s => s.PositionOffset.Y);

                foreach (var line in lines)
                {
                    float lineWidth = line.Sum(l => l.Size.X);

                    // handle trailing spaces
                    var lastSegment = line.Last();
                    if (lastSegment.Text.EndsWith(' ')) lineWidth -= lastSegment.Font.MeasureString(" ").X;

                    float xOffset = contentWidth - lineWidth;
                    foreach (var segment in line)
                    {
                        segment.PositionOffset.X += xOffset;
                    }
                }
            }
        }

        /// <summary>
        /// Shift segments horizontally to match the requirements of the vertical text alignment.
        /// </summary>
        /// <param name="segments">segments to shift.</param>
        /// <param name="contentHeight">height of the content box.</param>
        /// <param name="textVerticalAlignment">vertical text alignment.</param>
        private static void ApplyVerticalTextAlignment(IEnumerable<TextRunSegment> segments, float contentHeight, TextVerticalAlignment textVerticalAlignment)
        {
            if (textVerticalAlignment == TextVerticalAlignment.Center)
            {
                // shift everything by half the remaining distance
                var wordsHeight = segments.Max(s => s.PositionOffset.Y + s.Size.Y) - segments.Min(s => s.PositionOffset.Y);
                var verticalOffset = (contentHeight - wordsHeight) / 2;
                foreach (var segment in segments)
                {
                    segment.PositionOffset.Y += verticalOffset;
                }
            }
            else if (textVerticalAlignment == TextVerticalAlignment.Bottom)
            {
                // shift everything by the total remaining distance
                var wordsHeight = segments.Max(s => s.PositionOffset.Y + s.Size.Y) - segments.Min(s => s.PositionOffset.Y);
                var verticalOffset = contentHeight - wordsHeight;
                foreach (var segment in segments)
                {
                    segment.PositionOffset.Y += verticalOffset;
                }
            }
        }

        /// <summary>
        /// Attemp to parse a string into a color via either, hex, rgb, or name.
        /// </summary>
        /// <param name="value">string to parse.</param>
        /// <param name="color">resulting color.</param>
        /// <returns>Whether or not the parse was succesful.</returns>
        public static bool TryParseColor(string value, out Color color)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                color = default;
                return false;
            }

            // handle hex
            if (value.StartsWith('#'))
            {
                if (uint.TryParse(value.Replace("#", ""), NumberStyles.HexNumber, null, out uint rgba))
                {
                    color = new Color(rgba);
                    return true;
                }
            }

            // handle rgb
            if (value.Contains(','))
            {
                var parts = value.Split(',');
                if (parts.Length == 3 &&
                    byte.TryParse(parts[0], out byte r) &&
                    byte.TryParse(parts[1], out byte g) &&
                    byte.TryParse(parts[2], out byte b))
                {
                    color = new Color(r, g, b);
                    return true;
                }
                else
                {
                    color = default;
                    return false;
                }
            }

            // handle names
            var possibleColorProperty = typeof(Color).GetProperty(value, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (possibleColorProperty != null && possibleColorProperty.PropertyType == typeof(Color))
            {
                color = (Color)possibleColorProperty.GetValue(null)!;
                return true;
            }

            color = default;
            return false;
        }
    }
}
