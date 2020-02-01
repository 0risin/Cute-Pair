using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = 9;
        InnerStart();
    }

    public virtual void InnerStart()
    {

    }
}

