using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class GameMap
    {
        public int width;
        public int height;
        private AStarNode[,] nodes;
        private bool printMap = false;

        public static double STANDART_COST = 1;

        public GameMap(int width, int height)
        {
            this.width = width;
            this.height = height;


            nodes = new AStarNode[width, height];
            initEmptyNodes();
        }

        public void UpdateMap(int[,] map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    nodes[i, j].Value = map[i, j];
                }
            }
        }

        private void initEmptyNodes()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    nodes[i, j] = new AStarNode(i, j, STANDART_COST);
                }
            }
        }

        public AStarNode GetNode(int x, int y)
        {
            return nodes[x, y];
        }

        private double getDist(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public double Get(int x, int y)
        {
            return nodes[x, y].Value;
        }

        public void set(int x, int y, double newValue)
        {
            nodes[x, y].Value = newValue;
        }
    }
}
