using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMain : MonoBehaviour {

    Settings set;
    KeyLight keyEffect;
    bool[] temp = new bool[6];

    // Use this for initialization
	void Start () {
        int i;
        for (i = 0; i < 6; i++) temp[i] = false;
        set = GameObject.Find("ScriptManager").GetComponent<Settings>();
        keyEffect = GameObject.Find("ScriptManager").GetComponent<KeyLight>();
        StartCoroutine("playMusicGame");

    }

    IEnumerator playMusicGame()
    {
        Debug.Log("StartCoroutine");
        while (true)
        {
            
            int i;
            for (i = 0; i < 6; i++)
            {
                if (Input.GetKey(set.KeyConfig[i]) != temp[i])
                {
                    Debug.Log(i);
                    if (temp[i])
                    {
                        keyEffect.turnOff(i);
                        temp[i] = false;
                    }
                    else
                    { 
                        keyEffect.turnOn(i);
                        temp[i] = true;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Coroutine end.");
                yield break;
            }
            yield return new WaitForSeconds(0.03f);
        }
    }   


}
