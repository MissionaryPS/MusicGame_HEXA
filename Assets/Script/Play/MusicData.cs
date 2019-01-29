using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MusicData : PlayMain {

    PlayManager timeLine;

    private void Start()
    {
        timeLine = gameObject.GetComponent<PlayManager>();

    }

    public IEnumerator LoadAudioClip(string MusicTitle)
    {
        string path = Application.dataPath + "/Resources/" + MusicTitle + "/" + MusicTitle + ".wav";
        using (var wwwMusic = new WWW("file:///" + path))
        {
            yield return wwwMusic;
            Debug.Log(path);
            Debug.Log(wwwMusic.isDone);
            Debug.Log(wwwMusic.url);
            timeLine.music.clip = wwwMusic.GetAudioClip(false, true);
            Debug.Log(timeLine.music.clip.loadState);
        }
        yield break;
    }


    public IEnumerator LoadJson(string FilePath)
    {
        using (var www = new WWW("file:///" + Application.dataPath + "/Resources/" + FilePath))
        {
            yield return www;
            Debug.Log(www.text);
            timeLine.mapdata = JsonUtility.FromJson<Data>(www.text);
        }
        yield break;
    }


}
