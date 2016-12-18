using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class Extractor : IExtractor
    {

        private bool isWorking;

        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; }
        }

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

        private IResource res;

        public IResource Resource
        {
            get { return res; }
            set { res = value; }
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

        private double extract()
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

                if (resourceAmount > income)
                {
                    double cost = 0;
                    if (type == ResourceType.GAS)
                        cost = Util.GasCost;

                    if (type == ResourceType.OIL)
                        cost = Util.OilCost;

                    double money = income * cost;
                    resourceAmount -= income;
                    res.Amount -= income;
                    return money;
                }
                else
                {
                    double cost = 0;
                    if (type == ResourceType.GAS)
                        cost = Util.GasCost;

                    if (type == ResourceType.OIL)
                        cost = Util.OilCost;

                    double money = resourceAmount * cost;
                    resourceAmount = 0;
                    res.Amount = 0;
                    IsWorking = false;
                    return money;
                }
            }

            return 0;
        }

        public double Update()
        {
            return extract();
        }


        public Extractor(IResource res)
        {
            this.res = res;

            connectedTo = new HashSet<IObject>();

            id = Util.NewID;
            position = res.Position;
            income = Util.ExtractorIncome;

            if (res is Oilfield)
            {
                type = ResourceType.OIL;
            }
            if (res is Gasfield)
            {
                type = ResourceType.GAS;
            }
            if (res is Waterfield) type = ResourceType.WATER;

            waterConnected = false;

            resourceAmount = res.Amount;
            maxAmount = resourceAmount;

            if (res is Oilfield)
            {
                IsWorking = true;
            }
            if (res is Gasfield)
            {
                IsWorking = true;
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
