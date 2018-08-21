using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class Flower : MonoBehaviour
    {
        [SerializeField,Tooltip("触れたときに増えるタネの数")]
        int addSeedNum;
        [SerializeField]
        Sprite seedGotSprite;
        bool seedWasGot;//タネを取得した後か

        [SerializeField]
        private AudioClip bloomingSE;

        void Start()
        {
            GetComponent<AudioSource>().PlayOneShot(bloomingSE);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && !seedWasGot)
            {
                print("FlowerHit");
                GetComponent<SpriteRenderer>().sprite = seedGotSprite;
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