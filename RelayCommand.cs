using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.JanRoslan.WPFUtils {
    public class RelayCommand : ICommand {


        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> methodToExecute;

        private Func<bool> canExecuteEvaluator;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="methodToExecute"></param>
        /// <param name="canExecuteEvaluator"></param>
        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator) {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="methodToExecute"></param>
        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null) {
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) {
            if (this.canExecuteEvaluator == null) {
                return true;
            } else {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) {
            this.methodToExecute.Invoke(parameter);
        }
    }
}
