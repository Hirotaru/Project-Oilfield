using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oilfield
{
    public class Trainer
    {
        public World World;
        private Bot bot;
        int index = 0;
        int iterationCount = 1;
        public int curIteration = 1;
        List<string> trainingInfo = new List<string>();

        public Trainer(int width, int height, bool pretrained, int iteration = 2,  string name = "")
        {
            World = new World(width, height, name);
            bot = new Bot(World, pretrained);
            iterationCount = iteration;
        }

        public void Update(double dt)
        {
            if (curIteration > iterationCount)
            {
                return;
            }

            if (World.IsEnd())
            {
                Debug.WriteLine(String.Format("Iteration {0} end. Reward: {1}", curIteration,
                    bot.Reward));
                trainingInfo.Add(String.Format("Iteration {0} end. Reward: {1}", curIteration,
                    bot.Reward));
                curIteration++;
                if (curIteration > iterationCount)
                {
                    System.IO.File.WriteAllLines("trainingInfo.txt", trainingInfo);
                    bot.SaveTable();
                    //Application.Exit();
                }
                Reset();
            }
            else
            {
                World.Update(dt);
                index++;
                if (index > 25)
                {
                    index = 0;
                    bot.Step();
                }
            }
        }

        public void Reset()
        {
            World.Reset();
            bot.Reset();
        }


    }
}
