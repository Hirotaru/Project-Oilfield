using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public interface IConnectable : IObject
    {
        HashSet<IObject> ConnectedTo { get; }
    }
}
