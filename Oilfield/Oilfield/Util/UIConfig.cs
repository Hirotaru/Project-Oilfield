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
        public static readonly Color OilColor = Color.FromArgb(18, 18, 18);
        public static readonly Color GasColor = Color.DarkGray;
        public static readonly Color WaterColor = Color.CornflowerBlue;
        public static readonly Color ShoreColor = Color.FromArgb(145, 88, 49);

        public static readonly Color PipeColor = Color.Gray;

        public static readonly Color WaterExtColor = Color.MediumSlateBlue;
        public static readonly Color OilExtColor = Color.DimGray;
        public static readonly Color GasExtColor = Color.LightGray;

        public static Color DepotColor = Color.Coral;

        public static int WindowWidth;
        public static int WindowHeight;

        public static int WorldWidth;
        public static int WorldHeight;

        public static int Step = 6;

        public static int dx = 0, dy = 24;

        static readonly float ScrollSpeed = 0.9F;

        public static bool Move(Point MousePoint, int Width, int Height, double dt)
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
