using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using amecs;
using JetBrains.Annotations;

namespace Ameliorated.ConsoleUtils
{
    public class Menu
    {
        public List<MenuItem> Choices { get; set; } = new List<MenuItem>();
        private int TotalOffset = 2;

        private int? _offset = null;

        public int Offset
        {
            get
            {
                if (_offset == null)
                    _offset = BindToOpenFrame ? 5 : 2;

                return _offset.Value;
            }
            set { _offset = value; }
        }

        public ConsoleColor? SelectionForeground { get; set; } = ConsoleColor.Green;
        public ConsoleColor? SelectionBackgound { get; set; } = null;

        public bool CloseFrame { get; set; } = true;

        private bool _bindToOpenFrame;

        public bool BindToOpenFrame { get; set; } = true;

        private int menuStart;

        private int pages;
        private int choicesPerPage;

        public void Write() => Write(null);
        public void Write([CanBeNull] string text)
        {
            if (Choices.Count < 1) throw new ArgumentException("Property Choices must have at least one choice.");

            _bindToOpenFrame = ConsoleTUI.OpenFrame != null && BindToOpenFrame;

            int totalStaticChoices = Choices.Count(x => x.IsStatic || x.IsNextButton || x.IsPreviousButton);

            if (_bindToOpenFrame)
            {
                pages = (int)Math.Ceiling(((double)Choices.Count - totalStaticChoices) / (double)(ConsoleTUI.OpenFrame.DisplayHeight - totalStaticChoices + 1));
                choicesPerPage = (ConsoleTUI.OpenFrame.DisplayHeight - totalStaticChoices + 1);
            }
            else
            {
                pages = (int)Math.Ceiling(((double)Choices.Count - totalStaticChoices) / (double)(((Console.WindowHeight - Console.CursorTop) - totalStaticChoices)));
                choicesPerPage = (Console.WindowHeight - Console.CursorTop) - totalStaticChoices;
            }


            if (_bindToOpenFrame)
                ConsoleTUI.OpenFrame.Clear();

            TotalOffset = _bindToOpenFrame ? ConsoleTUI.OpenFrame.DisplayOffset + Offset : Offset;

            menuStart = Console.CursorTop;
            if (_bindToOpenFrame)
            {
                Console.SetCursorPosition(TotalOffset, Console.CursorTop);
            }

            var fluidChoices = Choices.Where(x => !x.IsStatic && !x.IsNextButton && !x.IsPreviousButton).Take(choicesPerPage);
            var staticChoices = Choices.Where(x => x.IsStatic || (pages > 1 && x.IsNextButton));
            WriteChoiceList(fluidChoices.Concat(staticChoices));

            Frame = ConsoleTUI.OpenFrame;
            if (_bindToOpenFrame && CloseFrame)
            {
                try
                {
                    var offset = ConsoleTUI.OpenFrame.DisplayOffset;
                    if (pages == 1)
                    {
                        ConsoleTUI.OpenFrame.Close();
                    }
                    else
                        ConsoleTUI.OpenFrame.Close($"Page 1/{pages}");
                    Console.WriteLine(new string(' ', offset) + (text ?? "Use the arrow keys to navigate"));
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                }
            }
        }

