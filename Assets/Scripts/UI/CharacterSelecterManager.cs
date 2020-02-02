using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class CharacterSelecterManager : MonoBehaviour, IUIInteractable
{
    public CharacterSelector[] selectors;
    public Sprite[] images;
    public GameObject[] prefabs;
    public Color[] colors;
    public GameObject[] bases;

    private bool starting = false;

    private List<PlayerInput> players;

    public AudioClip confirm, back, join, select, gameStart;
    private AudioSource[] audioSources;

    private void Start()
    {
        players = new List<PlayerInput>();
        audioSources = GetComponents<AudioSource>();
    }

    private void Play(AudioClip audioClip)
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.clip = audioClip;
                source.Play();
            }
        }
    }

    public void Interact(Type type, int index)
    {
        switch (type)
        {
            case Type.Left:
            case Type.Right:
                Play(select);
                break;
            case Type.Accept:
                Play(join);
                break;
            default:
                break;
        }
        selectors[index].gameObject.SetActive(true);
        selectors[index].Interact(type, index);
    }

    public void Register(PlayerInput playerInput)
    {
        players.Add(playerInput);
    }

    private IEnumerator StartGame()
    {
        Play(gameStart);
        yield return new WaitForSeconds(2);
        UIManager.Instance.Play();
    }

    private void Update()
    {
        if (starting)
            return;
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
        if (numberActive <= 1 || numberActive != numberReady)
        {
            return;
        }
        else
        {
            for (int i = 0; i < selectors.Length; i++)
            {
                if (!selectors[i].gameObject.activeInHierarchy)
                    break;
                Instantiate(prefabs[selectors[i].Selection], players[i].transform);
                players[i].SwitchCurrentActionMap("Player");
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            Character2D character = players[i].GetComponentInChildren<Character2D>();
            character.Color = colors[i];
            bases[i].SetActive(true);
            bases[i].GetComponentInChildren<WaifuBase>().robotType = character.robotType;
            bases[i].GetComponentInChildren<Light2D>().color = colors[i];
        }
        starting = true;
        StartCoroutine(StartGame());
    }
}