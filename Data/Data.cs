namespace Data
{
    internal class Data : DataApi
    {
        public Data()
        {
            windowHeight = 478;
            windowWidth = 798;
            ballSize = 20;
        }

        public override int windowHeight { get; }
        public override int windowWidth { get; }
        public override int ballSize { get; }
    }
}
