using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Neno.Scripts
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// 発射するための種
        /// </summary>
        [SerializeField] private GameObject sheed2Create;

        [SerializeField] private int SheedMaxNum = 10;

        /// <summary>
        /// ハムちゃんのスタミナ
        /// </summary>
        [SerializeField] private int hpMax = 1000;

        /// <summary>
        /// デフォルトで5秒間でハムちゃんのHPは全損200/s
        /// </summary>
        [SerializeField] private int DecreaseHpSpeed = 200;

        private Rigidbody2D playeRigidbody;
        private bool isGround = false;
        /// <summary>
        /// ハムちゃんが今持っている種の数
        /// </summary>
        public int SheedNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Hp { get; set; }


        // Use this for initialization
        void Start()
        {
            this.playeRigidbody = gameObject.GetComponent<Rigidbody2D>();
            Hp = hpMax;
        }

        void FixedUpdate()
        {

            var jumpVelocity = new Vector3(0, 10, 0);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (isGround)
                {
                    this.playeRigidbody.transform.position = this.playeRigidbody.transform.position + new Vector3(0.1f, 0, 0);
                    jumpVelocity += new Vector3(5, 0, 0);
                }
                else
                {
                    if (playeRigidbody.velocity.x < 10)
                    {
                        this.playeRigidbody.velocity += new Vector2(0.2f, 0);
                    }
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (isGround)
                {
                    this.playeRigidbody.transform.position = this.playeRigidbody.transform.position + new Vector3(-0.1f, 0, 0);
                    jumpVelocity += new Vector3(-5, 0, 0);
                }
                else
                {
                    if (-10 < Mathf.Abs(playeRigidbody.velocity.x))
                    {
                        this.playeRigidbody.velocity += new Vector2(-0.2f, 0);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(isGround);
                if (isGround)
                {
                    this.isGround = !this.isGround;

                    this.playeRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
                Debug.Log("Jump!!!");
            }

        }

        bool HasSheeds()
        {
            return this.SheedNum <= 0 ? true:false;
        }

        // Update is called once per frame
        void Update()
        {
            this.Hp -= this.DecreaseHpSpeed * Time.deltaTime;
            if (this.Hp <= 0)
            {
                if (HasSheeds())
                {
                    this.Hp = this.hpMax;
                }
                else
                {
                    //gameOver

                }
            }

            //球を発射
            if (Input.GetKey(KeyCode.Z))
            {
                ShootSeed();
            }
        }

        void ShootSeed()
        {
            GameObject sheed = Instantiate(this.sheed2Create, this.transform.position, Quaternion.LookRotation(this.transform.right));
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.CompareTag("Ground"))
            {
                this.isGround = true;
            }
        }

    }
}
