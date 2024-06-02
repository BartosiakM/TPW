using System.ComponentModel;
using System.Numerics;

namespace Model
{
    internal class BallModel(Vector2 postion, int radius) : BallModelAPI, INotifyPropertyChanged
    {
        public override float X
        {
            get => postion.X;
            set
            {
                postion.X = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public override float Y
        {
            get => postion.Y;
            set
            {
                postion.Y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public override int Radius => radius;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}