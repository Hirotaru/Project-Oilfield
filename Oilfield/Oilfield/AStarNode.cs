using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class AStarNode : IComparable<AStarNode>
    {
        public AStarNode PathParent;
        public double CostFromStart;
        public double EstimatedCostToGoal;
        public double Value;

        public int X;
        public int Y;


        public AStarNode(int x, int y, double value)
        {
            X = x;
            Y = y;
            this.Value = value;
        }


        public double getCost()
        {
            return CostFromStart + EstimatedCostToGoal;
        }


        public int compareTo(AStarNode other)
        {
            double thisValue = getCost();
            double otherValue = other.getCost();

            double v = thisValue - otherValue;
            return (v > 0) ? 1 : (v < 0) ? -1 : 0; // sign function
        }

        public double getEstimatedCost(AStarNode goal)
        {
            // System.out.println("this: " + X + ", " + Y);
            // System.out.println("goal: " + goal.X + ", " + goal.Y);

            double dist = Math.Sqrt((X - goal.X) * (X - goal.X) + (Y - goal.Y) * (Y - goal.Y));

            // System.out.println("return dist: " + dist);

            return dist;
        }

        public bool equals(AStarNode other)
        {
            if (other == null) return false;
            if (other == this) return true;

            if (X == other.X && Y == other.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(AStarNode other)
        {
            throw new NotImplementedException();
        }
    }
}
