using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : SelectMain {
    GameObject MusicTitle;
    GameObject Level;
    MusicList musicList;

    private void Start()
    {
        MusicTitle = GameObject.Find("MusicTitle");
        Level = GameObject.Find("LevelViewer");
    }

    public void SetUp (int center, int difficulty, MusicList list) {
        musicList = list;
        foreach (MusicProperty music in musicList.music) Debug.Log(music.title+music.difficulty[0].Level);
        ChangeMusic(center, difficulty);
    }

    public void PositionText(Vector3 center, float Radius)
    {
        float levelX = Radius * Mathf.Cos(60f*Mathf.Deg2Rad);
        float levelY = Radius * Mathf.Sin(60f*Mathf.Deg2Rad);
        Debug.Log(levelX + ":" + levelY);
        Level.GetComponent<Transform>().position = center + new Vector3(levelX,levelY, 0);
        MusicTitle.GetComponent<Transform>().position = center + new Vector3(0, Radius * 1.7f, 0);
    }

    public void ChangeMusic(int center,int difficulty) {
        if (center == 0 || center == musicList.music.Length + 1)
        {
            MusicTitle.GetComponent<TextMesh>().text = "";
            Level.GetComponent<TextMesh>().text = "";
        }
        else
        {
            MusicTitle.GetComponent<TextMesh>().text = musicList.music[center - 1].title;
            Level.GetComponent<TextMesh>().text = musicList.music[center - 1].difficulty[difficulty].Level.ToString();

            
        }
    }

}
