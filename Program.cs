using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Life life = new Life();
            Console.WriteLine("Enter width");
            life.Width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter height");
            life.Height = Convert.ToInt32(Console.ReadLine());

            //life.initFillAllZero();
            //life.initFill3PointsInLine();
            life.initFill3Square();
            life.printArray();
            life.IsLonely(1, 1);



            Console.Read();

        }
    }


    class Life
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int[,] LifeArray;
        public int[,] LifeArrayNextStep;

        public void Scan()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (IsLonely(i, j))
                    {
                        LifeArrayNextStep[i, j] = 0;
                    }

                    LifeArray[i, j] = 0;
                }

            }
        }



        public void initFillAllZero()
        {

            LifeArray = new int[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    LifeArray[i, j] = 0;
                }

            }

        }


        public void initFill3PointsInLine()
        {

            initFillAllZero();
            LifeArray[1, 1] = 1;
            LifeArray[1, 2] = 1;
            LifeArray[1, 3] = 1;

        }

        public void initFill3Square()
        {

            initFillAllZero();
            LifeArray[1, 1] = 1;
            LifeArray[1, 2] = 1;
            LifeArray[1, 3] = 1;

            LifeArray[2, 1] = 1;
            LifeArray[2, 2] = 1;
            LifeArray[2, 3] = 1;

            LifeArray[3, 1] = 1;
            LifeArray[3, 2] = 1;
            LifeArray[3, 3] = 1;

        }


        public void printArray()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Console.Write(LifeArray[i, j]);
                }
                Console.WriteLine();
            }
        }


        // 3 rules..

        public bool IsLonely(int i, int j)
        {

            // 3*3 borders
            int left = i - 1;
            int right = i + 1;
            int top = j - 1;
            int bottom = j + 1;

            int leftPositive = left;
            int topPositive = top;

            // cycle

            int AliveNeighboursCount = 0;

            for (int k = left; k <= right; k++)
            {
                for (int r = top; r <= bottom; r++)
                {
                    if (((LifeArray[k, r] == 1))&&(k>=0)&&(r>=0))//&&((k!= i) && (r!= j)))
                    {
                        AliveNeighboursCount++;
                    }
                }


            }

            //AliveNeighboursCount-= LifeArray[i, j];
            //Console.WriteLine(AliveNeighboursCount);
            if (AliveNeighboursCount < 2) return true; else return false;
        }



        public bool IsTooClose(int i, int j)
        {

            // 3*3 borders
            int left = i - 1;
            int right = i + 1;
            int top = j - 1;
            int bottom = j + 1;

            int leftPositive = left;
            int topPositive = top;

            // cycle

            int AliveNeighboursCount = 0;

            for (int k = left; k <= right; k++)
            {
                for (int r = top; r <= bottom; r++)
                {
                    if (((LifeArray[k, r] == 1)) && (k >= 0) && (r >= 0))//&&((k!= i) && (r!= j)))
                    {
                        AliveNeighboursCount++;
                    }
                }


            }

            //AliveNeighboursCount-= LifeArray[i, j];
            //Console.WriteLine(AliveNeighboursCount);
            if (AliveNeighboursCount >3) return true; else return false;
        }


        public bool IsBirth(int i, int j)
        {

            // 3*3 borders
            int left = i - 1;
            int right = i + 1;
            int top = j - 1;
            int bottom = j + 1;

            int leftPositive = left;
            int topPositive = top;

            // cycle

            int AliveNeighboursCount = 0;

            for (int k = left; k <= right; k++)
            {
                for (int r = top; r <= bottom; r++)
                {
                    if (((LifeArray[k, r] == 1)) && (k >= 0) && (r >= 0))//&&((k!= i) && (r!= j)))
                    {
                        AliveNeighboursCount++;
                    }
                }


            }

            //AliveNeighboursCount-= LifeArray[i, j];
            //Console.WriteLine(AliveNeighboursCount);
            if (AliveNeighboursCount == 3) return true; else return false;
        }




    }


}
