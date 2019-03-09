using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MainRoot : MonoBehaviour {

    static public KeyCode[] KeyConfig = new KeyCode[8] { KeyCode.V, KeyCode.D, KeyCode.R, KeyCode.U, KeyCode.K, KeyCode.N, KeyCode.Space, KeyCode.Escape };
    static public bool[] isOnKey = new bool[8];
    public float fps = 1.0f / 30;

    public float TheoryScore = 1000000.0f;
    public float TheoryBonus = 500000.0f;
    public float[] ScoreRatio = new float[3] { 1.0f, 0.7f, 0.3f };
    public int[] RankBorder = new int[4] { 600000,  720000, 850000, 950000 };

    public void UpdateInput()
    {
        for (int i = 0; i < KeyConfig.Length; i++) isOnKey[i] = Input.GetKey(KeyConfig[i]);
    }


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
        public int Score;
        public int MaxCombo;
        public int AllTarget;
        public int SkinScore;
        public int BonusScore; 
        public int[] breakdown;
    }

    //六角形系統
    [SerializeField]
    public GameObject HexBase;

    public GameObject CreateHexagon(Vector3 center, float radius)
    {
        var mesh = new Mesh();
        mesh.vertices = CalcVertices(center, radius, 6).ToArray();
        mesh.triangles = CalcIndex(6).ToArray();
        GameObject Hexagon = Instantiate(HexBase) as GameObject;
        Hexagon.GetComponent<MeshFilter>().sharedMesh = mesh;

        return Hexagon;
    }

    public List<Vector3> CalcVertices(Vector3 center, float radius, int vertNum)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        for (int i = 0; i < vertNum; i++)
        {
            float rad = (90f - (360f/vertNum) * i) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            vertices.Add(center + new Vector3(x, y, 0));
        }
        return vertices;
    }

    public List<int> CalcIndex(int vertNum)
    {
        List<int> index = new List<int>();
        for (int i = 1; i <= vertNum; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= vertNum) ? i + 1 : 1);
        }
        return index;
    }

    // Update is called once per frame

}
