using System;
using UnityEngine;

namespace Input
{
    public interface IInput
    {
        event Action<Vector2> ClickDown;
        event Action<Vector2> ClickUp;
        event Action<Vector2> Drag;
    }
}