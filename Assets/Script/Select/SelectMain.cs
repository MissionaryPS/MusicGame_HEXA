using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SelectMain : MainRoot {

    [Serializable]
    public class MusicList
    {
        public Music[] music;
    }

    [Serializable]
    public class Music
    {
        public string title;
        public string artist;
        public string FileName;
        public string JacketFileName;
        public int bpm;
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
        public int Level;
        public int AllTarget;
    }



}
