using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
 
public class StartGame : MonoBehaviour
{
    public GameObject UIHowTo;
    public Button fristButton;
    public EventSystem es;

    // Start is called before the first frame update
    void Start()
    {

       // es.SetSelectedGameObject(null);
       // es.SetSelectedGameObject(fristButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            UIHowTo.SetActive(false);
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }
    public void DisplayHowTo()
    {
        UIHowTo.SetActive(true);
    }

    public void QuitOnClick()
    {
        Application.Quit(3);
    }

}
