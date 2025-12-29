using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Mmc.MonoGame.UI.Primitives.Text
{
    public static class TextParser
    {
        public static TextRun[] ParseText(string text, FontFamily fontFamily, Color defaultColor)
        {
            var runs = new List<TextRun>();

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
                            runs.Add(new TextRun()
                            {
                                Text = displayText,
                                Font = selectedFont,
                                Color = currentColor,
                                IsUnderlined = isUnderlined,
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
                runs.Add(new TextRun()
                {
                    Text = displayText,
                    Font = selectedFont,
                    Color = currentColor,
                    IsUnderlined = isUnderlined,
                    Size = selectedFont.MeasureString(displayText)
                });
            }

            return runs.ToArray();
        }

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
