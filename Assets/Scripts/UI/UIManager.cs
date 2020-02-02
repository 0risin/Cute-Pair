using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIInteractable
{
    public Image playerWon, characterSelect, background;
    private IUIInteractable currentInteractable;

    public static UIManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        currentInteractable = characterSelect.gameObject.GetComponent<IUIInteractable>();
        characterSelect.gameObject.SetActive(true);
        background = transform.Find("Image").GetComponent<Image>();
    }

    public void Play()
    {
        characterSelect.gameObject.SetActive(false);
        currentInteractable = null;
        background.gameObject.SetActive(false);
    }  

    public void Register(PlayerInput playerInput)
    {
        characterSelect.GetComponent<CharacterSelecterManager>().Register(playerInput);
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
