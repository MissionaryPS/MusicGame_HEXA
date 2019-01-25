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

    public string select = "easy";

    public IEnumerator Start()
    {

        Debug.Log("playMain start.");
        for (int i = 0; i < 6; i++) isOnKey[i] = false; //キーの初期化

        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();

        //譜面読み込み
        yield return data.StartCoroutine("LoadJson", "HyperHyper");
        Debug.Log("bpm:" + mapdata.bpm + " / startTime:" + mapdata.startTime);

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
        float startTime = mapdata.startTime / 100;
        float NextTime = startTime;
        int bpm = 190;
        while (true)
        {

            if (music.isPlaying)
            {
                playTime = music.time;
            }
            else
            {
                playTime += fps;
                if(playTime > 0)
                {
                    Debug.Log("MusicStart");
                    music.Play();
                }
            }


            //playTime += fps;
            /*
             * 
             * パーフェクトの何秒前にノーツを生成するか
             * 次のノーツ生成のタイミングをノーツを生成したタイミングで確認、変数に保持。
             */

            if (playTime + CreateTime + notesDelay >= NextTime)
            {
                for (int key = 0; key < 6; key++)
                {
                    if (mapdata.map[NextNotes].note[key] != 0) draw.CreateNotes(NextNotes, key);
                }
                NextNotes++;
                NextTime = Notes2Time(mapdata.map[NextNotes].timing, bpm, startTime);
                Debug.Log(playTime + CreateTime + " >= " + NextTime);
            }

            for (int key = 0; key < 6; key++)
            {
                if (Input.GetKey(KeyConfig[key]) != isOnKey[key])
                {
                    //Debug.Log(i);
                    if (isOnKey[key])
                    {
                        draw.TurnOff(key);
                        isOnKey[key] = false;
                    }
                    else
                    {
                        draw.TurnOn(key, judge.OnKey(key, playTime));
                        isOnKey[key] = true;
                    }
                }
                /*
                if (Notes2Time(mapdata.map[judge.Next[key]].timing, bpm, startTime) < playTime + judge.targetArea)
                {
                    judge.Miss(key);
                    Debug.Log("Miss!");
                }
                */

            }



            yield return new WaitForSeconds(fps);
        }
    }


}
