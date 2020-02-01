﻿using UnityEngine;
using UnityEngine.InputSystem;

public class UIInteracter : MonoBehaviour
{
    public int controlIndex;

    private Type previousType;

    void OnNavigate(InputValue inputValue)
    {
        print("Som");
        Type? type = null;
        Vector2 direction = inputValue.Get<Vector2>();
        if (direction.x > 0.3f)
            type = Type.Right;
        else if (direction.x < -0.3f)
            type = Type.Left;
        else if (direction.y > 0.3f)
            type = Type.Up;
        else if (direction.y < -0.3f)
            type = Type.Down;
        else
            previousType = Type.Accept;
        if (type == null || type.Value == previousType)
            return;
        previousType = type.Value;
        UIManager.Instance.Interact(type.Value, controlIndex);
    }

    void OnSubmit()
    {
        UIManager.Instance.Interact(Type.Accept, controlIndex);
    }

    void OnCancel()
    {
        UIManager.Instance.Interact(Type.Cancel, controlIndex);
    }
}
