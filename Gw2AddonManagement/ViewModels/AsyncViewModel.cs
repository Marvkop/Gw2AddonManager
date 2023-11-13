using Gw2AddonManagement.Messages;

namespace Gw2AddonManagement.ViewModels;

public partial class AsyncViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    protected IDisposable StartLoading()
    {
        Increment();

        return new DisposableAction(Decrement);
    }

    private void Increment()
    {
        IsBusy = true;
        WeakReferenceMessenger.Default.Send<IsBusyMessage>();
    }

    private void Decrement()
    {
        WeakReferenceMessenger.Default.Send<IsDoneMessage>();
        IsBusy = false;
    }

    private class DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action) => _action = action;

        public void Dispose() => _action();
    }
}