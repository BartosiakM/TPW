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

        public double X => throw new NotImplementedException();

        public double Y => throw new NotImplementedException();

        public double VelocityX => throw new NotImplementedException();

        public double VelocityY => throw new NotImplementedException();

        public int Radius => throw new NotImplementedException();
    }
}
