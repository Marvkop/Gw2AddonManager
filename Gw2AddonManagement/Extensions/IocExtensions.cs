namespace Gw2AddonManagement.Extensions;

public static class IocExtensions
{
    public static void AddService<T>(this ServiceContainer container)
        where T : class, new()
    {
        container.AddService(typeof(T), (_, _) => new T());
    }

    public static void InitService<T>(this Ioc ioc, out T field)
        where T : class
    {
        field = ioc.GetRequiredService<T>();
    }
}