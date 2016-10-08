using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    class Depot : IDepot
    {
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



    }
}
