using Logic;

namespace Model
{

    internal class Model : AbstractModelAPI
    {
        private AbstractLogicAPI logic;

        public Model()
        {
            this.logic = AbstracLogicAPI.CreateInstance();
        }

        public void SpawnBalls(int numberOfBalls)
        {
            this.logic.SpawnBalls(numberOfBalls);
            this.logic.StartSim();
        }


    }

}