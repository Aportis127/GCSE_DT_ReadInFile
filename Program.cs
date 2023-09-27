using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Data;

namespace NotAnEscapeRoom
{
    class Program
    {
        static string Path = "Users.csv";
        static int NumOfDataPoints = 3;
        static int NumOfUsers;
        static string[,] UserData;
        static string CurrentUser;

        static void Main(string[] args)
        {
            if (File.Exists(Path) == false)
            {
                SetUpFile();
            }
            ReadIn();
        }

        static void SetUpFile()
        {
            File.WriteAllText(Path, "");
            Console.WriteLine("File successfully created!");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

            ReadIn();
        }

        static void ReadIn()
        {

            Console.Clear();

            //Finding out how many users are in the program

            NumOfUsers = File.ReadAllLines(Path).Length;
            UserData = new string[NumOfUsers, NumOfDataPoints];


            string[] RawData = new string[NumOfUsers];

            RawData = File.ReadAllLines(Path);
            string[] TempUser;

            for (int i = 0; i < RawData.Length; i++)
            {
                TempUser = RawData[i].Split(',');

                for (int j = 0; j < NumOfDataPoints; j++)
                {
                    UserData[i, j] = TempUser[j];
                }
            }
            Console.WriteLine("File successfully loaded!");
            Console.WriteLine("\nPress any key to continue.");


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
            LogInMenu();
        }

        static void LogInMenu()
        {
            Banner("Log-In Menu");
            Console.WriteLine("Enter an option:");
            Console.WriteLine("\n1. Log-In\n2. Add user");
            string Option = Console.ReadLine();

            switch (Option)
            {
                case "1":
                    LogIn();
                    break;
                case "2":
                //FINISH
            }

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
                        Console.WriteLine("\nPress any key to continue.");
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
            string Username = "", Password = "";
            do
            {    
                Console.Clear();
                Banner("Add User");


                Console.WriteLine("Enter a username: ");
                Username = Console.ReadLine();

                Console.WriteLine("Enter a password: ");
                Password = Console.ReadLine();

            } while (Username == "" && Password == "");

            string[] NewUser = new string[] { Username, Password, "0"}; //New user to write
            NumOfUsers += 1;
            string[,] NewUserData = new string[NumOfUsers, NumOfDataPoints]; //Creates the new array with one extra user

            for (int i = 0; i < NumOfUsers - 1; i++)//Adds all but new user to array
            {
                for (int j = 0; j < NumOfDataPoints; j++)
                {
                    NewUserData[i, j] = UserData[i, j];
                }
            }

            for (int i = NumOfUsers - 1; i < NumOfUsers; i++)//adds just the new user
            {
                for (int j = 0; j < NumOfDataPoints; j++)
                {
                    NewUserData[i, j] = NewUser[j];
                }
            }

            /*for(int i = 0; i<NumOfUsers + 1; i++)
            {
                for(int j=0; j<NumOfDataPoints; j++)
                {
                    Console.WriteLine(NewUserData[i,j]);
                }
            }*/
            //This is a test to see if the File is loading everything in correctly

            Console.WriteLine("New user successfully created!");
            Console.WriteLine("\nPress any key to write to file.");

            Console.ReadKey();
            SaveToFile(NewUserData);
        }

        static void Quiz()
        {
            Console.Clear();
            Banner("Quiz");
            
            



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

        static void SaveToFile(string[,] UserData)
        {
            Console.Clear();
            File.WriteAllText(Path, "");//Clears file
            string TempUser;

            for (int i = 0; i < NumOfUsers; i++)
            {
                for (int j = 0; j < NumOfDataPoints; j++)
                {
                    TempUser = UserData[i, j] + ",";
                    File.AppendAllText(Path, TempUser);
                }
                File.AppendAllText(Path, "\n");
            }

            Console.WriteLine("File saved successfully!");
            Console.WriteLine("\nPress any key to read-in file.");
            Console.ReadKey();
            ReadIn();
        }
    }
}