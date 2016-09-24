using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    interface IExtractor : IObject
    {
        int OwnerID { get; }

        double ResourceAmount { get; }

        ResourceType Type { get; }

        double Income { get; }

        double Durability { get; }

        double RepairCost { get; }

        bool WaterConnected { get; }

    }
}
