using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : SelectMain {
    Text MusicTitle;
    Text Level;
    MusicList musicList;

    private void Start()
    {
        MusicTitle = GameObject.Find("MusicTitle").GetComponent<Text>();
        Level = GameObject.Find("LevelViewer").GetComponent<Text>();
    }

    public void SetUp (int center, int difficulty, MusicList list) {
        musicList = list;
        foreach (Music music in musicList.music) Debug.Log(music.title+music.difficulty[0].Level);
        ChangeMusic(center, difficulty);
    }

    public void ChangeMusic(int center,int difficulty) {
        if (center == 0 || center == musicList.music.Length + 1)
        {
            MusicTitle.text = "";
            Level.text = "";
        }
        else
        {
            MusicTitle.text = musicList.music[center - 1].title;
            Level.text = musicList.music[center - 1].difficulty[difficulty].Level.ToString();

            
        }
    }

}
