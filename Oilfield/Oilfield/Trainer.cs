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

        public Trainer(int width, int height, string name = "")
        {
            World = new World(width, height, name);
            bot.Reset();
        }

        public void Reset()
        {
            World.Reset();
            bot.Reset();
        }
    }
}
