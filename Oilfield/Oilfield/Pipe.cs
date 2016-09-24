using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    class Pipe
    {
        private Point pointA;

        private Point pointB;

        public Point PointA
        {
            get { return pointA; }
        }

        public Point PointB
        {
            get { return pointB; }
        }



        private List<Point> pipePath;
    }
}
