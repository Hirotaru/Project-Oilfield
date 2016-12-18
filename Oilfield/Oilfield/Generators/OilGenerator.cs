using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    
    public static class OilGenerator
    {
        private static Random rand = new Random(DateTime.Now.Millisecond);
        public static Oilfield Generate(Point pos)
        {
            //20% хороших
            Oilfield field;

            int chance = rand.Next() % 100 + 1;

            if (chance > 80)
            {
                int CA = rand.Next() % 3 + rand.Next() % 4 + 5;
                int OA = rand.Next() % 3 + rand.Next() % 4 + 5;

                double amount = CalculateResourceAmount(CA, OA);

                field = new Oilfield(pos, rand.Next(100, 1000), amount, CA, OA);

                return field;
            }
            else
            {
                int CA = rand.Next() % 3 + rand.Next() % 2 + 1;
                int OA = rand.Next() % 3 + rand.Next() % 2 + 1;

                double amount = CalculateResourceAmount(CA, OA);

                field = new Oilfield(pos, rand.Next(100, 1000), amount, CA, OA);

                return field;
            }
        }

        private static double CalculateResourceAmount(int CA, int OA)
        {
            double x = CA * 1.1 + OA * 0.9;
            double amount = Math.Pow(x, 1.51);

            amount = amount * (1200 + rand.Next(-400, 400));

            return amount;
        }
    }
}
