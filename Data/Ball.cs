using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class Ball : DataAPI, IObservable<DataAPI>
    {
        private readonly List<IObserver<DataAPI>> _observers = [];
        private Vector2 _position;
        private Vector2 _velocity;
        private readonly int _size;
        private readonly int _mass;
        private bool _isStopped;
        private readonly Logger logger;
        


        private readonly object _positionLock = new();
        private readonly object _velocityLock = new();
        private readonly object _stopLock = new();


        public Ball(Vector2 position, int radius, float velocity, Random random, int id)
        {
            _position = position;
            Radius = radius;
            ID = id;
            logger = Logger.CreateInstance();
            Task.Run(() => Move(velocity, random));
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

        public override int ID { get; }

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
            while (true)
            {
                Stopwatch stopwatch = new();
                stopwatch.Start();
                float startTime = stopwatch.ElapsedMilliseconds;
                await Task.Delay(TimeSpan.FromSeconds(timeOfTravel));
                stopwatch.Stop();
                var timeElapsed = (float)stopwatch.Elapsed.TotalSeconds;
                var velocityChange = Velocity * timeElapsed;
                if (stopwatch.ElapsedMilliseconds - startTime >= 1f / 60f)
                {
                    lock (_positionLock)
                    {
                        _position += velocityChange;
                    }

                    NotifyObservers(this);
                    logger.addToBuffer(this, DateTime.Now);
                }
              
            }
        }

        private void NotifyObservers(DataAPI ball)
        {
            foreach (var observer in _observers) observer.OnNext(ball);
        }

    }

    internal class SubscriptionToken(ICollection<IObserver<DataAPI>> observers, IObserver<DataAPI> observer)
    : IDisposable
    {
        public void Dispose()
        {
            observers.Remove(observer);
        }
    }
}