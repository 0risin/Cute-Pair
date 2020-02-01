using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image mainMenuCanvas, playerSelect, playerWon;

    public static UIManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void SelectPlayFromMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        playerSelect.gameObject.SetActive(true);
    }

    public void PlayerWon()
    {
        playerWon.gameObject.SetActive(true);
    }

}
