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
    using static UIConfig;
    public class World
    {
        ObjectManager objectManager;

        private int[,] map;

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
        public World()
        {

        }

        private void GenerateResources()
        {
            for (int k = 0; k < width / 15 + height / 15; k++)
            {
                IResouce field = OilGenerator.Generate(FindFreeSpace());

                objectManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.OilDefaultValue;
                }

                field = GasGenerator.Generate(FindFreeSpace());

                objectManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.GasDefaultValue;
                }

                field = WaterGenerator.Generate(FindFreeSpace());

                objectManager.Add(field);

                for (int i = 0; i < 9; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.WaterDefaultValue;
                }
            }

            //Debug

            File.Delete("log.txt");

            var res = objectManager.GetResources(ResourceType.OIL);

            for (int i = 0; i < res.Count; i++)
            {
                Oilfield f = res[i] as Oilfield;
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Util.Log("CA: " + f.ChemicalAnalysis + " OA: " + f.OverallAnalysis + " Amount:" + f.Amount, w);
                }
            }
        }

        public World(int width, int height, string name = "")
        {
            objectManager = new ObjectManager();

            Width = width;
            Height = height;

            Map = new int[width, height];

            map = Landscape.MapGeneration(width, height);
            LevelGen.Util.MapSmoothing(map, width, height, colorMap = new int[width, height], waterColors = new int[width, height]);

            GenerateResources();
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



        public void Draw(Graphics g)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (step * x + dx <= WindowWidth && step * x + dx >= -step && step * y + dy <= WindowHeight && step * y + dy >= -step)
                    {
                        switch (map[x, y])
                        {
                            case LevelGen.Util.waterDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(LevelGen.Util.WaterColors[waterColors[x, y]]), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case LevelGen.Util.groundDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(LevelGen.Util.TerrainColors[colorMap[x, y]]), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case LevelGen.Util.shoreDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(145, 88, 49)), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case Util.OilDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(UIConfig.OilColor), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case Util.GasDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(UIConfig.GasColor), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case Util.WaterDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(UIConfig.WaterColor), step * x + dx, step * y + dy, step, step);
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }
}
