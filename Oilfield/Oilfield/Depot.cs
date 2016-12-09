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


        private ResourceType resType;

        public ResourceType ResType
        {
            get { return resType; }
        }

        private double capacity;

        public double Capacity
        {
            get { return capacity; }
            set { }
        }

        private int ownerID;

        public int OwnerID
        {
            get { return ownerID; }
            set { }
        }

        private double remainigCapacity;

        public double RemainingCapacity
        {
            get { return remainigCapacity; }
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


    }
}
