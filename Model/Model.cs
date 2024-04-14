using Logic;

namespace Model
{

    internal class Model : AbstractModelAPI
    {
        private LogicApi logic;
        private IDisposable? subscriber;

        public Model()
        {
            this.logic = LogicApi.CreateInstance();
        }

        public override void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public override void OnNext(IEnumerable<IBall> value)
        {
            throw new NotImplementedException();
        }

        public override void SpawnBalls(int numberOfBalls)
        {
            this.logic.SpawnBalls(numberOfBalls);
            this.logic.StartSim();
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<IBallModel>> provider)
        {
            subscriber =
        }
    }

}