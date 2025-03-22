using System.Windows.Input;

namespace DepartureBoard.Desktop.Commands;

public class RelayCommand(Func<Task> execute, Func<bool>? canExecute = null) : ICommand
{
    private readonly Func<Task>? _execute = execute;
    private readonly Func<bool>? _canExecute = canExecute;
    
    public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

    public async void Execute(object? parameter)
    {
        if (_execute != null)
        {
            await _execute();
        }
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}