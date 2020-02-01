using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelecterManager : MonoBehaviour, IUIInteractable
{
    public CharacterSelector[] selectors;
    public Sprite[] images;
    public GameObject[] prefabs;
    private List<PlayerInput> players;

    private void Start()
    {
        players = new List<PlayerInput>();
    }

    public void Interact(Type type, int index)
    {
        selectors[index].gameObject.SetActive(true);
        selectors[index].Interact(type, index);
    }

    public void Register(PlayerInput playerInput)
    {
        players.Add(playerInput);
    }

    private void Update()
    {
        int numberActive = 0, numberReady = 0;
        for (int i = 0; i < selectors.Length; i++)
        {
            if (selectors[i].gameObject.activeInHierarchy)
            {
                numberActive++;
                if (selectors[i].Ready)
                    numberReady++;
            }
        }
        if (numberActive > 1 && numberActive == numberReady)
        {
            for (int i = 0; i < selectors.Length; i++)
            {
                Instantiate(prefabs[i], players[i].transform);
            }
        }
    }
}