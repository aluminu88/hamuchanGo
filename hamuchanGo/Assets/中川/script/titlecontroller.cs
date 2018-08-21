using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlecontroller : MonoBehaviour {

    [SerializeField]
    private AudioClip buttonSE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStartButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        SceneManager.LoadScene("easy1");
    }

    public void OnClickQuitButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        Application.Quit();
    }
}
