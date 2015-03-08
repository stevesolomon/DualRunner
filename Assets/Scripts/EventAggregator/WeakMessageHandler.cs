using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

class WeakMessageHandler
{
    private readonly WeakReference weakReference;
    private readonly Dictionary<Type, MethodInfo> supportedHandlers;

    public bool IsDead { get { return weakReference.Target == null; } }

    public WeakMessageHandler(object handler)
    {
        weakReference = new WeakReference(handler);
        supportedHandlers = new Dictionary<Type, MethodInfo>();

        var interfaces = handler.GetType().GetInterfaces()
            .Where(i => typeof(IListener).IsAssignableFrom(i) && i.IsGenericType);

        foreach (var iface in interfaces)
        {
            var type = iface.GetGenericArguments()[0];
            var method = iface.GetMethod("HandleMessage");
            supportedHandlers[type] = method;
        }
    }

    public bool Matches(object instance)
    {
        return weakReference.Target == instance;
    }

    public bool HandleMessage(Type messageType, IMessage message)
    {
        var target = weakReference.Target;

        if (target == null)
        {
            return false;
        }

        foreach (var kvPair in supportedHandlers)
        {
            if (kvPair.Key.IsAssignableFrom(messageType))
            {
                var result = kvPair.Value.Invoke(target, new[] { message });
            }
        }

        return true;
    }

    public bool CanHandleMessage<T>()
    {
        return supportedHandlers.Any(kvPair => kvPair.Key.IsAssignableFrom(typeof(T)));
    }
}