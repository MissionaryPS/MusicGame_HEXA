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
        foreach (Music music in musicList.music) Debug.Log(music.title+music.difficulty.easy.Level);
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
            switch (difficulty) {
                case 0: Level.text = musicList.music[center - 1].difficulty.easy.Level.ToString(); break;
                case 1: Level.text = musicList.music[center - 1].difficulty.normal.Level.ToString(); break;
                case 2: Level.text = musicList.music[center - 1].difficulty.hard.Level.ToString(); break;
                default: Level.text = ""; break;
            }
            
        }
    }

}
