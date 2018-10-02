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
        [SerializeField]
        AudioClip getSound;

        bool seedWasGot;//タネを取得した後か

        bool flag = false;

        [SerializeField]
        private AudioClip bloomingSE;

        void Start()
        {
        }

        void OnEnable()
        {
            if (flag)
                transform.parent.parent.GetComponent<AudioSource>().PlayOneShot(bloomingSE);
            else flag = true;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && !seedWasGot)
            {
                print("FlowerHit");
                GetComponent<SpriteRenderer>().sprite = seedGotSprite;
                var player = collision.GetComponent<Neno.Scripts.Player>();
                transform.parent.parent.GetComponent<AudioSource>().PlayOneShot(getSound);
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