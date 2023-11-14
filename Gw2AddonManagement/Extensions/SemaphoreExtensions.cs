using Nito.Disposables;

namespace Gw2AddonManagement.Extensions;

public static class SemaphoreExtensions
{
    public static IDisposable WaitDisposable(this SemaphoreSlim semaphoreSlim)
    {
        semaphoreSlim.Wait();
        return Disposable.Create(() => semaphoreSlim.Release());
    }

    public static async Task<IDisposable> WaitAsyncDisposable(this SemaphoreSlim semaphoreSlim)
    {
        await semaphoreSlim.WaitAsync();
        return Disposable.Create(() => semaphoreSlim.Release());
    }
}