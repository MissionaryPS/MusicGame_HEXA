using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SelectMain : MainRoot {

    //JSON読み込み用クラス
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
        public LevelInfo[] difficulty;
    }
    [Serializable]
    public class LevelInfo
    {
        public int Level;
        public int AllTarget;
    }



}
