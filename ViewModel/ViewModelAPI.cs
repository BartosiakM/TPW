﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel;

public class ViewModelAPI : INotifyPropertyChanged
{
    private readonly ModelAPI _modelApi;
    private readonly Random _random = new Random();


    private int _ballsAmount;
    private int _radius;
    private bool _startButtonEnabled = true;
    private float _velocity;

    public ViewModelAPI()
    {
        _modelApi = ModelAPI.CreateApi();
        Width = _modelApi.Width;
        Height = _modelApi.Height;
        Balls = _modelApi.Balls;
        StartButtonClicked = new RelayCommand(o => { Start(BallsAmount, Radius, Velocity); }, o => CanStart());
        ResetButtonClicked = new RelayCommand(o => { ResetTable(); }, o => CanReset());

        BallsAmount = _random.Next(2, 10);
        Radius = _random.Next(10, 50);
        Velocity = (float)_random.NextDouble() * 100;
    }

    public ViewModelAPI(ModelAPI modelApi)
    {
        _modelApi = modelApi;
        Width = modelApi.Width;
        Height = modelApi.Height;
        Balls = [];
        StartButtonClicked = new RelayCommand(o => { Start(BallsAmount, Radius, Velocity); }, o => CanStart());
        ResetButtonClicked = new RelayCommand(o => { ResetTable(); }, o => CanReset());
    }

    public RelayCommand StartButtonClicked { get; set; }
    public RelayCommand ResetButtonClicked { get; set; }

    public int Width { get; }

    public int Height { get; }

    public ObservableCollection<BallModelAPI> Balls { get; }

    public int BallsAmount
    {
        get => _ballsAmount;
        set
        {
            if (_ballsAmount == value) return;
            _ballsAmount = value;
            OnPropertyChanged(nameof(BallsAmount));
            StartButtonClicked.RaiseCanExecuteChanged();
            ResetButtonClicked.RaiseCanExecuteChanged();
        }
    }

    public float Velocity
    {
        get => _velocity;
        set
        {
            if (value == _velocity) return;
            _velocity = value;
            OnPropertyChanged(nameof(Velocity));
            StartButtonClicked.RaiseCanExecuteChanged();
        }
    }

    public int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            OnPropertyChanged(nameof(Radius));
            StartButtonClicked.RaiseCanExecuteChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool CanStart()
    {
        return _startButtonEnabled && BallsAmount > 0 && Radius > 0 && Velocity > 0;
    }

    public bool CanReset()
    {
        return !_startButtonEnabled && Balls.Count > 0;
    }

    public void Start(int number, int radius, float velocity)
    {
        _modelApi.Start(number, radius, velocity);
        _startButtonEnabled = false;
        StartButtonClicked.RaiseCanExecuteChanged();
        ResetButtonClicked.RaiseCanExecuteChanged();
    }

    public void ResetTable()
    {
        _modelApi.ResetTable();
        _startButtonEnabled = true;
        StartButtonClicked.RaiseCanExecuteChanged();
        ResetButtonClicked.RaiseCanExecuteChanged();
    }
}