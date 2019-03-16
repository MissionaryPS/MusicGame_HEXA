using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SelectMain : MainRoot {

    //JSON読み込み用クラス
    [Serializable]
    public class MusicList
    {
        public MusicProperty[] music;
    }

    [Serializable]
    public class MusicProperty
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

    public class ButtonProperty
    {
        public GameObject button;
        public int dataIndex;
        public int difficulty;
    }


}
