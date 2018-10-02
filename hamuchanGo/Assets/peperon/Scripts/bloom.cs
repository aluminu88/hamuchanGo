using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloom : MonoBehaviour {

    [SerializeField,Tooltip("花の咲く音")]
    private AudioClip bloomingSE;

    void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(bloomingSE);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
