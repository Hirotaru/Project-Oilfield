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

        private static readonly Dictionary<ResourceType, double> price = new Dictionary<ResourceType, double>()
        {
            {ResourceType.WATER, 0 },
            {ResourceType.GAS, Util.GasCost },
            {ResourceType.OIL, Util.OilCost }
        };

        private static readonly Dictionary<ResourceType, Color> colors = new Dictionary<ResourceType, Color>()
        {
            {ResourceType.WATER, UIConfig.WaterExtColor },
            {ResourceType.GAS, UIConfig.GasExtColor },
            {ResourceType.OIL,UIConfig.OilExtColor }
        };

        public Color centerColor
        {
            get { return Color.FromArgb(red < 0 ? 0 : red > 255 ? 255 : red, green < 0 ? 0 : green > 255 ? 255 : green, 0); }
        }

        private double extract(double dt)
        {
            if (type != ResourceType.WATER)
            {
                /*if (!waterConnected)
                    return 0;

                if (resourceAmount == 0)
                    return 0;

                var d = (from i in connectedTo where i is IDepot select i);

                if (d == null)
                    return 0;

                if (d.ToList().Count == 0)
                    return 0;*/

                if (resourceAmount > income * dt)
                {
                    double money = income * dt * Util.OilCost;
                    resourceAmount -= income * dt;
                    return money;
                }
                else
                {
                    double money = resourceAmount * Util.OilCost;
                    resourceAmount = 0;
                    return money;
                }
            }

            return 0;
        }

        public double Update(double dt)
        {
            return extract(dt / 20);
        }

        private double estIncome;

        public double EstIncome
        {
            get { return estIncome; }
            set { estIncome = value; }
        }


        public Extractor(IResource res)
        {
            connectedTo = new HashSet<IObject>();

            id = Util.NewID;
            position = res.Position;
            income = 50;

            if (res is Oilfield)
            {
                type = ResourceType.OIL;
                estIncome = resourceAmount * Util.OilCost;
            }
            if (res is Gasfield)
            {
                type = ResourceType.GAS;
                estIncome = resourceAmount * Util.GasCost;
            }
            if (res is Waterfield) type = ResourceType.WATER;

            waterConnected = false;

            resourceAmount = res.Amount;
            maxAmount = resourceAmount;

            if (res is Oilfield)
            {
                estIncome = resourceAmount * Util.OilCost;
            }
            if (res is Gasfield)
            {
                estIncome = resourceAmount * Util.GasCost;
            }
        }

        private void drawRectangle(Graphics g, Color color, int x, int y)
        {
            g.FillRectangle(new SolidBrush(color), UIConfig.Step * x + UIConfig.dx, UIConfig.Step * y + UIConfig.dy, UIConfig.Step, UIConfig.Step);
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < 8; i++)
                drawRectangle(g, colors[type], position.X + Util.offsets[i, 0], position.Y + Util.offsets[i, 1]);

            drawRectangle(g, centerColor, position.X, position.Y);
        }

    }
}
