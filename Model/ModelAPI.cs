using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public abstract class ModelAPI
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract ObservableCollection<BallModelAPI> Balls { get; }

        public static ModelAPI CreateApi()
        {
            return new Model();
        }

        public static ModelAPI CreateApi(AbstractLogicAPI logic)
        {
            return new Model(logic);
        }
        public abstract void Start(int number, int radius, float velocity);
        public abstract void ResetTable();
    }
}
