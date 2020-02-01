using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIInteractable
{
    public Image mainMenuCanvas, playerSelect, playerWon;
    private IUIInteractable currentInteractable;

    public static UIManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        mainMenuCanvas.gameObject.SetActive(true);
        currentInteractable = mainMenuCanvas.gameObject.GetComponent<IUIInteractable>();
    }

    public void SelectPlayFromMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        playerSelect.gameObject.SetActive(true);
        currentInteractable = playerSelect.gameObject.GetComponent<IUIInteractable>();
    }

    public void Play()
    {
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
