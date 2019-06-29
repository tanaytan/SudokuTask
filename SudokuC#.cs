using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        { // These are grids
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1,}
            };
            //Tests if the method works
            SudokuChecker(goodSudoku1);
        }




        // The method
        static bool SudokuChecker(Array grid)
        { // RULES FOR VALIDATION:
            if (grid.GetLength(0) < 1)
            {
                return false;
            }

            foreach (Array i in grid)
            {
                if (i.GetLength(0) != grid.GetLength(0))
                {
                    return false;
                }
            }
            if ((Math.Pow(grid.GetLength(0), 0.5) !=  (int)(Math.Pow(grid.GetLength(0), 0.5)))){ 
                return false;
            }

            // FUNCTION TO CHECK FOR DUPLICATES: ( I can't find how to find range )
            bool DupChecker(Array x)
            {
                foreach (int i in System.Linq.Range(x.GetLength(0)))        //<- Range(len(x))?
                {
                    if (x.Length != x.Distinct().Count())                   //<- Python: if x[i] in x [i +1 :]
                                                                            // Here I tried finding a method online but it didn't work as expected
                    {
                        Console.WriteLine("Contains duplicates");
                        return true;
                    }
                    return false;
                }
                return false;
            }
                
            foreach(Array row in  grid){
                if (DupChecker(row)){
                    return false;
                }
            }
            
            foreach (Array col in Range(grid.GetLength(0)))                         //<- Python version: (Range of len(x))
            {
                int[][] column = { };
                foreach (Array row in Range(grid.GetLength(0)))                     //<- Same as above: (Range of len(x))
                {
                    int val = grid[row][col];
                    column.Append(val); 
                }
                
                if (DupChecker(column))
                {
                    return false;
                }
            }

            // Where it really starts to get messed up (Function to check boxes):
            bool BoxCheck(Array grid)
            {
                int boxSize = Convert.ToInt32(Math.Pow(grid.GetLength(0), 0.5));
                int sideSize = Convert.ToInt32(Math.Pow(grid.GetLength(0), 2));

                foreach (int i in range(boxSize, sideSize + 1, boxSize))
                {
                    foreach(int ii in range(boxSize, sideSize + 1, boxSize))
                    {
                        int[][] box = { };
                        foreach(Array row in grid[i-boxSize/*: i*/])                //<- the ': i' is in comments as it messes up the '{}s'
                        {
                            box.Append(row[ii - boxSize /*: ii*/]);                 //<- the ': ii' is in comments as it messes up the '{}s'
                        }
                        if (DupChecker(box)){
                            return false;
                        }
                    }
                }
                return true;
            }

            Console.WriteLine("Good Sudoku");
            return true;


        }




    }
    }

