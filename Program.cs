using System;
using System.IO;
using System.Timers;
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
        static bool LoggedIn;

        static System.Timers.Timer timer = new System.Timers.Timer(1000);
        static int CountDown = 11; // Seconds
        static char[] TypedCharacters = new char[100];
        static int CharacterCount;
        static bool AnsweredPrompt = false;
        static ConsoleKeyInfo CurrentKey;
        static int QuestionLineNum = 6;

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
            while (true)
            {
                Console.Clear();
                Banner("Log-In Menu");
                Console.WriteLine("Enter an option:");
                Console.WriteLine("\n1. Log-In\n2. Sign up");
                string Option = Console.ReadLine();

                switch (Option)
                {
                    case "1":
                        LogIn();
                        break;
                    case "2":
                        AddUser();
                        break;
                    default:
                        Console.WriteLine("\nEnter a valid option.");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        LogInMenu();
                        break;
                }
            }
        }

        static void LogIn()
        {
            for (int Attempts = 3; Attempts >= 0; Attempts--)
            {
                string UsernameAttempt = "", PasswordAttempt = "";

                Console.Clear();
                Banner("Log-In");
                Console.WriteLine("Attempts: " + Attempts);
                Console.WriteLine("Enter your username: ");
                UsernameAttempt = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                PasswordAttempt = Console.ReadLine();

                for (int j = 0; j < NumOfUsers; j++)
                {
                    if (UserData[j, 0] == UsernameAttempt && UserData[j, 1] == PasswordAttempt)
                    {
                        CurrentUser = UsernameAttempt;
                        LoggedIn = true;
                        Console.WriteLine("You have successfully logged into " + CurrentUser);
                        Console.ReadKey();
                        MainMenu();
                    }
                }
                Console.WriteLine("\nIncorrect username or password. Please try again.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine("You have ran out of password attempts, please re-enter the screen to try again.");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            LogInMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Banner("Main Menu");
                string Option = "0";
                
                Console.WriteLine("1. Quiz");
                Console.WriteLine("2. Stats");
                Console.WriteLine("0. Log Out");
                Console.WriteLine("\nSelect an option.");

                Option = Console.ReadLine();

                switch (Option)
                {
                    case "1":
                        Quiz();
                        break;

                    case "2":
                        Stats();
                        break;

                    case "0":
                        CurrentUser = "";
                        LoggedIn = false;
                        Console.WriteLine("You have successfully logged out");
                        Console.ReadKey();
                        LogInMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please enter: 1, 2 or 0");
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
            if (LoggedIn == true)
            {
                Console.WriteLine("You are currently logged in as " + CurrentUser);
            }
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

            string[] NewUser = new string[] { Username, Password, "0" }; //New user to write
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

            StartTimer();
            Console.Clear();
            Banner("Quiz");
            Console.WriteLine("What is the largest country by land mass?");

            while (!AnsweredPrompt)
            {
                CurrentKey = Console.ReadKey(true);

                switch (CurrentKey.Key)
                {
                    case (ConsoleKey.Enter):
                        CountDown = 0;
                        break;
                    case (ConsoleKey.Escape):
                        Environment.Exit(0);
                        break;
                    case (ConsoleKey.Delete):
                        DeleteChar();
                        break;
                    case (ConsoleKey.Backspace):
                        DeleteChar();
                        break;
                    default:
                        AddChar();
                        break;

                }

            }

                Console.ReadKey();
                MainMenu();
        }

        static void RedrawAnswer()
        {
            Console.SetCursorPosition(0, QuestionLineNum + 1);
            Console.Write("\n");
            Console.SetCursorPosition(0, QuestionLineNum + 1);
            for (int i = 0; i < TypedCharacters.Length; i++)
            {
                if (TypedCharacters[i].Equals(""))
                {
                    return;
                }
                Console.Write(TypedCharacters[i]);
            }
            Console.SetCursorPosition(CharacterCount, QuestionLineNum + 1);
        }
        static void AddChar()
        {
            if (CharacterCount < TypedCharacters.Length)
            {
                TypedCharacters[CharacterCount] = CurrentKey.KeyChar;
                CharacterCount++;
                TypeNewChar();
            }
        }
        static void DeleteChar()
        {

            if (CharacterCount >= 1 && CharacterCount < TypedCharacters.Length)
            {
                // Create a new array with the size one less than the original array
                char[] newArray = new char[TypedCharacters.Length - 1];

                // Copy elements before the index
                for (int i = 0; i < CharacterCount - 1; i++)
                {
                    newArray[i] = TypedCharacters[i];
                }

                // Copy elements after the index
                for (int i = CharacterCount - 1; i < newArray.Length; i++)
                {
                    newArray[i] = TypedCharacters[i + 1];
                }

                // Update the original array with the new array
                TypedCharacters = newArray;
                CharacterCount--;
                RedrawAnswer();
            }
        }
        static void TypeNewChar()
        {
            Console.SetCursorPosition(0, QuestionLineNum + 1);
            for (int i = 0; i < CharacterCount; i++)
            {
                Console.Write(TypedCharacters[i]);
            }
        }
        static void StartTimer()
        {
            timer.Elapsed += UpdateTimer;
            timer.Enabled = true;
        }

        static void UpdateTimer(object source, ElapsedEventArgs e)
        {
            if (AnsweredPrompt) return;
            if (CountDown-- <= 0)
            {
                // Put code for running out of time here
                CheckAnswer();
                return;


            }
            Console.SetCursorPosition(0, QuestionLineNum);
            Console.WriteLine(CountDown + " Seconds Left! ");
            Console.SetCursorPosition(CharacterCount, QuestionLineNum+1);
        }
        static void CheckAnswer()
        {
            string FinalAnswer = "";

            for (int i = 0; i < CharacterCount; i++)
            {
                FinalAnswer = FinalAnswer + TypedCharacters[i];
            }
            FinalAnswer = FinalAnswer.ToLower();

            Console.Clear();
            if (FinalAnswer == "russia")
            {
                Console.WriteLine("Well done! You got the correct answer.");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                if (FinalAnswer == "")
                {
                    Console.WriteLine("Too bad. The correct answer was Russia.");
                    Console.ReadKey();
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Too bad. The correct answer was Russia, and you answered" + FinalAnswer);
                    Console.ReadKey();
                    MainMenu();
                }

            }

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
