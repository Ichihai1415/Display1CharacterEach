using Display1CharacterEach.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Display1CharacterEach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setting");
            Console.WriteLine("1.Text setting");
            if (!File.Exists("DisplayText.txt"))
                File.WriteAllText("DisplayText.txt", Resources.SampleText);
            Console.WriteLine("If change text, enter \"t\".");
            if (Console.ReadLine().Contains("t"))
            {
                Console.WriteLine("Please write display text in \"DisplayText.txt\".");
                Process.Start("notepad.exe", "DisplayText.txt");
                Console.WriteLine("If finish write, press \"Enter\" key.");
                Console.ReadLine();
            }
            string Text = File.ReadAllText("DisplayText.txt");
            int Delay = -1;
            bool SkipSpace = false;
            Console.WriteLine("2.Delay setting");
            if (args.Count() >= 1)
                try
                {
                    Delay = Convert.ToInt32(args[0]);
                }
                catch
                {
                    Console.WriteLine("Failed to acquire delay.");
                }
            while (Delay < 0)
                try
                {
                    Console.WriteLine("Enter delay. (milliseconds)");
                    Delay = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Failed to acquire delay. Please try again.");
                }
            Console.WriteLine("3.Skip setting");
            if (args.Count() >= 2)
                SkipSpace = args[1] == "true";
            else
            {
                Console.WriteLine("If skip the delay when space(\" \"), enter \"t\".");
                SkipSpace = Console.ReadLine().Contains("t");
            }
            Console.WriteLine("Preparing for display...");
            Thread.Sleep(1000);
            while (true)
            {
                string _Text = Text;
                Console.Clear();
                while (_Text.Length > 0)
                {
                    if (SkipSpace == false || _Text.First() != ' ')
                        Thread.Sleep(Delay);
                    Console.Write(_Text.First());
                    _Text = _Text.Remove(0, 1);
                }
            }
        }
    }
}
