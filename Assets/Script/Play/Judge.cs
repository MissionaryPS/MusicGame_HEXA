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
        int ret = 0;
        float target = Manager.directMap[Next[key]].timing;
        float perS, perE;
        perS = time - perArea;
        perE = time + perArea;

        Debug.Log(key + ":" + perS + "/" + target + "/" + perE);
        if (time - targetArea < target && target < time + targetArea)
        {
            Manager.mapdata.map[Next[key]].note[key] *= -1;
            ret = 2;
            if (perS < target && target < perE)
            {
                ret = 1;
            }
        }
        if (Next[key] < Manager.directMap.Count) Next[key] = SearchNext(Next[key], key);
        return ret;
    }

    public bool CheckMiss(float PlayTime)
    {
        bool miss = false;
        for (int key = 0; key < 6; key++) {
            if (Manager.directMap[Next[key]].timing + targetArea < PlayTime && Manager.directMap[Next[key]].note[key] > 0)
            {
                Debug.Log("Miss:key" + key +", note:"+ Manager.directMap[Next[key]].note[key] );
                Manager.directMap[Next[key]].note[key] *= -1;
                if (Next[key] < Manager.directMap.Count) Next[key] = SearchNext(Next[key], key);
                miss = true;
            }
        }
        return miss;
    }

    private int SearchNext(int n, int key) {
        for (int i = n; i < Manager.directMap.Count ; i++) {
            if (Manager.directMap[i].note[key] > 0) return i;
        }
        return Manager.directMap.Count - 1;
    }


}
