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
        bool pretrained = false;
        double learningRate;
        WorldState state;
        WorldState prevState;
        Random rand = new Random(DateTime.Now.Millisecond);
        Dictionary<string, List<double>> qTable;
        static List<string> actions;
        List<string> states;

        public Bot(World w, bool pretrained, double discount=0.5, double explore=0.999,
                   double exploreDiscount=0.9999, double learningRate=0.1)
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
            this.explore = explore;
            this.pretrained = pretrained;
            if (pretrained)
            {
                LoadTable();
                this.explore = 0.01;
            }
            else
            {
                for (int i = 0; i < states.Count; i++)
                {
                    qTable[states[i]] = new List<double>();
                    for (int j = 0; j < actions.Count; j++)
                    {
                        qTable[states[i]].Add(0);
                    }
                }
            }


            this.discount = discount;
            this.exploreDiscount = exploreDiscount;
            this.learningRate = learningRate;

            Reward = 0;
        }

        public void Reset()
        {
            Reward = 0;
        }

        private WorldState TakeAction(string act)
        {
            if (act == actions[0])
            {
                BuildBestOilChemic();
            }
            else if (act == actions[1])
            {
                BuildBestOilBeauti();
            }
            else if (act == actions[2])
            {
                BuildBestOilBoth();
            }
            else if (act == actions[3])
            {
                BuildBestGasChemic();
            }
            else if (act == actions[4])
            {
                BuildBestGasBeauti();
            }
            else if (act == actions[5])
            {
                BuildBestGasBoth();
            }
            else if (act == actions[6])
            {
                BuildRandom();
            }
            else if (act == actions[7])
            {
                Idle();
            }
            else
            {
                throw new Exception("Wrong action!");
            }

            return world.GetState();
        }

        private string BestAction()
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
            double randVal = rand.Next(100) / 100.0D;
            if (randVal < explore)
            {
                act = actions[rand.Next(0, actions.Count - 1)];
                if (IsDebug)
                    Debug.Write("Random action: " + act);
            }
            else
            {
                act = BestAction();
                if (IsDebug)
                    Debug.Write("Best action: " + act);
            }

            explore *= exploreDiscount;
            if (explore < 0.2 && !pretrained)
            {
                explore = 0.2;
            }

            if (IsDebug)
                Debug.Write(". Explore rate: " + explore.ToString());

            prevState = state;

            state = TakeAction(act);
            if (IsDebug)
                Debug.Write(". New state: " + state.ToString() + "\n");

            Learn(act);
        }

        private void Learn(string act)
        {
            int rew = world.GetReward(state);
            Reward += rew;
            // most important part
            qTable[prevState.ToString()][actions.IndexOf(act)] = 
                qTable[prevState.ToString()][actions.IndexOf(act)] +
                learningRate * (rew + discount * qTable[state.ToString()].Max() -
                qTable[prevState.ToString()][actions.IndexOf(act)]);
        }

        private void BuildBestOilChemic()
        {
            try
            {
                world.BuildExtractor(world.GetBetterChemicalAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildBestOilBeauti()
        {
            try
            {
                world.BuildExtractor(world.GetBetterAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildBestOilBoth()
        {
            try
            {
                world.BuildExtractor(world.GetBetterOverallAnalysis(ResourceType.OIL)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildBestGasChemic()
        {
            try
            {
                world.BuildExtractor(world.GetBetterChemicalAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildBestGasBeauti()
        {
            try
            {
                world.BuildExtractor(world.GetBetterAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildBestGasBoth()
        {
            try
            {
                world.BuildExtractor(world.GetBetterOverallAnalysis(ResourceType.GAS)[0]);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void BuildRandom()
        {
            try
            {
                world.BuildExtractor(world.GetRandomResource());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void Idle()
        {
            // do nothing
        }

        public void LoadTable()
        {
            string[] lines = System.IO.File.ReadAllLines("qTable.txt");
            for (int i = 0; i < states.Count; i++)
            {
                qTable[states[i]] = new List<double>();
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < actions.Count; j++)
                {
                    qTable[states[i]].Add(double.Parse(line[j]));
                }
            }
        }

        public void SaveTable()
        {
            List<string> table = new List<string>();
            List<string> bestAction = new List<string>();
            for (int i = 0; i < states.Count; i++)
            {
                string bA = "";
                string tab = "";
                bA += states[i] + ": ";
                double maxVal = double.MinValue;
                int maxJ = 0;
                for (int j = 0; j < qTable[states[i]].Count; j++)
                {
                    tab += qTable[states[i]][j].ToString() + " ";
                    if (qTable[states[i]][j] > maxVal)
                    {
                        maxVal = qTable[states[i]][j];
                        maxJ = j;
                    }
                }
                bA += actions[maxJ];
                bestAction.Add(bA);
                table.Add(tab);
            }
            System.IO.File.WriteAllLines("qTable.txt", table);
            System.IO.File.WriteAllLines("bestAction.txt", bestAction);
        }

    }
}
