using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayMain : MainRoot {

    private Draw draw;
    private Judge judge;
    private MusicData data;
    private AudioSource music;
    public int[][] map;

    // Use this for initialization
    IEnumerator Start () {
        
        for (int i = 0; i < 6; i++) isOnKey[i] = false; //キーの初期化
        judge = gameObject.GetComponent<Judge>(); //判定有効化
        draw = gameObject.GetComponent<Draw>(); //描画系の有効化
        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();
        yield return data.StartCoroutine("LoadJson",MusicTitle);
        data.GetMap(select);
        foreach (int[] x  in map) 
        {
            foreach (int y in x)
            {
                Debug.Log(y);
            }
        }
        //準備ができたらコルーチンスタート
        yield return StartCoroutine("LoadMusicData",MusicTitle);

    }

    IEnumerator playMusicGame()
    {
        Debug.Log("StartCoroutine");
        float playTime = -1.0f;
        while (true)
        {
            if (music.isPlaying)
            {
                Debug.Log(music.time);
                playTime = music.time;
            }
            else
            {
                music.Play();

            }
            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKey(KeyConfig[i]) != isOnKey[i])
                {
                    //Debug.Log(i);
                    if (isOnKey[i])
                    {
                        draw.TurnOff(i);
                        isOnKey[i] = false;
                    }
                    else
                    {
                        draw.TurnOn(i, judge.OnKey(i, playTime));
                        isOnKey[i] = true;
                    }
                }
            }

            yield return new WaitForSeconds(0.03f);
        }
    }


    IEnumerator LoadMusicData(string MusicTitle)
    {
        string path = Application.dataPath + "/Resouces/" + MusicTitle + ".wav";
        using (var wwwMusic = new WWW("file:///" + path))
        {
            yield return wwwMusic;
            Debug.Log(path);
            Debug.Log(wwwMusic.isDone);
            Debug.Log(wwwMusic.url);
            //music.clip = wwwMusic.GetAudioClip(false, true);
            Debug.Log(music.clip.loadState);
            yield return StartCoroutine("WaitPressSpace");
            music.Play();
            yield return StartCoroutine("playMusicGame");

        }
        yield break;
    }

    IEnumerator WaitPressSpace()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space)) yield break;
            yield return new WaitForSeconds(0.03f);
        }
    }

    float Notes2Time(int n, float bpm, float first)
    {
        return first + n * (60f / bpm) / 48;
    }

}
