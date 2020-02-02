using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    BoxCollider2D Hitbox;
    List<Collider2D> alreadyHit;
    Character2D self;
    public enum State
    {
        Grabing, Pushing, Throwing
    }

    bool ignoreNextPush;

    private void Start()
    {
        Hitbox = GetComponent<BoxCollider2D>();
        alreadyHit = new List<Collider2D>();
        self = transform.parent.GetComponent<Character2D>();
    }

    public void push(Vector2 pushAngle, bool facingRight, State state)
    {
        if (ignoreNextPush)
            return;

        if (!facingRight)
            pushAngle.x *= -1;

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        position = facingRight ? position + Hitbox.offset : position - Hitbox.offset;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, Hitbox.size, 0);

        if (state == State.Throwing)
        {
            self.Grabbed.GetComponent<Rigidbody2D>().AddForce(pushAngle, ForceMode2D.Impulse);
            self.Grabbed = null;
            ignoreNextPush = true;
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (alreadyHit.Contains(colliders[i]))
                continue;

            if (colliders[i].TryGetComponent(out Item item))
            {
                if (state == State.Grabing && self.Grabbed == null)
                {
                    self.Grabbed = item.gameObject;
                    ignoreNextPush = true;
                    return;
                }
                else if (state == State.Pushing)
                {
                    item.GetComponent<Rigidbody2D>().AddForce(pushAngle, ForceMode2D.Impulse);
                }
                alreadyHit.Add(colliders[i]);
            }
            else if (colliders[i].TryGetComponent(out Character2D character2D) && colliders[i].gameObject != self.gameObject)
            {
                character2D.GetComponent<Rigidbody2D>().AddForce(pushAngle * 0.5f, ForceMode2D.Impulse);
                alreadyHit.Add(colliders[i]);
                character2D.Stun();
                // stun
            }
        }
    }

    public void ClearList()
    {
        alreadyHit.Clear();
        ignoreNextPush = false;
    }
}


