using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderChart : MainRoot {

    Mesh mesh;

    // Use this for initialization
    private void Start()
    {
        mesh = gameObject.GetComponent<Mesh>();
    }
    public void DrawRader(Vector3 center, float radius, List<float> parameter)
    {
        List<int> index = new List<int>();
        index.Add(0);
        index.Add(1);
        index.Add(2);
        List<Vector3> vertices = new List<Vector3>();
        for(int i = 0; i < 3; i++)
        {
            float rad = (90f - 120f * i) * Mathf.Deg2Rad;
            float x = radius * parameter[i] * Mathf.Cos(rad);
            float y = radius * parameter[i] * Mathf.Sign(rad);
            vertices.Add(center + new Vector3(x, y, 0));
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = index.ToArray();
    }




}
