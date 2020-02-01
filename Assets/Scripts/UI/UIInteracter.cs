using UnityEngine;
using UnityEngine.InputSystem;

public class UIInteracter : MonoBehaviour
{
    public int controlIndex;

    private Type previousType;

    private void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        UIManager.Instance.Register(playerInput);
        gameObject.GetComponent<UIInteracter>().controlIndex = playerInput.playerIndex;
    }

    void OnNavigate(InputValue inputValue)
    {
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
