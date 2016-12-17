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
        double exploreDiscount;
        double learningRate;
        WorldState state;
        WorldState prevState;
        double reward;
        Random rand = new Random(DateTime.Now.Millisecond);
        Dictionary<string, List<double>> qTable;
        static List<string> actions = new List<string>() { "BuildBestOilChemic", "BuildBestOilBeauti", "BuildBestOilBoth",
                                           "BuildBestGasChemic", "BuildBestGasBeauti", "BuildBestGasBoth",
                                           "BuildRandom", "Idle" };
        List<string> states;

        public Bot(World w, double discount=0.5, double explore=0.9,
                   double exploreDiscount=0.99, double learningRate=0.5)
        {
            // discount: how much the agent values future rewards over immediate rewards
            // explore: with what probability the agent "explores", i.e.chooses a random action
            // exploreDiscount: how mush decrease explore in each step
            // learning_rate: how quickly the agent learns
            world = w;
            state = world.GetState();
            prevState = world.GetState();

            states = new List<string>() ,{ };

            qTable = new Dictionary<string, List<double>>();
            for (int i = 0; i < states.Count; i++)
            {
                qTable[states[i]] = new List<double>();
                for (int j = 0; j < actions.Count; j++)
                {
                    qTable[states[i]].Add(0);
                }
            }

            this.discount = discount;
            this.explore = explore;
            this.exploreDiscount = exploreDiscount;
            this.learningRate = learningRate;

            reward = 0;
        }

        public void Reset()
        {
            reward = 0;
        }

        private void takeAction(string act)
        {
            if (act == actions[0])
            {
                buildBestOilChemic();
            }
            else if (act == actions[1])
            {
                buildBestOilBeauti();
            }
            else if (act == actions[2])
            {
                buildBestOilBoth();
            }
            else if (act == actions[3])
            {
                buildBestGasChemic();
            }
            else if (act == actions[4])
            {
                buildBestGasBeauti();
            }
            else if (act == actions[5])
            {
                buildBestGasBoth();
            }
            else if (act == actions[6])
            {
                buildRandom();
            }
            else if (act == actions[7])
            {
                idle();
            }
            else
            {
                throw new Exception("Wrong action!");
            }
        }

        private string bestAction()
        {
            List<double> actionsRewards = qTable[state.ToString()];
            double maxVal = actionsRewards[0];
            string maxAct = actions[0];

            for (int i = 1; i < actionsRewards.Count; i++)
            {
                if (actionsRewards[i] > maxVal)
                {
                    maxVal = actionsRewards[i];
                    maxAct = actions[i];
                }
            }
            return maxAct;
        }

        public void Step()
        {
            // main update method
            string act = "";
            if (rand.Next(100) / 100.0D < explore)
            {
                act = actions[rand.Next(0, actions.Count - 1)];
            }
            else
            {
                act = bestAction();
            }

            explore *= exploreDiscount;
        }

        private void buildBestOilChemic()
        {

        }

        private void buildBestOilBeauti()
        {

        }

        private void buildBestOilBoth()
        {

        }

        private void buildBestGasChemic()
        {

        }

        private void buildBestGasBeauti()
        {

        }

        private void buildBestGasBoth()
        {

        }

        private void buildRandom()
        {

        }

        private void idle()
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
