using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    class Oilfield : IResouce
    {
        private Point position;

        public Point Position
        {
            get { return position; }
            set { }
        }

        private double amount;

        public double Amount
        {
            get { return amount; }
            set { }
        }

        private double depth;

        public double Depth
        {
            get { return depth; }
            set { }
        }

    }
}
