﻿using System;
using System.Collections.Generic;
namespace Sudokusolver
{
    public class tests
    {
        public static int[,] PickRandomTest()
        {
            var rand = new Random(); 
            int pick = rand.Next(0, testslist.Count);
            return testslist[pick];
        }
        
        static List<int[,]> testslist = new List<int[,]>();
        
        //заполнить список, чтобы тесты работали
        //можно (нужно?) сделать элегантнее
        static public void EnableTests() 
        {
            
            testslist.Add(test1);
            testslist.Add(test2);
            testslist.Add(test3);
            testslist.Add(test4);
            testslist.Add(test5);
            testslist.Add(test6);
            testslist.Add(test7);
            testslist.Add(test8);
            testslist.Add(test9);
            testslist.Add(test10);
        }

        static private int[,] test1 = new int[9, 9] //средний
        {
        { 0,0,0, 0,0,0, 0,0,0},
        { 0,8,2, 0,0,0, 3,6,0},
        { 0,6,0, 9,0,8, 0,2,0},

        { 0,0,4, 0,2,0, 5,0,0},
        { 0,0,0, 8,0,7, 0,0,0},
        { 0,0,1, 0,4,0, 9,0,0},

        { 0,5,0, 6,0,4, 0,7,0},
        { 0,7,8, 0,0,0, 2,9,0},
        { 0,0,0, 0,0,0, 0,0,0}
        };

        static private int[,] test2 = new int[9, 9] //легкий
        {
        { 9,2,0, 8,0,0, 3,0,0},
        { 5,0,0, 0,9,6, 8,0,2},
        { 1,0,6, 0,5,3, 0,9,7},

        { 0,0,3, 9,0,0, 6,0,1},
        { 6,7,9, 0,0,5, 0,0,8},
        { 0,4,0, 6,3,8, 5,7,0},

        { 4,0,0, 3,0,0, 0,0,5},
        { 0,6,8, 5,0,4, 9,0,3},
        { 0,0,5, 0,8,2, 0,6,4}
        };

        static private int[,] test3 = new int[9, 9] //средний
        {
        { 0,5,0, 0,0,0, 0,8,0},
        { 2,8,0, 0,7,0, 0,6,3},
        { 4,0,0, 0,0,0, 0,0,2},

        { 0,0,5, 0,6,0, 7,0,0},
        { 0,7,0, 1,0,5, 0,2,0},
        { 0,0,9, 0,8,0, 3,0,0},

        { 3,0,0, 0,0,0, 0,0,5},
        { 5,1,0, 0,2,0, 0,3,4},
        { 0,6,0, 0,0,0, 0,1,0}
        };

        static private int[,] test4 = new int[9, 9] //средний
        {
        { 0,4,0, 8,0,3, 0,1,0},
        { 0,0,1, 0,0,0, 4,0,0},
        { 7,0,0, 0,9,0, 0,0,2},

        { 0,0,3, 0,0,0, 5,0,0},
        { 0,0,0, 2,6,4, 0,0,0},
        { 0,0,9, 0,0,0, 1,0,0},

        { 8,0,0, 0,2,0, 0,0,5},
        { 0,0,6, 0,0,0, 2,0,0},
        { 0,5,0, 7,0,8, 0,9,0}
        };


        static private int[,] test5 = new int[9, 9] //самый сложный, второй уровень перебора
        {
        { 0,0,5, 3,0,0, 0,0,0},
        { 8,0,0, 0,0,0, 0,2,0},
        { 0,7,0, 0,1,0, 5,0,0},

        { 4,0,0, 0,0,5, 3,0,0},
        { 0,1,0, 0,7,0, 0,0,6},
        { 0,0,3, 2,0,0, 0,8,0},

        { 0,6,0, 5,0,0, 0,0,9},
        { 0,0,4, 0,0,0, 0,3,0},
        { 0,0,0, 0,0,9, 7,0,0}
        };

        static private int[,] test6 = new int[9, 9] //несколько решений /перебор второго уровня
        {
        { 7,4,2, 0,8,0, 0,0,0},
        { 9,0,0, 0,0,3, 0,0,0},
        { 0,0,0, 0,6,7, 1,9,0},

        { 0,8,0, 0,7,2, 3,0,0},
        { 0,5,9, 0,0,0, 0,2,7},
        { 0,2,0, 8,0,0, 0,6,0},

        { 2,0,3, 0,4,0, 0,0,8},
        { 8,0,5, 0,0,0, 0,4,6},
        { 0,0,0, 0,0,8, 0,0,0}
        };

