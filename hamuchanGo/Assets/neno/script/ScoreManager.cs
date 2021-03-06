﻿using System.Collections;
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
            StageScoreList = new List<float>();
        }

        public List<float> GetSavedRecord(int stageNum)
        {
            var list = new List<float>();
            for(int i = 0; i < stageNum; i++)
            {
                list.Add(PlayerPrefs.GetFloat(i.ToString()));
            }
            return list;
        }

        public void SaveRecord(List<float> newRecord)
        {
            for (int i = 0; i < newRecord.Count; i++)
            {
                var record = PlayerPrefs.GetFloat(i.ToString());
                if (newRecord[i] < record||record==0)
                {
                    PlayerPrefs.SetFloat(i.ToString(),newRecord[i]);
                }
            }
        }

        public void ResetStageScore()
        {

        }

        public void ReSetPrefas()
        {
            for (int i = 0; i < 100; i++)
            {
                PlayerPrefs.SetFloat(i.ToString(), 0f);
            }
        }

        public void Test()
        {
            var list = new List<float> { Random.Range(0, 100f), Random.Range(0, 100f), Random.Range(0, 100f), Random.Range(0, 100f) };
            SaveRecord(list);
            StageScoreList = new List<float> { Random.Range(0, 100f), Random.Range(0, 100f), Random.Range(0, 100f), Random.Range(0, 100f) };
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


