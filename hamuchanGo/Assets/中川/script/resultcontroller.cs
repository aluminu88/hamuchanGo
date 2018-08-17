using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultcontroller : MonoBehaviour
{

    public void OnButton()
    {
        SceneManager.LoadScene("Title");
    }

}
