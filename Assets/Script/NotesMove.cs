using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : Draw {

    private int KeyNumber;
    private int PositionNumber;
    public void SetNotesData(int Key, int Position)
    {
        KeyNumber = Key;
        PositionNumber = Position;


        StartCoroutine("Move");

    }



    IEnumerator Move()
    {



        if (levelInfo.map[PositionNumber].note[KeyNumber] < 0)
        {
            Destroy(this);
            yield break;
        }
        yield return new WaitForSeconds(0.03f);
    }

    
    Mesh CreateNotes()
    {


        var mesh = new Mesh();
        return mesh;
    }


}
