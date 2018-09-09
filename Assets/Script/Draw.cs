using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Draw : MonoBehaviour {

    [SerializeField]
    private GameObject KeyBase; //鍵のprehub

    [SerializeField]
    private int VerticesCount = 6;    //頂点数

    [SerializeField]
    private float Radius = 5f;    //半径

    private GameObject[] Key = new GameObject[6];
    private MeshRenderer[] KeyColor = new MeshRenderer[6];
    private Color[] EffColor = new Color[2];
    private Color DefColor;

 


    public void TurnOn(int i, int color)
    {
        KeyColor[i].material.SetColor("_EmissionColor", EffColor[color]);
    }

    public void TurnOff(int i)
    {
        Debug.Log("TurnOff:" + i);
        KeyColor[i].material.SetColor("_EmissionColor", DefColor);
    }





    //以下、初期化系関数
    private void Start()
    {
        if (VerticesCount < 3)
        {
            Debug.LogError("頂点数は３以上を指定してください。");
            return;
        }
        for (int i = 0; i < VerticesCount; i++)
        {
            Key[i] = Instantiate(KeyBase);
            Key[i].GetComponent<MeshFilter>().sharedMesh = SetupKeyMesh(i);
            KeyColor[i] = Key[i].GetComponent<MeshRenderer>();
            KeyColor[i].material.EnableKeyword("_EMISSION");
        }
        DefColor = new Color(1.0f, 1.0f, 1.0f);
        EffColor[0] = new Color(1.0f, 0.5f, 1.0f);
        EffColor[1] = new Color(1.0f, 1.0f, 0.1f);
    }

    private Mesh SetupKeyMesh(int i)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //原点座標
        vertices.Add(Vector3.zero);
        //各頂点座標
        for (int j = i; j <= i + 1; j++) { 
            float rad = (210f - (360f / (float)this.VerticesCount) * (j - 1)) * Mathf.Deg2Rad;
            float x = Radius * Mathf.Cos(rad);
            float y = Radius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
        }

        //頂点インデクス
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        return mesh;
    }

}
