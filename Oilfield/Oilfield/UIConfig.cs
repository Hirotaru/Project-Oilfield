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

        public static Color OilColor = Color.FromArgb(18, 18, 18);
        public static Color GasColor = Color.FromArgb(200, 200, 200);
        public static Color WaterColor = Color.CornflowerBlue;
        public static Color ShoreColor = Color.FromArgb(145, 88, 49);

        public static Color PipeColor = Color.Gray;

        public static Color WaterExtColor = Color.MediumSlateBlue;
        public static Color OilExtColor = Color.FromArgb(38, 38, 38);
        public static Color GasExtColor = Color.FromArgb(220, 220, 220);

        public static Color DepotColor = Color.Crimson;

        public static int WindowWidth;
        public static int WindowHeight;

        public static int WorldWidth;
        public static int WorldHeight;

        public static int Step = 8;

        public static int dx = 0, dy = 24;

        static readonly float ScrollSpeed = 0.9F;

        public static bool Move(Point MousePoint, int Width, int Height, long dt)
        {
            bool changed = false;

            if (MousePoint.X <= 10)
            {
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
