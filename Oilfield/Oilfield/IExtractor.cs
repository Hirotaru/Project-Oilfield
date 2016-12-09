using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public interface IExtractor : IConnectable
    {
        double ResourceAmount { get; }

        ResourceType Type { get; }

        double Income { get; }

        bool WaterConnected { get; }
    }
}
