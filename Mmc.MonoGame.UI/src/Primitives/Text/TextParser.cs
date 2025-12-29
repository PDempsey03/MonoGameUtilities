using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Mmc.MonoGame.UI.Primitives.Text
{
    public static class TextParser
    {
        public static TextRun[] ParseText(string text, FontFamily fontFamily)
        {
            var runs = new List<TextRun>();

            var currentText = new StringBuilder();

            // track state
            bool isBold = false;
            bool isItalic = false;
            bool isUnderlined = false;
            Color color = Color.White;

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
                                Color = color,
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
                    Color = color,
                    IsUnderlined = isUnderlined,
                    Size = selectedFont.MeasureString(displayText)
                });
            }

            return runs.ToArray();
        }
    }
}
