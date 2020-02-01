using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] Vector2[] checkPoints;
    [SerializeField] private float timeForCheckPoint;
    [SerializeField] private float waitOnCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
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

}
