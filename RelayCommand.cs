using System;
using System.Windows.Input;

namespace De.JanRoslan.WPFUtils {


    /// <summary>
    /// A RelayCommand that can be used for delegating GUI commands to the ViewModel.
    /// (If Prism is used, its 'DelegateCommand' should be favored instead of this one. )
    /// </summary>
    public class RelayCommand : ICommand {


        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _methodToExecute;

        private readonly Func<bool> _canExecuteEvaluator;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="methodToExecute"></param>
        /// <param name="canExecuteEvaluator"></param>
        public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator) {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
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
            if (_canExecuteEvaluator == null) {
                return true;
            }

            bool result = _canExecuteEvaluator.Invoke();
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) {
            _methodToExecute.Invoke(parameter);
        }
    }
}
