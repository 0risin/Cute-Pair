using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] Vector2[] checkPoints;
    [SerializeField] private float timeForCheckPoint;
    [SerializeField] private float waitOnCheckPoint;

    private BoxCollider2D[] colliders;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<BoxCollider2D>();
        StartCoroutine(GetEnumerator());
    }

    private IEnumerator GetEnumerator()
    {
        while (true)
        {
            for (int i = 0; i < checkPoints.Length; i++)
            {
                Vector2 from = checkPoints[i];
                Vector2 to = checkPoints[i + 1 == checkPoints.Length ? 0 : i + 1];
                Vector2 dif = to - from;

                float timer = 0, percent = 0;

                while (percent < 1)
                {
                    timer += Time.deltaTime;
                    percent = timer / timeForCheckPoint;
                    if (percent > 1)
                        percent = 1;
                    transform.position = dif * animationCurve.Evaluate(percent) + from;
                    yield return null;
                }
                yield return new WaitForSeconds(waitOnCheckPoint);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool ignore = collision.attachedRigidbody.velocity.y > 0;
        for (int i = 0; i < colliders.Length; i++)
        {
            Physics2D.IgnoreCollision(collision, colliders[i], ignore);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            Physics2D.IgnoreCollision(collision, colliders[i], false);
        }
    }


}
