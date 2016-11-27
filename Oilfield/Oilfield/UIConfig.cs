using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Oilfield
{
    public static class UIConfig
    {
        //Изменение цвета из зеленого в красный
        public static Color OilColor = Color.FromArgb(12, 12, 12);
        public static Color GasColor = Color.CadetBlue;
        public static Color WaterColor = Color.SteelBlue;

        public static int WindowWidth;
        public static int WindowHeight;

        public static int WorldWidth;
        public static int WorldHeight;

        public static int step = 8;

        public static int dx = 0, dy = 24;

        static readonly float ScrollSpeed = 0.9F;

        public static bool Move(Point MousePoint, int Width, int Height, long dt)
        {
            bool changed = false;

            if (MousePoint.X <= 10)
            {
               
                //if (dx < 50)
                {
                    dx += (int)(ScrollSpeed * dt);
                }
                changed = true;
            }

            if (MousePoint.X >= Width - 10)
            {
                
                dx -= (int)(ScrollSpeed * dt);
                
                changed = true;
            }

            if (MousePoint.Y <= 10)
            {
                //if (dy < 74)
                dy += (int)(ScrollSpeed * dt);
                changed = true;
            }

            if (MousePoint.Y >= Height - 10)
            {
                dy -= (int)(ScrollSpeed * dt);
                changed = true;
            }

            return changed;
        }

        
    }
}
