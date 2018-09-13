using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicData : MonoBehaviour {

    public int bpm = 60;
    public int allTarget = ((60 * 1) + 37) * 48;
    public int[,] Humen = new int [7, ((60 * 1) + 37) * 48];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < allTarget; i++)
        {
            if (i % (48 * 4) == 0) Humen[6, i] = 1;
            if (i % 48 == 0) Humen[4, i] = 1;

        }
	}
	
	// Update is called once per frame
}
