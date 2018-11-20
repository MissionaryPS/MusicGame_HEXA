using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SelectMain : MainRoot {

    [Serializable]
    public class MusicList
    {
        public Music[] music;
    }

    [Serializable]
    public class Music
    {
        public string title;
        public string artist;
        public string scoreMake;
        public Difficulty difficulty;
        public float bpm;
        public int HiScore;
        public string FileName;
        public string JacketFileName;
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
    }

    [SerializeField]
    float radius;

    public MusicList musicList;
    public List<Material> ButtonSurface = new List<Material>();
    [SerializeField]
    public Material nomusic;
    [SerializeField]
    public Material existmusic;

    int devide = 15;
    int difficulty = 0;
    //private ButtonMaterial Button;

    IEnumerator Start()
    {
        yield return StartCoroutine("LoadList");
        //Button = gameObject.GetComponent<ButtonMaterial>();
        foreach (Music music in musicList.music) Debug.Log(music.title);
        Debug.Log(musicList.music.Length + "musics load");

        float CircleRadius = radius * Mathf.Cos((360f / devide) * Mathf.Deg2Rad) / Mathf.Tan((360f / devide / 2) * Mathf.Deg2Rad);


        //music button
        int center = 1;
        GameObject[] MusicButton = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            MusicButton[i] = Instantiate(HexBase) as GameObject;
            MusicButton[i].GetComponent<MusicButton>().SetUpButton(i, center, devide, radius, CircleRadius,difficulty);
        }

        //menubutton
        float CRadius = radius * Mathf.Sin(60 * Mathf.Deg2Rad) * 2;
        GameObject[] MenuButton = new GameObject[4];
        for (int i = 0; i < 6; i++)
        {
            int j = 0;
            if (i != 0 && i != 3)
            {
                float rad = (60f * i) * Mathf.Deg2Rad;
                float cx = CRadius * Mathf.Cos(rad);
                float cy = CRadius * Mathf.Sin(rad);
                
                MenuButton[j++] = CreateHexagon(new Vector3(cx, cy, -CircleRadius), radius-0.1f);
                //MenuButton[j].name = "MenuButton";
            }
        }
        
        

        //選択処理開始
        while (true)
        {
            if (!(isOnKey[1]) && Input.GetKey(KeyConfig[1]))
            {
                center--;
                foreach (GameObject Button in MusicButton)
                    Button.GetComponent<MusicButton>().ReDrawButton(center);

            }
            if (!(isOnKey[4]) && Input.GetKey(KeyConfig[4]))
            {
                center++;
                foreach (GameObject Button in MusicButton)
                    Button.GetComponent<MusicButton>().ReDrawButton(center);
            }
            if (!(isOnKey[3]) && Input.GetKey(KeyConfig[3]))
            {
                difficulty = (difficulty + 1) % 3;
                foreach (GameObject Button in MusicButton)
                    Button.GetComponent<MusicButton>().ChangeDifficulty(difficulty);
            }
            //if (!(isOnKey[7]) && Input.GetKey(KeyConfig[7])) yield break;

            for (int i = 0; i < KeyConfig.Length; i++) isOnKey[i] = Input.GetKey(KeyConfig[i]);
            yield return new WaitForSeconds(fps);
        }

    }

    //読み込み系統
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
        MusicList retList = musicList;

        return retList;
    }





}
