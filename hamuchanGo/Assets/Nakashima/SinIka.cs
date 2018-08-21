using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class SinIka : MonoBehaviour
    {
        [SerializeField, Tooltip("プレイヤーに接触した時に奪う種の量")]
        int steelSeedNum;
        [SerializeField, Tooltip("時機に接触した時消えるかどうか")]
        bool hitDestroy;
        [SerializeField, Tooltip("タネに当たった時のエフェクト")]
        GameObject damagedEffect;
        [SerializeField, Tooltip("ハムちゃんにダメージを与えたときのエフェクト")]
        GameObject steelParticle;
        [SerializeField, Tooltip("波の速さ")]
        float speed = 1;
        [SerializeField, Tooltip("Sinの振幅")]
        float amplitude = 1;
        [SerializeField, Tooltip("波長")]
        float waveLength = 1;

        Rigidbody2D rb;
        Vector2 firstPosition;
        float timer = 0;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            firstPosition = transform.position;
        }

        void Update()
        {
            timer += Time.deltaTime;
            Vector2 vec = GetSinVector(timer);
            rb.MovePosition(firstPosition + vec);
        }

        Vector2 GetSinVector(float timer)
        {
            Vector2 vec = new Vector2(0, 0);
            vec.x = timer * speed;
            vec.y = Mathf.Sin(vec.x / waveLength) * amplitude;
            if (transform.right.x < 0) { vec.x *= -1; }
            return vec;
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

        /// <summary>
        /// 見やすくするよう
        /// </summary>
        void OnDrawGizmos()
        {
            const int drawCount = 40;
            const float drawInterval = 0.5f;

            Vector2 firstPos = transform.position;
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                firstPos = firstPosition;
            }
#endif
            Vector2 currentPos = firstPos;
            Vector2 nextPos = firstPos + GetSinVector(1f * drawInterval);
            for (int i = 0; i < drawCount; i++)
            {
                Gizmos.DrawLine(currentPos, nextPos);
                currentPos = nextPos;
                nextPos = firstPos + GetSinVector((i + 1f) * drawInterval);
            }
        }
    }
}