using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEdge : MonoBehaviour {

    [SerializeField]
    Material[] DiffColor = new Material[3];

    public void SetColor(int diffculty)
    {
        gameObject.GetComponent<Renderer>().material = DiffColor[diffculty];
    }
}
