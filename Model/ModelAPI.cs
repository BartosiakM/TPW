using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public class ModelAPI : AbstractModelAPI
    {
        private AbstractLogicAPI logic;

        public ModelAPI(AbstractLogicAPI logic)
        {
            this.logic = logic;
        }

        public override void CreateBall()
        {
            logic.CreateBall();
        }

        public override ObservableCollection<object> GetBalls()
        {
            ObservableCollection<object> balls = new ObservableCollection<object>();
            foreach(object ball in logic.balls)
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
