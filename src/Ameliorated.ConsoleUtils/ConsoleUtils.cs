using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Ameliorated.ConsoleUtils
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class OptionalAttribute : Attribute
    {
    }

    public static class ConsoleUtils
    {
        public class LoadingIndicator : IDisposable
        {
            public LoadingIndicator() {}

            public LoadingIndicator(bool start)
            {
                if (start)
                    StartAsync();
            }
            
            private CancellationTokenSource cts = new CancellationTokenSource();
            Task currentTask = Task.CompletedTask;
            public async Task StartAsync()
            {
                Console.CursorVisible = false;
                cts = new CancellationTokenSource();
                currentTask = Task.Run(() =>
                {
                    if (cts.IsCancellationRequested)
                    {
                        Console.WriteLine("...");
                        return;
                    }
                    
                    Console.Write('.');
                    while (true)
                    {
                        Thread.Sleep(500);
                        Console.Write('.');
                        Thread.Sleep(500);
                        Console.Write('.');
                        Thread.Sleep(500);
                        if (cts.IsCancellationRequested)
                        {
                            Console.WriteLine();
                            return;
                        }

                        Console.Write("\b \b\b \b");
                    }
                });

                await currentTask;
            }

            public void Stop()
            {
                cts.Cancel();

                currentTask.Wait();
            }

            public void Dispose()
            {
                Stop();

                this.currentTask = null;
                this.cts = null;
            }
        }
        
        public static bool DisplayErrors { get; set; } = true;

        
        
        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        internal static void WriteError(string text)
        {
            if (DisplayErrors) Console.WriteLine("ConsoleUtils: " + text);
        }
        
        
        private static ConsoleColor foregroundCache;
        private static ConsoleColor backgroundCache;
        private static bool? foregroundOnly;
        
        internal static void SetColor([CanBeNull] ConsoleColor? foreground)
        {
            foregroundCache = Console.ForegroundColor;
            if (foreground.HasValue)
                Console.ForegroundColor = foreground.Value;

            foregroundOnly = true;
        }

        internal static void SetColor([CanBeNull] ConsoleColor? foreground, [CanBeNull] ConsoleColor? background)
        {
            foregroundCache = Console.ForegroundColor;
            backgroundCache = Console.BackgroundColor;
            
            if (foreground.HasValue)
                Console.ForegroundColor = foreground.Value;
            if (background.HasValue)
                Console.BackgroundColor = background.Value;

            foregroundOnly = false;
        }

        internal static void ResetColor()
        {
            if (!foregroundOnly.HasValue) throw new MethodAccessException("SetColor must be used before calling ResetColor.");

            if (foregroundOnly.Value)
            {
                Console.ForegroundColor = foregroundCache;
            } else
            {
                Console.ForegroundColor = foregroundCache;
                Console.BackgroundColor = backgroundCache;
            }

            foregroundOnly = null;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        

            public static void WriteLine(string text, ConsoleColor? foreground)
            {
                foreach (var line in text.SplitByLine())
                {
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }
            public static void WriteLine(string text, ConsoleColor? foreground, ConsoleColor? background)
            {
                foreach (var line in text.SplitByLine())
                {
                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public static void WriteLine(string text, int offset)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.WriteLine(line.Insert(0, new string(' ', offset)));
                }
            }

            public static void WriteLine(string text, int offset, ConsoleColor? foreground)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.Write(new string(' ', offset));

                    ConsoleUtils.SetColor(foreground);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public static void WriteLine(string text, int offset, ConsoleColor? foreground, ConsoleColor? background)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.Write(new string(' ', offset));

                    ConsoleUtils.SetColor(foreground, background);
                    Console.WriteLine(line);
                    ConsoleUtils.ResetColor();
                }
            }
            
            
            
            public static void Write(string text, ConsoleColor? foreground)
            {
                foreach (var line in text.SplitByLine())
                {
                    ConsoleUtils.SetColor(foreground);
                    Console.Write(line);
                    ConsoleUtils.ResetColor();
                }
            }
            public static void Write(string text, ConsoleColor? foreground, ConsoleColor? background)
            {
                foreach (var line in text.SplitByLine())
                {
                    ConsoleUtils.SetColor(foreground);
                    Console.Write(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public static void Write(string text, int offset)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.Write(line.Insert(0, new string(' ', offset)));
                }
            }

            public static void Write(string text, int offset, ConsoleColor? foreground)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.Write(new string(' ', offset));

                    ConsoleUtils.SetColor(foreground);
                    Console.Write(line);
                    ConsoleUtils.ResetColor();
                }
            }

            public static void Write(string text, int offset, ConsoleColor? foreground, ConsoleColor? background)
            {
                foreach (var line in text.SplitByLine())
                {
                    Console.Write(new string(' ', offset));

                    ConsoleUtils.SetColor(foreground, background);
                    Console.Write(line);
                    ConsoleUtils.ResetColor();
                }
            }
    }
}