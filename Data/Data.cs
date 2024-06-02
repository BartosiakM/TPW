using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Data : DataAPI
    {
        private int _boardWidth;
        private int _boardHeight;

        public Data()
        {
            _boardWidth = 315;
            _boardHeight = 150;
        }

        public override int getBoardWidth()
        {
            return _boardWidth;
        }

        public override int getBoardHeight()
        {
            return _boardHeight;
        }

        public override BallAPI createBall(bool isSimRunning)
        {
            Random random = new Random();
            int x = random.Next(20, _boardWidth - 20);
            int y = random.Next(20, _boardHeight - 20);
            int valueX = GenerateRandomVelocity();
            int valueY = GenerateRandomVelocity();
            Vector2 velocity = new Vector2((int)valueX, (int)valueY);
            Vector2 position = new Vector2((int)x, (int)y);

            int radius = 20;
            int mass = 200;
            return BallAPI.CreateBallAPI(position, velocity, radius, mass, isSimRunning);
        }

        private int GenerateRandomVelocity()
        {
            Random random = new Random();
            int[] possibleVelocities = { -3, -2, -1, 1, 2, 3 };
            return possibleVelocities[random.Next(possibleVelocities.Length)];
        }
    }
}