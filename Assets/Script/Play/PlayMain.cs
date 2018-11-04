using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayMain : MainRoot {
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

    public string select = "easy";
    [SerializeField]
    public string MusicTitle = "HyperHyper";


    //読みこみクラス
    private Draw draw;
    private Judge judge;
    private MusicData data;


    public AudioSource music;
    public Data mapdata;
    public LevelInfo levelInfo;
    public float speed;


    // Use this for initialization
    public IEnumerator PlayStart()
    {

        Debug.Log("playMain start.");
        for (int i = 0; i < 6; i++) temp[i] = false; //キーの初期化
        judge = gameObject.GetComponent<Judge>(); //判定有効化
        draw = gameObject.GetComponent<Draw>(); //描画系の有効化
        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();

        //譜面読み込み
        yield return data.StartCoroutine("LoadJson", "HyperHyper");
        Debug.Log("bpm:" + mapdata.bpm + " / startTime:" + mapdata.startTime);
        levelInfo = data.GetMap(select);

        //音源読み込み
        yield return data.StartCoroutine("LoadAudioClip", "HyperHyper");
        //準備ができたらコルーチンスタート
        Debug.Log("Press Space to Start");
        yield return StartCoroutine("WaitPressSpace");
        music.Play();
        yield return StartCoroutine("playMusicGame");

    }

    IEnumerator playMusicGame()
    {
        Debug.Log("StartCoroutine");
        float playTime = -1.0f;
        float CreateTime = 1.0f;
        int NextNotes = 0;
        float startTime = 1.25f;
        float NextTime = startTime;
        int bpm = 190;
        while (true)
        {
            
            if (music.isPlaying)
            {
                playTime = music.time;
            }

            
            //playTime += fps;
            /*
             * 
             * パーフェクトの何秒前にノーツを生成するか
             * 次のノーツ生成のタイミングをノーツを生成したタイミングで確認、変数に保持。
             */

            if (playTime + CreateTime >= NextTime)
            {
                for (int key = 0; key < 6; key++)
                {
                    if (levelInfo.map[NextNotes].note[key] != 0) draw.CreateNotes(NextNotes, key);
                }
                NextNotes++;
                NextTime = Notes2Time(levelInfo.map[NextNotes].timing, bpm, startTime);
                Debug.Log(playTime+CreateTime + " >= " + NextTime);
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

            yield return new WaitForSeconds(fps);
        }
    }

    IEnumerator WaitPressSpace()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space)) yield break;
            yield return new WaitForSeconds(fps);
        }
    }

    public float Notes2Time(int n, int bpm, float firstTime)
    {
        return firstTime + n * (60f / bpm) / 12;
    }

    //時間をnノーツ目に変換
    public int Time2Notes(float time, int bpm, float firstTime)
    {
        return (int)((time - firstTime) * (bpm / 60) * 48);
    }

}
