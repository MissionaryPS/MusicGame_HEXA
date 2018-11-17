
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaterial : SelectMain {

    public int Position;
    public int focus;
    SelectMain select;
    Material material;
    List<Vector3> LocalHexVertices = new List<Vector3>();

    public void SetUpButton(float rad, float HexRadius, Vector3 center, bool isMusic)
    {
        var mesh = new Mesh();
        //頂点の設定、メッシュ送り
        List<int> index = new List<int>();
        for (int i = 1; i <= 6; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 6) ? i + 1 : 1);
        }
        mesh.triangles = index.ToArray();

        if (isMusic)
        {
            LocalHexVertices = CalcLocalVertices(Vector3.zero, HexRadius);
            mesh.vertices = CalcRetVertices(center, rad).ToArray();
        }
        else
        {
            LocalHexVertices = CalcLocalVertices(center, HexRadius);
            mesh.vertices = LocalHexVertices.ToArray();
        }

        //メッシュの反映
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public void DrawButton()
    {

    }

    public void SetUpPosition(int x, int center)
    {
        material = gameObject.GetComponent<Renderer>().material;
        Position = x;
        //ChangeMaterial(center);
    }

    public void ChangeMaterial(int center)
    {
        focus = center + Position;
        if (focus < 0 || focus < select.musicList.music.Length) material=select.nomusic;
        else material = select.existmusic;

    }

    private List<Vector3> CalcLocalVertices(Vector3 center, float radius)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        for (int i = 0; i < 6; i++)
        {
            float rad = (90f - 60f * i) * Mathf.Deg2Rad;
            float xy = radius * Mathf.Cos(rad);
            float z = radius * Mathf.Sin(rad);
            vertices.Add(center + new Vector3(xy, xy, z));
        }
        return vertices;
    }

    private List<Vector3> CalcRetVertices(Vector3 center, float rad)
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);
        float radius = rad * Mathf.Deg2Rad;
        foreach(Vector3 LocalVert in LocalHexVertices)
        {
            float x = center.x + LocalVert.x * (-Mathf.Sin(radius));
            float y = center.x + LocalVert.x * (Mathf.Cos(radius));
            float z = center.z + LocalVert.x;
            vertices.Add(new Vector3(x, y, z));
        }
        return vertices;
    }


}