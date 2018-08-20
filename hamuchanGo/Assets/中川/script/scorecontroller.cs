using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neno.Scripts;
using UnityEngine.UI;

public class scorecontroller : MonoBehaviour {

    [SerializeField] private Text resultScore;

    void Start() {


        //GameRuleManager.Instance.TotalScore
        resultScore.text = GameRuleManager.Instance.TotalScore.ToString();

    }