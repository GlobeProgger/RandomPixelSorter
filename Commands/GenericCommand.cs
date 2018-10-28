using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RandomPixelSorter.Commands
{
    /// <summary>
    /// Represents a generic command
    /// </summary>
    public class GenericCommand : ICommand
    {
        /// <summary>
        /// Initializes a GenericCommand
        /// </summary>
        /// <param name="executeAction">The action to perform when the commands Execute function is called</param>
        /// <param name="canExecute">The function to call when the commands CanExecute function is called</param>
        public GenericCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Invokes the CanExecuteChanged event
        /// </summary>
        /// <param name="sender">The sender that calls the method </param>
        /// <param name="propertyName">The name of the property that changed</param>
        public void OnCanExecuteChanged(object sender, [CallerMemberName]string propertyName = null) =>
            CanExecuteChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _executeAction(parameter);
        #endregion

        #region Implementation
        private Func<object, bool> _canExecute;
        private Action<object> _executeAction;
        #endregion
    }
}
