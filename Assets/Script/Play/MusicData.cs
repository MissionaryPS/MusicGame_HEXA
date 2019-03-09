using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        using (var wwwMusic = UnityWebRequestMultimedia.GetAudioClip("file:///" + path,AudioType.WAV))
        {
            yield return wwwMusic.SendWebRequest();
            manager.music.clip = DownloadHandlerAudioClip.GetContent(wwwMusic);
            Debug.Log(manager.music.clip.loadState);
        }
        yield break;
    }


    public IEnumerator LoadJsonMap(string FilePath)
    {
        Debug.Log("LoadJson FilePath:" + FilePath);
        using (var www = new UnityWebRequest("file:///" + Application.dataPath + "/Resources/" + FilePath))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
            manager.mapdata = JsonUtility.FromJson<Data>(www.downloadHandler.text);
        }
        yield break;
    }


}
