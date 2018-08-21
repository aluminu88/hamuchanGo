using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;
using Naka;

namespace Neno.Scripts
{
    public class StageManager : MonoBehaviour
    {

        [SerializeField] private Player player;
        [SerializeField] private Canvas uiCanvas;

        private Animator uiAnimator;
        private SeedsStack seedsStack;

        public void NotifyEndCountdown()
        {
            this.player.isPlay = true;
            //uiCanvasのTimerを取得してタイマーをスタートする。
            TimerUI timerUi = uiCanvas.GetComponentInChildren<TimerUI>();
            timerUi.NotifyEndCountdown();
        }


        public void ChangePlayersSeeds(int seedsNum)
        {
            seedsStack.SetSeeds(seedsNum);
        }

        public void ClearStage()
        {
            this.player.isPlay = false;

        }

        // Use this for initialization
        void Awake()
        {
            uiAnimator = uiCanvas.GetComponent<Animator>();
            seedsStack = uiCanvas.GetComponentInChildren<SeedsStack>();
            //3,2,1,start!みたいなアニメーションを作成する。
            uiAnimator.SetTrigger("StartCountDown");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
