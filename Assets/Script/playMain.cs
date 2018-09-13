using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMain : MonoBehaviour {

    AudioSource music;
    Draw draw;
    Judge judge;
    bool[] temp = new bool[6];
    public KeyCode[] KeyConfig = new KeyCode[6] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N };
    public float notesDelay = 0.5f;



    // Use this for initialization
    void Start () {
        int i;
        for (i = 0; i < 6; i++) temp[i] = false; //キーの初期化
        judge = gameObject.GetComponent<Judge>(); //判定有効化
        draw = gameObject.GetComponent<Draw>(); //描画系の有効化
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

            
            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKey(KeyConfig[i]) != temp[i])
                {
                    //Debug.Log(i);
                    if (temp[i])
                    {
                        draw.TurnOff(i);
                        temp[i] = false;
                    }
                    else
                    {
                        draw.TurnOn(i, judge.OnKey(i, playTime));
                        temp[i] = true;
                    }
                }
            }

            yield return new WaitForSeconds(0.03f);
        }
    }
    


}
