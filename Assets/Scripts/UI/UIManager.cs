using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIInteractable
{
    public Image playerWon, characterSelect, background, selectImage;
    private IUIInteractable currentInteractable;

    public static UIManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        ItemSpawner.Instance.gameObject.SetActive(false);
        currentInteractable = characterSelect.gameObject.GetComponent<IUIInteractable>();
        characterSelect.gameObject.SetActive(true);
        background = transform.Find("Image").GetComponent<Image>();
        selectImage.gameObject.SetActive(true);
    }

    public void Play()
    {
        GetComponent<AudioSource>().Play();
        characterSelect.gameObject.SetActive(false);
        currentInteractable = null;
        background.gameObject.SetActive(false);
        ItemSpawner.Instance.gameObject.SetActive(true);
    }  

    public void Register(PlayerInput playerInput)
    {
        characterSelect.GetComponent<CharacterSelecterManager>().Register(playerInput);
        selectImage.gameObject.SetActive(false);
    }

    public void PlayerWon(RobotType type)
    {
        playerWon.gameObject.SetActive(true);
        currentInteractable = playerWon.gameObject.GetComponent<IUIInteractable>();
        GetComponentInChildren<PlayerWinsUI>().Display(type);
    }

    public void Interact(Type type, int index)
    {
        if (currentInteractable != null)
            currentInteractable.Interact(type, index);
    }
}
