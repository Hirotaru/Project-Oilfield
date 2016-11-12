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

        double Amount { get; } //Закрыть

        bool IsOccupied { get; }

        //Добавить "Красивость"

            //Добавить хим. анализ

        int ChemicalAnalysis { get; }

        int OverallAnalysis { get; }

        Color ResourceColor { get; }


    }
}
