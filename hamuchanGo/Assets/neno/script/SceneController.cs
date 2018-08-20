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
            SceneManager.LoadScene(nextScene);
        }

        public void GotoTitleScene()
        {
            SceneManager.LoadScene(this.tile);
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

