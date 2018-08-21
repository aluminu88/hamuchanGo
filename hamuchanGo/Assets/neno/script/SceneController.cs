using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Neno.Scripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private string nextScene;
        private string tile = "Title";

        [SerializeField] private AudioClip buttonSE;


        public void GotoNextScene()
        {
            GetComponent<AudioSource>().PlayOneShot(buttonSE);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        public void GotoTitleScene()
        {
            GetComponent<AudioSource>().PlayOneShot(buttonSE);
            SceneManager.LoadScene(this.tile);
        }

        public void GotoResultScene()
        {
            GetComponent<AudioSource>().PlayOneShot(buttonSE);
            SceneManager.LoadScene("Result");
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

