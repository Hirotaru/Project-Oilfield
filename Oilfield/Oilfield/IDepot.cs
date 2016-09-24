using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public interface IDepot : IObject
    {
        double Capacity { get; }

        int OwnerID { get; }

        double RemainingCapacity { get; }


    }
}
