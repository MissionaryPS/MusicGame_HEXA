using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Draw : playMain {

    [SerializeField]
    private GameObject KeyBase; //鍵のprefab



    [SerializeField]
    private int VerticesCount = 6;    //頂点数

    private const float KeyRadius = 5f;    //半径
    public const float HoleRadius = 1.5f;    //半径
    

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
            Key[i] = Instantiate(KeyBase) as GameObject;
            Key[i].name = "Key" + i;
            Key[i].GetComponent<MeshFilter>().sharedMesh = SetupKeyMesh(i);
            KeyColor[i] = Key[i].GetComponent<MeshRenderer>();
            KeyColor[i].material.EnableKeyword("_EMISSION");
        }

        CreateJudgeLine();
        CreateLaneLine();
        DefColor = new Color(1.0f, 1.0f, 1.0f);
        EffColor[0] = new Color(1.0f, 0.5f, 1.0f);
        EffColor[1] = new Color(1.0f, 1.0f, 0.1f);
    }

    private Mesh SetupKeyMesh(int i)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //各頂点座標
        for (int j = i; j <= i + 1; j++) { 
            float rad = (210f - (360f / VerticesCount) * (j - 1)) * Mathf.Deg2Rad;
            float x = KeyRadius * Mathf.Cos(rad);
            float y = KeyRadius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
            x = HoleRadius * Mathf.Cos(rad);
            y = HoleRadius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
        }

        //頂点インデクス
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);

        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        return mesh;
    }


    [SerializeField]
    private GameObject JudgeLineprefab;

    public const float JudgePoint = 4.5f;
    private const float LineWidth = 0.05f;
    private GameObject JudgeLine;
    private void CreateJudgeLine()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> index = new List<int>();
        //setting vector
        for (int i = 0; i < VerticesCount; i++)
        {
            float rad = (90f - (360f / VerticesCount) * i) * Mathf.Deg2Rad;
            for (int j = -1; j <= 1; j = j + 2)
            {
                float x = (JudgePoint + LineWidth * j) * Mathf.Cos(rad);
                float y = (JudgePoint + LineWidth * j) * Mathf.Sin(rad);
                vertices.Add(new Vector3(x, y, -1));
            }
        }
        //setting index
        for(int i = 0; i < VerticesCount; i++)
        {
            index.Add((i * 2) % vertices.Count);
            index.Add(((i * 2) + 1) % vertices.Count);
            index.Add(((i * 2) + 3) % vertices.Count);
            index.Add((i * 2) % vertices.Count);
            index.Add(((i * 2) + 3) % vertices.Count);
            index.Add(((i * 2) + 2) % vertices.Count);
        }
        //for (int i = 0; i < vertices.Count; i++) Debug.Log("vertices[" + i + "]:" +vertices[i].x + "," + vertices[i].y + "," + vertices[i].z);
        //for (int i = 0; i < index.Count; i++) Debug.Log("index[" + i + "]:" + index[i]);

        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = index.ToArray();
        JudgeLine = Instantiate(JudgeLineprefab) as GameObject;
        JudgeLine.name = "JudgeLine";
        JudgeLine.GetComponent<MeshFilter>().sharedMesh = mesh;
    }


    [SerializeField]
    private GameObject LaneLinePrefab;

    private GameObject LaneLine;
    private void CreateLaneLine()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> index = new List<int>();
        float RadWidth = 1f;
        //setting vector
        for (int i = 0; i < VerticesCount; i++)
        {
            for (int j = -1; j <= 1; j = j + 2)
            {
                float rad = (90f - (360f / VerticesCount) * i - RadWidth * j) * Mathf.Deg2Rad;
                float x = KeyRadius * Mathf.Cos(rad);
                float y = KeyRadius * Mathf.Sin(rad);
                vertices.Add(new Vector3(x, y, -0.5f));
                x = HoleRadius * Mathf.Cos(rad);
                y = HoleRadius * Mathf.Sin(rad);
                vertices.Add(new Vector3(x, y, 0));
            }
        }
        //setting index
        for (int i = 0; i < VerticesCount; i++)
        {
            int x = i * 4;
            index.Add(x);
            index.Add(x + 2);
            index.Add(x + 1);
            index.Add(x + 1);
            index.Add(x + 2);
            index.Add(x + 3);
        }
        //for (int i = 0; i < vertices.Count; i++) Debug.Log("vertices[" + i + "]:" +vertices[i].x + "," + vertices[i].y + "," + vertices[i].z);
        //for (int i = 0; i < index.Count; i++) Debug.Log("index[" + i + "]:" + index[i]);

        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = index.ToArray();
        LaneLine = Instantiate(LaneLinePrefab) as GameObject;
        LaneLine.name = "LaneLine";
        LaneLine.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
