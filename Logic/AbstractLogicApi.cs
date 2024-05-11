using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;


namespace Logic
{

    public abstract class AbstractLogicAPI
    {
        public abstract List<BallAPI> balls { get; }
        public abstract int BoardWidth { get; }
        public abstract int BoardHeight { get; }
        public abstract void CreateBall();
        public abstract void Start();
        public abstract void Stop();

        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetBallsNumber();

        public static AbstractLogicAPI CreateApi(int width, int height, DataAPI data)
        {
            if (data == null)
            {
                return new LogicAPI(DataAPI.CreateDataAPI(width, height));

            }
            else
            {
                return new LogicAPI(data);
            }

        }

        
    }
    internal class LogicAPI : AbstractLogicAPI
    {
        public override List<BallAPI> balls { get; }
        private static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        public override int BoardWidth { get; }
        public override int BoardHeight { get; }

        private DataAPI data;


        public LogicAPI(DataAPI data)
        {
            balls = new List<BallAPI>();
            this.BoardWidth = data.getBoardWidth();
            this.BoardHeight = data.getBoardHeight(); 
            this.data = data;

        }

        public override void CreateBall()
        {
            BallAPI ball = data.createBall(true);
            balls.Add(ball);
            ball.subscribeToPropertyChanged(CheckCollisions);
        }

        private bool CheckCollisionWithOtherBall(BallAPI ball1, BallAPI ball2)
        {
            int distanceX = ball2.X - ball1.X;
            int distanceY = ball2.Y - ball1.Y;
            int combinedRadius = ball1.Size / 2 + ball2.Size / 2;

            if (distanceX * distanceX + distanceY * distanceY <= combinedRadius * combinedRadius)
            {
                readerWriterLockSlim.EnterWriteLock();
                try
                {
                    int v1x = ball1.Vx;
                    int v1y = ball1.Vy;
                    int v2x = ball2.Vx;
                    int v2y = ball2.Vy;

                    int newV1X = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                    int newV1Y = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                    int newV2X = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                    int newV2Y = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                    ball1.Vx = newV1X;
                    ball1.Vy = newV1Y;
                    ball2.Vx = newV2X;
                    ball2.Vy = newV2Y;
                }
                finally
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
                return true; // There is a collision
            }
            return false; // No collision
        }

        private void CheckCollisionWithBoard(BallAPI ball)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                int Vx = ball.Vx;
                int Vy = ball.Vy;

                if (ball.X + Vx < 0 || ball.X + Vx >= BoardWidth)
                {
                    Vx = -Vx;
                }

                if (ball.Y + Vy < 0 || ball.Y + Vy >= BoardHeight)
                {
                    Vy = -Vy;
                }

                ball.Vx = Vx;
                ball.Vy = Vy;
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }


        private void CheckCollisions(object sender, PropertyChangedEventArgs e)
        {
            BallAPI ball = (BallAPI)sender;
            if (ball != null)
            {
                CheckCollisionWithBoard(ball);

                foreach (var ball2 in balls)
                {
                    if (!ball2.Equals(ball))
                    {
                        CheckCollisionWithOtherBall(ball, ball2);
                    }
                }
            }

        }

        public override int GetX(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return balls[index].X;
            }
            else
            {
                return -1;
            }
        }

        public override int GetY(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return balls[index].Y;
            }
            else
            {
                return -1;
            }
        }

        public override int GetBallsNumber()
        {
            return balls.Count;
        }

        public override void Start()
        {
            foreach (var ball in balls)
            {
                ball.isSimRunning = true;
            }
        }

        public override void Stop()
        {
            foreach (var ball in balls)
            {
                ball.isSimRunning = false;
            }
        }

        public override int GetSize(int i)
        {
            if (i >= 0 && i < balls.Count)
            {
                return balls[i].Size;
            }
            else
            {
                return -1;
            }
        }
    }
}