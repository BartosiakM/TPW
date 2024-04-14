using System.Collections.ObjectModel;
using Logic;

namespace Model
{

    public abstract class AbstractModelAPI
    {
        public abstract void Start();
        public abstract void CreateBall();

        public abstract ObservableCollection<object> GetBalls();
        
        public static AbstractModelAPI CreateAPI(int boardWidth, int boardHeight)
        {
            return new ModelAPI(boardWidth,boardHeight); 
        }

    }

}