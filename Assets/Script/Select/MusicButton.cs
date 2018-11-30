using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MainRoot {
    //曲のボタン限定
    //クラスのコピー
    SelectMain select;
    //ボタンのプロパティ
    private int SelfNum;
    private int focus;
    private int devide;
    private float CircleRadius;
    List<Vector3> LocalHexVertices = new List<Vector3>();
    Texture texture;
    //縁取り関係
    GameObject Edge;
    [SerializeField]
    GameObject EdgeFab;
    List<Vector3> LocalEdgeVertices = new List<Vector3>();
    float EdgeRate = 0.88f;
    
    public void SetUpButton(int number, int focus,int Devide, float HexRadius,float CRadius, int difficulty)
    {
        select = GameObject.Find("ScriptManager").GetComponent<SelectMain>();
        Edge = Instantiate(EdgeFab) as GameObject;
        Edge.name = "edge";
        SelfNum = number;
        CircleRadius = CRadius;
        devide = Devide;
        LocalHexVertices = CalcLocalVertices(Vector3.zero, HexRadius);
        List<int> HexIndex = new List<int>();
        List<int> EdgeIndex = new List<int>();
        for (int i = 1; i <= 6; i++)
        {
            HexIndex.Add(0);
            HexIndex.Add(i);
            HexIndex.Add(i + 1 <= 6? i + 1 : 1);

            EdgeIndex.Add((i - 1) * 2);
            EdgeIndex.Add((i - 1) * 2 + 1);
            EdgeIndex.Add(i * 2 < 12 ? i * 2 : 0);
            EdgeIndex.Add(i * 2 < 12 ? i * 2 : 0);
            EdgeIndex.Add((i - 1) * 2 + 1);
            EdgeIndex.Add(i * 2 + 1 < 12 ? i * 2 + 1 : 1);
        }

        var mesh = new Mesh();
        mesh.vertices = CalcRetVertices(DecidePosition(focus), LocalHexVertices).ToArray();
        mesh.triangles = HexIndex.ToArray();
        
        var Emesh = new Mesh();
        Emesh.vertices = CalcRetVertices(DecidePosition(focus), LocalEdgeVertices).ToArray();
        Emesh.triangles = EdgeIndex.ToArray();
        
        //メッシュの反映
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        Edge.GetComponent<MeshFilter>().sharedMesh = Emesh;
        Edge.GetComponent<ButtonEdge>().SetColor(difficulty);

    }

    public void ReDrawButton(int focus)
    {
        gameObject.GetComponent<MeshFilter>().mesh.SetVertices(CalcRetVertices(DecidePosition(focus), LocalHexVertices));
        Edge.GetComponent<MeshFilter>().mesh.SetVertices(CalcRetVertices(DecidePosition(focus), LocalEdgeVertices));
        
    }

    public void ChangeDifficulty(int difficulty)
    {
        Edge.GetComponent<ButtonEdge>().SetColor(difficulty);
    }

    int DecidePosition(int focus)
    {
        int ret = SelfNum - focus;
        return ret;
    }

    private List<Vector3> CalcRetVertices(int position, List<Vector3> vector3s)
    {
        float rad = position * (360f/devide) * Mathf.Deg2Rad;
        Vector3 center = CalcCenter(rad);
        List<Vector3> vertices = new List<Vector3>();
        foreach(Vector3 Vert in vector3s)
        {
            //Debug.Log("Verticies" + i);
            float x = Vert.x * (Mathf.Cos(rad));
            float y = Vert.y ;
            float z = Vert.x * (Mathf.Sin(rad));
            vertices.Add(center + new Vector3(x, y, z));            
        }
        //Debug.Log(vertices.Count + "vertices");
        return vertices;
    }

    Vector3 CalcCenter(float rad)
    {
        float x = CircleRadius * Mathf.Sin(rad);
        float y = 0;
        float z = -CircleRadius * Mathf.Cos(rad);
        return new Vector3(x, y, z);
    }

    

    private List<Vector3> CalcLocalVertices(Vector3 center, float radius)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        for (int i = 0; i < 6; i++)
        {
            float rad = (90f - 60f * i) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            Vector3 vector = new Vector3(x, y, 0);
            vertices.Add(center + vector * EdgeRate);
            LocalEdgeVertices.Add(center + vector * EdgeRate);
            LocalEdgeVertices.Add(center + vector);
        }
        return vertices;
    }
}