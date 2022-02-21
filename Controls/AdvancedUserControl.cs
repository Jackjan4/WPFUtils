using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace De.JanRoslan.WPFUtils.Controls
{
    /// <summary>
    ///     ''' Superclass of UserControl that extends UserControls with the functionality of providing a bindable command that is raised when the specific UserControl handles its main action (like a Button Click for example).
    ///     ''' This class is based on the source code of WPF ButtonBase.
    ///     ''' This class cannot be abstract due to it being instantiated by WPF.
    ///     ''' </summary>
    public class AdvancedUserControl :  UserControl, ICommandSource
    {

        // ' Properties


        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(AdvancedUserControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnCommandChanged)));

        public IInputElement CommandTarget
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Object CommandParameter
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        private bool _canExecute;

        private bool CanExecute
        {
            get => _canExecute;
            set
            {
                _canExecute = value;
                IsEnabled = value;
            }
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        public AdvancedUserControl()
        {
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="oldCommand"></param>
        ///         ''' <param name="newCommand"></param>
        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand!= null)
                UnhookCommand(oldCommand);
            if (newCommand!= null)
                HookCommand(newCommand);
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="cmd"></param>
        private void UnhookCommand(ICommand cmd)
        {
            CanExecuteChangedEventManager.RemoveHandler(cmd, OnCanExecuteChanged);
            UpdateCanExecute();
        }


        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="cmd"></param>
        private void HookCommand(ICommand cmd)
        {
            CanExecuteChangedEventManager.AddHandler(cmd, OnCanExecuteChanged);
            UpdateCanExecute();
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="sender"></param>
        ///         ''' <param name="e"></param>
        private void OnCanExecuteChanged(Object sender, EventArgs e)
        {
            UpdateCanExecute();
        }



        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        private void UpdateCanExecute()
        {
            if (Command!= null)
                CanExecute = Command.CanExecute(null/* TODO Change to default(_) if this Is Not a reference type */); // TODO: Parameter
            else
                CanExecute = true;
        }


        // ' Shared methods


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdvancedUserControl auc = (AdvancedUserControl)d;
            auc.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }
    }
}
