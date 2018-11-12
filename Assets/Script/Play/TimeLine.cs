using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : PlayMain {

    //読みこみクラス
    private Draw draw;
    private Judge judge;
    private MusicData data;
    public AudioSource music;
    public Data mapdata;
    public LevelInfo levelInfo;

    public IEnumerator Start()
    {

        Debug.Log("playMain start.");
        for (int i = 0; i < 6; i++) temp[i] = false; //キーの初期化

        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();

        //譜面読み込み
        yield return data.StartCoroutine("LoadJson", "HyperHyper");
        Debug.Log("bpm:" + mapdata.bpm + " / startTime:" + mapdata.startTime);
        levelInfo = data.GetMap(select);

        //音源読み込み
        yield return data.StartCoroutine("LoadAudioClip", "HyperHyper");

        //描画系の有効化
        draw = gameObject.GetComponent<Draw>();

        //判定有効化
        judge = gameObject.GetComponent<Judge>();
        judge.SetUpJudge();
        //準備ができたらコルーチンスタート
        Debug.Log("Start");
        //yield return StartCoroutine("WaitPressSpace");
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
        music.Play();
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
                Debug.Log(playTime + CreateTime + " >= " + NextTime);
            }

            for (int key = 0; key < 6; key++)
            {
                if (Input.GetKey(KeyConfig[key]) != temp[key])
                {
                    //Debug.Log(i);
                    if (temp[key])
                    {
                        draw.TurnOff(key);
                        temp[key] = false;
                    }
                    else
                    {
                        draw.TurnOn(key, judge.OnKey(key, playTime));
                        temp[key] = true;
                    }
                }
            }

            yield return new WaitForSeconds(fps);
        }
    }


}
