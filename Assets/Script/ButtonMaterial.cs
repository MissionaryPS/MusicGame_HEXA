using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaterial : SelectMain {

    public int Position;
    public int focus;
    Material material;

    public void SetUpPosition(int x,int center)
    {
        material = gameObject.GetComponent<Renderer>().material;
        Position = x;
        //ChangeMaterial(center);
    }

    public void ChangeMaterial(int center)
    {
        focus = center + Position;
        if (focus < 0 || focus < musicList.music.Length) material = nomusic;
        else material = existmusic;

    }


}
