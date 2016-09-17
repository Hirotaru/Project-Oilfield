using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public interface IResouce
    {
        Point Position { get; set; }

        double Depth { get; set; }

        double Amount { get; set; }


    }
}
