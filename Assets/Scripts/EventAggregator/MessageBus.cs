using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MessageBus : IMessageBus 
{
    private Dictionary<Type, List<Action<IMessage>>> subscribers;

    private static MessageBus instance;

    public static MessageBus Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MessageBus();
            }

            return instance;
        }
    }

    private MessageBus()
    {
        subscribers = new Dictionary<Type, List<Action<IMessage>>>();
    }

    public void Subscribe<T>(Action<IMessage> action) where T : IMessage
    {
        if (!subscribers.ContainsKey(typeof(T)))
        {
            subscribers.Add(typeof(T), new List<Action<IMessage>>());
        }

        subscribers[typeof(T)].Add(action);
    }

    public void Unsubscribe<T>(Action<IMessage> action) where T : IMessage
    {
        if (!subscribers.ContainsKey(typeof(T)))
        {
            return;
        }

        subscribers[typeof(T)].Remove(action);
    }

    public void SendMessage<T>(T message) where T : IMessage
    {
        if (!subscribers.ContainsKey(typeof(T)))
        {
            return;
        }

        subscribers[typeof(T)].ForEach(a => a.Invoke(message));
    }
}
