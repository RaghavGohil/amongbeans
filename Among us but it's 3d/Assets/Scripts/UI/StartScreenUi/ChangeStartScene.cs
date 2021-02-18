using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeStartScene : MonoBehaviour
{
    // Start is called before the first frame update

    private float startTime;

    void Start()
    {
        startTime = 5f;

        StartCoroutine(StartWait(startTime));

    }

    // Update is called once per frame
    IEnumerator StartWait(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene("MainStartScreen");

    }
}
