using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;
using Naka;
using UnityEngine.UI;

namespace Neno.Scripts
{
    public class StageManager : MonoBehaviour
    {

        [SerializeField] private Player player;
        [SerializeField] private Canvas uiCanvas;

        private Animator uiAnimator;
        private SeedsStack seedsStack;
        private Slider hpSlider;

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
            this.player.SavePlayerStatus();
            //uiがシュっと出てきます。
            RectTransform panelTransform = uiCanvas.transform.Find("Panel") as RectTransform;
            Animator panelAnimator = panelTransform.GetComponent<Animator>();
            panelAnimator.SetTrigger("StageClear");

            TimerUI timerUI = uiCanvas.transform.Find("Timer").GetComponent<TimerUI>();
            timerUI.PlayerGoal();
        }

        public void ChangeHp(float hp)
        {
            this.hpSlider.value = hp;
        }

        public void GameOver()
        {
            RectTransform panelTransform = uiCanvas.transform.Find("Panel") as RectTransform;
            Animator uiAnimator = panelTransform.GetComponent<Animator>();
            uiAnimator.SetTrigger("StageClear");
        }

        // Use this for initialization
        void Awake()
        {
            uiAnimator = uiCanvas.GetComponent<Animator>();
            seedsStack = uiCanvas.GetComponentInChildren<SeedsStack>();
            this.hpSlider = uiCanvas.GetComponentInChildren<Slider>();
            //3,2,1,start!みたいなアニメーションを作成する。
            uiAnimator.SetTrigger("StartCountDown");
        }

        void Start()
        {
            hpSlider.maxValue = PlayerStatusModel.Instance.PlayerMaxHp;
            hpSlider.minValue = 0;
            hpSlider.value = PlayerStatusModel.Instance.PlayerHp;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
