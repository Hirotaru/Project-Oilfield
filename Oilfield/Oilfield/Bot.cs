using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    class Bot
    {
        World world;
        double discount;
        double explore;
        double learningRate;
        // State state;
        double reward;
        // Q[,]

        public Bot(World w, double discount=0.5, double explore=0.5, double learningRate=0.5)
        {
            // give here starting state
            world = w;

            this.discount = discount;
            this.explore = explore;
            this.learningRate = learningRate;

            reward = 0;
        }

        public void SetState()
        {
            // сюда каждый апддейт передавать текущий стейт
        }

        public void BuildBestOilChemic()
        {

        }

        public void BuildBestOilBeauti()
        {

        }

        public void BuildBestOilBoth()
        {

        }

        public void BuildBestGasChemic()
        {

        }

        public void BuildBestGasBeauti()
        {

        }

        public void BuildBestGasBoth()
        {

        }

        public void BuildRandom()
        {

        }

        public void Idle()
        {

        }

        public int StateValue()
        {
            // возврашает награду или штраф за текущее положение. Перетащить в World?
            return 0;
        }

        public bool IsTerminalState()
        {
            // возвращает тру, когда симуляция завершена. Перетащить в World?
            return false;
        }


    }
}
