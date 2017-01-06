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

            if (chance > 75)
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
            double x = CA + OA;
            double amount = Math.Pow(x, 1.2);

            //amount = amount * (1200 + rand.Next(-200, 200));

            amount = amount * (4000 + rand.Next(-100, 100));

            return amount;
        }
    }
}
