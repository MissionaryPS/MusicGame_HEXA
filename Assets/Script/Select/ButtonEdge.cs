using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボタンの縁オブジェクトのプレハブにアタッチされている。

public class ButtonEdge : MonoBehaviour {

    [SerializeField]
    Material[] DiffColor = new Material[3];

    public void SetColor(int diffculty)
    {
        gameObject.GetComponent<Renderer>().material = DiffColor[diffculty];
    }
}
