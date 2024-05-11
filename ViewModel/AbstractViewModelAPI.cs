﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public abstract class AbstractViewModelAPI
    {
        public abstract ObservableCollection<object> Balls { get; set; }
        public abstract ICommand StartCommand { get; }
        public abstract ICommand CreateBallCommand { get; }

        public abstract void Start();
        public abstract void CreateBall();

        public abstract ObservableCollection<object> GetBalls();

        public static AbstractViewModelAPI createAPI(int boardWidth,int boardHeight)
        {
            return new ViewModelAPI(ModelAPI.CreateModelAPI(boardWidth, boardHeight, null));
        }

    }
}
