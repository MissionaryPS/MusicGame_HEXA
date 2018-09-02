using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class Draw : MonoBehaviour {

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private const float radius = 5f;

    [SerializeField]
    private const int vertices = 6;

    private Mesh mesh;

    private List<Vector3> Hex = new List<Vector3>();
    private List<int> indexHex = new List<int>();


    private Mesh CreateKeys() {
        //頂点設定
        Hex.Add(Vector3.zero);
        for (int i = 0; i < vertices; i++)
        {
            float rad = (90f - (360f / (float)vertices) * i * Mathf.Deg2Rad);
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            Hex.Add(new Vector3(x, y, 0));
            indexHex.Add(0);
            indexHex.Add(i + 1);
            indexHex.Add(i == vertices ? 1 : i + 2);
            
        }

        var mesh = new Mesh();
        mesh.vertices = Hex.ToArray();
        mesh.triangles = indexHex.ToArray();
        return mesh;
    }

// Use this for initialization
    void Start () {
        mesh = CreateKeys();
        meshFilter.mesh = mesh;

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
	}
	
	// Update is called once per frame
}
