using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public interface IResouce : IObject
    {

        double Depth { get; }

        double Amount { get; }

        bool IsOccupied { get; }

        Color ResourceColor { get; }


    }
}
