using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MainRoot : MonoBehaviour {

    public KeyCode[] KeyConfig = new KeyCode[8] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N , KeyCode.Space, KeyCode.Escape};
    public bool[] isOnKey = new bool[8];
    public float notesDelay = 0.5f;

    public int bpm;
    public int AllTarget;

    public float fps = 1.0f / 30;

    public string select = "easy";
    public string MusicTitle = "metronome";

    SelectMain selectProcess;

    // Use this for initialization
    void Start () {
        selectProcess = gameObject.GetComponent<SelectMain>();

        //selectProcess.StartCoroutine("SelectMusic");
    }
    
    // Update is called once per frame

}
