using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private Animator uiAnimator;
    [SerializeField]
    private Naka.TimerUI timerUI;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            //ゴール！
            uiAnimator.SetTrigger("StageClear");
            timerUI.PlayerGoal();
        }
    }
}
