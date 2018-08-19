using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class IkaDamagedEffect : MonoBehaviour
    {
        [SerializeField,Tooltip("消えるまでの時間")]
        float destroyTime;
        [SerializeField,Tooltip("吹き飛ぶスピード")]
        Vector2 speed;

        SpriteRenderer spriteRenderer;
        Rigidbody2D rb;
        float timer = 0;
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(speed, ForceMode2D.Impulse);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if(destroyTime <= timer) { Destroy(gameObject); return; }
            float rate = timer / destroyTime;
            Color col = spriteRenderer.color;
            col.a = 1 - rate;
            spriteRenderer.color = col;
        }
    }
}