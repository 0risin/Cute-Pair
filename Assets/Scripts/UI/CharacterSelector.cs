using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour, IUIInteractable
{
    private CharacterSelecterManager characterSelecter;
    private int selection;
    private Image image;
    public bool Ready { get; private set; }

    public void Interact(Type type, int index)
    {
        if (Ready)
            return;
        image = GetComponent<Image>();
        characterSelecter = transform.parent.GetComponent<CharacterSelecterManager>();
        switch (type)
        {
            case Type.Left:
                selection++;
                if (selection == characterSelecter.images.Length)
                    selection = 0;
                image.sprite = characterSelecter.images[selection];
                break;
            case Type.Right:
                selection--;
                if (selection == -1)
                    selection = characterSelecter.images.Length - 1;
                image.sprite = characterSelecter.images[selection];
                break;
            case Type.Accept:
                Ready = true;
                break;
            default:
                break;
        }
    }

}
