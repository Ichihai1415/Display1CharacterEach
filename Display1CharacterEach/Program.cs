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
            if (args.Length >= 2)
                args[1] = args[1].Replace("space", " ");
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
            string SkipChar_ = "";
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
            try//if (args.Count() >= 2) 無理やり処理統一
            {
                char SkipChar__ = Convert.ToChar(args[1]);//1文字である必要
                SkipChar_ = Convert.ToString(SkipChar__);
            }
            catch//spaceは置換済み
            {
                if (args.Count() >= 2)
                    if (args[1] != "null" && args[1] != "byte")
                        Console.WriteLine("Failed to set one character to skip delay.");
                while (SkipChar_ == "")
                    try
                    {
                        if (args.Count() >= 2)
                            if (args[1] == "null")
                                break;
                            else if (args[1] == "byte")
                            {
                                byte[] TextBinaly = System.Text.Encoding.UTF8.GetBytes(Text);
                                Text = "";
                                foreach (byte TextBinaly_ in TextBinaly)
                                    Text += TextBinaly_;
                                break;
                            }
                        Console.WriteLine("Enter one character to skip delay or \"byte\" to display binary(UTF-8) or　\"Enter\" to disable.");
                        string SkipChar__ = Console.ReadLine();
                        if (SkipChar__.Contains("byte"))
                        {
                            byte[] TextBinaly = System.Text.Encoding.UTF8.GetBytes(Text);
                            Text = "";
                            foreach (byte TextBinaly_ in TextBinaly)
                                Text += TextBinaly_;
                            break;
                        }
                        if (SkipChar__ == "")
                            break;
                        char SkipChar___ = Convert.ToChar(SkipChar__);//できるかチェック
                        SkipChar_ = Convert.ToString(SkipChar___);
                    }
                    catch
                    {
                        Console.WriteLine("Failed to set one character to skip delay. Must be one character or \"byte\" or null. Please try again.");
                    }
            }
            Console.WriteLine("Preparing for display...");
            if (args.Count() == 0)
                Thread.Sleep(1000);
            if (SkipChar_ != "")
            {
                char SkipChar = Convert.ToChar(SkipChar_);
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
