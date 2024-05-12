﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class ViewModelAPI : AbstractViewModelAPI, INotifyPropertyChanged
    {
        private readonly AbstractModelAPI _model;

        private ObservableCollection<object> balls;

        public event PropertyChangedEventHandler PropertyChanged;
        public override ICommand StartCommand { get; }
        public override ICommand StopCommand { get; }
        public override ICommand CreateBallCommand { get; }

        public ViewModelAPI()
        {
            _model = ModelAPI.CreateModelAPI(null);
            this.StartCommand = new Command(Start);
            this.StopCommand = new Command(Stop);
            this.CreateBallCommand = new Command(CreateBall);
            this.Balls = GetBalls();
        }


        public override void CreateBall()
        {
            _model.CreateBall();
            Balls = GetBalls();
        }

       

        public override void Start()
        {
            _model.Start();
        }

        public override void Stop()
        {
            _model.Stop();
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
            return _model.GetBalls();
        }
    }
}
