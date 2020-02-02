using UnityEngine;

public class Puller : MonoBehaviour
{
    private BoxCollider2D parentCollider;

    private void Start()
    {
        parentCollider = transform.parent.Find("Hitbox").GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigidBody = collision.attachedRigidbody;
        if (rigidBody != null)
        {
            bool ignore = collision.attachedRigidbody.velocity.y > 0;
            Physics2D.IgnoreCollision(collision, parentCollider, ignore);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, parentCollider, false);
    }

}
