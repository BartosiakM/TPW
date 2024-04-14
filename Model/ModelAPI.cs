using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    internal class ModelAPI : AbstractModelAPI
    {
        private AbstractLogicAPI logic;

        public ModelAPI()
        {
            this.logic = AbstractLogicAPI.createApi();
        }

        public override void CreateBall()
        {
            logic.CreateBall();
        }

        public override ObservableCollection<object> GetBalls()
        {
            ObservableCollection<object> balls = new ObservableCollection<object>();
            foreach(BallAPI ball in logic.balls)
            {
                balls.Add(ball);
            }
            return balls;
        }

        public override void Start()
        {
            logic.Start();
        }
    }
}
