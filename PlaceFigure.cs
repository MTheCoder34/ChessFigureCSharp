using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceFigure
{
    class Program
    {
        private static void FindStartNegative(ref int x, ref int y)
        {
            while (x != 0 && y != 0)
            {
                x--; y--;
            }
        }
        private static void FindStartPositive(ref int x, ref int y)
        {
            while (x != 0 && y != 7)
            {
                x--; y++;
            }

        }
        private static bool CheckIndex(int[,] map, int n, int k)
        {
            int i = 0;
            int g = 0;
            while (i < map.GetLength(0) && map[n, i] == 0)
            {
                i++;
            }
            if (i == map.GetLength(0))
            {
                while (g < map.GetLength(1) && map[g, k] == 0)
                {
                    g++;
                }
            }
            if (g == map.GetLength(1))
            {
                i = k; g = n;
                FindStartNegative(ref i, ref g);
                while ((i != map.GetLength(0) && g != map.GetLength(1)) && map[g, i] == 0)
                {
                    i++;
                    g++;
                }
            }
            if (i == map.GetLength(0) || g == map.GetLength(1))
            {
                i = k;
                g = n;
                FindStartPositive(ref i, ref g);
                while ((i != 0 && g != 0) && map[g, i] == 0)
                {
                    i--;
                    g--;
                }
            }
            if (i == 0 || g == 0)
            {
                return true;
            }
            return false;
        }
        private static int[] Prev_n = new int[8];
        //n notifies the current row
        public static void ChoosePath(int[,] map, ref int[] Path, int n, int k)
        {
            if(k <= Path.Length-1)
            {
                if(n < map.GetLength(0))
                {
                    try
                    {
                        Path[k] = n;
                        if (IsIndexGood(map, n, k))
                        {
                            map[n, k] = 1;
                            Prev_n[k] = n;
                            n = 0;
                            while (n < map.GetLength(0) && !IsIndexGood(map, n, k + 1))
                            {
                                n++;
                            }
                            ChoosePath(map, ref Path, n, k + 1);
                        }
                    }
                    catch(IndexOutOfRangeException e)
                    {

                    }
                }
                else
                {
                    k--;
                    map[Prev_n[k], k] = 0;
                    n = Prev_n[k] + 1;
                    while (n < map.GetLength(0) && !IsIndexGood(map, n, k))
                    {
                        n++;
                    }
                    Path[k] = n;
                    ChoosePath(map, ref Path, n, k);
                }
            }
        }
        public static bool IsIndexGood(int[,] map,int x, int y)
        {
            int i = 0;
            while (i < map.GetLength(0) && map[i, y] == 0)
            {
                i++;
            }
            if (i == map.GetLength(0))
            {
                int g = 0;
                while(g < map.GetLength(1) && map[x,g] == 0)
                {
                    g++;
                }
                if(g == map.GetLength(1))
                {
                    int x_ = x; int y_ = y;
                    FindStartNegative(ref x_, ref y_);
                    while(x_ != map.GetLength(0) && y_ != map.GetLength(1) && map[x_,y_] == 0)
                    {
                        x_++;y_++;
                    }
                    if(x_ == map.GetLength(0) || y_ == map.GetLength(1))
                    {
                        int _x = x;
                        int _y = y;
                        FindStartPositive(ref _x, ref _y);
                        while(_x < map.GetLength(0) && _y > 0 && map[_x, _y] == 0)
                        {
                            _x++;_y--;
                        }
                        if(_x == map.GetLength(0) || _y == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            int[,] map = new int[8, 8];//The chessboard
            int[] Path = new int[8];
            ChoosePath(map, ref Path, 0, 0);
            /*The array Path contains the index information of a chess figure, that which index should the figure be placed on in a row(eg. if Path[4] = 7,
            there should be a figure placed on map[4,7]*/
        }
    }
}
