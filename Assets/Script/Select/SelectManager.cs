using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SelectManager : SelectMain {

    [SerializeField]
    GameObject carrier;

    public MusicList musicList;
    private CanvasManager canvas;
    private ButtonManager buttonManager;
    int center;
    int difficulty;

    // Use this for initialization
    void Start() {
        UpdateInput();
        Debug.Log("Key Updated");
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

        yield return StartCoroutine("SelectProcess");
        MusicProperty pass = musicList.music[center - 1];
        GameObject carry = Instantiate(carrier) as GameObject;
        carry.name = "carrier";
        carry.GetComponent<Carrier>().PassSelect(difficulty, pass.difficulty[difficulty].Level, pass.title, pass.artist, pass.FileName);
        SceneManager.LoadScene("PlayGame");

    }

    


    IEnumerator SelectProcess()
    {
        Debug.Log("select process start");
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
            if (!(isOnKey[6]) && Input.GetKey(KeyConfig[6]))
            {
                //決定
                if (center != 0 && center != musicList.music.Length + 1)
                {
                    Debug.Log("ここでゲームプレイに移る処理");
                    yield break;
                }
                else
                {
                    Debug.LogError("端につきゲームプレイ不可");
                }
            }
            //if (!(isOnKey[7]) && Input.GetKey(KeyConfig[7])) yield break;

            UpdateInput();
            yield return new WaitForSeconds(fps);
        }

    }

    IEnumerator LoadList()
    {
        using (var www = new UnityWebRequest(Application.dataPath + "/Resources/MusicList.json"))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            Debug.Log(www.url);
            Debug.Log(www.isDone);
            Debug.Log(www.downloadHandler.text);
            yield return musicList = JsonUtility.FromJson<MusicList>(www.downloadHandler.text);
        }
        yield break;
    }

    private List<MusicProperty> SortList(MusicList musicList, int sortMode, bool reverse)
    {
        //実装終わってない
        List<MusicProperty> retList = new List<MusicProperty>();
        switch (sortMode)
        {
            
        }
        return retList;
    }

}
