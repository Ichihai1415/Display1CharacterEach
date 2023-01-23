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
            {
                File.WriteAllText("DisplayText.txt", Resources.SampleText);
                Console.WriteLine("\"DisplayText.txt\" not found. Sample text written to \"DisplayText.txt\".");
            }
            if (args.Count() == 0)//引数指定なし
            {
                Console.WriteLine("If change text, enter \"t\".");
                if (Console.ReadLine().Contains("t"))
                {
                    Console.WriteLine("Please write display text in \"DisplayText.txt\".");
                    Process.Start("notepad.exe", "DisplayText.txt");
                    Console.WriteLine("If finish writing, press \"Enter\" key.");
                    Console.ReadLine();
                }
            }
            string Text = File.ReadAllText("DisplayText.txt");
            int Delay = -1;
            string _SkipChar = "";
            Console.WriteLine("2.Delay setting");
            if (args.Count() >= 1)
                try
                {
                    Delay = Convert.ToInt32(args[0]);
                }
                catch
                {
                    Console.WriteLine("Failed to set delay.");
                }
            while (Delay < 0)
                try
                {
                    Console.WriteLine("Enter delay. (milliseconds)");
                    Delay = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Failed to set delay. Please try again.");
                }
            Console.WriteLine("3.Skip delay setting");
            if (args.Count() >= 2)
                try
                {
                    char SkipChar_ = Convert.ToChar(args[1].Remove(0, 1).Remove(1, 1));//1文字である必要
                    _SkipChar = Convert.ToString(SkipChar_);
                }
                catch
                {
                    Console.WriteLine("Failed to set skip character.");
                }
            while (_SkipChar == "")
            {
                try
                {
                    Console.WriteLine("Enter one character to skip delay or \"Enter\"");
                    string SkipChar__ = Console.ReadLine();
                    if (SkipChar__ == "")
                        break;
                    char SkipChar_ = Convert.ToChar(SkipChar__);
                    _SkipChar = Convert.ToString(SkipChar_);
                }
                catch
                {
                    Console.WriteLine("Failed to set one character to skip delay. Must be one character or null. Please try again.");
                }
            }
            if (_SkipChar != "")
            {
                char SkipChar = Convert.ToChar(_SkipChar);
                Console.WriteLine("Preparing for display...");
                if (args.Count() == 0)
                    Thread.Sleep(1000);
                while (true)
                {
                    string _Text = Text;
                    Console.Clear();
                    while (_Text.Length > 0)
                    {
                        if (_Text.First() != SkipChar)
                            Thread.Sleep(Delay);
                        Console.Write(_Text.First());
                        _Text = _Text.Remove(0, 1);
                    }
                }
            }
            else
            {
                Console.WriteLine("Preparing for display...");
                if (args.Count() == 0)
                    Thread.Sleep(1000);
                while (true)
                {
                    string _Text = Text;
                    Console.Clear();
                    while (_Text.Length > 0)
                    {
                        Thread.Sleep(Delay);
                        Console.Write(_Text.First());
                        _Text = _Text.Remove(0, 1);
                    }
                }
            }
        }
    }
}
