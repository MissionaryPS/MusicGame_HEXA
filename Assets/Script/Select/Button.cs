using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Button : MainRoot {

    [SerializeField]
    GameObject edgeFab;

    public GameObject edge;
    public Material material;
    float edgeRatio = 0.1f;

    void Start()
    {
        float hexRadius = ViewManager.GetHexRadius();
        float sphereRadius = ViewManager.GetSphereRadius();

        gameObject.transform.position = Vector3.zero;
        var mesh = new Mesh();
        mesh.SetTriangles(CalcIndex(6), 0);
        mesh.SetVertices(CalcVertices(new Vector3(0, 0, -sphereRadius), hexRadius * (1 - edgeRatio), 6));
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;

        edge = Instantiate(edgeFab) as GameObject;
        edge.transform.position = Vector3.zero;
        edge.transform.parent = gameObject.transform;
        edge.GetComponent<MeshFilter>().sharedMesh = EdgeMesh(new Vector3(0, 0, -sphereRadius), hexRadius, edgeRatio);
    }
    
    public void SetDrawingData(string textureName, Material edgeMaterial)
    {
        material = new Material(Shader.Find("Unlit/Texture"));
        string path = Application.dataPath + "/Resources/Jacket/" + textureName;
        StartCoroutine("TextureSetting", path);
        edge.GetComponent<MeshRenderer>().material = material;
    }

    public void SetRotate(Vector3 vec)
    {
        gameObject.transform.rotation = Quaternion.Euler(vec);
    }

    IEnumerator TextureSetting(string texturePath)
    {
        var wr = new UnityWebRequest(texturePath);
        var texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        yield return wr.SendWebRequest();
        if(wr.isNetworkError || wr.isHttpError)
        {
            Debug.LogError(wr.error);
            yield break;
        }
        material.SetTexture("_MainTex", texDl.texture);
        gameObject.GetComponent<MeshRenderer>().material = material;
        gameObject.GetComponent<Mesh>().SetUVs(0, CalcHexUVs(texDl.texture));
    }

    List<Vector2> CalcHexUVs(Texture2D texture)
    {
        int radius;
        if (texture.height < texture.width) radius = texture.height;
        else radius = texture.width;
        Vector2 center = new Vector2(0.5f, 0.5f);
        List<Vector2> UVs = new List<Vector2>();
        UVs.Add(center);
        for(int i = 0; i < 6; i++)
        {
            float rad = (90f - (360f / 6) * i) * Mathf.Deg2Rad;
            float x = radius / texture.width  * Mathf.Cos(rad);
            float y = radius / texture.height * Mathf.Sin(rad);
            UVs.Add(center + new Vector2(x, y));
        }
        return UVs;
    }

    private Mesh EdgeMesh(Vector3 center, float hexRadius, float edgeRatio)
    {
        List<int> index = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            index.Add(i);
            index.Add(i + 1 < 6 ? i + 1 : 0);
            index.Add(i + 6);
            index.Add(i + 6);
            index.Add(i + 1 < 6 ? i + 1 : 0);
            index.Add(i + 1 < 6 ? i + 7 : 6);
        }

        List<Vector3> vertices = new List<Vector3>();
        vertices.AddRange(CalcVertices(center, hexRadius, 6));
        vertices.RemoveAt(0);
        vertices.AddRange(CalcVertices(center, hexRadius*(1f - edgeRatio), 6));
        vertices.RemoveAt(6);

        var ret = new Mesh();
        ret.SetVertices(vertices);
        ret.SetTriangles(index.ToArray(), 0);
        return ret;
    } 

}
