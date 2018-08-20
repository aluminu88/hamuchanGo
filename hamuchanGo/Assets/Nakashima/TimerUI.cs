using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naka
{
    public class TimerUI : MonoBehaviour
    {
        public float Timer { private set; get; }
        public bool StartTimer { get; set; }

        public void NotifyEndCountdown()
        {
            StartTimer = true;
        }

        Text text;
        void Start()
        {
            text = GetComponent<Text>();
            Timer = 0;
            StartTimer = false;
        }

        void Update()
        {
            if (!StartTimer)
            {
                return;
            }
            Timer += Time.deltaTime;
            float tmp = Timer * 100;
            tmp= Mathf.Floor(tmp);
            tmp /= 100;
            var str = tmp.ToString().Split('.');
            if (str.Length == 1)//万が一,X秒00だった時用
            {
                text.text = str[0] + ":00";
            }
            else
            {
                if (str[1].Length == 1) { str[1] += "0"; }//桁数がずれる対策
                text.text = str[0] + ":" + str[1];
            }
        }
    }
}