using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public class Oilfield : IResouce
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

        private Color resourceColor;

        public Color ResourceColor
        {
            get { return resourceColor; }
            set { }
        }

        public Oilfield() { }

        public Oilfield(Point pos, double depth, double amount, int CA, int OA)
        {
            this.isOccupied = false;
            this.resourceColor = Color.White;

            this.depth = depth;
            this.amount = amount;
            this.position = pos;

            this.chemicalAnalysis = CA;
            this.overallAnalysis = OA;
        }

    }
}
