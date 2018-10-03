using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicListLoader : MonoBehaviour {

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
        public int AllTarget;
    }


    MusicList musicList;

    // Use this for initialization
    public MusicList GetMusicList () {
        StartCoroutine("LoadList");
        return musicList;
    }
	
    IEnumerator LoadList()
    {
        using (WWW www = new WWW("file:///" + Application.dataPath + "/Resouces/MusicList.json"))
        {
            yield return www;
            yield return musicList = JsonUtility.FromJson<MusicList>(www.text);

        }
    }


}