        static private int[,] test7 = new int[9, 9]
        {
        { 6,7,0, 0,8,0, 0,3,1},
        { 0,0,0, 4,0,7, 0,0,0},
        { 0,0,3, 0,0,0, 2,0,0},

        { 0,3,0, 0,0,0, 0,9,0},
        { 4,0,0, 3,9,6, 0,0,8},
        { 0,1,0, 0,0,0, 0,4,0},

        { 0,0,5, 0,0,0, 3,0,0},
        { 0,0,0, 7,0,5, 0,0,0},
        { 1,8,0, 0,4,0, 0,5,9}
        };

        static private int[,] test8 = new int[9, 9]
        {
        { 4,0,5, 0,3,0, 6,0,9},
        { 0,0,3, 4,0,1, 0,0,0},
        { 2,0,0, 0,7,6, 0,8,3},

        { 0,9,6, 0,4,0, 0,3,0},
        { 5,0,4, 9,0,3, 2,0,1},
        { 0,2,0, 0,8,0, 9,6,0},

        { 1,4,0, 6,5,0, 0,0,8},
        { 0,0,0, 8,0,4, 5,0,0},
        { 6,0,8, 0,2,0, 1,0,7}
        };

        static private int[,] test9 = new int[9, 9]
        {
        { 0,0,0, 7,0,9, 0,0,0},
        { 6,0,0, 0,0,0, 0,0,9},
        { 0,9,0, 4,0,2, 0,1,0},

        { 9,0,2, 0,0,0, 8,0,5},
        { 0,0,0, 0,8,0, 0,0,0},
        { 8,0,3, 0,0,0, 6,0,1},

        { 0,6,0, 9,0,5, 0,7,0},
        { 7,0,0, 0,0,0, 0,0,3},
        { 0,0,0, 1,0,3, 0,0,0}
        };

        static private int[,] test10 = new int[9, 9]
        {
        { 4,5,0, 0,0,1, 0,0,6},
        { 0,0,0, 0,4,0, 9,0,2},
        { 0,7,0, 0,0,0, 0,0,0},

        { 5,0,0, 3,0,7, 0,0,0},
        { 0,8,0, 0,0,0, 0,5,0},
        { 0,0,0, 9,0,6, 0,0,3},

        { 0,0,0, 0,0,0, 0,9,0},
        { 3,0,1, 0,7,0, 0,0,0},
        { 7,0,0, 5,0,0, 0,6,4}
        };

        // 2 уровень перебора
        static public int[,] test11 = new int[9, 9]
        {
        { 8,0,0, 0,0,0, 0,0,0},
        { 0,0,3, 6,0,0, 0,0,0},
        { 0,7,0, 0,9,0, 2,0,0},

        { 0,5,0, 0,0,7, 0,0,0},
        { 0,0,0, 0,4,5, 7,0,0},
        { 0,0,0, 1,0,0, 0,3,0},

        { 0,0,1, 0,0,0, 0,6,8},
        { 0,0,8, 5,0,0, 0,1,0},
        { 0,9,0, 0,0,0, 4,0,0}
        };

        // 1ый уровень перебора
        static public int[,] test12 = new int[9, 9]
        {
        { 7,0,8, 0,0,0, 3,0,0},
        { 0,0,0, 2,0,1, 0,0,0},
        { 5,0,0, 0,0,0, 0,0,0},

        { 0,4,0, 0,0,0, 0,2,6},
        { 3,0,0, 0,8,0, 0,0,0},
        { 0,0,0, 1,0,0, 0,9,0},

        { 0,9,0, 6,0,0, 0,0,4},
        { 0,0,0, 0,7,0, 5,0,0},
        { 0,0,0, 0,0,0, 0,0,0}
        };
        /** /
        static public int[,] test = new int[9, 9] 
        {
        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0},

        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0},

        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0},
        { 0,0,0, 0,0,0, 0,0,0}
        };
        /**/


    }

}