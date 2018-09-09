using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMain : MonoBehaviour {
    AudioSource music;
    Settings set;
    Draw keyEffect;
    Judge judge;
    bool[] temp = new bool[6];
    
    // Use this for initialization
	void Start () {
        int i;
        for (i = 0; i < 6; i++) temp[i] = false; //キーの初期化
        set = GameObject.Find("ScriptManager").GetComponent<Settings>(); //各種設定読み込み
        judge = GameObject.Find("ScriptManager").GetComponent<Judge>(); //判定有効化
        keyEffect = GameObject.Find("ScriptManager").GetComponent<Draw>(); //キーエフェクトの有効化
        music = GameObject.Find("testMusic").GetComponent<AudioSource>(); //音源読み込み
        //準備ができたらコルーチンスタート
        StartCoroutine("playMusicGame");

    }

    IEnumerator playMusicGame()
    {
        Debug.Log("StartCoroutine");
        bool musicPlaying = true;
        float playTime = -1.0f;
        music.PlayDelayed(1.0f);
        while (true)
        {
            if (musicPlaying)
            {
                playTime = music.time;
                //Debug.Log(playTime);
            }

            int i;
            for (i = 0; i < 6; i++)
            {
                if (Input.GetKey(set.KeyConfig[i]) != temp[i])
                {
                    //Debug.Log(i);
                    if (temp[i])
                    {
                        keyEffect.TurnOff(i);
                        temp[i] = false;
                    }
                    else
                    {
                        keyEffect.TurnOn(i, judge.OnKey(i, playTime));
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
