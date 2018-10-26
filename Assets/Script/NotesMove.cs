using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : PlayMain {

    private int Key;
    private int Position;
    private float radius;
    private Vector3 speed1;
    private Vector3 speed2;
    private float noteWidth = 0.3f;

    public void SetNotesData(int n, int key, float StartRadius, float BaseSpeed)
    {
        Key = key;
        Position = n;
        radius = StartRadius;
        float rad1 = (270f - n * 60f) * Mathf.Deg2Rad;
        float rad2 = (270f - (n + 1) * 60f) * Mathf.Deg2Rad;
        speed1 = new Vector3(BaseSpeed * Mathf.Cos(rad1), BaseSpeed * Mathf.Sin(rad1));
        speed2 = new Vector3(BaseSpeed * Mathf.Cos(rad2), BaseSpeed * Mathf.Sin(rad2));

        List<int> triangles = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        //各頂点座標
        for (int i = n; i <= n + 1; i++)
        {
            float rad = (270f - i * 60f)  * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
            x = (radius - noteWidth) * Mathf.Cos(rad);
            y = (radius - noteWidth) * Mathf.Sin(rad);
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
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        
        StartCoroutine("Move");
    }
    


    IEnumerator Move()
    {
        var mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.vertices[0] += speed1;
        mesh.vertices[1] += speed1;
        mesh.vertices[2] += speed2;
        mesh.vertices[3] += speed2;
        
       
        /*
        if (levelInfo.map[PositionNumber].note[KeyNumber] < 0)
        {
            Destroy(this);
            yield break;
        }
        */
        yield return new WaitForSeconds(fps);
    }

    


}
