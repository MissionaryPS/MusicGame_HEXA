using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderChart : MainRoot {

    GameObject RaderBack;
    GameObject RaderFill;
    [SerializeField]
    GameObject RaderPrefab;
    float MaxRadius = 5.0f;
    float MaxTime = 3.0f;

    // Use this for initialization
    private void Start()
    {
        RaderBack = CreateHexagon(Vector3.zero, MaxRadius);

        List<int> index = new List<int>();
        for (int i = 1; i <= 3; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 3) ? i + 1 : 1);
        }

        var mesh = new Mesh();
        mesh.vertices = CalcVertices(new Vector3(0, 0, -1f), 0.01f, 3).ToArray();
        mesh.triangles = index.ToArray();

        RaderFill = Instantiate(RaderPrefab) as GameObject;
        RaderFill.GetComponent<Transform>().position = new Vector3(0,0,-1f);
        RaderFill.GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public void DrawRader(Vector3 center, float radius, List<float> parameter)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        for (int i = 0; i < 3; i++)
        {
            float rad = (90f - (360f / 3) * i) * Mathf.Deg2Rad;
            float x = radius * parameter[i] * Mathf.Cos(rad);
            float y = radius * parameter[i] * Mathf.Sin(rad);
            vertices.Add(center + new Vector3(x, y, 0));
        }

        RaderFill.GetComponent<MeshFilter>().mesh.SetVertices(vertices);
    }

    IEnumerator Rader(List<float> parameter)
    {
        Debug.Log("RaderDrawing");

        float Speed = MaxRadius / MaxTime * fps;
        float radius = 0f;
        while (true)
        {
            if (radius > MaxRadius) yield break;
            //Debug.Log("radius:" + radius);
            DrawRader(new Vector3(0, 0, -1f), radius, parameter);
            radius += Speed;
            yield return new WaitForSeconds(fps);
        }
    }
}
