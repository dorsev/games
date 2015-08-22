using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GameCommon
{
    public class DorCommand:ICommand
    {
        public DorCommand(Action<object> actionToPerform,Func<bool> CanExecute)
        {
            action = actionToPerform;
            canExecute = CanExecute;
        }
        public Action<object> action { get; set; }
        public Func<bool> canExecute { get; set; }
   
        public bool CanExecute(object parameter)
        {
            return canExecute.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
