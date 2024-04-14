using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    internal class ViewModelAPI : AbstractViewModelAPI, INotifyPropertyChanged
    {
        private readonly AbstractModelAPI model;

        public ICommand StartCommand { get; }

        private ObservableCollection<object> balls;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModelAPI()
        {
            model = AbstractModelAPI.CreateAPI();
            StartCommand = new RelayCommand(Start);
            CreateBallCommand = new RelayCommand(CreateBall);
            Balls = GetBalls();
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
        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<object> GetBalls()
        {
            return model.GetBalls();
        }
    }
}
