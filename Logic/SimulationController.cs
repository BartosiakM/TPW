using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class SimulationController : LogicApi
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers;
        private DataApi _data;
        private SimulationManager _simulationManager;
        private bool _isRunning = false;

        public SimulationController(DataApi? data = default) : base(dataApi: data)
        {
            _data = data ?? DataApi.dataFactory();
            _simulationManager = new SimulationManager(new Window(_data.windowWidth, _data.windowHeight), _data.ballSize);
            _observers = new HashSet<IObserver<IEnumerable<Ball>>>();
        }

        internal override IEnumerable<Ball> Balls => _simulationManager.Balls;

        public override void SpawnBalls(int numberOfBalls)
        {
            _simulationManager.RandomizedBallSpawning(numberOfBalls);
        }

        public override void Simulation()
        {
            while (_isRunning)
            {
                _simulationManager.ApplyForceToBalls();
                TrackBalls(Balls);
                Thread.Sleep(10);
            }
        }

        public override void StartSim()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                Task.Run(Simulation);
            }
        }

        public override void StopSim()
        {
            if (_isRunning)
            {
                _isRunning = false;
            }
        }

        private class SubscriptionController : IDisposable
        {
            private ISet<IObserver<IEnumerable<Ball>>> _observers;
            private IObserver<IEnumerable<Ball>> _observer;

            public SubscriptionController(ISet<IObserver<IEnumerable<Ball>>> observers, IObserver<IEnumerable<Ball>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers != null)
                {
                    _observers.Remove(_observer);
                }
            }
        }

        public void TrackBalls(IEnumerable<Ball> balls)
        {
            foreach (var observer in _observers)
            {
                if (observer != null && balls != null)
                {
                    observer.OnNext(balls);
                }
            }
        }

        public void CompleteTracking()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }
            _observers.Clear();
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer)
        {
            throw new NotImplementedException();
        }
    }
}
