using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<System.Type, object> _services = new Dictionary<System.Type, object>();

    public static void Register<T>(T service)
    {
        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (_services.TryGetValue(typeof(T), out object service))
        {
            return (T)service;
        }

        return default;
    }

    public static bool TryGet<T>(out T service)
    {
        if (_services.TryGetValue(typeof(T), out object s))
        {
            service = (T)s;
            return true;
        }

        service = default;
        return false;
    }
}