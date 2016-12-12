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
