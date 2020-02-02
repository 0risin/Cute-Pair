using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float destroyTime = 20;
    float countdown;
    bool dropped = true;
    private void Start()
    {
        gameObject.layer = 9;
        InnerStart();
        countdown = destroyTime;
    }

    public virtual void InnerStart()
    {
    }

    private void Update()
    {
        handleDestruction(0, dropped);
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
}

