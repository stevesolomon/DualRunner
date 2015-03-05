using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseMessage : IMessage
{
    public BaseMessage() 
    { 
        Timestamp = DateTime.Now;
    }

    public BaseMessage(GameObject source)
        : this()
    {
        this.Source = source;
    }

    public GameObject Source
    {
        get;
        protected set;
    }


    public DateTime Timestamp
    {
        get;
        protected set;
    }
}