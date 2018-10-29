using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MusicData : PlayMain {
    IEnumerator LoadJson(string MusicTitle)
    {
        using (var www = new WWW("file:///" + Application.dataPath + "/Resources/" + MusicTitle + ".json"))
        {
            yield return www;
            Debug.Log(www.text);
            yield return mapdata = JsonUtility.FromJson<Data>(www.text);
        }
        yield break;
    }
    
    public LevelInfo GetMap(string SelectLevel)
    {
        Debug.Log("start get map.");
        switch (SelectLevel)
        {
            case "easy": return mapdata.difficulty.easy;
            case "normal": return mapdata.difficulty.normal;
            case "hard": return mapdata.difficulty.hard;
            default : return null;
        }   
    }
}
