using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : PlayMain {

    PlayManager Manager;

    public int[] Next = new int[6];

    //public int[,] humen = new int[7,48*4*4];
    public void SetUpJudge()
    {
        Manager = gameObject.GetComponent<PlayManager>();
        for (int key = 0; key < 6; key++) Next[key] = SearchNext(0, key);
    }

    public int OnKey(int key, float time)
    {

        float target = Manager.directMap[Next[key]].timing;
        float perS, perE;
        perS = time - perArea;
        perE = time + perArea;

        Debug.Log(key + ":" + perS + "/" + target + "/" + perE);
        if (time - targetArea < target && target < time + targetArea)
        {
            if(Next[key] < Manager.directMap.Count) Next[key] = SearchNext(Next[key], key);
            if (perS < target && target < perE)
            {
                Manager.mapdata.map[Next[key]].note[key] *= -1;
                return 1;
            }
            return 2;
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

    public void CheckMiss(float PlayTime)
    {
        for (int key = 0; key < 6; key++) {
            if (Manager.directMap[Next[key]].timing + targetArea < PlayTime)
            {
                Debug.Log("Miss:key" + key);
                Manager.directMap[Next[key]].note[key] *= -1;
                if (Next[key] < Manager.directMap.Count) Next[key] = SearchNext(Next[key], key);
            }
        }
    }

    private int SearchNext(int n, int key) {
        for (int i = n; i < Manager.directMap.Count ; i++) {
            if (Manager.directMap[i].note[key] > 0) return i;
        }
        return Manager.directMap.Count - 1;
    }


}
