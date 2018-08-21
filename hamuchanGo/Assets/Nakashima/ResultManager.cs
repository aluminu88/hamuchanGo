using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Neno.Scripts;

namespace Naka
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField]
        GameObject node;
        [SerializeField]
        bool test;

        List<GameObject> nodelist = new List<GameObject>();
        void Start()
        {
            if(test)Test();
            var currenRecord = ScoreManager.Instance.StageScoreList;
            if (currenRecord == null||currenRecord.Count==0) { return; }
            var savedRecord = ScoreManager.Instance.GetSavedRecord(currenRecord.Count);
            nodelist.Add(node);
            for (int i = 1; i < currenRecord.Count; i++)
            {
                var instNode = Instantiate(node, node.transform.parent);
                nodelist.Add(instNode);
            }
            for (int i = 0; i < nodelist.Count; i++)
            {
                var text = nodelist[i].GetComponentsInChildren<Text>();
                text[0].text = GetRoundupFloat(currenRecord[i]);
                text[1].text = GetRoundupFloat(savedRecord[i]);
            }
            ScoreManager.Instance.SaveRecord(currenRecord);
            ScoreManager.Instance.ClearStageScore();
        }

        string GetRoundupFloat(float num)
        {
            float tmp = num * 100;
            tmp = Mathf.Floor(tmp);
            tmp /= 100;
            var str = tmp.ToString().Split('.');
            if (str.Length == 1)//万が一,X秒00だった時用
            {
                return str[0] + ":00";
            }
            else
            {
                if (str[1].Length == 1) { str[1] += "0"; }//桁数がずれる対策
                return str[0] + ":" + str[1];
            }
        }

        [ContextMenu("ResetPrefas")]
        void ReSetPrefas()
        {
            ScoreManager.Instance.ReSetPrefas();

        }

        [ContextMenu("Test")]
        void Test()
        {
            ScoreManager.Instance.Test();
        }
    }
}