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

            BuildPipe(start, startWater);

            

            BuildDepot(FindFreeSpaceWithDistance(start.Position, 15));

            BuildPipe(start, objManager.Depots[0]);
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
            for (int k = 0; k < width / 15 + height / 15; k++)
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

                field = WaterGenerator.Generate(FindFreeSpace());

                objManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.WaterDefaultValue;
                }
            }

            //Debug

            File.Delete("log.txt");

            var res = objManager.GetResources(ResourceType.OIL);

            for (int i = 0; i < res.Count; i++)
            {
                Oilfield f = res[i] as Oilfield;
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Util.Log("CA: " + f.ChemicalAnalysis + " OA: " + f.OverallAnalysis + " Amount:" + f.Amount, w);
                }
            }
        }

        private List<Point> findPath(Point start, Point end)
        {
            gameMap.UpdateMap(AStarMap);
            return search.FindPath(start, end);
        }

        private List<Point> findPath(IObject start, IObject end)
        {
            gameMap.UpdateMap(AStarMap);
            return search.FindPath(start.Position, end.Position);
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

        public void Update(double dt)
        {
            foreach (var item in objManager.GetExtractors())
            {
                (item as Extractor).Update(dt);
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

                            /*case Util.OilDefaultValue:
                                {
                                    drawRectangle(g, UIConfig.OilColor, x, y);
                                    break;
                                }

                            case Util.GasDefaultValue:
                                {
                                    drawRectangle(g, UIConfig.GasColor, x, y);
                                    break;
                                }

                            case Util.WaterDefaultValue:
                                {
                                    drawRectangle(g, UIConfig.WaterColor, x, y);
                                    break;
                                }
                                */
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

            var a = objManager.GetExtractors();

            for (int i = 0; i < a.Count; i++)
            {
                (a[i] as Extractor).Draw(g);
            }

            foreach (var item in objManager.Depots)
            {
                for (int i = 0; i < 9; i++)
                    drawRectangle(g, UIConfig.DepotColor, item.Position.X + Util.offsets[i, 0], item.Position.Y + Util.offsets[i, 1]);
            }

        }
    }
}
