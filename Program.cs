using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;

namespace NotAnEscapeRoom
{
    class Program
    {
        static string Path = "Users.csv";
        static int NumOfDataPoints = 2; //Just username and password for now
        static string[,] Users = new string[3, NumOfDataPoints];

        static void Main(string[] args)
        {
            SetUpFile();
        }

        static void SetUpFile()
        {
            if (File.Exists(Path) == false)
            {
                File.Create(Path);
            }
            ReadIn();
        }

        static void ReadIn()
        {
            string[] RawData = new string[3];
            RawData = File.ReadAllLines(Path);
            string[] TempUser;

            for (int i = 0; i < RawData.Length; i++)
            {
                TempUser = RawData[i].Split(',');

                for (int j = 0; j < NumOfDataPoints; j++)
                {
                    Users[i, j] = TempUser[j];
                }
            }
            Console.WriteLine("File successfully loaded!");

            /*for(int i = 0; i<3; i++)
            {
                for(int j=0; j<NumOfDataPoints; j++)
                {
                    Console.WriteLine(Users[i,j]);
                }
            }
            */
            //This is a test to see if the File is loading everything in correctly
            Console.ReadKey();
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Banner("Main Menu");
                string Option = "0";
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Quiz");
                Console.WriteLine("3. Stats");
                Console.WriteLine("0. Quit");
                Console.WriteLine("\nSelect an option.");

                Option = Console.ReadLine();

                switch (Option)
                {
                    case "1":
                        AddUser();
                        break;

                    case "2":
                        Quiz();
                        break;

                    case "3":
                        Stats();
                        break;

                    case "0":
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please enter: 1, 2, 3 or 0");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Banner(string Location)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Welcome to NOT An Esacpe Room: " + Location);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        }

        static void AddUser()
        {
            Console.Clear();
            Banner("Add User");
            Console.WriteLine("(Add User)");
            Console.ReadKey();
            MainMenu();
        }

        static void Quiz()
        {
            Console.Clear();
            Banner("Quiz");
            Console.WriteLine("(Quiz)");
            Console.ReadKey();
            MainMenu();
        }

        static void Stats()
        {
            Console.Clear();
            Banner("Stats");
            Console.WriteLine("(Stats)");
            Console.ReadKey();
            MainMenu();
        }
    }
}
