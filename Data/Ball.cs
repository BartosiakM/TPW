using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : DataAPI, IObservable<DataAPI>
    {
        private readonly List<IObserver<DataAPI>> _observers = new();
        private Vector2 _position;
        private Vector2 _velocity;
        private bool _isStopped;
        private bool run = true;

        private readonly object _positionLock = new();
        private readonly object _velocityLock = new();
        private readonly object _stopLock = new();
        private readonly object fileLock = new();

        private readonly Stopwatch _stopwatch = new();

        public Ball(Vector2 position, int radius, float velocity, Random random)
        {
            _position = position;
            Radius = radius;
            Task.Run(() => Move(velocity, random));
        }

        public override void StopLogging()
        {
            run = false;
        }

        private async Task BallLogger(ConcurrentQueue<DataAPI> queue)
        {
            while (run)
            {
                _stopwatch.Restart();
                if (queue.TryDequeue(out DataAPI ball) && ball != null)
                {
                    string log = $"{{\n\t\"Date\": \"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff}\",\n\t\"Info\":{JsonSerializer.Serialize(ball)}\n}}";

                    lock (fileLock)
                    {
                        using (var writer = new StreamWriter("..\\..\\..\\..\\..\\Log.json", true, Encoding.UTF8))
                        {
                            writer.WriteLine(log);
                        }
                    }
                }
                _stopwatch.Stop();
                await Task.Delay((int)_stopwatch.ElapsedMilliseconds + 100);
            }
        }

        public override async Task StartLogging(ConcurrentQueue<DataAPI> queue)
        {
            run = true;
            await BallLogger(queue);
        }

        public override Vector2 Position
        {
            get
            {
                lock (_positionLock)
                {
                    return _position;
                }
            }
        }

        public override Vector2 Velocity
        {
            get
            {
                lock (_velocityLock)
                {
                    return _velocity;
                }
            }
            set
            {
                lock (_velocityLock)
                {
                    _velocity = value;
                }
            }
        }

        public override int Radius { get; }

        public override bool IsStopped
        {
            set
            {
                lock (_stopLock)
                {
                    _isStopped = value;
                }
            }
        }

        public override IDisposable Subscribe(IObserver<DataAPI> observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
            return new SubscriptionToken(_observers, observer);
        }

        private async void Move(float velocity, Random random)
        {
            float moveAngle = random.Next(0, 360);
            Velocity = new Vector2(velocity * (float)Math.Cos(moveAngle), velocity * (float)Math.Sin(moveAngle));
            const float timeOfTravel = 1f / 60f;
            while (!_isStopped)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                await Task.Delay(TimeSpan.FromSeconds(timeOfTravel));
                stopwatch.Stop();
                var timeElapsed = (float)stopwatch.Elapsed.TotalSeconds;
                var velocityChange = Velocity * timeElapsed;
                lock (_positionLock)
                {
                    _position += velocityChange;
                }

                NotifyObservers(this);
            }
        }

        private void NotifyObservers(DataAPI ball)
        {
            foreach (var observer in _observers) observer.OnNext(ball);
        }
    }

    internal class SubscriptionToken : IDisposable
    {
        private readonly ICollection<IObserver<DataAPI>> _observers;
        private readonly IObserver<DataAPI> _observer;

        public SubscriptionToken(ICollection<IObserver<DataAPI>> observers, IObserver<DataAPI> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
