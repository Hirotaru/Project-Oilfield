using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class AStarSearch
    {
        bool detailedPathPrint = false;
        bool pathPrint = true;
        private int delta = 12;
        private List<long> times = new List<long>();

        List<AStarNode> openList = new List<AStarNode>();
        List<AStarNode> closedList = new List<AStarNode>();

        private GameMap gameMap;
        int[,] neighborsMove = new int[,] {
            {0, 1},
            {1, 0},
            {0, -1},
            {-1, 0},
        };

        public AStarSearch(GameMap gameMap)
            {
                this.gameMap = gameMap;
            }

        /**
         Construct the path, not including the start node.
         */
        protected List<AStarNode> constructPath(AStarNode node)
        {
            LinkedList<AStarNode> path = new LinkedList<AStarNode>();
            while (node.PathParent != null)
            {
                path.AddFirst(node);
                node = node.PathParent;
            }
            return path.ToList<AStarNode>();
        }

        private AStarNode getNearestNode(List<AStarNode> list)
        {
            int size = list.Count;
            if (list.Count == 0)
                return null;
            else if (list.Count == 1)
                return list[0];

            AStarNode minNode = list[0];
            double minValue = minNode.getCost();

            for (int i = 1; i < size; i++)
            {
                AStarNode node = list[i];
                double val = node.getCost();
                if (val < minValue)
                {
                    minValue = val;
                    minNode = node;
                }
            }

            list.Remove(minNode);
            return minNode;
        }

        // DLYA ANDRUXI!!1
        public List<Point> FindPath(Point start, Point end)
        {
            AStarNode startNode = gameMap.GetNode(start.X, start.Y);
            AStarNode endNode = gameMap.GetNode(end.X, end.Y);
            List<AStarNode> path = findPath(startNode, endNode);
            List<Point> intPath = new List<Point>();
            for (int i = 0; i < path.Count; i++)
            {
                intPath.Add(new Point(path[i].X, path[i].Y));
            }
            return intPath;
        }

        private List<AStarNode> findPath(AStarNode startNode, AStarNode goalNode)
        {
            openList.Clear();
            closedList.Clear();

            startNode.CostFromStart = 0;
            startNode.EstimatedCostToGoal = startNode.getEstimatedCost(goalNode);
            startNode.PathParent = null;
            openList.Add(startNode);

            int count = 0;
            while (openList.Count > 0)
            {
                if (count++ > 2000)
                {
                    // System.out.println("PATH TOO LONG");
                    // return null;
                }
                // System.out.println(count++ + " - size: " + openList.size());
                AStarNode node = getNearestNode(openList);

                //if (detailedPathPrint)
                //    System.out.println("Node from open: " + node.X + ", " + node.Y);

                if (node.equals(goalNode))
                {
                    // construct the path from start to goal
                    if (detailedPathPrint)
                    {
                        //System.out.println("Goal founded for " + count);
                    }
                    return constructPath(goalNode);
                }

                // for each neighbor
                for (int i = 0; i < 4; i++)
                {
                    int x_cord = node.X + neighborsMove[i, 0];
                    int y_cord = node.Y + neighborsMove[i, 1];

                    // if not in map
                    if (x_cord < 0 || x_cord >= gameMap.width ||
                            y_cord < 0 || y_cord >= gameMap.height)
                    {
                        continue;
                    }
                    AStarNode neighborNode = gameMap.GetNode(x_cord, y_cord);

                    //if (detailedPathPrint)
                    //    System.out.println("Neighbor: " + neighborNode.X + ", " + neighborNode.Y + ", " + neighborNode.Value);

                    bool isOpen = openList.Contains(neighborNode); 
                    bool isClosed = closedList.Contains(neighborNode);
                    double costFromStart = node.CostFromStart + neighborNode.Value + i;
                    // if (detailedPathPrint)
                    //    System.out.println("Cost from start: " + costFromStart);

                    // check if the neighbor node has not been
                    // traversed or if a shorter path to this
                    // neighbor node is found.
                    if ((!isOpen && !isClosed) ||
                            costFromStart < neighborNode.CostFromStart)
                    {
                        neighborNode.PathParent = node;
                        neighborNode.CostFromStart = costFromStart;
                        neighborNode.EstimatedCostToGoal = neighborNode.getEstimatedCost(goalNode);
                        //if (detailedPathPrint)
                        //    System.out.println("Estimated to goal: " + neighborNode.EstimatedCostToGoal);

                        if (isClosed)
                        {
                            closedList.Remove(neighborNode);
                            //if (detailedPathPrint)
                            //    System.out.println("Deleted from closed");
                        }
                        if (!isOpen)
                        {
                            openList.Add(neighborNode);
                            //if (detailedPathPrint)
                            //    System.out.println("Added to open");
                        }
                    }
                }
                closedList.Add(node);
                //if (detailedPathPrint)
                //    System.out.println("Added to closed");
            }
        // no path found
        return null;
        }
    }
}
