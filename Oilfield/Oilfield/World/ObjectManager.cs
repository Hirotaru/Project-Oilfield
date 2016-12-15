using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class ObjectManager
    {
        public ObjectManager()
        {
            objects = new List<IObject>();
        }

        private List<IObject> objects;

        public List<IObject> Objects
        {
            get { return objects; }
            set { }
        }

        public int Count
        {
            get { return objects.Count; }
        }

        public List<IObject> GetResources(ResourceType type = ResourceType.ALL)
        {
            switch (type)
            {
                case ResourceType.OIL:
                    {
                        var res = from i in objects where i is Oilfield select i;
                        return res.ToList();
                    }

                case ResourceType.GAS:
                    {
                        var res = from i in objects where i is Gasfield select i;
                        return res.ToList();
                    }

                case ResourceType.WATER:
                    {
                        var res = from i in objects where i is Waterfield select i;
                        return res.ToList();
                    }

                default:
                    {
                        var res = from i in objects where i is IResource select i;
                        return res.ToList();
                    }
            }
        }

        public List<IObject> Extractors
        {
            get
            {
                var ext = from i in objects where i is IExtractor select i;
                return ext.ToList();
            }
        }

        public List<IObject> Depots
        {
            get
            {
                var dep = from i in objects where i is IDepot select i;
                return dep.ToList();
            }
        }

        public List<IObject> Pipes
        {
            get
            {
                var pipes = from i in objects where i is Pipe select i;
                return pipes.ToList();
            }
        }

        public List<IObject> GetBetterAnalysis(ResourceType type)
        {
            var res = from i in objects where i is IResource select i;

            switch (type)
            {
                case ResourceType.OIL:
                    {
                        var a = from i in res where i is Oilfield select i;

                        return (from i in a where !(i as IResource).IsOccupied orderby (i as Oilfield).OverallAnalysis + (i as Oilfield).ChemicalAnalysis descending select i).ToList();
                    }
                case ResourceType.GAS:
                    {
                        var a = from i in res where i is Gasfield select i;

                        return (from i in a where !(i as IResource).IsOccupied orderby (i as Gasfield).OverallAnalysis + (i as Gasfield).ChemicalAnalysis descending select i).ToList();
                    }
                case ResourceType.WATER:
                    {
                        return null;
                    }
                case ResourceType.ALL:
                    {
                        return (from i in res where !(i as IResource).IsOccupied orderby (i as IResource).OverallAnalysis + (i as IResource).ChemicalAnalysis descending select i).ToList();
                    }
                default:
                    break;
            }

            return null;
        }

        public List<IObject> GetNearestObjects(IObject obj, bool connectable = false)
        {
            if (connectable)
            {
                return (from i in objects where i is IConnectable && i != obj orderby Util.GetDistance(obj, i) select i).ToList();
            }
            else
            {
                return (from i in objects where i != obj orderby Util.GetDistance(obj, i) select i).ToList();
            }
        }

        public List<IObject> GetNearestWater(IObject obj)
        {
            return (from i in objects where i is Waterfield && !(i as IResource).IsOccupied && i != obj orderby Util.GetDistance(obj, i) select i).ToList();
        }

        public List<IObject> GetWorkingExts()
        {
            return (from i in objects where i is IExtractor && (i as IExtractor).IsWorking select i).ToList();
        }

        public List<IObject> GetNearestGas(IObject obj)
        {
            return (from i in objects where i is Gasfield && !(i as IResource).IsOccupied && i != obj orderby Util.GetDistance(obj, i) select i).ToList();
        }

        public List<IObject> GetNearestOil(IObject obj)
        {
            return (from i in objects where i is Oilfield && !(i as IResource).IsOccupied && i != obj orderby Util.GetDistance(obj, i) select i).ToList();
        }
        public void Add(IObject obj)
        {
            objects.Add(obj);
        }

        public bool Remove(IObject obj)
        {
            return objects.Remove(obj);
        }

        public void RemoveAt(int index)
        {
            objects.RemoveAt(index);
        }

        public IObject GetResourceByPosition(Point position)
        {
            IObject res = (IObject)from i in objects where i.Position == position && i is IResource select i;
            return res;
        }

        public IObject GetExctractorByPosition(Point position)
        {
            IObject res = (IObject)from i in objects where i.Position == position && i is IExtractor select i;
            return res;
        }

        public IObject GetObjectByID(int id)
        {
            var a = from i in objects where i.ID == id select i;
            return a as IObject;
        }
        
        public List<IObject> GetExtractors()
        {
            return (from i in objects where i is IExtractor select i).ToList();
        }
    }
}
