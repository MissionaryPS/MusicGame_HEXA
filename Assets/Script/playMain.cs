using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class playMain : MonoBehaviour {

    Draw draw;
    Judge judge;
    MusicData data;  
    bool[] temp = new bool[6];
    public KeyCode[] KeyConfig = new KeyCode[6] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N };
    public float notesDelay = 0.5f;
    public int[][] map;
    public int bpm;
    public int AllTarget;

    string select = "easy";
    string MusicTitle = "metronome";

    // Use this for initialization
    IEnumerator Start () {
        
        for (int i = 0; i < 6; i++) temp[i] = false; //キーの初期化
        judge = gameObject.GetComponent<Judge>(); //判定有効化
        draw = gameObject.GetComponent<Draw>(); //描画系の有効化
        data = gameObject.GetComponent<MusicData>();
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
        StartCoroutine("LoadMusicData",MusicTitle);

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
                //playTime = music.time;
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

    [SerializeField]
    private GameObject MusicPrehub;

    private GameObject MusicSource;
    private AudioSource music;
    IEnumerator LoadMusicData(string MusicTitle)
    {
        MusicSource = Instantiate(MusicPrehub) as GameObject;
        MusicSource.name = "Speaker";
        music = MusicSource.GetComponent<AudioSource>();
        string path = Application.dataPath + "/Resouces/" + MusicTitle + ".wav";
        using (var www = new WWW("file:///" + path))
        {
            yield return www;
            music.clip = www.GetAudioClip(true, false);
            yield return StartCoroutine("playMusicGame");

        }
        yield break;
    }

    float Notes2Time(int n, float bpm, float first)
    {
        return first + n * (60f / bpm) / 48;
    }

}
