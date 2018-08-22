using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlecontroller_HowTo : MonoBehaviour {

    [SerializeField]
    private AudioClip buttonSE;

    public void OnClickStartButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        SceneManager.LoadScene("HowTo");
    }
}
