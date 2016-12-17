using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class Trainer
    {
        public World World;
        private Bot bot;
        int index = 0;
        int iterationCount = 1;
        int curIteration = 1;

        public Trainer(int width, int height, int iteration = 10, string name = "")
        {
            World = new World(width, height, name);
            bot = new Bot(World);
            iterationCount = iteration;
        }

        public void Update(double dt)
        {
            if (World.IsEnd())
            {
                Debug.WriteLine(String.Format("Iteration {0} end. Reward: {1}", curIteration,
                    bot.Reward));
                curIteration++;
                if (curIteration >= iterationCount) return;
                Reset();
            }
            else
            {
                World.Update(dt);
                index++;
                if (index > 100)
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
