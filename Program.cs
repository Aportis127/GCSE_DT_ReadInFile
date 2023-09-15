using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnEscapeRoom
{
    class Program
    {
        static string Path = "Users.csv";
        static int NumOfDataPoints = 8;
        static string[,] Users = new string[3, NumOfDataPoints];
        
        static void Main(string[] args)
        {
            SetUpFile();
            
            
        }
        static void SetUpFile()
        {
            if(File.Exists(Path) == false)
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

            for (int i = 0; i<RawData.Length; i++)
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
            *///This is a test to see if the File is loading everything in correctly
            Console.ReadKey();
            MainMenu();
        }
        static void MainMenu()
        {
            
            Banner("Main Menu");
            int Option = 0;
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("\nSelect an option.");
            
            
        }
        static void Banner(string Location)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("NOT An Escape Room: " + Location);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        }
    }
}
