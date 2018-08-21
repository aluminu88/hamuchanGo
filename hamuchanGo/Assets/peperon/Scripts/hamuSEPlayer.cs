using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamuSEPlayer : MonoBehaviour {

    public AudioClip nowSE;

    void OnMouseEnter()
    {
        GetComponent<AudioSource>().PlayOneShot(nowSE);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
