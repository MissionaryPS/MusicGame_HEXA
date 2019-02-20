using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MainRoot {

    Play2Result result;
    RaderChart rader;
    List<float> parameter = new List<float>();
    Text BreakdownView;
    string[] judgeName = new string[4] { "miss", "perfect", "great", "bad" };

    void Start()
    {
        UpdateInput();
        //リザルト取得
        result = GameObject.Find("carrier").GetComponent<Carrier>().GetResult();
        
        BreakdownView = GameObject.Find("ResultBreakdown").GetComponent<Text>();
        BreakdownView.text = "Result\n\n";
        for (int i = 1; i < 4; i++)
        {
            BreakdownView.text += judgeName[i] + ":" + result.breakdown[i].ToString() + "\n";
        }
        BreakdownView.text += judgeName[0] + ":" + result.breakdown[0].ToString() + "\n";
        BreakdownView.text += "\n";
        BreakdownView.text += "Combo:" + result.MaxCombo + "/" + result.AllTarget;

        parameter.Add(result.Score / (TheoryScore + TheoryBonus));
        parameter.Add(result.SkinScore / TheoryScore);
        parameter.Add(result.BonusScore / TheoryBonus);
        //for (int i = 0; i < 3; i++) parameter.Add(1.0f);
        foreach (float value in parameter) Debug.Log("parameter:" + value);

        rader = gameObject.GetComponent<RaderChart>();
        StartCoroutine("ResultManager");
    }

    IEnumerator ResultManager()
    {
        rader.DisplayScore(result.Score, 0);
        rader.DisplayScore(result.SkinScore, 1);
        rader.DisplayScore(result.BonusScore, 2);
        yield return rader.StartCoroutine("Rader",parameter);
        Debug.Log("Input any key, Load SelectMusic");
        while (true)
        {
            if (isOnKey[6] && Input.GetKey(KeyConfig[6]))
            {
                SceneManager.LoadScene("SelectMusic");
                yield break;
            }
            yield return new WaitForSeconds(fps);
        }
    }

}
