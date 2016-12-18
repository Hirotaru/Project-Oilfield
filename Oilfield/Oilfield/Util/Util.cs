using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public static class Util
    {
        public const double ExtractorIncome = 400;
        public const double GasCost = 0.4;
        public const double OilCost = 1.7;

        public static readonly double StartMoney = 125000;

        public const int ExtCost = 75000;

        public static int[,] offsets = new int[9, 2]
            { { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 },
            { 1, 1 },{ 0, 1 },{ -1, 1 },{ -1, 0 }, { 0, 0 } };

        public static int[,] advOffsets = new int[25, 2]
        {
            { -2, -2 },
            { -2, -1 },
            { -2, 0 },
            { -2, 1 },
            { -2, 2 },
            { -1, -2 },
            { -1, -1 },
            { -1, 0 },
            { -1, 1 },
            { -1, 2 },
            { 0, -2 },
            { 0, -1 },
            { 0, 0 },
            { 0, 1 },
            { 0, 2 },
            { 1, -2 },
            { 1, -1 },
            { 1, 0 },
            { 1, 1 },
            { 1, 2 },
            { 2, -2 },
            { 2, -1 },
            { 2, 0 },
            { 2, 1 },
            { 2, 2 },
        };

        public const int OilDefaultValue = 34;
        public const int GasDefaultValue = 35;
        public const int WaterDefaultValue = 36;

        public const int PipeValue = 100;

        private static int id = 0;

        public static int NewID
        {
            get { return id++; }
        }

        public static int LastID
        {
            get { return id - 1; }
        }

        public static double GetDistance(IObject obj1, IObject obj2)
        {
            return GetDistance(obj1.Position, obj2.Position);
        }

        public static double GetDistance(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static PointF ConvertIndexToInt(PointF index)
        {
            return new PointF(index.X * UIConfig.Step + UIConfig.Step / 2,
                index.Y * UIConfig.Step + UIConfig.Step / 2);
        }

        //Converts screen coordinates into matrix indexes
        public static Point ConvertIntToIndex(PointF num)
        {
            int x = (int)((num.X - UIConfig.Step / 2) / UIConfig.Step);

            int y = (int)((num.Y - UIConfig.Step / 2) / UIConfig.Step);

            if (x >= UIConfig.WorldWidth)
                x = UIConfig.WorldWidth - 1;

            if (y >= UIConfig.WorldHeight)
                y = UIConfig.WorldHeight - 1;

            if (x < 0)
                x = 0;

            if (y < 0)
                y = 0;

            return new Point(x, y);
        }


        public static void Log(string logMessage, TextWriter writer)
        {
            {
                writer.Write("\r\nLog Entry : ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                writer.WriteLine("  :");
                writer.WriteLine("  :{0}", logMessage);
                writer.WriteLine("-------------------------------\n");
            }
        }
    }
}
