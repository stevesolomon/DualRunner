using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IMessage
{
    GameObject Source { get; }
    DateTime Timestamp { get; }
}
