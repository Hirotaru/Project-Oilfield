using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oilfield
{
    public class FloatingText
    {
        private Color color = Color.LimeGreen;
        private int Alpha = 255;
        private int height = -30;
        private int currentHeight = 0;
        private int AlphaStep
        {
            get { return -255 / height; }
        }

        private string text;

        public bool IsDone
        {
            get { return currentHeight == height; }
        }

        private Point startPosition;

        public Point StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        private Point CurrentPosition;

        public FloatingText(Point position, string text)
        {
            this.text = "+" + text;
            startPosition = position;
            startPosition.X -= UIConfig.Step * 3;
            startPosition.Y -= UIConfig.Step * 3;
            CurrentPosition = startPosition;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(text, new Font("Courier New", 12), new SolidBrush(color), CurrentPosition.X + UIConfig.dx, CurrentPosition.Y + UIConfig.dy);
        }

        public void Update()
        {
            Alpha -= AlphaStep;
            if (Alpha < 0) Alpha = 0;
            currentHeight--;
            color = Color.FromArgb(Alpha, color);
            CurrentPosition.Y = startPosition.Y + currentHeight;
        }


    }
}
