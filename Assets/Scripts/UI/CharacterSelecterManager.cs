using UnityEngine;

public class CharacterSelecterManager : MonoBehaviour, IUIInteractable
{
    public CharacterSelector[] selectors;
    public Sprite[] images;
    public GameObject[] prefabs;

    public void Interact(Type type, int index)
    {
        selectors[index].gameObject.SetActive(true);
        selectors[index].Interact(type, index);
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

            }
        }
    }
}