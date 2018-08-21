using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private StageManager stageManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            //ゴール！
            stageManager.ClearStage();
        }
    }
}
