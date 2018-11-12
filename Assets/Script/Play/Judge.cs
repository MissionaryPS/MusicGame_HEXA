using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : PlayMain {

    TimeLine timeLine;
    int bpm;
    float startTime;

    int[] Next = new int[6];

    //public int[,] humen = new int[7,48*4*4];
    public void SetUpJudge()
    {
        timeLine = gameObject.GetComponent<TimeLine>();
        bpm = timeLine.mapdata.bpm;
        startTime = timeLine.mapdata.startTime;
        for (int key = 0; key < 6; key++) Next[key] = SearchNext(0, key);
    }
    
    const float perArea = 0.05f;
    const float targetArea = 0.06f;
    public int OnKey(int key,float time)
    {
        
        float target = Notes2Time(timeLine.levelInfo.map[Next[key]].timing,bpm,startTime/100);
        float perS,perE;
        perS = time - perArea;
        perE = time + perArea;

        Debug.Log(perS + "/" + target + "/" + perE);
        if (perS < target && target < perE)
        {
            timeLine.levelInfo.map[Next[key]].note[key] *= -1;
            Next[key] = SearchNext(Next[key], key);
            return 1;
        }

        //Debug.Log(perS);
        //Debug.Log(perE);

        /*
        for(i = Next[key]; timeLine.levelInfo.map[i].timing < perE; i++)
        {
            if (timeLine.levelInfo.map[i].note[key] > 0)
            {
                timeLine.levelInfo.map[i].note[key] *= -1;
                Debug.Log("perfect!");
                Debug.Log(time);
                Debug.Log(Time2Notes(time, bpm, startTime));
                return 1;
            }
        }
        */
        return 0;
    }

    private int SearchNext(int n, int key) {
        int ret;
        for (int i = n; ; i++) {
            if (timeLine.levelInfo.map[i].note[key] > 0) {
                ret = i;
                break;
            }
        }
        return ret;
    }


}
