using System.Collections.ObjectModel;
using Logic;

namespace Model
{

    public abstract class AbstractModelAPI
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();

        public abstract ObservableCollection<object> GetBalls();
        public static ModelAPI CreateModelAPI( AbstractLogicAPI logicAPI)
        {
            if (logicAPI == null)
            {
                return new ModelAPI(AbstractLogicAPI.CreateApi(null));
            }
            else
            {
                return new ModelAPI(logicAPI);
            }
        }

    }

}