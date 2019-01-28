using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MainRoot {

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator enumerator()
    {
        if (Input.anyKey)
        {
            
        }
        yield return new WaitForSeconds(fps);
    }    

}
