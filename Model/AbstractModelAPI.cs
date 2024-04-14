using System.Numerics;
using Logic;

namespace Model
{

    public abstract class AbstractModelAPI : IObserver<IEnumerable<IBall>>, IObservable<IEnumerable<IBallModel>>
    {

        public abstract void SpawnBalls(int numberOfBalls);

        public static AbstractModelAPI CreateInstance()
        {
            return new Model();
        }

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(IEnumerable<IBall> value);

        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBallModel>> observer);
    }

    public interface IBallModel
    {
        double X { get; }
        double Y { get; }
        double VelocityX { get; }
        double VelocityY { get; }
        int Radius { get; }   
    }

}