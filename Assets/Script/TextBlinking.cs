using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MainRoot {

    Text text;
    float change = 0.025f;

	// Use this for initialization
	void Start () {
        Debug.Log("Scene:Start");
        text = gameObject.GetComponent<Text>();
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            Color now = text.color;
            //Debug.Log("a:" + now.a);
            if (text.color.a > 1.0f) text.color = new Color(now.r, now.g, now.b, 0.0f);
            else text.color = new Color(now.r, now.g, now.b, now.a + change);
            if (Input.anyKey) yield break;
            yield return new WaitForSeconds(fps);
        }
    }
	
}
