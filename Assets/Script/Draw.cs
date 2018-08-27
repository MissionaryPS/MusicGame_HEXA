using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    [SerializeField]
    private MeshFilter meshFilter;
    private Mesh mesh;
    private List<Vector3> Hex = new List<Vector3>();
    private List<int> indexHex = new List<int>();


    private Mesh CreateKeys() {
        var mesh = new Mesh();
        //頂点設定
        Hex.Add(new Vector3(0, 0));
        for (int i = 0; i< 5; i++) Hex.Add(new Vector3(5 * Mathf.Cos(i* 60), 5 * Mathf.Sin(i* 60)));
        indexHex.AddRange(new[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 });

        mesh.SetVertices(Hex);
        mesh.SetIndices(indexHex.ToArray(), MeshTopology.Triangles, 0);
        return mesh;
    }

// Use this for initialization
    void Start () {
        mesh = CreateKeys();
        meshFilter.mesh = mesh;

	}
	
	// Update is called once per frame
}
