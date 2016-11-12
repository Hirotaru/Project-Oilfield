using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public static class Util
    {
        public static int[,] offsets = new int[8, 2]
            { { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 },
            { 1, 1 },{ 0, 1 },{ -1, 1 },{ -1, 0 }};

        public const int OilDefaultValue = 34;
    }
}
