using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaifuBase : MonoBehaviour
{
    [SerializeField]
    Sprite WaifuHead, WaifuBod, WaifuArm1, WaifuArm2, WaifuLeg1, WaifuLeg2;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WaifuHead>() && WaifuHead == null)
        {
            WaifuHead = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }
        if (collision.GetComponent<WaifuBod>() && WaifuBod == null)
        {
            WaifuBod = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }
        if (collision.GetComponent<WaifuArm>() && WaifuArm1 == null)
        {
            WaifuArm1 = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }
        if (collision.GetComponent<WaifuArm>() && WaifuArm2 == null)
        {
            WaifuArm2 = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }
        if (collision.GetComponent<WaifuLeg>() && WaifuLeg1 == null)
        {
            WaifuLeg1 = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }
        if (collision.GetComponent<WaifuLeg>() && WaifuLeg2 == null)
        {
            WaifuLeg2 = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision);
            CheckWin();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            WinHandler.Instance.PlayerWon(gameObject);
    }

    void CheckWin()
    {
        if (WaifuHead != null && WaifuHead != null && WaifuArm1 != null && WaifuArm2 != null && WaifuLeg1 != null && WaifuLeg2 != null)
            WinHandler.Instance.PlayerWon(gameObject);
    }
}
