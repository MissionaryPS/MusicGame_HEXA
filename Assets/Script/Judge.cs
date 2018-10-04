using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : playMain {
 
    //public int[,] humen = new int[7,48*4*4];
   

	// Use this for initialization
	void Start () {
   	
	}

    const float perArea = 0.05f;
    const float targetArea = 0.06f;
    public int OnKey(int key,float time)
    {
        //Debug.Log("OnkeyStart");
        int i;
        int perS,perE;
        perS = Time2Notes(time - perArea + notesDelay, mapdata.bpm, mapdata.startTime);
        perE = Time2Notes(time + perArea + notesDelay, mapdata.bpm, mapdata.startTime);
        //Debug.Log(perS);
        //Debug.Log(perE);
        for(i = perS; i < perE; i++)
        {
            if (levelInfo.map[i].note[key] > 0)
            {
                levelInfo.map[i].note[key] *= -1;
                Debug.Log("perfect!");
                Debug.Log(time);
                Debug.Log(Time2Notes(time, mapdata.bpm, mapdata.startTime));
                return 1;
            }
        }
        return 0;
    }


}
