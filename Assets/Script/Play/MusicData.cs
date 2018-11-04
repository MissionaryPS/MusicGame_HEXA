using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MusicData : PlayMain {
    public IEnumerator LoadAudioClip(string MusicTitle)
    {
        string path = Application.dataPath + "/Resources/" + MusicTitle + ".wav";
        using (var wwwMusic = new WWW("file:///" + path))
        {
            yield return wwwMusic;
            Debug.Log(path);
            Debug.Log(wwwMusic.isDone);
            Debug.Log(wwwMusic.url);
            music.clip = wwwMusic.GetAudioClip(false, true);
            Debug.Log(music.clip.loadState);
        }
        yield break;
    }


    public IEnumerator LoadJson(string MusicTitle)
    {
        using (var www = new WWW("file:///" + Application.dataPath + "/Resources/" + MusicTitle + ".json"))
        {
            yield return www;
            Debug.Log(www.text);
            mapdata = JsonUtility.FromJson<Data>(www.text);
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
            default: return null;
        }
    }

}
