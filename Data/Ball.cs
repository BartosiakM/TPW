using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    public abstract class BallAPI
    {
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract bool isSimRunning { get; set; }
        public abstract void subscribeToPropertyChanged(PropertyChangedEventHandler handler);
        public abstract int Vx { get; set; }
        public abstract int Vy { get; set; }
        public abstract int Mass { get; }
        public abstract int Diameter { get; }
        public abstract int Size { get; }
        public static BallAPI CreateBallAPI(int _x, int _y, int _deltaX, int _deltaY, int _size, int _mass, bool isSimRunning)
        {
            return new Ball(_x, _y, _deltaX, _deltaY, _size, _mass, isSimRunning);
        }
    }

    internal class Ball : BallAPI, INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _deltaX;
        private int _deltaY;
        private readonly int _size;
        private readonly int _mass;
        private bool _isSimRunning;


        public Ball(int _x, int _y, int _deltaX, int _deltaY, int _size, int _mass, bool _isSimRunning)
        {
            this._x = _x;
            this._y = _y;
            this._deltaX = _deltaX;
            this._deltaY = _deltaY;
            this._size = _size;
            this._mass = _mass;
            Task.Run(() => Move());
            this._isSimRunning = _isSimRunning;

        }

        public override int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public override int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public override void subscribeToPropertyChanged(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }

        public override bool isSimRunning
        {
            get { return _isSimRunning; }
            set { _isSimRunning = value; }
        }

        public override int Vx
        {
            get { return _deltaX; }
            set { _deltaX = value; }
        }

        public override int Vy
        {
            get { return _deltaY; }
            set { _deltaY = value; }
        }


        public override int Diameter
        {
            get { return _size * 2; }
        }

        public override int Size { get => _size; }

        public override int Mass => _mass;

        private async Task Move()
        {
            while (true)
            {
                if (isSimRunning)
                {
                    X += _deltaX;
                    Y += _deltaY;
                }

                double velocity = Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);
                await Task.Delay(TimeSpan.FromMilliseconds(1000 / 360 * velocity));
            }
        }
    public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}