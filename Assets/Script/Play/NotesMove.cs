using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMove : MainRoot {

    private PlayManager timeLine;
    private float Radius;
    private float Speed;
    private int Key;
    private int NTime;
    private Vector3 speed1;
    private Vector3 speed2;
    private Vector3 KillPoint;
    private float noteWidth = 0.2f;

    public void SetNotesData(int n, int key, float StartRadius, float BaseSpeed)
    {
        timeLine = GameObject.Find("ScriptManager").GetComponent<PlayManager>(); 
        Radius = StartRadius;
        Speed = BaseSpeed*fps;
        Key = key;
        NTime = n;
        //頂点インデクス
        List<int> triangles = new List<int>();
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);

        var mesh = new Mesh();
        mesh.vertices = CalcPoint(Key,Radius).ToArray();
        mesh.triangles = triangles.ToArray();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        
    }
    
    IEnumerator Move()
    {
        while (true)
        {
            Radius += Speed;
            var mesh = gameObject.GetComponent<MeshFilter>().mesh;
            mesh.SetVertices(CalcPoint(Key, Radius));

            if (timeLine.directMap[NTime].note[Key] < 0 || Radius > 5.6f) 
            {
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(fps);
        }
    }


    private List<Vector3> CalcPoint(int key, float Radius)
    {
        List<Vector3> vertices = new List<Vector3>();
        //各頂点座標
        for (int i = key; i <= key + 1; i++)
        {
            float rad = (270f - i * 60f) * Mathf.Deg2Rad;
            float x = this.Radius * Mathf.Cos(rad);
            float y = this.Radius * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, 0));
            x = (this.Radius - noteWidth) * Mathf.Cos(rad);
            y = (this.Radius - noteWidth) * Mathf.Sin(rad);
            vertices.Add(new Vector3(x, y, -1.0f));
        }
        return vertices;
    }
    


}
