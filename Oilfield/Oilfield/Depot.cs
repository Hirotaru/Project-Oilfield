using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public class Depot : IDepot
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

        private HashSet<IObject> connectedTo;

        public HashSet<IObject> ConnectedTo
        {
            get { return connectedTo; }
        }

        public Depot(Point position)
        {
            id = Util.NewID;
            this.position = position;
            connectedTo = new HashSet<IObject>();
        }


    }
}
