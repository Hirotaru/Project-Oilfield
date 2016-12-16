﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public static class GasGenerator
    {
        private static Random rand = new Random(DateTime.Now.Millisecond);
        public static Gasfield Generate(Point pos)
        {
            //20% хороших
            Gasfield field;

            int chance = rand.Next() % 100 + 1;

            if (chance > 80)
            {
                int CA = rand.Next() % 3 + rand.Next() % 4 + 5;
                int OA = rand.Next() % 3 + rand.Next() % 4 + 5;

                double amount = CalculateResourceAmount(CA, OA);

                field = new Gasfield(pos, rand.Next(100, 1000), amount, CA, OA);

                return field;
            }
            else
            {
                int CA = rand.Next() % 3 + rand.Next() % 2 + 1;
                int OA = rand.Next() % 3 + rand.Next() % 2 + 1;

                double amount = CalculateResourceAmount(CA, OA);

                field = new Gasfield(pos, rand.Next(100, 1000), amount, CA, OA);

                return field;
            }
        }

        private static double CalculateResourceAmount(int CA, int OA)
        {
            double x = CA * 1.1 + OA * 0.9;
            double amount = Math.Pow(x, 1.5);

            amount = amount * (2000 + rand.Next(-250, 250));

            return amount;
        }
    }
}