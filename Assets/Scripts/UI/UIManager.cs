using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIInteractable
{
    public Image playerWon, characterSelect;
    private IUIInteractable currentInteractable;

    public static UIManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        currentInteractable = characterSelect.gameObject.GetComponent<IUIInteractable>();
        characterSelect.gameObject.SetActive(true);
    }

    public void Play()
    {
        characterSelect.gameObject.SetActive(false);
        currentInteractable = null;
    }

    public void PlayerWon()
    {
        playerWon.gameObject.SetActive(true);
        currentInteractable = playerWon.gameObject.GetComponent<IUIInteractable>();
    }

    public void Interact(Type type, int index)
    {
        if (currentInteractable != null)
            currentInteractable.Interact(type, index);
    }
}
