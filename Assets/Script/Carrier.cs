using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MainRoot {

    private Select2Play select;
    private Play2Result result;

    public void PassSelect(int difficulty, int level, string title, string artist, string FileName)
    {
        select.difficulty = difficulty;
        select.level = level;
        select.title = title;
        select.artist = artist;
        select.FileName = FileName;
        DontDestroyOnLoad(gameObject);
    }

    public Select2Play GetSelect()
    {
        return select;
    }
    public void PassResult(Play2Result data)
    {
        result = data;
    }

    public Play2Result GetResult()
    {
        Destroy(gameObject);
        return result;
    }

}
