using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MainRoot {

    Play2Result result;
    RaderChart rader;
    List<float> parameter = new List<float>();

    void Start()
    {
        rader = gameObject.GetComponent<RaderChart>();
        result = GameObject.Find("carrier").GetComponent<Carrier>().GetResult();
        parameter.Add(result.Score / (TheoryScore+TheoryBonus));
        parameter.Add(result.SkinScore / TheoryScore);
        parameter.Add((float)result.MaxCombo / result.AllTarget);
        foreach (float value in parameter) Debug.Log("parameter:" + value);
        StartCoroutine("ResultManager");
    }

    IEnumerator ResultManager()
    {
        yield return rader.StartCoroutine("Rader",parameter);
        Debug.Log("Input any key, Load SelectMusic");
        while (true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("SelectMusic");
                yield break;
            }
            yield return new WaitForSeconds(fps);
        }
    }
}
