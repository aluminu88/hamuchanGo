using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlecontroller_HowTo : MonoBehaviour {

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("HowTo");
    }
}
