using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawler.ViewModels
{
    internal class CrawlCommand : ICommand
    {
        private bool _canExecute = true;
        private readonly Func<Task> _command;

        public event EventHandler CanExecuteChanged;

        public CrawlCommand(Func<Task> command)
        {
            _command = command;
        }

        public Task ExecuteAsync(object parameter)
        {
            return _command();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                if (_canExecute == value) return;
                _canExecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
