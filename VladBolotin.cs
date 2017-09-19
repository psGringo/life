using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Life
{
    class VladBolotin
    {

     
            int Height = File.ReadAllLines("LifeGameInitial.txt").Length;
            int Width = File.ReadAllLines("LifeGameInitial.txt")[0].Length;
            public int[,] LifeArrayPrevStep;
            public int[,] LifeArrayNextStep;

            public void initFillAllZero()
            {
                LifeArrayPrevStep = new int[Height + 2, Width + 2];
                LifeArrayNextStep = new int[Height + 2, Width + 2];

                for (int i = 0; i < Height + 2; i++)
                {
                    for (int j = 0; j < Width + 2; j++)
                    {
                        LifeArrayPrevStep[i, j] = 0;
                        LifeArrayNextStep[i, j] = 0;
                    }

                }

            }
            public void InitState()
            {
                for (int i = 1; i < Height + 1; i++)
                {
                    for (int j = 1; j < Width + 1; j++)
                    {
                        LifeArrayPrevStep[i, j] = Int32.Parse((File.ReadAllLines("LifeGameInitial.txt")[i - 1]).Substring(j - 1, 1));
                    }

                }

            }
            public void NextState()
            {
                for (int i = 1; i < Height + 1; i++)
                {
                    for (int j = 1; j < Width + 1; j++)
                    {
                        int AliveNeighbCount = LifeArrayPrevStep[i - 1, j - 1] + LifeArrayPrevStep[i - 1, j] + LifeArrayPrevStep[i - 1, j + 1] +
                            LifeArrayPrevStep[i, j - 1] + LifeArrayPrevStep[i, j + 1] +
                            LifeArrayPrevStep[i + 1, j - 1] + LifeArrayPrevStep[i + 1, j] + LifeArrayPrevStep[i + 1, j + 1];
                        if ((AliveNeighbCount < 2) || (AliveNeighbCount > 3)) LifeArrayNextStep[i, j] = 0;
                        else if (AliveNeighbCount == 3) LifeArrayNextStep[i, j] = 1;
                        else LifeArrayNextStep[i, j] = LifeArrayPrevStep[i, j];
                    }

                }

                LifeArrayPrevStep = LifeArrayNextStep;
            }

            public void printInitState()
            {
                for (int i = 1; i < Height + 1; i++)
                {
                    for (int j = 1; j < Width + 1; j++)
                    {
                        Console.Write(LifeArrayPrevStep[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            public void printNewState()
            {
                for (int i = 1; i < Height + 1; i++)
                {
                    for (int j = 1; j < Width + 1; j++)
                    {
                        Console.Write(LifeArrayNextStep[i, j]);
                    }
                    Console.WriteLine();
                }
            }

        }



   
}
