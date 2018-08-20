using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

namespace Neno.Scripts
{
    /// <summary>
    /// ゲーム全体のスコアを管理するシングルトン。MonoBehaviourは継承してないシンプルなもの。
    /// </summary>
    public class ScoreManager : Singleton<ScoreManager>
    {

        public ScoreManager()
        {
            this.StageScoreList = new List<float>();
        }

        //トータルのスコア
        public float TotalScore { get; private set; }

        public float CurrentStageScore { get; private set; }

        public List<float> StageScoreList { get; private set; }

        public float CalcStageScore()
        {
            float stageScore = 0;
            //なんか計算する
            CurrentStageScore = stageScore;
            return stageScore;
        }


        public float CalcTotalScore()
        {
            float totalScore = 0;
            //なんか計算する;
            return totalScore;
        }

        public void ClearStageScore()
        {

        }

        public void ClearAllScore()
        {
            CurrentStageScore = 0;
            TotalScore = 0;
        }

        /// <summary>
        /// ステージが始まったら呼ばれるようにしておく？
        /// </summary>
        public void StartStage()
        {

        }

        /// <summary>
        /// 1ステージ終わったら
        /// </summary>
        public void EndStage()
        {

        }
    }
}


