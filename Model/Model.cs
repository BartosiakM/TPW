using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Model : ModelAPI, IObserver<AbstractLogicAPI>
    {
        private readonly ObservableCollection<BallModelAPI> _balls = new ObservableCollection<BallModelAPI>();
        private readonly object _ballsLock = new object();
        private readonly AbstractLogicAPI _logic;

        public Model()
        {
            _logic = AbstractLogicAPI.CreateApi(700, 400);
            Subscribe(_logic);
        }

        public Model(AbstractLogicAPI logic)
        {
            _logic = logic;
            Subscribe(_logic);
        }

        public override int Width => _logic.Width;

        public override int Height => _logic.Height;

        public override ObservableCollection<BallModelAPI> Balls
        {
            get
            {
                lock (_ballsLock)
                {
                    return _balls;
                }
            }
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(AbstractLogicAPI value)
        {
            lock (_ballsLock)
            {
                var ballPositions = _logic.GetBallPositions();
                if (_balls.Count != ballPositions.Count) return;
                for (var i = 0; i < ballPositions.Count; i++)
                {
                    if (_balls[i].X != ballPositions[i][0]) _balls[i].X = ballPositions[i][0];
                    if (_balls[i].Y != ballPositions[i][1]) _balls[i].Y = ballPositions[i][1];
                }
            }
        }

        public override void Start(int number, int radius, float velocity)
        {
            _logic.Start(number, radius, velocity);
            var ballPositions = _logic.GetBallPositions();
            lock (_ballsLock)
            {
                foreach (var ballPosition in ballPositions)
                {
                    var ball = new BallModel(new Vector2(ballPosition[0], ballPosition[1]), radius);
                    _balls.Add(ball);
                }
            }
        }

        public override void ResetTable()
        {
            lock (_ballsLock)
            {
                _balls.Clear();
            }

            _logic.ResetTable();
        }

        private void Subscribe(AbstractLogicAPI provider)
        {
            provider.Subscribe(this);
        }
    }
}
