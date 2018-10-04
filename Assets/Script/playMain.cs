using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class playMain : MainRoot {
    [Serializable]
    public class Data
    {
        public int bpm;
        public int startTime;
        public Difficulty difficulty;
    }
    [Serializable]
    public class Difficulty
    {
        public LevelInfo easy;
        public LevelInfo normal;
        public LevelInfo hard;
    }
    [Serializable]
    public class LevelInfo
    {
        public int level;
        public int allTarget;
        public Map[] map;
    }
    [Serializable]
    public class Map
    {
        public int timing;
        public int[] note;
        public bool isHead;
    }

    private Draw draw;
    private Judge judge;
    private MusicData data;
    private AudioSource music;

    public Data mapdata;
    public LevelInfo levelInfo;


    // Use this for initialization
    IEnumerator Start() {
        Debug.Log("playMain start.");
        for (int i = 0; i < 6; i++) temp[i] = false; //キーの初期化
        judge = gameObject.GetComponent<Judge>(); //判定有効化
        draw = gameObject.GetComponent<Draw>(); //描画系の有効化
        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();
        yield return data.StartCoroutine("LoadJson", MusicTitle);
        levelInfo = data.GetMap(select);
        foreach (Map x in levelInfo.map)
            foreach (int y in x.note)
                Debug.Log(y);
        //準備ができたらコルーチンスタート
        yield return StartCoroutine("LoadMusicData", MusicTitle);

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

    public float Notes2Time(int n, int bpm, float firstTime)
    {
        return firstTime + n * (60f / bpm) / 48;
    }

    //時間をnノーツ目に変換
    public int Time2Notes(float time, int bpm, float firstTime)
    {
        return (int)((time - firstTime) * (bpm / 60) * 48);
    }

    [SerializeField]
    GameObject NotePrefab;
    void CreateNotes(int n, int key)
    {
        GameObject note;
        note = Instantiate(NotePrefab) as GameObject;
        note.GetComponent<NotesMove>().SetNotesData(key,n);
    }


}
