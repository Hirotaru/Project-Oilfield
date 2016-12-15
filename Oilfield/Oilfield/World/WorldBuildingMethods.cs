using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public partial class World
    {
        public void BuildPipe(IObject start, IObject end)
        {
            BuildPipe(start.Position, end.Position);

            if (start is IConnectable && end is IConnectable)
            {
                IConnectable obj1 = start as IConnectable;
                IConnectable obj2 = end as IConnectable;

                obj1.ConnectedTo.Add(obj2);
                obj2.ConnectedTo.Add(obj1);

                obj1.ConnectedTo.UnionWith(obj2.ConnectedTo);
                obj2.ConnectedTo.UnionWith(obj1.ConnectedTo);
            }
        }

        public void BuildDepot(Point pos)
        {
            IDepot d = new Depot(pos);
            objManager.Add(d);
            BuildPipe(d, objManager.GetNearestObjects(d, true)[0]);
        }

        public void BuildPipe(Point start, Point end)
        {
            List<Point> path = findPath(start, end);

            if (path == null) return;

            for (int i = 0; i < path.Count; i++)
            {
                objManager.Add(new Pipe(path[i]));
                map[path[i].X, path[i].Y] = Util.PipeValue;
            }
        }

        public void BuildExtractor(IResource res)
        {
            Extractor ext = new Extractor(res);
            res.IsOccupied = true;
            objManager.Add(ext);

            var a = objManager.GetNearestObjects(ext, true);

            if (a != null)
            {
                if (a.Count > 0)
                {
                    BuildPipe(res, a[0]);
                }
            }
        }
    }
}
