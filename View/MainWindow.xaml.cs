﻿using ViewModel;
using System.Windows;


namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = AbstractViewModelAPI.createAPI(300,150);
        }
    }
}