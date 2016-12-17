using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oilfield
{
    public class Trainer
    {
        public World World;
        private Bot bot;
        int index = 0;

        public Trainer(int width, int height, string name = "")
        {
            World = new World(width, height, name);
            bot = new Bot(World);
        }

        public void Update(double dt)
        {
            World.Update(dt);
            index++;
            if (index > 100)
            {
                index = 0;
                bot.Step();
            }
        }

        public void Reset()
        {
            World.Reset();
            bot.Reset();
        }


    }
}
