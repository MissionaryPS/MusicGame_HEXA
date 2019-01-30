using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : PlayMain {

    string[] diff = new string[3] { "easy", "normal", "hard" };

    //読みこみクラス
    private Draw draw;
    private Judge judge;
    private MusicData data;
    private Select2Play select;
    public AudioSource music;
    public Data mapdata;

    void Start()
    {
        Debug.Log("playMain start.");
        UpdateInput();
        GameObject carrier = GameObject.Find("carrier");
        select = carrier.GetComponent<Carrier>().GetSelect();
        //描画系の有効化
        draw = gameObject.GetComponent<Draw>();

        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();
        Debug.Log("Loading");
        StartCoroutine("Load");
    }

    public IEnumerator Load()
    {
        //譜面読み込み
        Debug.Log(select.FileName);
        yield return data.StartCoroutine("LoadJson", select.FileName + "/" + diff[select.difficulty] + ".json");
        //yield return data.StartCoroutine("LoadJson", select.FileName);
        Debug.Log("bpm:" + mapdata.bpm + " / startTime:" + mapdata.startTime);

        //音源読み込み
        yield return data.StartCoroutine("LoadAudioClip", select.FileName);

        //判定有効化
        judge = gameObject.GetComponent<Judge>();
        judge.SetUpJudge();
        //準備ができたらコルーチンスタート
        Debug.Log("Start");
        //yield return StartCoroutine("WaitPressSpace");
        StartCoroutine("playMusicGame");
        yield break;
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
            bool willEnd = false;
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
                    if (willEnd)
                    {
                        yield return StartCoroutine("EndMusic");
                        yield break;
                    }
                    if (mapdata.map[NextNotes].note[key] != 0) draw.CreateNotes(NextNotes, key);
                }
                NextNotes++;
                NextTime = Notes2Time(mapdata.map[NextNotes].timing, bpm, startTime);
                if (mapdata.map[NextNotes].note[0] < 0) willEnd = true;
                Debug.Log(playTime + CreateTime + " >= " + NextTime );
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
            if (Input.GetKey(KeyConfig[7]))
            {
                yield return StartCoroutine("EndMusic");
                yield break;
            }


            yield return new WaitForSeconds(fps);
        }
    }

    IEnumerator EndMusic()
    {
        Debug.Log("Music End");
        while (music.volume > 0)
        {
            Debug.Log(music.volume);
            music.volume -= 0.1f;
            yield return new WaitForSeconds(fps);
        }
        yield break;
    }
}
