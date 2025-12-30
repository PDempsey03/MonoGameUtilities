using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Mmc.MonoGame.UI.Primitives.Text
{
    public static class TextLayoutProcessor
    {
        /// <summary>
        /// Reformats raw rich text into list of text run segments.
        /// </summary>
        /// <param name="text">Rich text to reformat.</param>
        /// <param name="fontFamily">Font family used to format rich text.</param>
        /// <param name="defaultColor">Default color for text.</param>
        /// <returns>List of TextRunSegments</returns>
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
        /// <param name="segments">Text run segments to reformat.</param>
        /// <returns>List of MeasuredWords.</returns>
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
                    currentWord.Width += newSegment.Size.X;
                    currentWord.Height = Math.Max(currentWord.Height, newSegment.Size.Y);

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
        /// <returns>Size of the resulting wrap.</returns>
        public static Vector2 WrapWords(List<MeasuredWord> words, float maxWidth)
        {
            Vector2 cursor = Vector2.Zero;
            float currentLineHeight = 0;
            float maxSeenWidth = 0;

            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];
                if (cursor.X + word.Width > maxWidth && cursor.X > 0)
                {
                    maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);
                    cursor.X = 0;
                    cursor.Y += currentLineHeight;
                    currentLineHeight = 0;
                }

                var segments = word.Segments;

                // edge case if word is too large to fit on one line
                if (word.Width > maxWidth)
                {
                    for (int j = 0; j < segments.Count; j++)
                    {
                        var segment = segments[j];

                        currentLineHeight = Math.Max(currentLineHeight, segment.Size.Y);

                        for (int k = 0; k < segment.Text.Length; k++)
                        {
                            char c = segment.Text[k];
                            float charWidth = segment.Font.MeasureString(c.ToString()).X;

                            // if this character crosses border, then take the previous component and make
                            if (cursor.X + charWidth > maxWidth && cursor.X > 0)
                            {
                                maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);
                                cursor.X = 0;

                                string firstHalfText = segment.Text.Substring(0, k);
                                string secondHalfText = segment.Text.Substring(k);

                                segment.PositionOffset = cursor;
                                segment.Text = firstHalfText;
                                segment.Size = segment.Font.MeasureString(segment.Text);
                                segments[j] = segment;

                                cursor.Y += currentLineHeight;

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
                                segments.Insert(j + 1, secondHalfSplitSegment);

                                break;
                            }

                            cursor.X += charWidth;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < segments.Count; j++)
                    {
                        var segment = segments[j];
                        segment.PositionOffset = cursor;
                        segments[j] = segment;

                        cursor.X += segment.Size.X;
                        currentLineHeight = Math.Max(currentLineHeight, segment.Size.Y);
                    }
                }
            }

            maxSeenWidth = Math.Max(maxSeenWidth, cursor.X);
            return new Vector2(maxSeenWidth, cursor.Y + currentLineHeight);
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
