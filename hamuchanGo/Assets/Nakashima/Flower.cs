using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class Flower : MonoBehaviour
    {
        [SerializeField,Tooltip("触れたときに増えるタネの数")]
        int addSeedNum;

        bool seedWasGot;//タネを取得した後か
        void Start()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && !seedWasGot)
            {
                print("FlowerHit");
                var player = collision.GetComponent<Neno.Scripts.Player>();
                if (!player)//一応チェック
                {
                    Debug.LogWarning("Playerタグはあるがスクリプトが取得できません");
                    return;
                }
                player.SheedNum += addSeedNum;
                seedWasGot = true;
            }
        }
    }
}