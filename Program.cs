using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Life life = new Life(20, 10);
            

            string q="N";

            do
            {

                int i = 1;
                int MaxCount = 100;

                //life.randomInit();
                life.initSomeColony();
                //life.showArray(life.CurrentArray);
                //life.initFill3PointsInLine(); // test case
                //life.initFillSquare(); // test case
              

                do
                {
                    Console.Clear();
                    Console.WriteLine("Game life started");
                    Console.WriteLine("Stas games development inc. All right reserved");

                    Console.WriteLine();
                    Console.WriteLine("step "+i.ToString()+"/"+MaxCount.ToString());
                    if (!life.Next())
                    {
                        Console.WriteLine("Apocalypsis happened");
                        break;
                    }

                    for (int k = 0; k < life.W; k++) Console.Write("-");
                    Console.WriteLine();
                    life.showArray(life.NextArray);
                    Console.WriteLine();
                    for (int k = 0; k < life.W; k++) Console.Write("-");
                    i++;
                    Thread.Sleep(200);

                }
                while (i != MaxCount+1);

                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("One more game Y/N ?");
                q = Convert.ToString(Console.ReadLine());
            }
            while (q == "Y");


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

        public char[,] CurrentArray;
        public char[,] NextArray;
        public int[,] NeighboursArray;



        public Life(int w, int h)
        {
            W = w;
            H = h;

            // init arrays
            CurrentArray = new char[H, W];
            NextArray = new char[H, W];
            NeighboursArray = new int[H, W];

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
            CurrentArray=NextArray;

            return true;
        }


        // 3 rules..

        public bool IsLonely(int i, int j)
        {
            return (NeighboursArray[i, j] < 2);

            //bool result = false;
            //result = CountAliveNeighbours(i, j) < 2; // counts not correct...
            //return result;
        }


        public bool IsTooClose(int i, int j)
        {
            return (NeighboursArray[i, j] > 3);
            //  return (CountAliveNeighbours(i, j) > 3); // counts not correct...
        }

        public bool IsBirth(int i, int j)
        {
            return (CurrentArray[i, j] == Dead) && (NeighboursArray[i, j] == 3);
        }


        public void countNeighbours()
        {
            // count neighbours
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    NeighboursArray[i, j] = CountAliveNeighbours(i, j);
                   // Console.Write(CountAliveNeighbours(i, j));
                }
               // Console.WriteLine();
            }
        }


        public void initFill3PointsInLine()
        {
            CurrentArray[1, 1] = Alive;
            CurrentArray[2, 1] = Alive;
            CurrentArray[3, 1] = Alive;
        }

        public void initFillSquare()
        {
            CurrentArray[1, 1] = Alive;
            CurrentArray[1, 2] = Alive;
            //  CurrentArray[1, 3] = Alive;

            CurrentArray[2, 1] = Alive;
            CurrentArray[2, 2] = Alive;
            //  CurrentArray[2, 3] = Alive;

            //CurrentArray[3, 1] = Alive;
            //CurrentArray[3, 2] = Alive;
            //CurrentArray[3, 3] = Alive;
        }

        public void initSomeColony()
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




        public void showArray(char[,] SomeArray)
        {
            for (int i = 0; i < SomeArray.GetLength(0); i++)
            {
                for (int j = 0; j < SomeArray.GetLength(1); j++)
                {
                    Console.Write(SomeArray[i, j]);
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
