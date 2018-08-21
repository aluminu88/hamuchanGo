using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka {
    public class NormalIka : MonoBehaviour {
        [SerializeField]
        float moveSpeed;
        [SerializeField,Tooltip("プレイヤーに接触した時に奪う種の量")]
        int steelSeedNum;
        [SerializeField,Tooltip("時機に接触した時消えるかどうか")]
        bool hitDestroy;
        [SerializeField, Tooltip("タネに当たった時のエフェクト")]
        GameObject damagedEffect;
        [SerializeField,Tooltip("ハムちゃんにダメージを与えたときのエフェクト")]
        GameObject steelParticle;
        

        Transform player;
        void Start() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update() {
            Vector2 vec = player.position - transform.position;
            vec.Normalize();
            transform.Translate(vec * moveSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Neno.Scripts.Player>().SheedNum -= steelSeedNum;
                var pos = collision.transform.position;
                Instantiate(steelParticle, pos, Quaternion.identity);
                if (hitDestroy)
                {
                    Death();
                }
            }
            //タグで検知のほうが良いがBulletタグを作ってなかったので
            //これで行く
            //と思ったけどSeedが作られた，しかし，変えると不安なのでこのまま
            if (collision.GetComponent<SeedBullet>())
            {
                Instantiate(damagedEffect, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Death();
            }
        }

        void Death()
        {
            Destroy(gameObject);
        }
    }
}