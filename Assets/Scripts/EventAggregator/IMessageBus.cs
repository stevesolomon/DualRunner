using UnityEngine;
using System.Collections;
using System;

public interface IMessageBus {

    void Subscribe<T>(IListener<T> subscriber) where T : IMessage;
    //void Unsubscribe<T>(Action<IMessage> action) where T : IMessage;

    void SendMessage<T>(T message) where T : IMessage;
}
