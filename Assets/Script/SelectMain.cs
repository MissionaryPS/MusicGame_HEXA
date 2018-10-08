using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectMain : MainRoot {

    [Serializable]
    public class MusicList
    {
        public Music[] music;
    }

    [Serializable]
    public class Music
    {
        public string title;
        public string artist;
        public string scoreMake;
        public Difficulty difficulty;
        public float bpm;
        public int HiScore;
        public string FileName;
        public string JacketFileName;
    }

    [Serializable]
    public class Difficulty
    {
        public LevelInfo easy;
        public LevelInfo normal;
        public LevelInfo hard;
    }

    [Serializable]
    public class LevelInfo
    {
        public int level;
        public int allTarget;
    }


    MusicList musicList;

    [SerializeField]
    float radius;

    IEnumerator Start () {
        yield return StartCoroutine("LoadList");

        //このへんに描画
        float CRadius = radius*Mathf.Sin(60 * Mathf.Deg2Rad) * 2;

        for(int i = 0; i < 6; i++)
        {
            float rad = (60f * i) * Mathf.Deg2Rad;
            float cx = CRadius * Mathf.Cos(rad);
            float cy = CRadius * Mathf.Sin(rad);
            DrawHexagon(new Vector3(cx, cy, 0), radius-0.1f);
        }

        DrawHexagon(Vector3.zero, radius);

    }

    IEnumerator LoadList()
    {
        using (WWW www = new WWW("file:///" + Application.dataPath + "/Resouces/MusicList.json"))
        {
            yield return www;
            yield return musicList = JsonUtility.FromJson<MusicList>(www.text);
        }
    }

    private MusicList SortList(MusicList musicList, string type ,bool reverse)
    {
        MusicList retList = musicList;

        return retList;
    }

    [SerializeField]
    private GameObject HexBase;

    private void DrawHexagon(Vector3 center, float radius)
    {
        List<int> index = new List<int>();
        List<Vector3> vertices = new List<Vector3>();

        vertices.Add(Vector3.zero + center);
        for(int i = 0; i < 6; i++)
        {
            float rad = (90f - 60f * i) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            vertices.Add(center + new Vector3(x, y, 0));

        }
        for(int i = 1; i <= 6; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 6) ? i + 1 : 1);
        }
        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = index.ToArray();
        GameObject Hexagon = Instantiate(HexBase) as GameObject;
        Hexagon.GetComponent<MeshFilter>().sharedMesh = mesh;

    }
     
}
