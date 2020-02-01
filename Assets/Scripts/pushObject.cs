using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    BoxCollider2D Hitbox;
    List<Collider2D> alreadyHit;

    private void Start()
    {
        Hitbox = GetComponent<BoxCollider2D>();
        alreadyHit = new List<Collider2D>();
    }

    public void push(Vector2 pushAngle, bool facingRight)
    {
        if (!facingRight)
            pushAngle.x = pushAngle.x * -1;

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        position = facingRight ? position + Hitbox.offset : position - Hitbox.offset;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, Hitbox.size, 0);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (alreadyHit.Contains(colliders[i]))
                continue;

            if (colliders[i].TryGetComponent(out Item item))
            {
                item.GetComponent<Rigidbody2D>().AddForce(pushAngle, ForceMode2D.Impulse);
                alreadyHit.Add(colliders[i]);
            }
            else if (colliders[i].TryGetComponent(out Character2D character2D))
            {
                character2D.GetComponent<Rigidbody2D>().AddForce(pushAngle * 0.5f, ForceMode2D.Impulse );
                alreadyHit.Add(colliders[i]);
                // stun
            }
        }
    }

    public void ClearList() => alreadyHit.Clear();
}


