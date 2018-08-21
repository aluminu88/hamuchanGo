using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlecontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("easy1");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
