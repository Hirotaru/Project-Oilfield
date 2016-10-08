using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    class Gasfield : IResouce
    {
        private bool isOccupied;

        public bool IsOccupied
        {
            get { return isOccupied; }
        }

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

        private Color resourceColor;

        public Color ResourceColor
        {
            get { return resourceColor; }
            set { }
        }

    }
}
