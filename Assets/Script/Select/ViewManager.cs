using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : SelectMain {

    [SerializeField]
    Material[] edgeMaterials = new Material[4];
    [SerializeField]
    GameObject buttonFab;

    GameObject levelText;
    GameObject titleText;

    static readonly float hexRadius = 10f;
    static float sphereRadius;
    int devide = 15;
    int center;

    // Start is called before the first frame update
    void Start()
    {
        sphereRadius = hexRadius * Mathf.Cos((360f / devide) * Mathf.Deg2Rad)
                                 / Mathf.Tan((360f / devide / 2) * Mathf.Deg2Rad);

        levelText = GameObject.Find("LevelViewer");
        titleText = GameObject.Find("MusicTitle");

    }

    MusicList musicList;
    List<ButtonProperty> MusicButtons = new List<ButtonProperty>();

    //初回呼び出し限定
    void SetUp(MusicList list, int sortMode)
    {
        center = 1;
        musicList = list;
        Sort(sortMode);
    }

    void Sort(int sortMode)
    {
        List<ButtonProperty> list = new List<ButtonProperty>();
        ButtonProperty selected;
        if (MusicButtons != null) selected = MusicButtons[center];
        else selected = null;
        switch (sortMode) {

            default:
                //0~2を想定,それ以外はエラー
                if (sortMode < 0 || 2 < sortMode) break;
                for (int i = 0; i < musicList.music.Length; i++)
                {
                    if (musicList.music[i].difficulty[sortMode].Level != 0)
                    {
                        ButtonProperty button = new ButtonProperty();
                        button.dataIndex = i;
                        button.difficulty = sortMode;
                        list.Add(button);
                    }
                }
                break;
        }
    }

    GameObject CreateButton(Vector3 normal, string texture, Material edgeMaterial)
    {
        GameObject ret = Instantiate(buttonFab) as GameObject;
        ret.GetComponent<Button>().SetRotate(normal);
        ret.GetComponent<Button>().SetDrawingData(texture, edgeMaterial);
        return ret;
    }
    

    public static float GetHexRadius()
    {
        return hexRadius;
    }

    public static float GetSphereRadius()
    {
        return sphereRadius;
    }

    public int[] GetSelectMusic()
    {
        int[] ret = new int[2];
        return ret;
    }

}
