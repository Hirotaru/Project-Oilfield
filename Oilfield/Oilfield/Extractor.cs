using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class Extractor : IExtractor
    {
        private HashSet<IObject> connectedTo;

        public HashSet<IObject> ConnectedTo
        {
            get { return connectedTo; }
        }

        private double income;

        public double Income
        {
            get { return income; }
        }

        private int id;

        public int ID
        {
            get { return id; }
        }

        private ResourceType type;

        public ResourceType Type
        {
            get { return type; }
        }

        private bool waterConnected;

        public bool WaterConnected
        {
            get { return waterConnected; }
        }

        private double resourceAmount;

        public double ResourceAmount
        {
            get { return resourceAmount; }
        }

        private Point position;

        public Point Position
        {
            get { return position; }
        }

        private readonly double maxAmount;

        private double percentage
        {
            get { return resourceAmount / maxAmount * 100; }
        }

        private int green
        {
            get { return percentage > 50 ? 255 : 255 / 50 * (int)percentage; }
        }

        private int red
        {
            get { return percentage < 50 ? 255 : -255 / 50 * (int)percentage + 510; }
        }

        private static Dictionary<ResourceType, double> price;
        private static Dictionary<ResourceType, Color> colors;

        public Color centerColor
        {
            get { return Color.FromArgb(red < 0 ? 0 : red > 255 ? 255 : red, green < 0 ? 0 : green > 255 ? 255 : green, 0); }
        }

        private void extract(double dt)
        {
            if (type != ResourceType.WATER)
            {
                if (!waterConnected)
                    return;

                if (resourceAmount == 0)
                    return;

                var d = (from i in connectedTo where i is IDepot select i);

                if (d == null)
                    return;

                if (d.ToList().Count == 0)
                    return;

                //TODO
                /*if (resourceAmount > income * dt)
                {
                    double money = income * dt * ;
                    resourceAmount -= income * dt;
                }*/
            }
        }

        public void Update(double dt)
        {
            
        }

        public Extractor(IResource res)
        {
            id = Util.NewID;
            position = res.Position;

            if (res is Oilfield) type = ResourceType.OIL;
            if (res is Gasfield) type = ResourceType.GAS;
            if (res is Waterfield) type = ResourceType.WATER;

            waterConnected = false;

            resourceAmount = res.Amount;
            maxAmount = resourceAmount;


        }

        private void drawRectangle(Graphics g, Color color, int x, int y)
        {
            g.FillRectangle(new SolidBrush(color), UIConfig.Step * x + UIConfig.dx, UIConfig.Step * y + UIConfig.dy, UIConfig.Step, UIConfig.Step);
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < 8; i++)
                drawRectangle(g, UIConfig.WaterExtColor, position.X + Util.offsets[i, 0], position.Y + Util.offsets[i, 1]);

            drawRectangle(g, centerColor, position.X, position.Y);
        }

    }
}
