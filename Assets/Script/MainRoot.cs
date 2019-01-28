using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MainRoot : MonoBehaviour {

    public KeyCode[] KeyConfig = new KeyCode[8] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N, KeyCode.Space, KeyCode.Escape };
    public bool[] isOnKey = new bool[8];
    public float fps = 1.0f / 30;

    public bool LoadData = false;
    
    public class Select2Play
    {
        public int difficulty;
        public int level;
        public string title;
        public string artist;
        public string FileName;
    }

    public class Play2Result
    {
        public int difficulty;
        public int level;
        public string title;
        public string artist;
        //ここから追加の情報
        public int score;
        public int MaxCombo;
        public int allTarget;
        public int SkinScore;
    }

    //六角形系統
    [SerializeField]
    public GameObject HexBase;

    public GameObject CreateHexagon(Vector3 center, float radius)
    {
        List<int> index = new List<int>();
        for (int i = 1; i <= 6; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 6) ? i + 1 : 1);
        }
        var mesh = new Mesh();
        mesh.vertices = CalcVertices(center, radius).ToArray();
        mesh.triangles = index.ToArray();
        GameObject Hexagon = Instantiate(HexBase) as GameObject;
        Hexagon.GetComponent<MeshFilter>().sharedMesh = mesh;

        return Hexagon;
    }

    public List<Vector3> CalcVertices(Vector3 center, float radius)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        for (int i = 0; i < 6; i++)
        {
            float rad = (90f - 60f * i) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            vertices.Add(center + new Vector3(x, y, 0));
        }
        return vertices;
    }

    // Update is called once per frame

}
