using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public static class Util
    {
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
