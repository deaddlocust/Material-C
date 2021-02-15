using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialC
{
    class Program
    {
        static void Main(string[] args)
        {
            var ogcolor = Console.ForegroundColor;
            var ogback = Console.BackgroundColor;
            string banner = @"___  ___      _            _       _   _____ 
|  \/  |     | |          (_)     | | /  __ \
| .  . | __ _| |_ ___ _ __ _  __ _| | | /  \/
| |\/| |/ _` | __/ _ \ '__| |/ _` | | | |    
| |  | | (_| | ||  __/ |  | | (_| | | | \__/\
\_|  |_/\__,_|\__\___|_|  |_|\__,_|_|  \____/";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(banner);
            Console.WriteLine();
            if (args.Length < 1)
            {
                Console.WriteLine("SYNTAX: materialc [filename]");
                Console.ForegroundColor = ogcolor;
                Environment.Exit(0);
            }
            if (File.Exists("memory.temp"))
            {
                File.Delete("memory.temp");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Preparing to compile " + args[0] + "...");

            using (StreamWriter sw = File.CreateText("memory.temp"))
            {
                string tocompile = @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {";
                sw.Write(tocompile);
            }

            Parse(args[0], ogcolor, ogback);
        }

        static void Parse(string codefile, ConsoleColor ogcolor, ConsoleColor ogback)
        {
            if (File.Exists(codefile) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Could not find Material C script file (.mcs) in the current directory.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Environment.Exit(0);
            }
            if (codefile.Contains(Path.GetExtension(codefile)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Invalid file | Not .mcs file");
                Console.ForegroundColor = ConsoleColor.Gray;
                Environment.Exit(0);
            }
            Console.WriteLine("Parsing code...");
            string[] lines = File.ReadAllLines(codefile);
            foreach (string line in lines)
            {
                string[] parse = line.Split(null);
                if (parse[0] == "print")
                {
                    string parse1 = parse[1];
                    if (Convert.ToString(parse1[0]) == "\"")
                    {
                        string content = line.Remove(0, line.IndexOf(' ') + 1);
                        if (Convert.ToString(content.Last()) == "\"")
                        {
                            string newcode = Environment.NewLine + "\t\tConsole.WriteLine(\"" + content.Replace("\"", string.Empty) + "\");";
                            using (TextWriter writer = new StreamWriter("memory.temp", true))
                            {
                                writer.WriteLine(newcode);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("[Error] Missing \" | Invalid Syntax");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        }
                    }
                    else
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.WriteLine(" + line.Remove(0, line.IndexOf(' ') + 1) + ");";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                }
                if (parse[0] == "set")
                {
                    string split = "= ";
                    string tempcontent = line.Substring(line.IndexOf(split) + split.Length);
                    if (Convert.ToString(tempcontent[0]) == "\"")
                    {
                        if (Convert.ToString(tempcontent.Last()) == "\"")
                        {
                            string content = tempcontent.Replace("\"", string.Empty);
                            string newcode = Environment.NewLine + "\t\tvar " + parse[1] + " = \"" + content + "\";";
                            using (TextWriter writer = new StreamWriter("memory.temp", true))
                            {
                                writer.WriteLine(newcode);
                            }
                        }
                    }
                    else
                    {
                        string content = tempcontent;
                        string newcode = Environment.NewLine + "\t\tvar " + parse[1] + " = " + content + ";";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                }
                if (parse[0] == "forecolor")
                {
                    if (parse[1] == "red")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Red;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "blue")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Blue;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "cyan")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Cyan;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "green")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Green;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "gray")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Gray;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "white")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.White;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "black")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Black;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "yellow")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Yellow;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "magenta")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.ForegroundColor = ConsoleColor.Magenta;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                }

                if (parse[0] == "backcolor")
                {
                    if (parse[1] == "red")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Red;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "blue")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Blue;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "cyan")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Cyan;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "green")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Green;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "gray")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Gray;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "white")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.White;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "black")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Black;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "yellow")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Yellow;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                    if (parse[1] == "magenta")
                    {
                        string newcode = Environment.NewLine + "\t\tConsole.BackgroundColor = ConsoleColor.Magenta;";
                        using (TextWriter writer = new StreamWriter("memory.temp", true))
                        {
                            writer.WriteLine(newcode);
                        }
                    }
                }

                if (parse[0] == "clear")
                {
                    string newcode = Environment.NewLine + "\t\tConsole.Clear();";
                    using (TextWriter writer = new StreamWriter("memory.temp", true))
                    {
                        writer.WriteLine(newcode);
                    }
                }

                if (parse[0] == "if")
                {
                    string newcode = Environment.NewLine + "\t\tif (" + parse[1] + " " + parse[2] + " " + parse[3] + ")" + Environment.NewLine + "{";
                    using (TextWriter writer = new StreamWriter("memory.temp", true))
                    {
                        writer.WriteLine(newcode);
                    }
                }

                if (parse[0] == "end")
                {
                    string newcode = Environment.NewLine + "\t\t}";
                    using (TextWriter writer = new StreamWriter("memory.temp", true))
                    {
                        writer.WriteLine(newcode);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[-] Compiling...");
            using (TextWriter writer = new StreamWriter("memory.temp", true))
            {
                string end = @"        }
    }
}";
                writer.WriteLine(end);
            }
            string exename = "out.exe";
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters compars = new CompilerParameters();

            compars.GenerateExecutable = true;
            compars.OutputAssembly = exename;
            compars.GenerateInMemory = false;
            compars.TreatWarningsAsErrors = false;

            CompilerResults res = provider.CompileAssemblyFromFile(compars, "memory.temp");

            if (res.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in res.Errors)
                {
                    int line = Convert.ToInt32(CompErr.Line) - 9;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] " + CompErr.ErrorText);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                foreach (CompilerError CompErr in res.Errors)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[C#] Error code: " + CompErr.ErrorNumber + "| Line " + CompErr.Line);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Code compiled successfully.");
                Console.ForegroundColor = ogcolor;
            }
            File.Delete("memory.temp");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}