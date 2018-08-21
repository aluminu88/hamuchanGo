using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private Animator uiAnimator;
    [SerializeField]
    private Naka.TimerUI timerUI;

    [SerializeField] private StageManager stageManager;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            //ゴール！
            uiAnimator.SetTrigger("StageClear");
            timerUI.PlayerGoal();
            stageManager.ClearStage();
        }
    }
}
