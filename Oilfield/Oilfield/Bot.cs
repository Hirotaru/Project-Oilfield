using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Oilfield
{
    class Bot
    {
        public bool IsDebug = true;
        public double Reward;

        World world;
        double discount;
        double explore;
        double exploreDiscount;
        double learningRate;
        WorldState state;
        WorldState prevState;
        Random rand = new Random(DateTime.Now.Millisecond);
        Dictionary<string, List<double>> qTable;
        static List<string> actions;
        List<string> states;

        public Bot(World w, double discount=0.5, double explore=0.9,
                   double exploreDiscount=0.99, double learningRate=0.3)
        {
            // discount: how much the agent values future rewards over immediate rewards
            // explore: with what probability the agent "explores", i.e.chooses a random action
            // exploreDiscount: how mush decrease explore in each step
            // learning_rate: how quickly the agent learns

            world = w;
            state = world.GetState();
            prevState = world.GetState();

            states = new List<string>() { "000", "001", "002", "010", "011", "012", "020", "021", "022",
                                          "100", "101", "102", "110", "111", "112", "120", "121", "122",
                                          "200", "201", "202", "210", "211", "212", "220", "221", "222"};

            actions = new List<string>() { "BuildBestOilChemic", "BuildBestOilBeauti", "BuildBestOilBoth",
                                           "BuildBestGasChemic", "BuildBestGasBeauti", "BuildBestGasBoth",
                                           "BuildRandom", "Idle" };

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

            Reward = 0;
        }

        public void Reset()
        {
            Reward = 0;
        }

        private WorldState takeAction(string act)
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

            return world.GetState();
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
            if (IsDebug)
                Debug.WriteLine("Step. ");
            string act = "";
            if (rand.Next(100) / 100.0D < explore)
            {
                act = actions[rand.Next(0, actions.Count - 1)];
                if (IsDebug)
                    Debug.Write("Random action: " + act);
            }
            else
            {
                act = bestAction();
                if (IsDebug)
                    Debug.Write("Best action: " + act);
            }

            explore *= exploreDiscount;
            if (IsDebug)
                Debug.Write(". Explore rate: " + explore.ToString());

            prevState = state;

            state = takeAction(act);
            if (IsDebug)
                Debug.Write(". New state: " + state.ToString() + "\n");

            learn(act);
        }

        private void learn(string act)
        {
            int rew = world.GetReward(state);
            Reward += rew;
            // most important part
            qTable[prevState.ToString()][actions.IndexOf(act)] = 
                qTable[prevState.ToString()][actions.IndexOf(act)] +
                learningRate * (rew + discount * qTable[state.ToString()].Max() -
                qTable[prevState.ToString()][actions.IndexOf(act)]);
        }

        private void buildBestOilChemic()
        {
            try
            {
                world.BuildExtractor(world.GetBetterChemicalAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildBestOilBeauti()
        {
            try
            {
                world.BuildExtractor(world.GetBetterAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildBestOilBoth()
        {
            try
            {
                world.BuildExtractor(world.GetBetterOverallAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildBestGasChemic()
        {
            try
            {
                world.BuildExtractor(world.GetBetterChemicalAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildBestGasBeauti()
        {
            try
            {
                world.BuildExtractor(world.GetBetterAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildBestGasBoth()
        {
            try
            {
                world.BuildExtractor(world.GetBetterOverallAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {

            }
        }

        private void buildRandom()
        {
            try
            {
                world.BuildExtractor(world.GetRandomResource());
            }
            catch (Exception ex)
            {

            }
        }

        private void idle()
        {
            // do nothing
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
