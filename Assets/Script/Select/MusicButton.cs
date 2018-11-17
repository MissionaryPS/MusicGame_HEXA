using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MainRoot {
    //曲のボタン限定

    private int SelfNum;
    private int focus;
    private int devide;
    private float CircleRadius;
    List<Vector3> LocalHexVertices = new List<Vector3>();


    public void SetUpButton(int number, int focus,int Devide, float HexRadius,float CRadius)
    {
        SelfNum = number;
        CircleRadius = CRadius;
        devide = Devide;
        LocalHexVertices = CalcLocalVertices(Vector3.zero, HexRadius);
        List<int> index = new List<int>();
        for (int i = 1; i <= 6; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 6) ? i + 1 : 1);
        }

        var mesh = new Mesh();
        mesh.vertices = CalcRetVertices(DecidePosition(focus)).ToArray();
        //mesh.vertices = LocalHexVertices.ToArray();
        mesh.triangles = index.ToArray();
        //メッシュの反映
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public void ReDrawButton(int focus)
    {
        gameObject.GetComponent<MeshFilter>().mesh.SetVertices(CalcRetVertices(DecidePosition(focus)));
    }

    int DecidePosition(int focus)
    {
        int ret = SelfNum - focus;
        return ret;
    }

    private List<Vector3> CalcRetVertices(int position)
    {
        float rad = position * (360f/devide) * Mathf.Deg2Rad;
        Vector3 center = CalcCenter(rad);
        List<Vector3> vertices = new List<Vector3>();
        for(int i= 0; i < 7; i ++)
        {
            //Debug.Log("Verticies" + i);
            float x = LocalHexVertices[i].x * (Mathf.Cos(rad));
            float y = LocalHexVertices[i].y ;
            float z = LocalHexVertices[i].x * (Mathf.Sin(rad));
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
            vertices.Add(center + new Vector3(x, y, 0));
        }
        return vertices;
    }
}