﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public class Gasfield : IResource
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
            set { isOccupied = value; }
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
            set { amount = value; }
        }

        private double depth;

        public double Depth
        {
            get { return depth; }
            set { }
        }

        public Gasfield(Point pos, double depth, double amount, int CA, int OA)
        {
            id = Util.NewID;

            this.isOccupied = false;

            this.depth = depth;
            this.amount = amount;
            this.position = pos;

            this.chemicalAnalysis = CA;
            this.overallAnalysis = OA;
        }

    }
}
