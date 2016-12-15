using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{

    public partial class World
    {

        private List<IResource> convertObjectToResource(List<IObject> obj)
        {
            List<IResource> res = new List<IResource>();

            foreach (var item in obj)
            {
                res.Add(item as IResource);
            }

            return res;
        }
        public List<IResource> GetBetterAnalysis(ResourceType type)
        {
            var a = objManager.GetBetterAnalysis(type);

            return convertObjectToResource(a);
        }

        public List<IResource> GetBetterChemicalAnalysis(ResourceType type)
        {
            var a = objManager.GetBetterChemicalAnalysis(type);

            return convertObjectToResource(a);
        }

        public List<IResource> GetBetterOverallAnalysis(ResourceType type)
        {
            var a = objManager.GetBetterOverallAnalysis(type);

            return convertObjectToResource(a);
        }
    }
}
