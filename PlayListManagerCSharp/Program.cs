using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListManagerCSharp
{
    class Program
    {
        public static IPlaylist p; // Global Playlist
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome To PlayList Manager");
            Console.WriteLine("Type help for list of commands");
            ExecuteCommand();
        }

        public static void ExecuteCommand()
        {
            try
            {
                String command = Console.ReadLine();
                String[] commandWithParameters = command.Split(' ');
                switch (commandWithParameters[0].ToLower())
                {
                    case "help":
                        {
                            Console.WriteLine("Use Following Commands");
                            Console.WriteLine("1: Create <n>, creates a playlist with n tracks");
                            Console.WriteLine("2: Play <n>, plays track at ordinal number n");
                            Console.WriteLine("3: Insert <t> <x>, inserts track with identifier t at ordinal number x");
                            Console.WriteLine("4: Delete <n>, deletes record at ordinal number n");
                            Console.WriteLine("5: Print , prints order of playlist");
                            Console.WriteLine("6: Shuffle, shuffles the playlist");
                            Console.WriteLine("7: Exit , exits");
                            break;
                        }
                    case "exit":
                        {
                            return;
                        }       
                    case "create":
                        {
                            int N = Int32.Parse(commandWithParameters[1]);
                            p = new PlaylistVector(N);
                            p.Print();
                            break;
                        }
                    case "play":
                        {
                            int N = Int32.Parse(commandWithParameters[1]);
                            p.Play(N);
                            p.Print();
                            break;
                        }
                    case "print":
                        {
                            p.Print();
                            break;
                        }  
                    case "delete":
                        {
                            int N = Int32.Parse(commandWithParameters[1]);
                            p.Delete(N);
                            p.Print();
                            break;
                        }
                    case "insert":
                        {
                            int T = Int32.Parse(commandWithParameters[1]);
                            int X = Int32.Parse(commandWithParameters[2]);
                            p.Insert(T, X);
                            p.Print();
                            break;
                        }
                    case "shuffle":
                        {
                            p.Shuffle();
                            p.Print();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Command");
                            Console.WriteLine("Type help for list of commands");
                            break;
                        }
                        
                }
                ExecuteCommand();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error : {0} ", ex.Message);
                Console.ResetColor();
                ExecuteCommand();
            }


        }
    }
}
