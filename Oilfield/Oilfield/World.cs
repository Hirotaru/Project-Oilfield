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

        public World(int width, int height, string name = "")
        {

            Width = width;
            Height = height;

            Map = new int[width, height];

            map = Landscape.MapGeneration(width, height);
            LevelGen.Util.MapSmoothing(map, width, height, colorMap = new int[width, height], waterColors = new int[width, height]);

        }



        public void Draw(Graphics g)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (step * x + dx <= WindowWidth && step * x + dx >= -step && step * y + dy <= WindowHeight && step * y + dy >= -step)

                        switch (map[x, y])
                        {
                            case Util.mapDefaultValue:
                                {
                                    g.FillRectangle(new SolidBrush(LevelGen.Util.WaterColors[waterColors[x, y]]), step * x + dx, step * y + dy, step, step);

                                    break;
                                }

                            case 1:
                                {
                                    g.FillRectangle(new SolidBrush(LevelGen.Util.TerrainColors[colorMap[x, y]]), step * x + dx, step * y + dy, step, step);

                                    break;
                                }

                            case 2:
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(145, 88, 49)), step * x + dx, step * y + dy, step, step);

                                    break;
                                }
                        }
                }
            }


        }
    }
}
