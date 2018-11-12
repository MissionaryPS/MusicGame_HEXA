using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayMain : MainRoot {

    //譜面ファイルの定義(JSON)
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
    public float speed;
    


    IEnumerator WaitPressSpace()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space)) yield break;
            yield return new WaitForSeconds(fps);
        }
    }

    //time<-->note
    public float Notes2Time(int n, int bpm, float firstTime)
    {
        return firstTime + n * (60f / bpm) / 12;
    }
    public int Time2Notes(float time, int bpm, float firstTime)
    {
        return (int)((time - firstTime) * (bpm / 60) * 12);
    }

}
