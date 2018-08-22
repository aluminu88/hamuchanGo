using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultcontroller : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSE;

    public void OnButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        SceneManager.LoadScene("Title");
    }

}
