using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLight : playMain {

    Settings set;
    private Renderer[] keyColor = new Renderer[6];
    private Color[] EffColor = new Color[2];  
    private Color DefColor;

    // Use this for initialization
    void Start () {
        set = GameObject.Find("ScriptManager").GetComponent<Settings>();
        int i; 
        for(i = 0; i < 6; i++) {
            Debug.Log(i);
            keyColor[i] = GameObject.Find(set.ObjName[i]).GetComponent<Renderer>();
            keyColor[i].material.EnableKeyword("_EMISSION");
            Debug.Log("emission enabled");
            DefColor = new Color(1.0f, 1.0f, 1.0f);
            EffColor[0] = new Color(1.0f, 0.5f, 1.0f);
            EffColor[1] = new Color(1.0f, 1.0f, 0.1f);
        }

    }

    public void TurnOn(int i, int color) {
        keyColor[i].material.SetColor("_EmissionColor", EffColor[color]);
    }

    public void TurnOff(int i)
    {
        keyColor[i].material.SetColor("_EmissionColor", DefColor);
    }

}
