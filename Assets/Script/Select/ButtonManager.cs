using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : SelectMain {

    [SerializeField]
    float HexRadius;

    public MusicList musicList;
    public List<Material> ButtonSurface = new List<Material>();
    int devide = 15;
    //private ButtonMaterial Button;
    GameObject[] MusicButton;


    public void SetUp(int center, int difficulty, MusicList musicList) {
      
        float CircleRadius = HexRadius * Mathf.Cos((360f / devide) * Mathf.Deg2Rad) / Mathf.Tan((360f / devide / 2) * Mathf.Deg2Rad);
        
        //music button
        MusicButton = new GameObject[musicList.music.Length + 2];
        for (int i = 0; i < musicList.music.Length + 2; i++)
        {
            MusicButton[i] = Instantiate(HexBase) as GameObject;
            MusicButton[i].GetComponent<MusicButton>().SetUpButton(i, center, devide, HexRadius, CircleRadius, difficulty);
        }

        //menubutton
        float HexCenterDistance = HexRadius * Mathf.Sin(60 * Mathf.Deg2Rad) * 2;
        GameObject[] MenuButton = new GameObject[4];
        for (int i = 0; i < 6; i++)
        {
            int j = 0;
            if (i != 0 && i != 3)
            {
                float rad = (60f * i) * Mathf.Deg2Rad;
                float cx = HexCenterDistance * Mathf.Cos(rad);
                float cy = HexCenterDistance * Mathf.Sin(rad);

                MenuButton[j++] = CreateHexagon(new Vector3(cx, cy, -CircleRadius), HexRadius - 0.1f);
                //MenuButton[j].name = "MenuButton";
            }
        }
        gameObject.GetComponent<CanvasManager>().PositionText(new Vector3(0, 0, -CircleRadius), HexCenterDistance);
    }

    public void Replace(int center)
    {
        foreach (GameObject Button in MusicButton)
            Button.GetComponent<MusicButton>().ReDrawButton(center);

    }

    public void ChangeDifficulty(int difficulty) {
        foreach (GameObject Button in MusicButton)
            Button.GetComponent<MusicButton>().ChangeDifficulty(difficulty);
    }



}
