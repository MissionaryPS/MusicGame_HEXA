using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MainRoot {

	// Use this for initialization
	void Start () {
        StartCoroutine("enumerator");
	}

    IEnumerator enumerator()
    {
        while (true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("SelectMusic");
            }
            yield return new WaitForSeconds(fps);
        }
    }
}
