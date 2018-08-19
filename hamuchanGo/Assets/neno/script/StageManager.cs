using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

namespace Neno.Scripts
{
    public class StageManager : MonoBehaviour
    {

        [SerializeField] private Player player;
        [SerializeField] private Animator uiAnimator;

        public void NotifyEndCountdown()
        {
            this.player.isPlay = true;
        }

        // Use this for initialization
        void Start()
        {
            //3,2,1,start!みたいなアニメーションを作成する。
            uiAnimator.SetTrigger("StartCountDown");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
