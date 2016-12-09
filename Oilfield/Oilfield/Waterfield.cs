using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public class Waterfield : IResource
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { }
        }

        private int chemicalAnalysis;

        public int ChemicalAnalysis
        {
            get { return chemicalAnalysis; }
            private set { chemicalAnalysis = value; }
        }

        private int overallAnalysis;

        public int OverallAnalysis
        {
            get { return overallAnalysis; }
            private set { overallAnalysis = value; }
        }

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

        public Waterfield()
        {

        }

        public Waterfield(Point pos, double depth, double amount)
        {
            id = Util.NewID;

            this.isOccupied = false;

            this.depth = depth;
            this.amount = amount;
            this.position = pos;

            this.chemicalAnalysis = 0;
            this.overallAnalysis = 0;
        }

    }
}
