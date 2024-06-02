using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI : IObservable<DataAPI>
    {
        public abstract Vector2 Position { get; }
        public abstract Vector2 Velocity { get; set; }
        public abstract int Radius { get; }
        public abstract bool IsStopped { set; }
        public abstract IDisposable Subscribe(IObserver<DataAPI> observer);
        public abstract void StopLogging();
        public abstract Task StartLogging(ConcurrentQueue<DataAPI> queue);
        public static ConcurrentQueue<DataAPI> Queue { get; } = new ConcurrentQueue<DataAPI>();

        public static DataAPI CreateBall(Vector2 position, int radius, float velocity, Random random)
        {
            return new Ball(position, radius, velocity, random);
        }
    }
}