        private void WriteChoiceList(IEnumerable<MenuItem> list)
        {
            bool selectedWritten = false;
            Console.SetCursorPosition(0, Console.CursorTop);
            foreach (MenuItem choice in list)
            {
                if (_bindToOpenFrame)
                {
                    Console.Write(
                        new string(' ', TotalOffset));
                    if (choice.IsEnabled && !selectedWritten)
                    {
                        choice.WriteSelected(SelectionForeground, SelectionBackgound);
                        selectedWritten = true;
                        Console.WriteLine();
                        continue;
                    }

                    choice.Write();
                }
                else
                {
                    Console.Write(
                        new string(' ', TotalOffset));
                    if (choice.IsEnabled && !selectedWritten)
                    {
                        choice.WriteSelected(SelectionForeground, SelectionBackgound);
                        selectedWritten = true;
                        Console.WriteLine();
                        continue;
                    }

                    choice.Write();
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }

        private List<MenuItem> ValidChoices = new List<MenuItem>();

        public ConsoleTUI.Frame Frame;

        public object Load(bool clearFrame, SemaphoreSlim lockObject = null)
        {
            var visCache = Console.CursorVisible;
            Console.CursorVisible = false;


            int index = Math.Max(Choices.FindIndex(x => x.IsEnabled), 0);
            int validIndex = 0;
            ConsoleKey keyPressed;

            Console.SetCursorPosition(Console.CursorLeft, menuStart + index);

            var allFluidChoices = Choices.Where(x => !x.IsStatic && !x.IsNextButton && !x.IsPreviousButton).ToList();
            var allStaticChoices = Choices.Where(x => x.IsStatic || x.IsNextButton || x.IsPreviousButton).ToList();

            var startingChoices = new List<MenuItem>(Choices);

            int pageIndex = 1;
            ConsoleUtils.ClearInputBuffer();

            var currentChoices = allFluidChoices.Take(choicesPerPage).Concat(Choices.Where(x => x.IsStatic || (pages > 1 && x.IsNextButton))).ToList();
            var currentValidChoices = currentChoices.Where(x => x.IsEnabled).ToList();

            while (true)
            {
                lockObject?.Release();
                keyPressed = Console.ReadKey(true).Key;
                lockObject?.Wait();
                if (!Choices.SequenceEqual(startingChoices))
                {
                    startingChoices = Choices;
                    
                    allFluidChoices = Choices.Where(x => !x.IsStatic && !x.IsNextButton && !x.IsPreviousButton).ToList();
                    allStaticChoices = Choices.Where(x => x.IsStatic || x.IsNextButton || x.IsPreviousButton).ToList();
                    currentChoices = allFluidChoices.Take(choicesPerPage).Concat(Choices.Where(x => x.IsStatic || (pages > 1 && x.IsNextButton))).ToList();
                    currentValidChoices = currentChoices.Where(x => x.IsEnabled).ToList();
                }
                
                if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                {
                    if (validIndex >= currentValidChoices.Count - 1) continue;
                    Console.SetCursorPosition(Math.Max(TotalOffset - 2, 0), Console.CursorTop);

                    Console.Write("  ");
                    currentValidChoices[validIndex].Write();

                    var fromTop = currentChoices.Skip(index + 1).TakeWhile(x => !x.IsEnabled).Count() + 1;
                    var choice = currentChoices.Skip(index + 1).First(x => x.IsEnabled);

                    Console.SetCursorPosition(TotalOffset, Console.CursorTop + fromTop);
                    choice.WriteSelected(SelectionForeground, SelectionBackgound);

                    index += fromTop;
                    validIndex += 1;
                }

                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
                {
                    if (!(validIndex > 0)) continue;

                    Console.SetCursorPosition(Math.Max(TotalOffset - 2, 0), Console.CursorTop);

                    Console.Write("  ");
                    currentValidChoices[validIndex].Write();

                    var fromTop = -currentChoices.Take(index).Reverse().TakeWhile(x => !x.IsEnabled).Count() - 1;
                    var choice = currentChoices.Take(index).Last(x => x.IsEnabled);

                    Console.SetCursorPosition(TotalOffset, Console.CursorTop + fromTop);
                    choice.WriteSelected(SelectionForeground, SelectionBackgound);

                    index += fromTop;
                    validIndex -= 1;
                }

                if (keyPressed == ConsoleKey.RightArrow  || keyPressed == ConsoleKey.A || keyPressed == ConsoleKey.PageUp ||
                    (keyPressed == ConsoleKey.Enter && currentValidChoices[validIndex].IsNextButton))
                {
                    if (pageIndex == pages)
                        continue;

                    Frame.Clear();

                    pageIndex++;

                    var pIndex = pageIndex;
                    var fluidChoices = allFluidChoices.Skip((pIndex - 1) * choicesPerPage).Take(choicesPerPage).ToList();
                    var staticChoices = allStaticChoices.Where(x => !(pages <= pIndex && x.IsNextButton));
                    Console.SetCursorPosition(Console.CursorLeft, menuStart);

                    currentChoices = fluidChoices.Concat(staticChoices).ToList();
                    WriteChoiceList(currentChoices);

                    if (_bindToOpenFrame && CloseFrame)
                    {
                        try
                        {
                            var offset = ConsoleTUI.OpenFrame.DisplayOffset;
                            if (pages == 1)
                                ConsoleTUI.OpenFrame.Close();
                            else
                                ConsoleTUI.OpenFrame.Close($"Page {pIndex}/{pages}");
                            Console.WriteLine(new string(' ', offset) + "Use the arrow keys to navigate");
                        } catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Console.ReadLine();
                        }
                    }

                    Console.SetCursorPosition(Console.CursorLeft, menuStart);

                    currentValidChoices = currentChoices.Where(x => x.IsEnabled).ToList();
                    index = 0;
                    validIndex = 0;

                    continue;
                }

                if (keyPressed == ConsoleKey.LeftArrow || keyPressed == ConsoleKey.D || keyPressed == ConsoleKey.PageDown ||
                    (keyPressed == ConsoleKey.Enter && currentValidChoices[validIndex].IsPreviousButton))
                {
                    if (pageIndex == 1)
                        continue;

                    Frame.Clear();

                    pageIndex--;

                    var pIndex = pageIndex;
                    var fluidChoices = allFluidChoices.Skip((pIndex - 1) * choicesPerPage).Take(choicesPerPage).ToList();
                    var staticChoices = allStaticChoices.Where(x => !(pIndex <= 1 && x.IsPreviousButton));
                    Console.SetCursorPosition(Console.CursorLeft, menuStart);

                    currentChoices = fluidChoices.Concat(staticChoices).ToList();
                    WriteChoiceList(currentChoices);

                    if (_bindToOpenFrame && CloseFrame)
                    {
                        try
                        {
                            var offset = ConsoleTUI.OpenFrame.DisplayOffset;
                            if (pages == 1)
                                ConsoleTUI.OpenFrame.Close();
                            else
                                ConsoleTUI.OpenFrame.Close($"Page {pIndex}/{pages}");
                            Console.WriteLine(new string(' ', offset) + "Use the arrow keys to navigate");
                        } catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Console.ReadLine();
                        }
                    }


                    Console.SetCursorPosition(Console.CursorLeft, menuStart);

                    currentValidChoices = currentChoices.Where(x => x.IsEnabled).ToList();
                    index = 0;
                    validIndex = 0;

                    continue;
                }

                if (keyPressed == ConsoleKey.Enter)
                {
                    break;
                }
            }

            if (clearFrame && CloseFrame)
                Frame.Clear();
            
            Console.CursorVisible = visCache;

            return currentValidChoices[validIndex].ReturnValue;
        }

