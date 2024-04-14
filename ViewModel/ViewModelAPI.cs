using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    internal class ViewModelAPI : AbstractViewModelAPI, INotifyPropertyChanged
    {
        private readonly AbstractModelAPI model;

        private ObservableCollection<object> balls;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelAPI(int boardWidth, int boardHeight)
        {
            model = AbstractModelAPI.CreateAPI(boardWidth,boardHeight);
            this.StartCommand = new Command(Start);
            this.CreateBallCommand = new Command(CreateBall);
            this.Balls = GetBalls();
        }

        public override ICommand StartCommand { get; }

        public override ICommand CreateBallCommand { get; }

        public override void CreateBall()
        {
            model.CreateBall();
        }

       

        public override void Start()
        {
            model.Start();
        }

        public override ObservableCollection<object> Balls
        {
            get => balls;
            set
            {
                balls = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override ObservableCollection<object> GetBalls()
        {
            return model.GetBalls();
        }
    }
}
