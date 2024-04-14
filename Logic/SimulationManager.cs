using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class SimulationManager
    {
        private Window _window;
        private int _ballDiameter;
        private int _ballRadius;
        private const float _maxVelocity = 50;
        private Random _random;

        public IList<Ball> Balls { get; private set; }

        public SimulationManager(Window window, int ballDiameter)
        {
            _window = window;
            _ballDiameter = ballDiameter;
            _ballRadius = ballDiameter / 2;
            _random = new Random();
            Balls = new List<Ball>();
        }

        public void ApplyForceToBalls(float force = 0.1f)
        {
            foreach (var ball in Balls)
            {
                ball.ChangingPosition(_window.Height, _window.Width);
            }
        }

        private double getRandomNumber(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        private double getRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public IList<Ball> RandomizedBallSpawning(int numberOfBalls)
        {
            Balls = new List<Ball>(numberOfBalls);

            for (int i = 0; i < numberOfBalls; i++)
            {
                double x = getRandomNumber(_ballRadius, _window.Width - _ballRadius);
                double y = getRandomNumber(_ballRadius, _window.Height - _ballRadius);
                double velocityX = getRandomNumber(1, 3);
                double velocityY = getRandomNumber(1, 3);

                Balls.Add(new Ball { X = x, Y = y, VelocityX = velocityX, VelocityY = velocityY, Radius = _ballRadius });
            }

            return Balls;
        }
    }
}

