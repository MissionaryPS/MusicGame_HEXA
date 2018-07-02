using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLight : MonoBehaviour {

    public GameObject[]key = new GameObject[6];
    public Renderer[] rend = new Renderer[6];
    public Color EffColor = new Color(1.0f, 0.5f, 1.0f);
    public Color DefColor = new Color(1.0f, 1.0f, 1.0f);
    public string[] ObjName = new string[6] { "Key0", "Key1", "Key2", "Key3", "Key4", "Key5" };
    public KeyCode[] KeyConfig = new KeyCode[6] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N };

    // Use this for initialization
    void Start () {
        int i; 
        for(i = 0; i < 6; i++) {
            Debug.Log(i);
            key[i] = GameObject.Find(ObjName[i]);
            Debug.Log("key is found");
            rend[i] = key[i].GetComponent<Renderer>();
            Debug.Log("renderer is gotten");
            rend[i].material.EnableKeyword("_EMISSION");
            Debug.Log("emissiona enabled");
        }

    }
    
    void Update()
    {
        int i;
        for (i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown(KeyConfig[i]))
            {
                Debug.Log(i);
                rend[i].material.SetColor("_EmissionColor", EffColor);

            }
            if (Input.GetKeyUp(KeyConfig[i]))
            {
                rend[i].material.SetColor("_EmissionColor", DefColor);
            }
        }
    }
}
