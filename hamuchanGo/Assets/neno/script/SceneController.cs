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

        public void GotoNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        public void GotoTitleScene()
        {
            SceneManager.LoadScene(this.tile);
        }

        public void GotoResultScene()
        {
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

