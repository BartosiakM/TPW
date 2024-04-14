using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    internal class Command : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute;

        public Command(Action action, Func<bool> canExecute = null)
        {
            this.action = action ?? throw new Exception();
            this.canExecute = canExecute;
        }

        
        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter) => action();
    }
}
