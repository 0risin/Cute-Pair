using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stayer : MonoBehaviour
{
    private List<GameObject> toMove;
    private Vector3 prevPoint;

    private void Start()
    {
        toMove = new List<GameObject>();
    }

    private void Update()
    {
        Vector3 dif = prevPoint - transform.position;
        prevPoint = transform.position;
        toMove.ForEach(x => x.transform.position -= dif);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        toMove.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        toMove.Remove(collision.gameObject);
    }
}
