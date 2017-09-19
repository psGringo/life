using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;




namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] fileStrings = File.ReadAllLines("LifeGameInitial.txt");
            //int Height = File.ReadAllLines("LifeGameInitial.txt").Length;
            int Height = File.ReadLines(@"LifeGameInitial.txt").Count();
            int Width = File.ReadAllLines(@"LifeGameInitial.txt")[0].Length;
                  
            
            Life life = new Life(Height, Width,true);
            string consoleAnswer = "N";

            VladBolotin vb = new VladBolotin();

            do
            {
                int i = 1;
                int MaxCount = 100;                
                
                Console.WriteLine("Game life started");
                Console.WriteLine("How many steps in the game ?");
                MaxCount = Convert.ToInt32(Console.ReadLine());
                

                //life.initCurrentArray.initFillSomeColony(); // test case 
                //life.initCurrentArray.initFill3PointsInLine(); // test case
                if (life.IsInitFromFile) life.initCurrentArray.FromFile();
                else life.initCurrentArray.FillSomeColony();

                vb.initFillAllZero();
                vb.InitState();

                //life.showArray(life.CurrentArray);
                //Console.ReadLine();

                do
                {
                    Console.Clear();                 
                    Console.WriteLine("step " + i.ToString() + "/" + MaxCount.ToString());
                    if (!life.Next())
                    {
                        Console.WriteLine("Apocalypsis happened");
                        break;
                    }
                    vb.NextState();
                    Console.WriteLine("Stas                        Vlad");
                    for (int k = 0; k < life.W; k++) Console.Write("-");
                    Console.WriteLine();
                    //life.showArray(life.NextArray);
                    life.showBothArrays(life.NextArray,vb.LifeArrayNextStep);
                    Console.WriteLine();
                    for (int k = 0; k < life.W; k++) Console.Write("-");
                    i++;                    
                    
                    Thread.Sleep(6000);
                }
                while (i != MaxCount + 1);
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("One more game Y/N ?");
                consoleAnswer = Convert.ToString(Console.ReadLine());
            }
            while (consoleAnswer == "Y");
            Console.WriteLine("Stas games development inc. All right reserved");
            Console.Read();
    
        }

    }

    class Life
    {
        private char Alive = '*';
        private char Dead = ' ';

        public int W { get; set; }
        public int H { get; set; }
        public bool IsInitFromFile { get; set; }

        public char[,] CurrentArray;
        public InitCurrentArray initCurrentArray;
        public char[,] NextArray;
        public int[,] NeighboursCountArray;
        

        public Life(int h, int w, bool AIsInitFromFile)
        {
            W = w;
            H = h;
          
            // init arrays
            CurrentArray = new char[H, W];
            initCurrentArray = new InitCurrentArray(CurrentArray, Alive, Dead);
            IsInitFromFile = AIsInitFromFile;
            if (IsInitFromFile) { initCurrentArray.H = H; initCurrentArray.W = W; }

            NextArray = new char[H, W];
            NeighboursCountArray = new int[H, W];
            // fill them dead
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    CurrentArray[i, j] = Dead;
                    NextArray[i, j] = Dead;
                }
            }
        }
        public bool Next()
        {
            if (IsApocalypsis()) return false;
            countNeighbours();
            NextArray = CurrentArray;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    if ((IsLonely(i, j) || IsTooClose(i, j))) NextArray[i, j] = Dead;
                    if (IsBirth(i, j)) NextArray[i, j] = Alive;
                }
            }
            CurrentArray = NextArray;
            return true;
        }


        // 3 rules..
        public bool IsLonely(int i, int j)
        {
            return (NeighboursCountArray[i, j] < 2);
        }


        public bool IsTooClose(int i, int j)
        {
            return (NeighboursCountArray[i, j] > 3);
        }

        public bool IsBirth(int i, int j)
        {
            return (CurrentArray[i, j] == Dead) && (NeighboursCountArray[i, j] == 3);
        }


        public void countNeighbours()
        {
            // count neighbours
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    NeighboursCountArray[i, j] = CountAliveNeighbours(i, j);
                    // Console.Write(CountAliveNeighbours(i, j));
                }
                // Console.WriteLine();
            }
        }
        public bool IsApocalypsis()
        {
            bool b = true;
            for (int i = 0; i < CurrentArray.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentArray.GetLength(1); j++)
                {
                    if (CurrentArray[i, j] == Alive) b = false;
                }
            }
            return b;
        }

        public void showArray(char[,] AArray)
        {
            for (int i = 0; i < AArray.GetLength(0); i++)
            {
                for (int j = 0; j < AArray.GetLength(1); j++)
                {
                    Console.Write(AArray[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void showBothArrays(char[,] AArray, int[,] VladArray)
        {
            for (int i = 0; i < AArray.GetLength(0); i++)
            {
                for (int j = 0; j < AArray.GetLength(1); j++)
                {
                    Console.Write(AArray[i, j]);
                                   
                }
                Console.Write("      ");
                for (int k = 0; k < VladArray.GetLength(1); k++)
                {
                    if (VladArray[i+1, k] == 1) Console.Write(Alive);
                    else Console.Write(Dead);
                }
                Console.WriteLine();
            }
        }



        public int CountAliveNeighbours(int i, int j)
        {
            // 3*3 borders
            int left = j - 1;
            int right = j + 1;
            int top = i - 1;
            int bottom = i + 1;
            // cycle
            int AliveNeighboursCount = 0;
            for (int s = top; s <= bottom; s++)
            {
                for (int c = left; c <= right; c++)
                {
                    if (
                           (s >= 0)
                        && (c >= 0)
                        && (s < CurrentArray.GetLength(0))
                        && (c < CurrentArray.GetLength(1))
                        && (CurrentArray[s, c] == Alive)
                        )
                        AliveNeighboursCount++;
                }
            }
            if ((AliveNeighboursCount > 0) && (CurrentArray[i, j] == Alive)) --AliveNeighboursCount;
            return AliveNeighboursCount;
        }

    }


}