        private bool NextButtonPresent()
        {
            return Choices.Any(x => x.IsNextButton);
        }

        private bool PreviousButtonPresent()
        {
            return Choices.Any(x => x.IsNextButton);
        }


        public class MenuItem
        {
            public object ReturnValue { get; set; }

            public static readonly MenuItem Blank = new MenuItem("", null);
            public static readonly MenuItem BlankStatic = new MenuItem("", null) { IsStatic = true };

            public bool IsNextButton { get; set; } = false;
            public bool IsPreviousButton { get; set; } = false;

            public bool IsStatic { get; set; } = false;

            public string PrimaryText { get; set; } = "";
            public string SecondaryText { get; set; } = "";


            public MenuItem(string primaryText, object returnValue)
            {
                PrimaryText = primaryText;
                ReturnValue = returnValue;
            }

            public bool AddBetweenSpace { get; set; } = true;

            private ConsoleColor? _primaryTextForeground = null;

            public ConsoleColor? PrimaryTextForeground
            {
                get
                {
                    if (_primaryTextForeground.HasValue)
                        return _primaryTextForeground;
                    if (IsEnabled) return null;
                    if (Console.ForegroundColor == ConsoleColor.White) return ConsoleColor.DarkGray;
                    if (Console.ForegroundColor == ConsoleColor.White) return ConsoleColor.DarkGray;
                    return null;
                }
                set { _primaryTextForeground = value; }
            }

            public ConsoleColor? PrimaryTextBackground { get; set; } = null;
            public ConsoleColor? SecondaryTextForeground { get; set; } = null;
            public ConsoleColor? SecondaryTextBackground { get; set; } = null;

            private bool? _isEnabled = null;

            public bool IsEnabled
            {
                get
                {
                    if (_isEnabled.HasValue)
                        return _isEnabled.Value;
                    if (!String.IsNullOrEmpty(PrimaryText) || !String.IsNullOrEmpty(SecondaryText))
                    {
                        return true;
                    }

                    return false;
                }
                set { _isEnabled = value; }
            }

            public bool StretchSelection = false;

            public void Write()
            {
                if (!String.IsNullOrEmpty(PrimaryText))
                {
                    ConsoleUtils.SetColor(PrimaryTextForeground, PrimaryTextBackground);
                    Console.Write(PrimaryText);
                    ConsoleUtils.ResetColor();

                    if (AddBetweenSpace)
                        Console.Write(' ');
                }

                if (String.IsNullOrEmpty(SecondaryText))
                    return;

                ConsoleUtils.SetColor(SecondaryTextForeground, SecondaryTextBackground);
                Console.Write(SecondaryText);
                ConsoleUtils.ResetColor();
            }

            public void WriteSelected(ConsoleColor? foreground, ConsoleColor? background)
            {
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);

                if (!String.IsNullOrEmpty(PrimaryText))
                {
                    ConsoleUtils.SetColor(foreground, background);
                    Console.Write("> " + PrimaryText);

                    if (!StretchSelection)
                        ConsoleUtils.ResetColor();

                    if (AddBetweenSpace)
                        Console.Write(' ');
                }

                if (String.IsNullOrEmpty(SecondaryText))
                {
                    if (StretchSelection)
                        ConsoleUtils.ResetColor();
                    return;
                }

                if (!String.IsNullOrEmpty(PrimaryText) && !StretchSelection)
                    ConsoleUtils.SetColor(SecondaryTextForeground, SecondaryTextBackground);

                Console.Write(SecondaryText);
                ConsoleUtils.ResetColor();
            }
        }
    }
}