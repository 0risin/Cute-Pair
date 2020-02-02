using UnityEngine;
using UnityEngine.UI;

public class PlayerWinsUI : MonoBehaviour
{
    [System.Serializable]
    public struct Finder
    {
        public Sprite sprite;
        public RobotType robotType;
    }

    public Finder[] finder;
    public Image image;

    public void Display(RobotType type)
    {
        for (int i = 0; i < finder.Length; i++)
        {
            if (finder[i].robotType == type)
            {
                image.sprite = finder[i].sprite;
                return;
            }
        }
    }

}
