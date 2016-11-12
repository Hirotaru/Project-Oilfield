using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LevelGen;
using System.Drawing;

namespace Oilfield
{
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
            for (int k = 0; k < width / 10 + height / 10; k++)
            {
                Oilfield field = OilfieldGenerator.Generate(FindFreeSpace());

                objectManager.Add(field);

                for (int i = 0; i < 8; i++)
                {
                    map[field.Position.X + Util.offsets[i, 0], field.Position.Y + Util.offsets[i, 1]] = Util.OilDefaultValue;
                }

                map[field.Position.X, field.Position.Y] = Util.OilDefaultValue;
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

        Random rand = new Random(DateTime.Now.Millisecond);

        private Point FindFreeSpace()
        {

            Point p;
            bool ok = true;

            do
            {
                ok = true;
                p = new Point(rand.Next(1, width - 1), rand.Next(1, height - 1));

                if (map[p.X, p.Y] != LevelGen.Util.groundDefaultValue)
                {
                    ok = false;
                    continue;
                }

                for (int i = 0; i < 8; i++)
                {
                    if (map[p.X + Util.offsets[i, 0], p.Y + Util.offsets[i, 1]] != LevelGen.Util.groundDefaultValue)
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
                                    //g.FillRectangle(new SolidBrush(Color.FromArgb(0, 146, 179)), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case LevelGen.Util.groundDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(LevelGen.Util.TerrainColors[colorMap[x, y]]), step * x + dx, step * y + dy, step, step);
                                    
                                    //g.FillRectangle(new SolidBrush(Color.FromArgb(126, 64, 25)), step * x + dx, step * y + dy, step, step);
                                    break;
                                }

                            case LevelGen.Util.shoreDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(145, 88, 49)), step * x + dx, step * y + dy, step, step);

                                    break;
                                }
                        }
                    }
                }
            }



            for (int i = 0; i < objectManager.Count; i++)
            {

                var a = objectManager.GetResources(ResourceType.OIL);
                int x = objectManager.GetResources(ResourceType.OIL)[i].Position.X;
                int y = objectManager.GetResources(ResourceType.OIL)[i].Position.Y;


                g.FillRectangle(new SolidBrush(Color.Black), step * x + dx, step * y + dy, step, step);


            }
        }
    }
}
