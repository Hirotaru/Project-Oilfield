using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LevelGen;
using System.Drawing;

namespace Oilfield
{
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using static UIConfig;
    public partial class World
    {
        public bool debug = true;

        public double totalResourceAmount = 0;
        public double totalResourceCost = 0;
        public double avgResourceCost = 0;

        ObjectManager objManager;
        private GameMap gameMap;
        private AStarSearch search;

        int[,] map;

        public int[,] Map
        {
            get { return map; }
            private set { map = value; }
        }

        private Point mousePosition;

        public Point MousePosition
        {
            get { return mousePosition; }
            set { mousePosition = value; }
        }

        int[,] waterColors;
        int[,] colorMap;


        private int width;

        public int Width
        {
            get { return width; }
            private set { width = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            private set { height = value; }
        }

        private double income = 1;

        public double Income
        {
            get { return income; }
        }

        private double money;

        public double Money
        {
            get { return money; }
            set { money = value; }
        }


        public int[,] AStarMap
        {
            get
            {
                int[,] res = new int[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        switch (map[x, y])
                        {
                            case LevelGen.Util.groundDefaultValue:
                                {
                                    res[x, y] = 2;
                                    break;
                                }
                            case Util.PipeValue:
                                {
                                    res[x, y] = 1;
                                    break;
                                }
                            default:
                                {
                                    res[x, y] = int.MaxValue;
                                    break;
                                }
                        }
                    }
                }
                
                return res;
            }
        }



        private bool ready = false;

        public bool Ready
        {
            get { return ready; }
            //set { ready = value; }
        }


        Random rand = new Random(DateTime.Now.Millisecond);

        public World()
        {

        }

        public void Reset()
        {
            ready = false;
            objManager.Reset();
            totalMoney = 0;

            Init();
        }

        private void Init()
        {
            objManager = new ObjectManager();

            Map = new int[width, height];

            map = Landscape.MapGeneration(width, height);
            //map = LevelGen.Map.Generate(5, width, height);
            LevelGen.Util.MapSmoothing(map, width, height, colorMap = new int[width, height], waterColors = new int[width, height]);

            GenerateResources();

            gameMap = new GameMap(width, height);
            gameMap.UpdateMap(AStarMap); // вызывать после каждого изменения мапы

            search = new AStarSearch(gameMap);

            //IResource start = GetBetterAnalysis(ResourceType.OIL)[0];
            //IResource startWater = (IResource)objManager.GetNearestWater(start)[0];


            money = Util.ExtCost * 2;

            totalMoney += money;

            BuildDepot(FindFreeSpace());

            //BuildExtractor(start);
            //BuildExtractor(startWater);

            //BuildExtractor((IResource)objManager.GetNearestGas(start)[0]);

            ready = true;

        }

        public World(int width, int height, string name = "")
        {
            Width = width;
            Height = height;

            Init();
        }

        private Color GetWaterColor(int x, int y)
        {
            return LevelGen.Util.WaterColors[waterColors[x, y]];
        }


        private Color GetTerrainColor(int x, int y)
        {
            return LevelGen.Util.TerrainColors[colorMap[x, y]];
        }


        public double TotalMoney
        {
            get { return totalMoney; }
        }
        private double totalMoney = 0;

        private void GenerateResources()
        {
            for (int k = 0; k < (width + height) / 11; k++)
            {
                IResource field = OilGenerator.Generate(FindFreeSpace());

                objManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.OilDefaultValue;
                }

                field = GasGenerator.Generate(FindFreeSpace());

                objManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.GasDefaultValue;
                }
            }

            int count = 0;

            foreach (var item in objManager.GetResources())
            {
                count++;

                IResource i = item as IResource;

                totalResourceAmount += i.Amount;

                if (i is Gasfield) totalResourceCost += i.Amount * Util.GasCost;

                if (i is Oilfield) totalResourceCost += i.Amount * Util.OilCost;

            }

