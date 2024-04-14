using ViewModel;
using System.Windows;


namespace View
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();
            DataContext = AbstractViewModelAPI.createAPI();
        }
    }
}
