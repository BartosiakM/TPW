using System;

namespace Logic
{
    public class Ball
    {
        private Random random = new Random();
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double Radius { get; set; }

        public Ball()
        {
            Random random = new Random();
            X = GetRandomNumber(21.0, 679.0);
            Y = GetRandomNumber(21.0, 479.0);
            VelocityX = GetRandomNumber(1, 3);
            VelocityY = GetRandomNumber(1, 3);
            Radius = 10;
        }

        private double GetRandomNumber(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public void ChangingPosition(double height, double width)
        {
            double nextX = X + VelocityX;
            double nextY = Y + VelocityY;

            if (nextX + Radius > width || nextX < Radius)
            {
                VelocityX *= -1.0;
            }
            if (nextY + Radius > height || nextY < Radius)
            {
                VelocityY *= -1.0;
            }
            X = nextX;
            Y = nextY;
        }
    }
}
