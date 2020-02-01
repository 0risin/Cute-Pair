using System.Collections;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    public Vector2 range;
    public float timeForOneCheckpoint;
    public AnimationCurve animationCurve;

    private void Start()
    {
        StartCoroutine(GetEnumerator());
    }

    private IEnumerator GetEnumerator()
    {
        transform.localPosition = new Vector3(-range.x, Random.Range(-range.y, range.y), 0.1f);
        while (true)
        {
            Vector3 temp = transform.localPosition;
            temp.z = 1;
            transform.localPosition = temp;

            Vector3 currentPos = transform.localPosition;
            Vector3 right = new Vector3(range.x, Random.Range(-range.y, range.y), 0.1f);
            Vector3 dif = right - currentPos;

            float timer = 0, percent = 0;
            while (percent < 1)
            {
                timer += Time.deltaTime;
                percent = timer / timeForOneCheckpoint;
                if (percent > 1)
                    percent = 1;
                transform.localPosition = dif * animationCurve.Evaluate(percent) + currentPos;
                yield return null;
            }
            temp = transform.localPosition;
            temp.z = -1;
            transform.localPosition = temp;

            currentPos = transform.localPosition;
            Vector3 left = new Vector3(-range.x, Random.Range(-range.y, range.y), 0.1f);
            dif = left - currentPos;
            timer = 0;
            percent = 0;
            while (percent < 1)
            {
                timer += Time.deltaTime;
                percent = timer / timeForOneCheckpoint;
                if (percent > 1)
                    percent = 1;
                transform.localPosition = dif * animationCurve.Evaluate(percent) + currentPos;
                yield return null;
            }
        }
    }

}
