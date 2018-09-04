using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Draw : MonoBehaviour {

    [SerializeField]
    private Material _mat;

    //頂点数
    [SerializeField]
    private int VerticesCount = 6;

    //半径
    [SerializeField]
    private float Radius = 5f;

    private List<MeshFilter> Keys = new List<MeshFilter>();

    private void Start()
    {
        if (VerticesCount < 3)
        {
            Debug.LogError("頂点数は３以上を指定してください。");
            return;
        }

        List<Vector3> vertices = new List<Vector3>();
        List<List<int>> triangles = new List<List<int>>();

        //原点座標
        vertices.Add(Vector3.zero);

        //各頂点座標
        for (int i = 1; i <= this.VerticesCount; i++)
        {
            float rad = (90f - (360f / (float)this.VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
            float x = Radius * Mathf.Cos(rad);
            float y = Radius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
            var tri = new List<int>();
            tri.Add(0);
            tri.Add(i);
            tri.Add(i == this.VerticesCount ? 1 : i + 1);
            triangles.Add(tri);
        }
    

        for (int i = 0; i < 4; i++) {
            var mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles[i].ToArray();
            var filter = GetComponent<MeshFilter>();
            Keys.Add(filter);
            Keys[i].sharedMesh = mesh;
        }

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;
    }
}
