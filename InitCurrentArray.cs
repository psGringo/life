using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Life
{
    class InitCurrentArray
    {
        public char Alive { get; set; }
        public char Dead { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public char[,] CurrentArray { get; set; }

        public InitCurrentArray(char [,] ACurrentArray, char AAlive, char ADead)
        {
            CurrentArray = ACurrentArray;
            Alive = AAlive;
            Dead = ADead;
        }

        // different init CurrentArray methods     

        public void FromFile()
        {
           string[] stringsInfile = File.ReadAllLines(@"LifeGameInitial.txt");
           int[,] InitialArrayInFile = new int[H,W];           
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    string s=(stringsInfile[i]).Substring(j, 1);
                    InitialArrayInFile[i, j] = Int32.Parse(s);
                    if (s == "1") CurrentArray[i, j] = Alive; else CurrentArray[i, j] = Dead;
                }
            }
        }



        public void Fill3PointsInLine()
        {
            CurrentArray[1, 1] = Alive;
            CurrentArray[2, 1] = Alive;
            CurrentArray[3, 1] = Alive;
        }
        public void FillSquare()
        {
            CurrentArray[1, 1] = Alive;
            CurrentArray[1, 2] = Alive;

            CurrentArray[2, 1] = Alive;
            CurrentArray[2, 2] = Alive;
        }
        public void FillSomeColony()
        {
            CurrentArray[1, 1] = Alive;
            CurrentArray[1, 2] = Alive;
            CurrentArray[1, 3] = Alive;

            CurrentArray[2, 1] = Alive;
            CurrentArray[2, 2] = Alive;
            CurrentArray[2, 3] = Alive;

            CurrentArray[3, 1] = Alive;
            CurrentArray[3, 2] = Alive;
            CurrentArray[3, 3] = Alive;

            CurrentArray[1, 4] = Alive;
            CurrentArray[2, 4] = Alive;
            CurrentArray[3, 4] = Alive;

            CurrentArray[1, 5] = Alive;
            CurrentArray[2, 5] = Alive;
            CurrentArray[3, 5] = Alive;

            CurrentArray[7, 1] = Alive;
            CurrentArray[7, 2] = Alive;
            CurrentArray[7, 3] = Alive;

            CurrentArray[8, 4] = Alive;
            CurrentArray[8, 2] = Alive;
            CurrentArray[8, 3] = Alive;

        }
        public bool NextBool()
        {
            // as simple as possible
            Random r = new Random();
            return r.Next(0, 2) == 1;
        }

        public void randomInit()
        {
            for (int i = 0; i < CurrentArray.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentArray.GetLength(1); j++)
                {
                    bool b = NextBool();
                    if (b) CurrentArray[i, j] = Alive; else CurrentArray[i, j] = Dead;
                }
            }
        }

    }
}
