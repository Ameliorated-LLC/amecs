using System;
using System.Linq;

namespace Ameliorated.ConsoleUtils
{
    public static partial class ConsoleTUI
    {
        public static Frame OpenFrame;

        public partial class Frame
        {
            private int _displayWidth;

            private int _sidePadding;

            /// <summary>
            ///     Sets the visual "frame" to be used later.
            /// </summary>
            /// <param name="header">The header to be displayed just under the top of the frame.</param>
            /// <param name="ignoreHeader">
            ///     Indicates whether an exception should be raised if the header cannot be aligned with the
            ///     frame.
            /// </param>
            /// <exception cref="MethodAccessException">Function was called before a TUI initialization.</exception>
            public Frame(string header, bool throwOnMisalignedHeader = true)
            {
                if (!IsInitialized) throw new MethodAccessException("Console TUI must be initialized before calling other TUI functions.");
                if (header.Split(new[]
                    { "\r\n", "\n" }, StringSplitOptions.None).Any(x => x.Length > InitializedWidth)) throw new ArgumentException("Header must not be longer than the window width.");

                int frameWidth;
                if (InitializedWidth.IsEven())
                {
                    if (!header.Length.IsEven() && throwOnMisalignedHeader) throw new ArgumentException("Header length should be even while window width is even.");
                    frameWidth = (.725 * InitializedWidth).RoundToEven();
                } else
                {
                    if (header.Length.IsEven() && throwOnMisalignedHeader) throw new ArgumentException("Header length should be odd while window width is odd.");
                    frameWidth = (.725 * InitializedWidth).RoundToOdd();
                }

                Header = header;
                FrameWidth = frameWidth;
                DisplayWidth = frameWidth - SidePadding * 2;
                DisplayOffset = (InitializedWidth - DisplayWidth) / 2;
            }

            public string Header { get; set; }
            public char FrameChar { get; set; } = '_';
            public int FrameWidth { get; set; }

            public int SidePadding
            {
                get => _sidePadding;
                set
                {
                    _sidePadding = value;
                    DisplayWidth = FrameWidth - value * 2;
                    DisplayOffset = (InitializedWidth - DisplayWidth) / 2;
                }
            }

            public int DisplayWidth
            {
                get => _displayWidth;
                set
                {
                    _displayWidth = value;
                    DisplayOffset = (InitializedWidth - value) / 2;
                }
            }

            internal int DisplayOffset { get; private set; }
            internal int DisplayHeight { get; } = Console.WindowHeight - 13;
            
            
            public void Open(bool paddingLines = true)
            {
                Console.WriteLine();
                var frameOffset = (InitializedWidth - FrameWidth) / 2;
                Console.WriteLine(new string(' ', frameOffset) + new string(FrameChar, FrameWidth));
                Console.WriteLine();
                WriteCenteredLine(Header);
                if (paddingLines)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                }
                
                OpenFrame = this;
            }

            public object Close(Prompt prompt = null, bool alignToBottom = false)
            {
                OpenFrame = null;
                
                if (alignToBottom)
                    Console.SetCursorPosition(0, Console.WindowHeight - 7);
                
                Console.WriteLine();
                Console.WriteLine();
                var frameOffset = (InitializedWidth - FrameWidth) / 2;
                Console.WriteLine(new string(' ', frameOffset) + new string(FrameChar, FrameWidth));
                Console.WriteLine();

                if (prompt != null)
                {
                    if (!prompt.MaxLength.HasValue)
                        prompt.MaxLength = DisplayWidth - prompt.Text.LastLine().Length;
                    prompt.BindToOpenFrame = false;
                    prompt.Text = new string(' ', frameOffset) + prompt.Text;

                    try { return ((ChoicePrompt)prompt).Start(); } catch (InvalidCastException) {}
                    try { return ((InputPrompt)prompt).Start(); } catch (InvalidCastException) {}
                }

                return null;
            }
            public object Close(string text, Prompt prompt = null, bool alignToBottom = false)
            {
                OpenFrame = null;
                
                if (alignToBottom)
                    Console.SetCursorPosition(0, Console.WindowHeight - 7);

                Console.WriteLine();
                WriteCenteredLine(text, true);
                var frameOffset = (InitializedWidth - FrameWidth) / 2;
                Console.WriteLine(new string(' ', frameOffset) + new string(FrameChar, FrameWidth));
                Console.WriteLine();

                if (prompt != null)
                {
                    if (!prompt.MaxLength.HasValue)
                        prompt.MaxLength = DisplayWidth - prompt.Text.LastLine().Length;
                    prompt.BindToOpenFrame = false;
                    prompt.Text = new string(' ', frameOffset) + prompt.Text;
                    
                    try { return ((ChoicePrompt)prompt).Start(); } catch (InvalidCastException) {}
                    try { return ((InputPrompt)prompt).Start(); } catch (InvalidCastException) {}
                }

                return null;
            }
            public object Close(string text, ConsoleColor foreground, ConsoleColor background, Prompt prompt = null, bool alignToBottom = false)
            {
                OpenFrame = null;
                
                if (alignToBottom)
                    Console.SetCursorPosition(0, Console.WindowHeight - 7);
                
                Console.WriteLine();
                WriteCenteredLine(text, foreground, background, true);
                var frameOffset = (InitializedWidth - FrameWidth) / 2;
                Console.WriteLine(new string(' ', frameOffset) + new string(FrameChar, FrameWidth));
                Console.WriteLine();

                if (prompt != null)
                {
                    if (!prompt.MaxLength.HasValue)
                        prompt.MaxLength = DisplayWidth - prompt.Text.LastLine().Length;
                    prompt.BindToOpenFrame = false;
                    prompt.Text = new string(' ', frameOffset) + prompt.Text;
                    
                    try { return ((ChoicePrompt)prompt).Start(); } catch (InvalidCastException) {}
                    try { return ((InputPrompt)prompt).Start(); } catch (InvalidCastException) {}
                }

                return null;
            }

            public void Clear(int bottomOffset = 2)
            {
                for (int i = 0; i < Console.WindowHeight - (6 + bottomOffset); i++)
                {
                    Console.SetCursorPosition(DisplayOffset, i + 6);
                    Console.Write(new string(' ', DisplayWidth));
                }
                Console.SetCursorPosition(0, 6);
                OpenFrame = this;
            }

            public int AvailableLines()
            {
                var totalLines = DisplayHeight - (Math.Max(Console.CursorTop - 6, 0));

                return totalLines;
            }

            public int AvailableChars()
            {
                var totalChars = Math.Max(Console.CursorLeft - DisplayOffset, 0) + (AvailableLines() * DisplayWidth);

                return totalChars;
            }
        }
    }
}