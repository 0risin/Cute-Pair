using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyTime = 20;
    float countdown;
    bool dropped = true;
    private Collider2D thrownCollider;
    private Collider2D ownCollider;

    private void Start()
    {
        gameObject.layer = 9;
        InnerStart();
        countdown = destroyTime;
        thrownCollider = null;
        ownCollider = GetComponent<Collider2D>();
    }

    public virtual void InnerStart()
    {
    }

    private void Update()
    {
        handleDestruction(0, dropped);
        if (thrownCollider != null)
        {
            Collider2D[] colliders = new Collider2D[10];
            ContactFilter2D contactFilter = new ContactFilter2D();
            int colliderCount = ownCollider.OverlapCollider(contactFilter, colliders);
            for (int i = 0; i < colliderCount; i++)
            {
                if (colliders[i] == thrownCollider)
                {
                    Physics2D.IgnoreCollision(thrownCollider, ownCollider, false);
                    thrownCollider = null;
                }
            }
        }
    }
    public void handleDestruction(float t, bool dropped)
    {
        this.dropped = dropped;
        countdown += t;
        if (this.dropped)
            countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void thrown(Collider2D coll)
    {
        thrownCollider = coll;
        Physics2D.IgnoreCollision(coll, ownCollider, true);
    }
}

