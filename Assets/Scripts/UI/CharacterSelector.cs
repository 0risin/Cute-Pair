using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour, IUIInteractable
{
    private CharacterSelecterManager characterSelecter;
    public int Selection { get; private set; }
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
                Selection++;
                if (Selection == characterSelecter.images.Length)
                    Selection = 0;
                image.sprite = characterSelecter.images[Selection];
                break;
            case Type.Right:
                Selection--;
                if (Selection == -1)
                    Selection = characterSelecter.images.Length - 1;
                image.sprite = characterSelecter.images[Selection];
                break;
            case Type.Accept:
                Ready = true;
                break;
            default:
                break;
        }
    }

}
