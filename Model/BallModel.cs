using System.Numerics;
using Logic;

namespace Model
{
    internal class BallModel : IBallModel
    {
        private Ball ball;

        public BallModel(Ball ball)
        {
            this.ball = ball;
        }

        public Vector2 Velocity => throw new NotImplementedException();

        public Vector2 Position => throw new NotImplementedException();

        public int Radius => throw new NotImplementedException();

        public int Diameter => throw new NotImplementedException();
    }
}
