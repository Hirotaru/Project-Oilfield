using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public interface IDepot : IConnectable
    {
        double Capacity { get; }

        int OwnerID { get; }

        double RemainingCapacity { get; }

        ResourceType ResType { get; }
    }
}
