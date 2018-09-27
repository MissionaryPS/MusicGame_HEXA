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
        perS = Time2Notes(time - perArea + notesDelay);
        perE = Time2Notes(time + perArea + notesDelay);
        //Debug.Log(perS);
        //Debug.Log(perE);
        for(i = perS; i < perE; i++)
        {
            if (map[key][i] > 0)
            {
                map[key][i] = 0;
                Debug.Log("perfect!");
                Debug.Log(time);
                Debug.Log(Time2Notes(time));
                return 1;
            }
        }
        return 0;
    }

	//時間をnノーツ目に変換
    int Time2Notes(float time) {
        int note;
        note = (int)(time * (bpm / 60) * 48);
        if (note < 0) note = 0;
         if (note > AllTarget) note = AllTarget;
        return note;
    }

}
