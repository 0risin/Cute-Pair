using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    public static WinHandler Instance { get; private set; }
    public string sceneToLoadAfterWin;

    private void Start()
    {
        Instance = this;
    }

    public void PlayerWon(GameObject waifu, RobotType robotType)
    {
        StartCoroutine(ZoomToWaifu(waifu, robotType));
    }

    private IEnumerator ZoomToWaifu(GameObject waifu, RobotType type)
    {
        Camera camera = Camera.main;
        Vector3 oldPosition = camera.transform.position;
        Vector3 newPosition = waifu.transform.position;
        newPosition.z = oldPosition.z;
        float oldSize = camera.orthographicSize;
        float newSize = oldSize * 0.5f;

        float timeToZoom = 3, timer = 0, percent = 0;
        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / timeToZoom;
            if (percent > 1)
                percent = 1;
            camera.transform.position = Vector3.Lerp(oldPosition, newPosition, percent);
            camera.orthographicSize = Mathf.Lerp(oldSize, newSize, percent);
            yield return null;
        }
        UIManager.Instance.PlayerWon(type);
        ItemSpawner.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneToLoadAfterWin);
    }


}
