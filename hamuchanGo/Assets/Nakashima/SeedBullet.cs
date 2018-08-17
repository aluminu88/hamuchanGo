﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class SeedBullet : MonoBehaviour
    {
        [SerializeField,Tooltip("発射した時のスピード")]
        float shotVelocity;
        [SerializeField, Tooltip("発射した時の回転スピード(演出)")]
        float shotAngularVelocity;
        [SerializeField,Tooltip("種が床に当たった時に生成するめ")]
        GameObject sprout;

        public Transform test;
        Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * shotVelocity, ForceMode2D.Impulse);
            rb.AddTorque(shotAngularVelocity, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                InstSprout(collision);
            }
        }

        void InstSprout(Collision2D collision)
        {
            //接触点
            var contactPoint = collision.contacts[0].point;
            //接触点へのベクトル
            var contactDist = contactPoint - (Vector2)transform.position;
            contactDist.Normalize();

            float radius = Mathf.Atan2(contactDist.x, contactDist.y) * Mathf.Rad2Deg;
            if (135f <= Mathf.Abs(radius))//絶対値が135度以上なら床に当たっているとする
            {
                var instSprout = Instantiate(sprout, contactPoint, Quaternion.identity);
                print("床");
                Destroy(gameObject);
            }
            else
            {
                const float WallInstAngle = 30f;
                var instSprout = Instantiate(sprout, contactPoint, Quaternion.identity);
                if (0 <= contactDist.x)
                {
                    instSprout.transform.Rotate(0, 0, WallInstAngle);
                }
                else
                {
                    instSprout.transform.Rotate(0, 0, -WallInstAngle);
                }
                print("壁");
                Destroy(gameObject);
            }
        }
    }
}