            avgResourceCost = totalResourceCost / count;
        }

        public WorldState GetState()
        {
            WorldState res = new WorldState();

            if (money < Util.ExtCost) res.Money = (int)State.LOW;
            else if (money < Util.ExtCost * 2) res.Money = (int)State.MEDIUM;
            else if (money >= Util.ExtCost * 2) res.Money = (int)State.HIGH;

            /*if (income < 400) res.Income = (int)State.LOW;
            else if (income < 800) res.Income = (int)State.MEDIUM;
            else if (income >= 800) res.Income = (int)State.HIGH;*/

            if (income < Util.GasExtractorIncome * 2 * Util.GasCost) res.Income = (int)State.LOW;
            else if (income < Util.OilExtractorIncome * (Util.OilCost * 4)) res.Income = (int)State.MEDIUM;
            else if (income >= Util.OilExtractorIncome * (4 * Util.OilCost)) res.Income = (int)State.HIGH;

            var a = objManager.GetWorkingExts();

            if (a.Count < 2) res.ExtCount = (int)State.LOW;
            else if (a.Count < 4) res.ExtCount = (int)State.MEDIUM;
            else if (a.Count >= 4) res.ExtCount = (int)State.HIGH;

            return res;
        }

        private List<Point> FindPath(Point start, Point end)
        {
            gameMap.UpdateMap(AStarMap);
            var a = search.FindPath(start, end);
            return a;
        }

        private List<Point> FindPath(IObject start, IObject end)
        {
            return FindPath(start.Position, end.Position);
            /*gameMap.UpdateMap(AStarMap);
            var a = search.FindPath(start.Position, end.Position);
            return a;*/
        }

        private Point FindFreeSpace()
        {
            Point p;

            bool ok = true;
            do
            {
                ok = true;
                p = new Point(rand.Next(2, width - 2), rand.Next(2, height - 2));

                if (map[p.X, p.Y] != LevelGen.Util.groundDefaultValue)
                {
                    ok = false;
                    continue;
                }

                for (int i = 0; i < 25; i++)
                {
                    if (map[p.X + Util.advOffsets[i, 0], p.Y + Util.advOffsets[i, 1]] != LevelGen.Util.groundDefaultValue)
                    {
                        ok = false;
                    }
                }

            }
            while (!ok);

            return p;
        }

        private void DrawRectangle(Graphics g, Color color, int x, int y)
        {
            g.FillRectangle(new SolidBrush(color), Step * x + dx, Step * y + dy, Step, Step);
        }

        public void Update(double dt)
        {
            if (!ready)
                return;

            income = 0;

            foreach (var item in objManager.GetExtractors())
            {
                double i = (item as Extractor).Update();
                income += i;
            }

            money += income;

            totalMoney += income;
        }

        public bool Drawing
        {
            get { return drawing; }
            set { drawing = value; }
        }

        private bool drawing = true;

        public void Draw(Graphics g)
        {
            if (!drawing) return;

            foreach (var item in objManager.Pipes)
            {
                DrawRectangle(g, UIConfig.PipeColor, item.Position.X, item.Position.Y);
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (Step * x + dx <= WindowWidth && Step * x + dx >= -Step && Step * y + dy <= WindowHeight && Step * y + dy >= -Step)
                    {
                        switch (map[x, y])
                        {
                            case LevelGen.Util.waterDefaultValue:
                                {
                                    DrawRectangle(g, GetWaterColor(x, y), x, y);
                                    break;
                                }

                            case LevelGen.Util.groundDefaultValue:
                                {
                                    DrawRectangle(g, GetTerrainColor(x, y), x, y);
                                    break;
                                }

                            case LevelGen.Util.shoreDefaultValue:
                                {
                                    DrawRectangle(g, UIConfig.ShoreColor, x, y);
                                    break;
                                }
                        }
                    }
                }
            }

            foreach (var item in objManager.GetResources(ResourceType.GAS))
            {
                for (int i = 0; i < 9; i++)
                    DrawRectangle(g, UIConfig.GasColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.GetResources(ResourceType.OIL))
            {
                for (int i = 0; i < 9; i++)
                    DrawRectangle(g, UIConfig.OilColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.GetResources(ResourceType.WATER))
            {
                for (int i = 0; i < 9; i++)
                    DrawRectangle(g, UIConfig.WaterColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.Pipes)
            {
                DrawRectangle(g, UIConfig.PipeColor, item.Position.X, item.Position.Y);
            }

            foreach (var item in objManager.GetExtractors())
            {
                (item as Extractor).Draw(g);
            }

            foreach (var item in objManager.Depots)
            {
                for (int i = 0; i < 9; i++)
                    DrawRectangle(g, UIConfig.DepotColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            //debug

            if (!debug) return;

            foreach (var item in objManager.GetResources(ResourceType.ALL))
            {
                if (item is Gasfield)
                    g.DrawString(((item as IResource).Amount * Util.GasCost).ToString("f3"), new Font("Courier New", 9), Brushes.LimeGreen, item.Position.X * Step + dx - Step * 3, item.Position.Y * Step + dy + Step + 2);

                if (item is Oilfield)
                    g.DrawString(((item as IResource).Amount * Util.OilCost).ToString("f3"), new Font("Courier New", 9), Brushes.LimeGreen, item.Position.X * Step + dx - Step * 3, item.Position.Y * Step + dy + Step + 2);
            }

        }

        public int GetReward(WorldState state)
        {
            switch (state.ToString())
            {
                case "000":
                    {
                        return (int)Reward.VERYBAD;
                    }

                case "001":
                    {
                        return (int)Reward.VERYBAD;
                    }

                case "002":
                    {
                        return (int)Reward.BAD;
                    }

                case "010":
                    {
                        return (int)Reward.BAD;
                    }

                case "011":
                    {
                        return (int)Reward.NORMAL;
                    }

                case "012":
                    {
                        return (int)Reward.NORMAL;
                    }

                case "020":
                    {
                        return (int)Reward.GOOD;
                    }

                case "021":
                    {
                        return (int)Reward.GOOD;
                    }

                case "022":
                    {
                        return (int)Reward.VERYGOOD;
                    }

                case "100":
                    {
                        return (int)Reward.BAD;
                    }

                case "101":
                    {
                        return (int)Reward.BAD;
                    }

                case "102":
                    {
                        return (int)Reward.NORMAL;
                    }

                case "110":
                    {
                        return (int)Reward.NORMAL;
                    }

                case "111":
                    {
                        return (int)Reward.GOOD;
                    }

                case "112":
                    {
                        return (int)Reward.GOOD;
                    }

                case "120":
                    {
                        return (int)Reward.GOOD;
                    }

                case "121":
                    {
                        return (int)Reward.GOOD;
                    }

                case "122":
                    {
                        return (int)Reward.VERYGOOD;
                    }

                case "200":
                    {
                        return (int)Reward.VERYBAD;
                    }

                case "201":
                    {
                        return (int)Reward.BAD;
                    }

                case "202":
                    {
                        return (int)Reward.NORMAL;
                    }

                case "210":
                    {
                        return (int)Reward.GOOD;
                    }

                case "211":
                    {
                        return (int)Reward.GOOD;
                    }

                case "212":
                    {
                        return (int)Reward.GOOD;
                    }

                case "220":
                    {
                        return (int)Reward.GOOD;
                    }

                case "221":
                    {
                        return (int)Reward.VERYGOOD;
                    }

                case "222":
                    {
                        return (int)Reward.VERYGOOD;
                    }
            }

            return 0;
        }

        public bool IsEnd()
        {
            if (GetState().ToString() == "000" && income == 0 && objManager.GetWorkingExts().Count == 0)
                return true;
            else return false;
        }
    }
}
