using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MessageBus : IMessageBus 
{
    private readonly List<WeakMessageHandler> handlers;

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
        handlers = new List<WeakMessageHandler>();
    }

    public void Subscribe<T>(IListener<T> listener) where T : IMessage
    {
        if (listener == null)
        {
            return;
        }

        lock (handlers)
        {
            if (!handlers.Any(h => h.Matches(listener)))
            {
                handlers.Add(new WeakMessageHandler(listener));
            } 
        }
    }

    public void SendMessage<T>(T message) where T : IMessage
    {
        if (message == null)
        {
            return;
        }

        WeakMessageHandler[] toNotify;

        lock (handlers)
        {
            toNotify = handlers.ToArray();
        }

        var messageType = typeof(T);

        var deadHandlers = handlers.Where(h => !h.HandleMessage(messageType, message)).ToList();

        if (deadHandlers.Any())
        {
            foreach (var deadHandler in deadHandlers)
            {
                handlers.Remove(deadHandler);
            }
        } 
    }

}
