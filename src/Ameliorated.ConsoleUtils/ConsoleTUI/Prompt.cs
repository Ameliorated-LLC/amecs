using System;
using System.Collections;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace Ameliorated.ConsoleUtils
{

        /// <summary>
        ///     Represents a input prompt to be used with the Start method.
        /// </summary>
        public class ChoicePrompt : Prompt
        {
            public string Choices { get; set; } = "YN";
            public bool BeepSound { get; set; } = true;
            public bool CaseSensitive { get; set; } = false;
            public bool AllowEscape { get; set; } = true;
            public bool AnyKey { get; set; } = false;

            public ConsoleColor? TextForeground { get; set; }

            private bool _bindToOpenFrame;
            
            [CanBeNull]
            public new int? Start()
            {
                if (Choices.Length < 1 && !AnyKey) throw new ArgumentException("There must be at least 1 choice.");
                _bindToOpenFrame = ConsoleTUI.OpenFrame != null && BindToOpenFrame;
                
                if (_bindToOpenFrame && ConsoleTUI.OpenFrame.AvailableLines() < 1)
                    ConsoleTUI.OpenFrame.Clear();

                if (TextForeground.HasValue)
                    ConsoleUtils.SetColor(TextForeground);
                if (_bindToOpenFrame)
                    ConsoleTUI.OpenFrame.Write(Text);
                else
                    Console.Write(Text);
                if (TextForeground.HasValue)
                    ConsoleUtils.ResetColor();

                var cursorVisibility = Console.CursorVisible;
                int? result;

                while (true)
                {
                    Console.CursorVisible = true;
                    ConsoleUtils.ClearInputBuffer();
                    var key = Console.ReadKey(true);
                    if (AnyKey)
                    {
                        Console.CursorVisible = cursorVisibility;
                        return key.KeyChar;
                    }
                    
                    if (key.Key == ConsoleKey.Escape && AllowEscape)
                    {
                        Console.CursorVisible = cursorVisibility;
                        return null;
                    }

                    if (CaseSensitive)
                        result = Choices.IndexOf(key.KeyChar.ToString(), StringComparison.Ordinal);
                    else
                        result = Choices.IndexOf(key.KeyChar.ToString(), StringComparison.OrdinalIgnoreCase);
                    
                    if (result >= 0)
                    {
                        if (InputForeground.HasValue && InputBackground.HasValue) ConsoleUtils.SetColor(InputForeground.Value, InputBackground.Value);
                        else if (InputForeground.HasValue) ConsoleUtils.SetColor(InputForeground.Value);
                        else if (InputBackground.HasValue) ConsoleUtils.SetColor(Console.ForegroundColor, InputBackground.Value);
                        
                        if (!CaseSensitive) Console.Write(key.KeyChar.ToString().ToUpper());
                        else Console.Write(key.KeyChar.ToString());
                        
                        if (InputForeground.HasValue || InputBackground.HasValue) ConsoleUtils.ResetColor();
                        
                        break;
                    }
                    else if (BeepSound) Console.Beep();
                } 

                Console.CursorVisible = cursorVisibility;
                Console.WriteLine();

                return result.Value;
            }
        }

        public class InputPrompt : Prompt
        {
            [Optional] public ConsoleColor? BoxBackground { get; set; }
            public bool MaskInput { get; set; } = false;


            public bool AlignInput { get; set; } = true;
            [Optional] public int? SplitWidth { get; set; }
            
            
            private bool _bindToOpenFrame;
            private int? _splitWidth;

            [CanBeNull]
            public new string Start()
            {
                if (SplitWidth.HasValue && !AlignInput) throw new ArgumentException("Property SplitWidth must not be used without property AlignInput set to true.");

                _bindToOpenFrame = ConsoleTUI.OpenFrame != null && BindToOpenFrame;
                if (_bindToOpenFrame && ConsoleTUI.OpenFrame.AvailableLines() < 1)
                    ConsoleTUI.OpenFrame.Clear();

                if (!MaxLength.HasValue)
                    MaxLength = AlignInput ? ConsoleTUI.OpenFrame.AvailableChars() - (Text.LastLine().Length * ConsoleTUI.OpenFrame.AvailableLines()) : ConsoleTUI.OpenFrame.AvailableChars();

                int startLeft = Console.CursorLeft + Text.LastLine().Length;

                _splitWidth = null;
                if (AlignInput && !_bindToOpenFrame)
                {
                    if (Text.LastLine().Length > Console.WindowWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = Console.WindowWidth - startLeft;
                }

                if (_bindToOpenFrame)
                {
                    if (SplitWidth.HasValue && (SplitWidth.Value + startLeft + ConsoleTUI.OpenFrame.DisplayOffset >= Console.WindowWidth))
                        throw new ArgumentException($"Property SplitWidth must be less than the available width.");
                    
                    // TODO: BAD WORDING
                    if (Text.LastLine().Length > ConsoleTUI.OpenFrame.DisplayWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = ConsoleTUI.OpenFrame.DisplayWidth - (startLeft);
                    if (!AlignInput)
                        _splitWidth = ConsoleTUI.OpenFrame.DisplayWidth;
                }

                if (SplitWidth.HasValue)
                {
                    if (SplitWidth.Value + startLeft >= Console.WindowWidth)
                        throw new ArgumentException($"Property SplitWidth must be less than the available width.");
                    
                    if (Text.LastLine().Length > Console.WindowWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = SplitWidth.Value;
                }
                
                /*
                int maxLines = Console.WindowHeight - Console.CursorTop;
                if (_bindToOpenFrame)
                {
                    maxLines = ConsoleTUI.OpenFrame.DisplayHeight - (Console.CursorTop - 6) - Text.SplitByLine().Length;
                    if (maxLines < 2)
                    {
                        ConsoleTUI.OpenFrame.Clear();
                        if (_bindToOpenFrame) maxLines = ConsoleTUI.OpenFrame.DisplayHeight - (Console.CursorTop - 6) - Text.SplitByLine().Length;
                    }
                }
                */

                ConsoleUtils.ClearInputBuffer();

                if (_bindToOpenFrame)
                    ConsoleTUI.OpenFrame.Write(Text);
                else
                    Console.Write(Text);

                if (BoxBackground.HasValue && _splitWidth.HasValue)
                {
                    if (AlignInput)
                        WriteBackground(_splitWidth.Value, BoxBackground.Value);
                    else
                        WriteBackground(_splitWidth.Value - Text.LastLine().Length, BoxBackground.Value);
                }
                if (BoxBackground.HasValue && !_splitWidth.HasValue) WriteBackground(Console.WindowWidth - Console.CursorLeft, BoxBackground.Value);

                var cursorVisibility = Console.CursorVisible;
                var input = new StringBuilder();
                ConsoleKeyInfo key;

                do
                {
                    Console.CursorVisible = true;
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length <= 0) continue;

                        input.Remove(input.Length - 1, 1);

                        bool movedLines = false;
                        if (_bindToOpenFrame)
                        {
                            if ((((input.Length + 1) - _splitWidth.Value + Text.LastLine().Length).IsDivisibleBy(ConsoleTUI.OpenFrame.DisplayWidth) || input.Length + 1 == _splitWidth.Value - Text.LastLine().Length) && !AlignInput)
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value - (Console.CursorLeft - ConsoleTUI.OpenFrame.DisplayOffset), Console.BackgroundColor);
                                
                                Console.SetCursorPosition((ConsoleTUI.OpenFrame.DisplayOffset) + _splitWidth.Value - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                            else if ((input.Length + 1).IsDivisibleBy(_splitWidth.Value) && AlignInput)
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, Console.BackgroundColor);

                                Console.SetCursorPosition(ConsoleTUI.OpenFrame.DisplayOffset + startLeft + _splitWidth.Value - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }
                        else if (AlignInput)
                        {
                            if ((input.Length + 1).IsDivisibleBy(_splitWidth.Value))
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, Console.BackgroundColor);

                                Console.SetCursorPosition(startLeft + _splitWidth.Value, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }
                        else
                        {
                            if (((input.Length + 1) - (Console.WindowWidth - startLeft)).IsDivisibleBy(Console.WindowWidth) || input.Length + 1 == Console.WindowWidth - startLeft)
                            {
                                if (BoxBackground.HasValue) WriteBackground(Console.WindowWidth, Console.BackgroundColor);

                                Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }

                        if (movedLines)
                        {
                            if (BoxBackground.HasValue) ConsoleUtils.SetColor(Console.ForegroundColor, BoxBackground.Value);
                            Console.Write(" \b");
                            if (BoxBackground.HasValue) ConsoleUtils.ResetColor();

                        }
                        else
                        {
                            if (BoxBackground.HasValue) ConsoleUtils.SetColor(Console.ForegroundColor, BoxBackground.Value);
                            Console.Write("\b \b");
                            if (BoxBackground.HasValue) ConsoleUtils.ResetColor();
                        }
                        
                        if (MaxLength.HasValue && MaxLength.Value - 1 == input.Length) Console.CursorVisible = true;

                        continue;
                    }

                    if (Char.IsControl(key.KeyChar)) continue;

                    if (MaxLength.HasValue && MaxLength.Value <= input.Length)
                    {
                        ConsoleUtils.SetColor(ConsoleColor.DarkRed, InputBackground);
                        Console.CursorVisible = false;
                        Console.Write("!");

                        Thread.Sleep(200);
                        ConsoleUtils.ClearInputBuffer();
                        ConsoleUtils.ResetColor();
                        ConsoleUtils.SetColor(null, BoxBackground);
                        Console.Write("\b \b");
                        ConsoleUtils.ResetColor();
                        continue;
                    }

                    input.Append(key.KeyChar);
                    
                    ConsoleUtils.SetColor(InputForeground, InputBackground);

                    if (MaskInput) Console.Write("*");
                    else Console.Write(key.KeyChar);
                    
                    ConsoleUtils.ResetColor();

                    if (MaxLength.HasValue && MaxLength.Value <= input.Length + 1)
                        continue;
                    if (_bindToOpenFrame)
                    {
                        if (((input.Length - _splitWidth.Value + Text.LastLine().Length).IsDivisibleBy(_splitWidth.Value) || input.Length == _splitWidth.Value - Text.LastLine().Length) && !AlignInput)
                        {
                            Console.SetCursorPosition(ConsoleTUI.OpenFrame.DisplayOffset, Console.CursorTop + 1);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                        else if (input.Length.IsDivisibleBy(_splitWidth.Value) && AlignInput)
                        {
                            Console.SetCursorPosition(startLeft + ConsoleTUI.OpenFrame.DisplayOffset, Console.CursorTop + 1);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                    }
                    else if (AlignInput)
                    {
                        if (input.Length.IsDivisibleBy(_splitWidth.Value))
                        {
                            if (SplitWidth.HasValue)
                                Console.SetCursorPosition(startLeft, Console.CursorTop + 1);
                            else
                                // Console will have automatically moved the cursor down
                                Console.SetCursorPosition(startLeft, Console.CursorTop);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                    }
                    else
                    {
                        if ((input.Length - (Console.WindowWidth - startLeft)).IsDivisibleBy(Console.WindowWidth) || input.Length == Console.WindowWidth - startLeft)
                        {
                            // Console will have automatically moved the cursor down
                            Console.SetCursorPosition(startLeft - Text.LastLine().Length, Console.CursorTop);

                            if (BoxBackground.HasValue) WriteBackground(Console.WindowWidth - (startLeft - Text.LastLine().Length), BoxBackground.Value);
                        }
                    }
                } while (key.Key != ConsoleKey.Enter && (!AllowEscape || (AllowEscape && key.Key != ConsoleKey.Escape)));

                if (input.Length == 0)
                {
                    ConsoleUtils.SetColor(ConsoleColor.DarkGray);
                    Console.Write("None");
                    ConsoleUtils.ResetColor();
                }
                
                Console.CursorVisible = cursorVisibility;
                Console.WriteLine();

                if (key.Key == ConsoleKey.Escape && AllowEscape) return null;
                return input.ToString();
            }
        }

        public class SecureInputPrompt : Prompt
        {
            [Optional] public ConsoleColor? BoxBackground { get; set; }
            public bool MaskInput { get; set; } = true;

            public bool AlignInput { get; set; } = true;
            [Optional] public int? SplitWidth { get; set; }


            private bool _bindToOpenFrame;
            private int? _splitWidth;

            [CanBeNull]
            public new SecureString Start()
            {if (SplitWidth.HasValue && !AlignInput) throw new ArgumentException("Property SplitWidth must not be used without property AlignInput set to true.");

                _bindToOpenFrame = ConsoleTUI.OpenFrame != null && BindToOpenFrame;
                if (_bindToOpenFrame && ConsoleTUI.OpenFrame.AvailableLines() < 1)
                    ConsoleTUI.OpenFrame.Clear();

                if (!MaxLength.HasValue)
                    MaxLength = AlignInput ? ConsoleTUI.OpenFrame.AvailableChars() - (Text.LastLine().Length * ConsoleTUI.OpenFrame.AvailableLines()) : ConsoleTUI.OpenFrame.AvailableChars();

                int startLeft = Console.CursorLeft + Text.LastLine().Length;

                _splitWidth = null;
                if (AlignInput && !_bindToOpenFrame)
                {
                    if (Text.LastLine().Length > Console.WindowWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = Console.WindowWidth - startLeft;
                }

                if (_bindToOpenFrame)
                {
                    if (SplitWidth.HasValue && (SplitWidth.Value + startLeft + ConsoleTUI.OpenFrame.DisplayOffset >= Console.WindowWidth))
                        throw new ArgumentException($"Property SplitWidth must be less than the available width.");
                    
                    // TODO: BAD WORDING
                    if (Text.LastLine().Length > ConsoleTUI.OpenFrame.DisplayWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = ConsoleTUI.OpenFrame.DisplayWidth - (startLeft);
                    if (!AlignInput)
                        _splitWidth = ConsoleTUI.OpenFrame.DisplayWidth;
                }

                if (SplitWidth.HasValue)
                {
                    if (SplitWidth.Value + startLeft >= Console.WindowWidth)
                        throw new ArgumentException($"Property SplitWidth must be less than the available width.");
                    
                    if (Text.LastLine().Length > Console.WindowWidth - 3) throw new ArgumentException("Last line of property Text must not be within 3 characters of the available width.");
                    _splitWidth = SplitWidth.Value;
                }
                
                /*
                int maxLines = Console.WindowHeight - Console.CursorTop;
                if (_bindToOpenFrame)
                {
                    maxLines = ConsoleTUI.OpenFrame.DisplayHeight - (Console.CursorTop - 6) - Text.SplitByLine().Length;
                    if (maxLines < 2)
                    {
                        ConsoleTUI.OpenFrame.Clear();
                        if (_bindToOpenFrame) maxLines = ConsoleTUI.OpenFrame.DisplayHeight - (Console.CursorTop - 6) - Text.SplitByLine().Length;
                    }
                }
                */

                ConsoleUtils.ClearInputBuffer();

                if (_bindToOpenFrame)
                    ConsoleTUI.OpenFrame.Write(Text);
                else
                    Console.Write(Text);

                if (BoxBackground.HasValue && _splitWidth.HasValue)
                {
                    if (AlignInput)
                        WriteBackground(_splitWidth.Value, BoxBackground.Value);
                    else
                        WriteBackground(_splitWidth.Value - Text.LastLine().Length, BoxBackground.Value);
                }
                if (BoxBackground.HasValue && !_splitWidth.HasValue) WriteBackground(Console.WindowWidth - Console.CursorLeft, BoxBackground.Value);

                var cursorVisibility = Console.CursorVisible;
                var input = new SecureString();
                ConsoleKeyInfo key;

                do
                {
                    Console.CursorVisible = true;
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length <= 0) continue;

                        input.RemoveAt(input.Length - 1);

                        bool movedLines = false;
                        if (_bindToOpenFrame)
                        {
                            if ((((input.Length + 1) - _splitWidth.Value + Text.LastLine().Length).IsDivisibleBy(ConsoleTUI.OpenFrame.DisplayWidth) || input.Length + 1 == _splitWidth.Value - Text.LastLine().Length) && !AlignInput)
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value - (Console.CursorLeft - ConsoleTUI.OpenFrame.DisplayOffset), Console.BackgroundColor);
                                
                                Console.SetCursorPosition((ConsoleTUI.OpenFrame.DisplayOffset) + _splitWidth.Value - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                            else if ((input.Length + 1).IsDivisibleBy(_splitWidth.Value) && AlignInput)
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, Console.BackgroundColor);

                                Console.SetCursorPosition(ConsoleTUI.OpenFrame.DisplayOffset + startLeft + _splitWidth.Value - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }
                        else if (AlignInput)
                        {
                            if ((input.Length + 1).IsDivisibleBy(_splitWidth.Value))
                            {
                                if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, Console.BackgroundColor);

                                Console.SetCursorPosition(startLeft + _splitWidth.Value, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }
                        else
                        {
                            if (((input.Length + 1) - (Console.WindowWidth - startLeft)).IsDivisibleBy(Console.WindowWidth) || input.Length + 1 == Console.WindowWidth - startLeft)
                            {
                                if (BoxBackground.HasValue) WriteBackground(Console.WindowWidth, Console.BackgroundColor);

                                Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop - 1);
                                movedLines = true;
                            }
                        }

                        if (movedLines)
                        {
                            if (BoxBackground.HasValue) ConsoleUtils.SetColor(Console.ForegroundColor, BoxBackground.Value);
                            Console.Write(" \b");
                            if (BoxBackground.HasValue) ConsoleUtils.ResetColor();

                        }
                        else
                        {
                            if (BoxBackground.HasValue) ConsoleUtils.SetColor(Console.ForegroundColor, BoxBackground.Value);
                            Console.Write("\b \b");
                            if (BoxBackground.HasValue) ConsoleUtils.ResetColor();
                        }
                        
                        if (MaxLength.HasValue && MaxLength.Value - 1 == input.Length) Console.CursorVisible = true;
                    }

                    if (Char.IsControl(key.KeyChar)) continue;

                    if (MaxLength.HasValue && MaxLength.Value <= input.Length)
                    {
                        ConsoleUtils.SetColor(ConsoleColor.DarkRed, InputBackground);
                        Console.CursorVisible = false;
                        Console.Write("!");

                        Thread.Sleep(200);
                        ConsoleUtils.ClearInputBuffer();
                        ConsoleUtils.ResetColor();
                        ConsoleUtils.SetColor(null, BoxBackground);
                        Console.Write("\b \b");
                        ConsoleUtils.ResetColor();
                        continue;
                    }

                    input.AppendChar(key.KeyChar);
                    
                    ConsoleUtils.SetColor(InputForeground, InputBackground);

                    if (MaskInput) Console.Write("*");
                    else Console.Write(key.KeyChar);
                    
                    ConsoleUtils.ResetColor();

                    if (MaxLength.HasValue && MaxLength.Value <= input.Length + 1)
                        continue;
                    if (_bindToOpenFrame)
                    {
                        if (((input.Length - _splitWidth.Value + Text.LastLine().Length).IsDivisibleBy(_splitWidth.Value) || input.Length == _splitWidth.Value - Text.LastLine().Length) && !AlignInput)
                        {
                            Console.SetCursorPosition(ConsoleTUI.OpenFrame.DisplayOffset, Console.CursorTop + 1);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                        else if (input.Length.IsDivisibleBy(_splitWidth.Value) && AlignInput)
                        {
                            Console.SetCursorPosition(startLeft + ConsoleTUI.OpenFrame.DisplayOffset, Console.CursorTop + 1);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                    }
                    else if (AlignInput)
                    {
                        if (input.Length.IsDivisibleBy(_splitWidth.Value))
                        {
                            if (SplitWidth.HasValue)
                                Console.SetCursorPosition(startLeft, Console.CursorTop + 1);
                            else
                                // Console will have automatically moved the cursor down
                                Console.SetCursorPosition(startLeft, Console.CursorTop);

                            if (BoxBackground.HasValue) WriteBackground(_splitWidth.Value, BoxBackground.Value);
                        }
                    }
                    else
                    {
                        if ((input.Length - (Console.WindowWidth - startLeft)).IsDivisibleBy(Console.WindowWidth) || input.Length == Console.WindowWidth - startLeft)
                        {
                            // Console will have automatically moved the cursor down
                            Console.SetCursorPosition(startLeft - Text.LastLine().Length, Console.CursorTop);

                            if (BoxBackground.HasValue) WriteBackground(Console.WindowWidth - (startLeft - Text.LastLine().Length), BoxBackground.Value);
                        }
                    }
                } while (key.Key != ConsoleKey.Enter && (!AllowEscape || (AllowEscape && key.Key != ConsoleKey.Escape)));

                if (input.Length == 0)
                {
                    ConsoleUtils.SetColor(ConsoleColor.Gray);
                    Console.Write("None");
                    ConsoleUtils.ResetColor();
                }
                
                Console.CursorVisible = cursorVisibility;
                Console.WriteLine();

                if (key.Key == ConsoleKey.Escape && AllowEscape) return null;
                return input;
            }
        }


        public abstract class Prompt
        {
            /// <summary>
            ///     Text to be displayed before the input.
            /// </summary>
            [Optional]
            public string Text { get; set; } = "";
            
            [Optional] public int? MaxLength { get; set; }

            /// <summary>
            ///     (Optional)
            /// </summary>
            [Optional]
            public ConsoleColor? InputForeground { get; set; } = null;

            [Optional] public ConsoleColor? InputBackground { get; set; } = null;

            public bool BindToOpenFrame { get; set; } = true;
            public bool AllowEscape { get; set; } = true;

            internal static void WriteBackground(int length, ConsoleColor color)
            {
                if (Console.CursorLeft + length > Console.WindowWidth) throw new Exception("Critical Error");
                int leftStart = Console.CursorLeft;
                int topStart = Console.CursorTop;

                ConsoleUtils.SetColor(Console.ForegroundColor, color);
                Console.Write(new string(' ', length));
                ConsoleUtils.ResetColor();
                Console.SetCursorPosition(leftStart, topStart);
            }
        }
}