using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : PlayMain {

    Text ComboText;
    Text ScoreText;
    Slider ScoreSlider;
    List<Color> SliderColor = new List<Color>();
    Image FillImage;
    int Rank;

	// Use this for initialization
	void Start () {
        ComboText = GameObject.Find("Combo").GetComponent<Text>();
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        ScoreSlider = GameObject.Find("Slider").GetComponent<Slider>();
        FillImage = GameObject.Find("Fill").GetComponent<Image>();
        SliderColor.Add(new Color(0.6f, 0.6f, 0.6f)); //D
        SliderColor.Add(new Color(0.0f, 0.7f, 0.85f)); //C
        SliderColor.Add(new Color(0.0f, 1.0f, 0.0f)); //B
        SliderColor.Add(new Color(1.0f, 0.0f, 0.8f)); //A 
        SliderColor.Add(new Color(1.0f, 1.0f, 0.1f)); //S yellow
        FillImage.color = SliderColor[0];
        Rank = 0;
    }
	
    public void Chenge(int Score, int Combo)
    {
        ScoreSlider.value = Score < TheoryScore ? Score / TheoryScore : 1.0f;
        if(Rank != 4 && RankBorder[Rank] < Score)
        {
            Rank++;
            FillImage.color = SliderColor[Rank];  
        }
        ScoreText.text = Score.ToString();
        ComboText.text = (Combo.ToString() + "\nCombo");
    }
}
