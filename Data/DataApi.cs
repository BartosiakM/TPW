using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract BallAPI createBall(bool isSimRunning);
        public abstract int getBoardWidth();
        public abstract int getBoardHeight();
        public static DataAPI CreateDataAPI()
        {
            return new Data();
        }
    }
}
