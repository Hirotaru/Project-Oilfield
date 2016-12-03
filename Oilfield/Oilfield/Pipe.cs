using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    class Pipe : IObject
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { }
        }

        private Point position;

        public Point Position
        {
            get { return position; }
            set { }
        }
    }
}
