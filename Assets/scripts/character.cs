using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class controller: MonoBehaviour
{
    private float speed=5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSceneAfterDelay(7f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }
    IEnumerator StartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Start");
    }
}
