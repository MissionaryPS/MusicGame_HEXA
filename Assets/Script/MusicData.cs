using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MusicData : playMain {
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
        public int AllTarget;
        public string[] map;
    }


    public Data mapdata;
    IEnumerator LoadJson(string MusicTitle)
    {
        using (var www = new WWW("file:///" + Application.dataPath + "/Resources/" + MusicTitle + ".json"))
        {
            yield return www;
            Debug.Log(www.text);
            mapdata = JsonUtility.FromJson<Data>(www.text);
        }
    }
    
    public void GetMap(string SelectLevel)
    {
        LevelInfo level;
        switch (SelectLevel)
        {
            case "easy": level = mapdata.difficulty.easy; break;
            case "normal": level = mapdata.difficulty.normal; break;
            case "hard": level = mapdata.difficulty.hard; break;
            default : return;
        }
        map = new int[level.map.Length][];
        for(int i = 0; i < level.map.Length; i++)
        {
            var temp =  level.map[i].Split(',').Select(int.Parse).ToArray();
            map[i] = new int[7];
            Array.Copy(temp, map[i], 7);
        }
        Debug.Log("retmap clear");
    }

 

}
