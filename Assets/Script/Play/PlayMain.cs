﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayMain : MainRoot {

    //譜面ファイルの定義(JSON)
    [Serializable]
    public class Data
    {
        public int bpm;
        public int startTime;
        public Map[] map;
    }
    [Serializable]
    public class Map
    {
        public int timing;
        public int[] note;
        public bool isHead;
    }

    public class DirectMap
    {
        public float timing;
        public int[] note;
        public bool isHead;
    }

    public float NoteOffset = -1.2f;
    public float HiSpeed = 3.0f;


    //judge
    public float targetArea = 0.5f;
    public float perArea = 0.3f;

    IEnumerator WaitPressSpace()
    {
        Debug.Log("Now Waiting...");
        while (true)
        {
            if (Input.GetKey(KeyCode.Space)) yield break;
            yield return new WaitForSeconds(fps);
        }
    }

    //time<-->note
    public float Notes2Time(int n, int bpm, float firstTime)
    {
        return firstTime + n * (60f / bpm) / 12;
    }
    public int Time2Notes(float time, int bpm, float firstTime)
    {
        return (int)((time - firstTime) * (bpm / 60) * 12);
    }

}
