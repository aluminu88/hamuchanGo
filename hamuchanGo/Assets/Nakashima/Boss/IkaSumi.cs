using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class IkaSumi : MonoBehaviour
    {
        [SerializeField]
        GameObject steelParticle;
        [SerializeField]
        bool hitDestroy = true;
        [SerializeField]
        int steelSeedNum = 1;
        [SerializeField]
        float jumpImpulse;

        float timer=0;
        Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            float randomAngle = Random.Range(90f, 180f);
            Vector2 jumpVector = new Vector2();
            jumpVector.x = Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            jumpVector.y = Mathf.Sin(randomAngle * Mathf.Deg2Rad);
            jumpVector.Normalize();
            rb.velocity = Vector2.zero;
            rb.AddForce(Random.Range( jumpImpulse/2,jumpImpulse) * jumpVector, ForceMode2D.Impulse);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (10 <= timer) { Destroy(gameObject); }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                var player = collision.GetComponent<Neno.Scripts.Player>();
                player.SheedNum -= steelSeedNum;
                player.damade(8,transform);
                var pos = collision.transform.position;
                Instantiate(steelParticle, pos, Quaternion.identity);
                if (hitDestroy)
                {
                    Death();
                }
            }
        }

        void Death()
        {
            Destroy(gameObject);
        }
    }
}