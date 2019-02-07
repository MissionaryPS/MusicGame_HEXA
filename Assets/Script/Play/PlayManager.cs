using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : PlayMain {

    string[] diff = new string[3] { "easy", "normal", "hard" };

    //取得するクラス
    private Draw draw;
    private Judge judge;
    private MusicData data;
    private TextManager canvas;

    private Select2Play select;
    public AudioSource music;
    public Data mapdata;
    public List<DirectMap> directMap;
    public float BaseScore;
    public float ComboRatio;
    public int AllTarget;


    int Score = 0;
    float SkinScore = 0.0f;
    float BonusScore = 0.0f;
    int Combo = 0;

    void Start()
    {
        Debug.Log("playMain start.");
        UpdateInput();
        //Selectシーンの変数の受け渡し
        GameObject carrier = GameObject.Find("carrier");
        select = carrier.GetComponent<Carrier>().GetSelect();

        //各クラスの取得
        draw = gameObject.GetComponent<Draw>();
        data = gameObject.GetComponent<MusicData>();
        music = gameObject.GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas").GetComponent<TextManager>();
        Debug.Log("Loading");
        StartCoroutine("Load");
    }

    public IEnumerator Load()
    {
        //音源読み込み
        yield return data.StartCoroutine("LoadAudioClip", select.FileName);

        //譜面読み込み
        Debug.Log(select.FileName);
        yield return data.StartCoroutine("LoadJsonMap", select.FileName + "/" + diff[select.difficulty] + ".json");
        directMap = new List<DirectMap>();
        directMap = Map2Direct(mapdata,music);    //配列に変換
        //yield return data.StartCoroutine("LoadJson", select.FileName);
        Debug.Log("bpm:" + mapdata.bpm + " / startTime:" + mapdata.startTime);
        AllTarget = GetAllTarget(directMap);
        BaseScore = TheoryScore / AllTarget;
        ComboRatio = TheoryBonus * 2 / (BaseScore * AllTarget * (AllTarget + 1));

        //判定有効化
        judge = gameObject.GetComponent<Judge>();
        judge.SetUpJudge();
        //準備ができたらコルーチンスタート
        //yield return StartCoroutine("WaitPressSpace");
        Debug.Log("Start");
        StartCoroutine("playMusicGame");
        yield break;
    }

    IEnumerator playMusicGame()
    {
        Debug.Log("StartCoroutine");
        float CreateTime = Notes2Time(96, mapdata.bpm, 0) / HiSpeed;
        Debug.Log("CreateTime:" + CreateTime);
        float playTime = 0;
        int NextCreate = 0;
        bool MapEnd = false;
        while (true)
        {
            //音源再生待機→再生まで
            if (music.isPlaying)
            {
                playTime = music.time - NoteOffset;
            }
            else
            {
                playTime += fps;
                if(!MapEnd && playTime > 0 - NoteOffset)
                {
                    Debug.Log("MusicStart");
                    music.Play();
                }
            }

            //鍵盤を押したときの処理
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
                        int judgeResult = judge.OnKey(key, playTime);
                        draw.TurnOn(key, judgeResult);
                        if(judgeResult > 0)
                        {
                            SkinScore += BaseScore * ScoreRatio[judgeResult - 1];
                            BonusScore += BaseScore*ComboRatio*(++Combo);
                            Score = (int)SkinScore + (int)BonusScore;
                            canvas.Chenge(Score, Combo);
                        }
                        isOnKey[key] = true;
                    }
                }
            }

            //譜面読み込み・ノーツ生成処理
            if (!MapEnd　&& playTime + CreateTime> directMap[NextCreate].timing) {

                for(int i = 0; i < 6; i++)
                {
                    if(directMap[NextCreate].note[i] == 1)
                    {
                        draw.CreateNotes(NextCreate, i, CreateTime);
                    }
                }
                NextCreate++;
                if (directMap[NextCreate].note[0] < 0) MapEnd = true;
                
            }

            //
            if (judge.CheckMiss(playTime))
            {
                Combo = 0;
                canvas.Chenge(Score, Combo);
            }

            //中断処理(Escキー)
            if (Input.GetKey(KeyConfig[7]) || (MapEnd && !music.isPlaying))
            {
                yield return StartCoroutine("EndMusic");
                Debug.Log("END");
                break;
            }

            yield return new WaitForSeconds(fps);
        }
        SceneManager.LoadScene("Result");
        yield break;
    }

    IEnumerator EndMusic()
    {
        float decrease = 0.01f;
        Debug.Log("Music End");
        while (music.isPlaying && music.volume > 0)
        {
            //Debug.Log(music.volume);
            music.volume -= decrease; 
            yield return new WaitForSeconds(fps);
        }
        music.Stop();
        yield break;
    }

    int GetAllTarget(List<DirectMap> data)
    {
        int counter = 0;
        foreach (DirectMap map in data)
            foreach (int note in map.note)
                if (note > 0) counter++;
        return counter;
    }

    List<DirectMap> Map2Direct(Data data, AudioSource music)
    {
        Debug.Log("Jsonmap:" + data.map.Length);
        List<DirectMap> directMaps = new List<DirectMap>();
        foreach(Map map in data.map)
        {
            DirectMap directMap = new DirectMap();
            directMap.timing = Notes2Time(map.timing, data.bpm, (float)data.startTime / 100);
            directMap.note = map.note;
            directMap.isHead = map.isHead;
            directMaps.Add(directMap);
        }
        DirectMap EndMap = new DirectMap();
        EndMap.timing = music.clip.length;
        EndMap.note = new int[6] { -1, 0, 0, 0, 0, 0 };
        EndMap.isHead = false;
        directMaps.Add(EndMap);
        Debug.Log("directmap:"+directMaps.Count);
        return directMaps;
    }

}
