using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughHelper : MonoBehaviour
{
    public Vector2 add;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D body))
        {
            body.position += add;
            Vector3 velocity = body.velocity;
            velocity.y = 0;
            body.velocity = velocity;
        }
    }
}
