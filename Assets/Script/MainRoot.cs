using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MainRoot : MonoBehaviour {

    public KeyCode[] KeyConfig = new KeyCode[6] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N };
    public bool[] temp = new bool[6];
    public float notesDelay = 0.5f;

    public int bpm;
    public int AllTarget;

    public string select = "easy";
    public string MusicTitle = "metronome";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	
}
