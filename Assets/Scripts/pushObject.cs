using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    BoxCollider2D Hitbox;

    private void Start()
    {
        Hitbox = GetComponent<BoxCollider2D>();

    }

    public void push(Vector2 pushAngle)
    {

        Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + Hitbox.offset, Hitbox.size, 0);
    }

}


