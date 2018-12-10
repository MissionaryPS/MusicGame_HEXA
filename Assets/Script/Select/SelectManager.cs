using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : SelectMain {

    public MusicList musicList;
    private CanvasManager canvas;
    private ButtonManager buttonManager;
    int center;
    int difficulty;

    // Use this for initialization
    void Start() {

        canvas = gameObject.GetComponent<CanvasManager>();
        buttonManager = gameObject.GetComponent<ButtonManager>();
        center = 1;
        difficulty = 0;
        StartCoroutine("enumerator");
    }
    IEnumerator enumerator (){
        yield return StartCoroutine("LoadList");
        Debug.Log(musicList.music.Length + "musics load");
        canvas.SetUp(center, difficulty, musicList);
        buttonManager.SetUp(center, difficulty, musicList);
        while (true)
        {
            if (!(isOnKey[1]) && Input.GetKey(KeyConfig[1]))
            {
                center = center == 0 ? musicList.music.Length + 1 : center - 1;
                canvas.ChangeMusic(center, difficulty);
                buttonManager.Replace(center);
            }
            if (!(isOnKey[4]) && Input.GetKey(KeyConfig[4]))
            {
                center = center == musicList.music.Length + 1 ? 0 : center + 1;
                canvas.ChangeMusic(center, difficulty);
                buttonManager.Replace(center);
            }
            if (!(isOnKey[3]) && Input.GetKey(KeyConfig[3]))
            {
                difficulty = (difficulty + 1) % 3;
                canvas.ChangeMusic(center, difficulty);
                buttonManager.ChangeDifficulty(difficulty);
            }
            //if (!(isOnKey[7]) && Input.GetKey(KeyConfig[7])) yield break;

            for (int i = 0; i < KeyConfig.Length; i++) isOnKey[i] = Input.GetKey(KeyConfig[i]);
            yield return new WaitForSeconds(fps);
        }
    }

    IEnumerator LoadList()
    {
        using (WWW www = new WWW("file:///" + Application.dataPath + "/Resources/MusicList.json"))
        {
            yield return www;
            Debug.Log(www.url);
            Debug.Log(www.isDone);
            Debug.Log(www.text);
            yield return musicList = JsonUtility.FromJson<MusicList>(www.text);
        }
        yield break;
    }

    private MusicList SortList(MusicList musicList, string type, bool reverse)
    {
        //実装終わってない
        MusicList retList = musicList;

        return retList;
    }

}
