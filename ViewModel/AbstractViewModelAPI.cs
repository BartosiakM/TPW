﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    internal abstract class AbstractViewModelAPI
    {
        public ObservableCollection<object> Balls { get; set; }
        public abstract ICommand StartCommand { get; }
        public abstract ICommand CreateBallCommand { get; }

        public abstract void Start();
        public abstract void CreateBall();

        public abstract ObservableCollection<object> GetBalls();

        public static AbstractViewModelAPI createAPI()
        {
            return new ViewModelAPI();
        }

    }
}