using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace graphics
{
    public class shape
    {
        public Point[,] Point_side = new Point[4, 4];
        public int SizeOfSqure = 64;
        public void init()
        {
            for(int i=0 ;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    Point_side[i, j].Y = 9 + i * 72;
                    Point_side[i, j].X = 9 + j * 72;
                }
                Point_side[3, i].Y++;
                Point_side[i, 3].X++;
            }
            Point_side[3, 3].Y--;
        }
    }

    public class vector2
    {
        private int x,y;
        vector2(int nx, int ny)
        {
            x = nx;
            y = ny;
        }
    }

    
}
