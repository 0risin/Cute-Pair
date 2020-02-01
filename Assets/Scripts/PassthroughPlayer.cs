using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassthroughPlayer : MonoBehaviour
{
    public Character2D character2D;

    private void OnMove(InputValue value)
    {
        if (character2D != null)
            character2D.OnMove(value);
    }
    private void OnAttackSide()
    {
        if (character2D != null)
            character2D.OnAttackSide();
    }
    private void OnAttackUp()
    {
        if (character2D != null)
            character2D.OnAttackUp();
    }

    private void OnAttackDown()
    {
        if (character2D != null)
            character2D.OnAttackDown();
    }
    private void OnGrab()
    {
        if (character2D != null)
            character2D.OnGrab();
    }

    void OnJump()
    {
        if (character2D != null)
            character2D.OnJump();
    }
}
