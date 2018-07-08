using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour {
 
    public int[,] humen = new int[7,48*4*4];
    MusicData data;

	// Use this for initialization
	void Start () {
        data = GameObject.Find("SpriptManager").GetComponent<MusicData>();
        
		
	}
	int onKey(int key,float time)
    {
        int justnote;
        int result = 0;
        justnote = time2notes(time);

        return result;
    }

	//時間をnノーツ目に変換
    int time2notes(float time) {
        return (int)(time * (float)(data.bpm / 60) * 48);
    }
}
