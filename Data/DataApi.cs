using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI : IObservable<DataAPI>
    {
        public abstract int ID { get; }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Velocity { get; set; }
        public abstract int Radius { get; }
        public abstract bool IsStopped { set; }
        public abstract IDisposable Subscribe(IObserver<DataAPI> observer);
        public static DataAPI CreateBall(Vector2 position, int radius, float velocity, Random random, int id)
        {
            return new Ball(position, radius, velocity, random, id);
        }
    }
}