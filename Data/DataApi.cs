using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataApi
    {
        public abstract int windowHeight { get; }
        public abstract int windowWidth { get; }
        public abstract int ballSize { get; }
        public static DataApi dataFactory()
        {
            return new Data();
        }
    }
}
