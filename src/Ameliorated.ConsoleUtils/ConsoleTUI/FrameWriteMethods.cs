using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Ameliorated.ConsoleUtils
{
    public partial class ConsoleTUI
    {
        public partial class Frame
        {
            /// <summary>
            ///     Centered Line
            /// </summary>
            public enum LineCenterOptions
            {
                Word = 0,
                Character = 1
            }



            /// <summary>
            ///     Normal (Indented) Line
            /// </summary>
            public void WriteLine()
            {
                Console.WriteLine();
            }
            
            public void Write(string text)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines.Take(lines.Length - 1))
                {
                    Console.WriteLine(line.Insert(0, new string(' ', DisplayOffset)));
                }
                Console.Write(lines.Last().Insert(0, new string(' ', DisplayOffset)));
            }

            public void WriteLine(string text)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.WriteLine(line.Insert(0, new string(' ', DisplayOffset)));
                }
            }

            public void WriteLine(string text, ConsoleColor? foreground)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.Write(new string(' ', DisplayOffset));

                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteLine(string text, ConsoleColor? foreground, ConsoleColor? background)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.Write(new string(' ', DisplayOffset));

                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(text);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteLine(string text, int offset)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.WriteLine(line.Insert(0, new string(' ', DisplayOffset + offset)));
                }
            }

            public void WriteLine(string text, int offset, ConsoleColor? foreground)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.Write(new string(' ', DisplayOffset + offset));

                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteLine(string text, int offset, ConsoleColor? foreground, ConsoleColor? background)
            {
                var lines =  text.SplitByLine();
                if (lines.Length > AvailableLines())
                    Clear();

                foreach (var line in lines)
                {
                    Console.Write(new string(' ', DisplayOffset + offset));

                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteCenteredLine(string text, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count > AvailableLines() && !ignoreDisplayHeight)
                {
                    Clear();
                    centeredLines = CenterLines(text.TrimStart('\r').TrimStart('\n'), options);
                }

                foreach (var line in centeredLines) Console.WriteLine(line.Value);
            }

            public void WriteCenteredLine(string text, int maxWidth, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);

                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();
                foreach (var line in centeredLines) Console.WriteLine(line.Value);
            }

            public void WriteCenteredLine(string text, ConsoleColor? foreground, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines)
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
            }


            public void WriteCenteredLine(string text, int maxWidth, ConsoleColor? foreground, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);
                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines)
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteCenteredLine(string text, ConsoleColor? foreground, ConsoleColor? background, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count > AvailableLines() && !ignoreDisplayHeight)
                {
                    Clear();
                    centeredLines = CenterLines(text.TrimStart('\r').TrimStart('\n'), options);
                }

                foreach (var line in centeredLines)
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteCenteredLine(string text, int maxWidth, ConsoleColor? foreground, ConsoleColor? background, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);
                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines)
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
            }

            public void WriteCentered(string text, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {

                
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count > AvailableLines() && !ignoreDisplayHeight)
                {
                    Clear();
                    centeredLines = CenterLines(text.TrimStart('\r').TrimStart('\n'), options);
                }
                    

                foreach (var line in centeredLines.Take(centeredLines.Count - 1)) Console.WriteLine(line.Value);
                Console.Write(centeredLines.Last().Value);
            }

            public void WriteCentered(string text, int maxWidth, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);

                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();
                foreach (var line in centeredLines.Take(centeredLines.Count - 1)) Console.WriteLine(line.Value);
                Console.Write(centeredLines.Last().Value);
            }

            public void WriteCentered(string text, ConsoleColor? foreground, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines.Take(centeredLines.Count - 1))
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
                Console.Write(new string(' ', centeredLines.Last().LeaderLength));
                ConsoleUtils.SetColor(foreground);
                Console.WriteLine(centeredLines.Last().RawString);
                ConsoleUtils.ResetColor();
            }


            public void WriteCentered(string text, int maxWidth, ConsoleColor? foreground, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);
                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines.Take(centeredLines.Count - 1))
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
                Console.Write(new string(' ', centeredLines.Last().LeaderLength));
                ConsoleUtils.SetColor(foreground);
                Console.WriteLine(centeredLines.Last().RawString);
                ConsoleUtils.ResetColor();
            }

            public void WriteCentered(string text, ConsoleColor? foreground, ConsoleColor? background, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                var centeredLines = CenterLines(text, options);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines.Take(centeredLines.Count - 1))
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
                Console.Write(new string(' ', centeredLines.Last().LeaderLength));
                ConsoleUtils.SetColor(foreground, background);
                Console.WriteLine(centeredLines.Last().RawString);
                ConsoleUtils.ResetColor();
            }

            public void WriteCentered(string text, int maxWidth, ConsoleColor? foreground, ConsoleColor? background, bool ignoreDisplayHeight = false, LineCenterOptions options = LineCenterOptions.Word)
            {
                Contract.Requires<ArgumentException>(maxWidth > 1);
                var centeredLines = CenterLines(text, options, maxWidth);
                if (centeredLines.Count >AvailableLines() && !ignoreDisplayHeight)
                    Clear();

                foreach (var line in centeredLines.Take(centeredLines.Count - 1))
                {
                    Console.Write(new string(' ', line.LeaderLength));
                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line.RawString);
                    ConsoleUtils.ResetColor();
                }
                Console.Write(new string(' ', centeredLines.Last().LeaderLength));
                ConsoleUtils.SetColor(foreground, background);
                Console.WriteLine(centeredLines.Last().RawString);
                ConsoleUtils.ResetColor();
            }

            // TODO: Fix this splitting lines when a piece of text can fit on one line
            private List<CenteredString> CenterLines(string text, LineCenterOptions options, int maxWidth = 0)
            {
                var _maxWidth = DisplayWidth;
                if (maxWidth != 0) _maxWidth = maxWidth;

                var list = new List<CenteredString>();

                var lines = new List<string>();

                foreach (var line in text.SplitByLine())
                {
                    if (line == "")
                    {
                        list.Add(new CenteredString
                        { Value = "", LeaderLength = 0 });
                        continue;
                    }

                    if (line.Length > _maxWidth)
                    {
                        for (var index = 0; index < line.Length;)
                        {
                            var splitLine = line.Substring(index, Math.Min(_maxWidth, line.Length - index));
                            var trimmedLength = splitLine.Length - splitLine.Trim(' ').Length;
                            splitLine = splitLine.Trim(' ');

                            var wordIndex = splitLine.LastIndexOf(' ');
                            if (wordIndex != -1 && options == LineCenterOptions.Word) splitLine = splitLine.Substring(0, wordIndex);

                            index += splitLine.Length + trimmedLength;

                            list.Add(CenterLine(splitLine));
                        }

                        continue;
                    }

                    list.Add(CenterLine(line));
                }

                return list;
            }

            private CenteredString CenterLine(string text, int maxWidth = 0)
            {
                var _maxWidth = DisplayWidth;
                if (maxWidth != 0) _maxWidth = maxWidth;

                var centeredString = new CenteredString();

                var space = "";
                if (!(text.Length % 2).Equals(0) && text.Length != _maxWidth) space = " ";

                var leadingLength = (_maxWidth - text.Length) / 2;

                centeredString.Value = space + text.PadLeft(text.Length + leadingLength, ' ').Insert(0, new string(' ', DisplayOffset));
                centeredString.LeaderLength = leadingLength + DisplayOffset + space.Length;
                centeredString.RawString = text;

                return centeredString;
            }

            private class CenteredString
            {
                public int LeaderLength;
                public string RawString;
                public string Value;
            }
        }
    }
}