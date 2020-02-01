using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaifuBase : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer WaifuHead, WaifuBod, WaifuWings, WaifuArm1, WaifuArm2, WaifuLeg1, WaifuLeg2;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.GetComponent<WaifuHead>() && WaifuHead.sprite == null)
        {
            WaifuHead.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuBod waifubod) && WaifuBod.sprite == null)
        {
            WaifuBod.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuArm waifuArm1) && WaifuArm1.sprite == null)
        {
            WaifuArm1.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuArm1 waifuArm2) && WaifuArm2.sprite == null)
        {
            WaifuArm2.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuLeg waifuLeg1) && WaifuLeg1.sprite == null)
        {
            WaifuLeg1.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuLeg1 waifuLeg2) && WaifuLeg2.sprite == null)
        {
            WaifuLeg2.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }
        if (collision.TryGetComponent(out WaifuWings waifuWings) && WaifuWings.sprite == null)
        {
            WaifuWings.sprite = collision.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
            CheckWin();
        }

    }

    private void Update()
    {
    }

    void CheckWin()
    {
        if (WaifuHead.sprite != null && WaifuHead.sprite != null && WaifuArm1.sprite != null && WaifuArm2.sprite != null && WaifuLeg1.sprite != null && WaifuLeg2.sprite != null)
            WinHandler.Instance.PlayerWon(gameObject);
    }
}
