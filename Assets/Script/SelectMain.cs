﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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

    [SerializeField]
    float radius;

    MusicList musicList;

    IEnumerator Start () {
        yield return StartCoroutine("LoadList");
        foreach (Music music in musicList.music) Debug.Log(music.title);


        //このへんに描画
        float CRadius = radius * Mathf.Sin(60 * Mathf.Deg2Rad) * 2;
        GameObject[] MenuButton = new GameObject[4];
        for (int i = 0; i < 6; i++)
        {
            int j = 0;
            if (i != 0 && i != 3)
            {
                float rad = (60f * i) * Mathf.Deg2Rad;
                float cx = CRadius * Mathf.Cos(rad);
                float cy = CRadius * Mathf.Sin(rad);
                
                MenuButton[j++] = CreateHexagon(new Vector3(cx, cy, 0), radius - 0.1f);
            }
        }

        GameObject[] MusicButton = new GameObject[musicList.music.Length + 2];
        for (int i = 0; i < musicList.music.Length + 2; i++) {
            MusicButton[i] = CreateHexagon(new Vector3(CRadius * (i - 1) , 0, 0) , radius);
            MusicButton[i].GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            if (i == 0 || i == musicList.music.Length)
            {
                MusicButton[0].GetComponent<MeshRenderer>().material.SetColor("_EMISSION", new Color(0, 0, 0));
            }
        }

        while (true)
        {
            if (!(isOnKey[1]) && Input.GetKey(KeyConfig[1]))
            {
                 for(int i = 0; i < MusicButton.Length; i++)
                {
                    var mesh = MusicButton[i].GetComponent<MeshFilter>().mesh;
                    mesh.SetVertices(CalcVertices(mesh.vertices[0] - new Vector3(CRadius, 0), radius));
                }
            }
            if (!(isOnKey[4]) && Input.GetKey(KeyConfig[4]))
            {
                for (int i = 0; i < MusicButton.Length; i++)
                {
                    var mesh = MusicButton[i].GetComponent<MeshFilter>().mesh;
                    mesh.SetVertices(CalcVertices(mesh.vertices[0] + new Vector3(CRadius, 0), radius));
                }
            }


            for (int i = 0; i < KeyConfig.Length; i++) isOnKey[i] = Input.GetKey(KeyConfig[i]);
            yield return new WaitForSeconds(fps);
        }

    }





    //読み込み系統
    IEnumerator LoadList()
    {
        using (WWW www = new WWW("file:///" + Application.dataPath + "/Resources/MusicList.json"))
        {
            yield return www;
            Debug.Log(www.url);
            Debug.Log(www.isDone);
            Debug.Log(www.text);
            yield return musicList = JsonUtility.FromJson<MusicList>(www.text);
        }
        yield break;
    }

    private MusicList SortList(MusicList musicList, string type ,bool reverse)
    {
        MusicList retList = musicList;

        return retList;
    }

    //六角形系統
    [SerializeField]
    private GameObject HexBase;

    private GameObject CreateHexagon(Vector3 center, float radius)
    {
        List<int> index = new List<int>();
        for (int i = 1; i <= 6; i++)
        {
            index.Add(0);
            index.Add(i);
            index.Add((i + 1 <= 6) ? i + 1 : 1);
        }
        var mesh = new Mesh();
        mesh.vertices = CalcVertices(center,radius).ToArray();
        mesh.triangles = index.ToArray();
        GameObject Hexagon = Instantiate(HexBase) as GameObject;
        Hexagon.GetComponent<MeshFilter>().sharedMesh = mesh;

        return Hexagon;
    }

    private List<Vector3> CalcVertices(Vector3 center, float radius)
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
