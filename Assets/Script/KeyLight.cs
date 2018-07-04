using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLight : MonoBehaviour {

    Settings set;
    public GameObject[]key = new GameObject[6];
    public Renderer[] rend = new Renderer[6];
    public Color EffColor = new Color(1.0f, 0.5f, 1.0f);
    public Color DefColor = new Color(1.0f, 1.0f, 1.0f);

    // Use this for initialization
    void Start () {
        set = GameObject.Find("ScriptManager").GetComponent<Settings>();
        int i; 
        for(i = 0; i < 6; i++) {
            Debug.Log(i);
            rend[i] = GameObject.Find(set.ObjName[i]).GetComponent<Renderer>();
            Debug.Log("renderer is gotten");
            rend[i].material.EnableKeyword("_EMISSION");
            Debug.Log("emissiona enabled");
        }

    }

    public void turnOn(int i) {
        rend[i].material.SetColor("_EmissionColor", EffColor);
    }

    public void turnOff(int i)
    {
        rend[i].material.SetColor("_EmissionColor", DefColor);
    }

}
