using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public static class WaterGenerator
    {
        private static Random rand = new Random(DateTime.Now.Millisecond);

        public static Waterfield Generate(Point pos)
        {
            Waterfield field = new Waterfield();
            //20% хороших

            int amount = rand.Next(10000, 100000);

            field = new Waterfield(pos, rand.Next(100, 1000), amount);

            return field;
        }
    }
}
