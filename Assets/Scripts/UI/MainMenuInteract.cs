using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuInteract : MonoBehaviour, IUIInteractable
{
    public Button[] UIClickable;
    private int at;

    private void Start()
    {
        at = UIClickable.Length - 1;
    }

    public void Interact(Type type, int index)
    {
        if (index == 0)
        {
            switch (type)
            {
                case Type.Up:
                    at++;
                    if (at == UIClickable.Length)
                        at = 0;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(UIClickable[at].gameObject);
                    break;
                case Type.Down:
                    at--;
                    if (at == -1)
                        at = UIClickable.Length - 1;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(UIClickable[at].gameObject);
                    break;
                case Type.Accept:
                    UIClickable[at].onClick.Invoke();
                    break;
                default:
                    break;
            }
        }
    }

}
