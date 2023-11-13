namespace Gw2AddonManagement.Extensions;

public static class TaskExtensions
{
    public static void WaitNotBlocking(Task task)
    {
        Task.Run(async () => { await task; }).Wait();
    }
}