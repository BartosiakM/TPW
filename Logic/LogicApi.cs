using Data;
using System.Numerics;

namespace Logic
{
    public abstract class LogicApi
    {
        private readonly DataApi dataApi;

        public LogicApi(DataApi dataApi)
        {
            this.dataApi = dataApi;
        }
        internal abstract IEnumerable<Ball> Balls { get; }
        public abstract void SpawnBalls(int numberOfBalls);
        public abstract void Simulation();
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer);

        public static LogicApi CreateInstance(DataApi? data = default)
        {
            if (data == null)
            {
                return new SimulationController(DataApi.dataFactory());
            }
            else
            {
                return new SimulationController(data);
            }
        }


    }



    public interface IBall
    {
        double X { get; set; }
        double Y { get; set; }
        double VelocityX { get; set; }
        double VelocityY { get; set; }
        double Radius { get; set; }
    }
}
