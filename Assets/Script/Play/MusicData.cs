using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MusicData : PlayMain {

    PlayManager manager;

    private void Start()
    {
        manager = gameObject.GetComponent<PlayManager>();

    }

    public IEnumerator LoadAudioClip(string FileName)
    {
        Debug.Log("LoadAudioClip FileName:" + FileName);
        string path = Application.dataPath + "/Resources/" + FileName + "/" + FileName + ".wav";
        using (var wwwMusic = new WWW("file:///" + path))
        {
            yield return wwwMusic;
            Debug.Log(path);
            Debug.Log(wwwMusic.isDone);
            Debug.Log(wwwMusic.url);
            manager.music.clip = wwwMusic.GetAudioClip(false, true);
            Debug.Log(manager.music.clip.loadState);
        }
        yield break;
    }


    public IEnumerator LoadJson(string FilePath)
    {
        Debug.Log("LoadJson FilePath:" + FilePath);
        using (var www = new WWW("file:///" + Application.dataPath + "/Resources/" + FilePath))
        {
            yield return www;
            Debug.Log(www.text);
            manager.mapdata = JsonUtility.FromJson<Data>(www.text);
        }
        yield break;
    }


}
