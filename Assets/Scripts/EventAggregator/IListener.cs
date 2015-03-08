using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IListener { }

public interface IListener<T> : IListener where T : IMessage
{
    void HandleMessage(T message);
}

