using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour {
 
    //public int[,] humen = new int[7,48*4*4];
    MusicData data;
    const float perArea = 0.03f;
    const float targetArea = 0.06f;

	// Use this for initialization
	void Start () {
        data = GameObject.Find("SpriptManager").GetComponent<MusicData>();
        
		
	}
	public int OnKey(int key,float time)
    {
        Debug.Log("OnkeyStart");
        int i;
        int perS,perE;
        perS = Time2Notes(time - perArea);
        perE = Time2Notes(time + perArea);
        Debug.Log(perS);
        Debug.Log(perE);
        for(i = perS; i < perE; i++)
        {
            if (data.Humen[i, key] > 0)
            {
                data.Humen[i, key] = 0;
                return 1;
            }
        }
        return 0;
    }

	//時間をnノーツ目に変換
    int Time2Notes(float time) {
        int ret;
        ret = (int)(time * (float)(data.bpm / 60) * 48);
        if (ret < 0) ret = 0;
        if (ret > data.allTarget) ret = data.allTarget;
        return ret;
    }
}
