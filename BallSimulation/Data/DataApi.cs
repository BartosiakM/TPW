using System;

namespace Data
{
    public abstract class DataApi
    {
        public abstract int BallSize { get; }
        public abstract int WindowHeight { get; }
        public abstract int WindowWidth { get; }
    }

    public static DataApi CreateInstance()
    {
        return new Data();
    }
}
