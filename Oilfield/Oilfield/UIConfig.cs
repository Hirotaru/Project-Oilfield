using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public static class UIConfig
    {
        public static int WindowWidth;
        public static int WindowHeight;

        public static int step = 10;

        public static int dx = 0, dy = 24;

        static readonly float ScrollSpeed = 500;

        public static bool Move(Point MousePoint, int Width, int Height, long dt)
        {
            bool changed = false;

            if (MousePoint.X <= 10)
            {
                dx += (int)(ScrollSpeed / dt);
                changed = true;
            }

            if (MousePoint.X >= Width - 10)
            {
                dx -= (int)(ScrollSpeed / dt);
                changed = true;
            }

            if (MousePoint.Y <= 10)
            {
                dy += (int)(ScrollSpeed / dt);
                changed = true;
            }

            if (MousePoint.Y >= Height - 10)
            {
                dy -= (int)(ScrollSpeed / dt);
                changed = true;
            }

            return changed;
        }
    }
}
