﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class Sprout : MonoBehaviour
    {

        [SerializeField, Tooltip("芽が伸びる時間")]
        float sproutTime;
        [SerializeField, Tooltip("芽が伸び切ってから花が咲くまでの時間")]
        float bloomTime;
        [SerializeField, Tooltip("芽の足場の高さ")]
        float sproutHight;
        [SerializeField, Tooltip("芽から花までの高さ")]
        float flowerHight;
        [SerializeField, Tooltip("一個下の階層のLeefをアタッチする")]
        Transform childLeef;//足場の部分
        [SerializeField]
        GameObject sunFlower;

        float timer = 0;
        float firstLeefLocalY;
        void Start()
        {
            firstLeefLocalY = childLeef.localPosition.y;
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer <= sproutTime)
            {//発芽するまで
                float growRate = timer / sproutTime;
                var localPos = childLeef.localPosition;
                localPos.y = firstLeefLocalY + growRate * sproutHight;
                childLeef.localPosition = localPos;
            }
            else if (timer <= sproutTime + bloomTime)//発芽してから咲くまで
            {

            }
            else //咲くとき
            {
                var flower = Instantiate(sunFlower, transform);
                var localPos = Vector2.up * (sproutHight + flowerHight);
                flower.transform.localPosition = localPos;
            }
        }
    }
}