using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LevelGen;
using System.Drawing;

namespace Oilfield
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using static UIConfig;
    public partial class World
    {
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

        private double money = 125000;

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

        public World(int width, int height, string name = "")
        {
            objManager = new ObjectManager();

            Width = width;
            Height = height;

            Map = new int[width, height];

            map = Landscape.MapGeneration(width, height);
            //map = LevelGen.Map.Generate(1, width, height);
            LevelGen.Util.MapSmoothing(map, width, height, colorMap = new int[width, height], waterColors = new int[width, height]);


            GenerateResources();

            gameMap = new GameMap(width, height);
            gameMap.UpdateMap(AStarMap); // вызывать после каждого изменения мапы

            search = new AStarSearch(gameMap);

            IResource start = (IResource)objManager.GetResources()[0];
            IResource startWater = (IResource)objManager.GetNearestWater(start)[0];

            BuildExtractor(start);
            BuildExtractor(startWater);

            BuildExtractor((IResource)objManager.GetNearestGas(start)[0]);

            BuildDepot(FindFreeSpaceWithDistance(start.Position, 15));

            

            ready = true;
        }

        private Color getWaterColor(int x, int y)
        {
            return LevelGen.Util.WaterColors[waterColors[x, y]];
        }

        private Color getTerrainColor(int x, int y)
        {
            return LevelGen.Util.TerrainColors[colorMap[x, y]];
        }

        public void randomPath()
        {
            path = findPath(FindFreeSpace(), FindFreeSpace());
        }

        List<Point> path;

        private void GenerateResources()
        {
            for (int k = 0; k < width / 12 + height / 12; k++)
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

            for (int k = 0; k < 3; k++)
            {
                IResource field = WaterGenerator.Generate(FindFreeSpace());

                objManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.WaterDefaultValue;
                }
            }
        }

        public WorldState GetState()
        {
            WorldState res = new WorldState();

            if (money < Util.ExtCost) res.Money = (int)State.LOW;
            else if (money < Util.ExtCost * 2) res.Money = (int)State.MEDIUM;
            else if (money >= Util.ExtCost * 2) res.Money = (int)State.HIGH;

            if (income < 150) res.Income = (int)State.LOW;
            else if (income < 250) res.Income = (int)State.MEDIUM;
            else if (income >= 250) res.Income = (int)State.HIGH;

            var a = objManager.GetWorkingExts();

            if (a.Count < 2) res.ExtCount = (int)State.LOW;
            else if (a.Count < 4) res.ExtCount = (int)State.MEDIUM;
            else if (a.Count >= 4) res.ExtCount = (int)State.HIGH;

            return res;
        }
        private List<Point> findPath(Point start, Point end)
        {
            gameMap.UpdateMap(AStarMap);
            Util.sw.Stop();
            var a = search.FindPath(start, end);
            Util.sw.Start();
            return a;
        }

        private List<Point> findPath(IObject start, IObject end)
        {
            gameMap.UpdateMap(AStarMap);
            Util.sw.Stop();
            var a = search.FindPath(start.Position, end.Position);
            Util.sw.Start();
            return a;
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

        public Point FindFreeSpaceWithDistance(Point point, int distance)
        {
            Point p;
            do
                p = FindFreeSpace();
            while (Util.GetDistance(point, p) > distance);

            return p;
        }

        private void drawRectangle(Graphics g, Color color, int x, int y)
        {
            g.FillRectangle(new SolidBrush(color), Step * x + dx, Step * y + dy, Step, Step);
        }

        private double income;

        public double Income
        {
            get { return income; }
        }


        public void Update(double dt)
        {
            if (!ready)
                return;

            income = 0;

            foreach (var item in objManager.GetExtractors())
            {
                double i = (item as Extractor).Update(dt);
                money += i;
                income += i;
            }

            if (money > Util.ExtCost)
            {
                if (rand.Next() % 2 == 0)
                    BuildExtractor((IResource)objManager.GetBetterAnalysis(ResourceType.OIL)[0]);
                else
                    BuildExtractor((IResource)objManager.GetBetterAnalysis(ResourceType.GAS)[0]);

                money -= Util.ExtCost;
            }
        }

        

        public void Draw(Graphics g)
        {
            foreach (var item in objManager.Pipes)
            {
                drawRectangle(g, UIConfig.PipeColor, item.Position.X, item.Position.Y);
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
                                    drawRectangle(g, getWaterColor(x, y), x, y);
                                    break;
                                }

                            case LevelGen.Util.groundDefaultValue:
                                {
                                    drawRectangle(g, getTerrainColor(x, y), x, y);
                                    break;
                                }

                            case LevelGen.Util.shoreDefaultValue:
                                {
                                    drawRectangle(g, UIConfig.ShoreColor, x, y);
                                    break;
                                }
                        }
                    }
                }
            }

            foreach (var item in objManager.GetResources(ResourceType.GAS))
            {
                for (int i = 0; i < 9; i++)
                    drawRectangle(g, UIConfig.GasColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.GetResources(ResourceType.OIL))
            {
                for (int i = 0; i < 9; i++)
                    drawRectangle(g, UIConfig.OilColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.GetResources(ResourceType.WATER))
            {
                for (int i = 0; i < 9; i++)
                    drawRectangle(g, UIConfig.WaterColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            foreach (var item in objManager.Pipes)
            {
                drawRectangle(g, UIConfig.PipeColor, item.Position.X, item.Position.Y);
            }

            foreach (var item in objManager.GetExtractors())
            {
                (item as Extractor).Draw(g);
            }

            foreach (var item in objManager.Depots)
            {
                for (int i = 0; i < 9; i++)
                    drawRectangle(g, UIConfig.DepotColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

            //debug

            foreach (var item in objManager.GetResources(ResourceType.ALL))
            {
                g.DrawString((item as IResource).Amount.ToString(), new Font("Courier New", 8), Brushes.LimeGreen, item.Position.X * Step + dx, item.Position.Y * Step + dy);
            }

        }
    }
}
