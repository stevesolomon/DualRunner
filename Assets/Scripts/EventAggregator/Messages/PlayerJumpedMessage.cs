using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerJumpedMessage : BaseMessage
{
    public Vector2 Position
    {
        get;
        private set;
    }
}
