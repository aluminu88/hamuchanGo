using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Neno.Scripts
{
    public class HowToPlayer : MonoBehaviour
    {
        /// <summary>
        /// 発射するための種
        /// </summary>
        [SerializeField] private GameObject sheed2Create;


        /// <summary>
        /// ハムちゃんのスタミナ
        /// </summary>
        [SerializeField] private int hpMax = 1000;

        /// <summary>
        /// デフォルトで5秒間でハムちゃんのHPは全損200/s
        /// </summary>
        [SerializeField] private int DecreaseHpSpeed = 200;

        [SerializeField] private float JumpPower = 5;

        public bool isPlay { get; set; }

        private Rigidbody2D playeRigidbody;
        private bool isGround = false;

        private int seedNum;
        /// <summary>
        /// ハムちゃんが今持っている種の数
        /// </summary>
        public int SheedNum
        {
            get { return seedNum; }

            set
            {
                seedNum = value;
                if (10 < seedNum)
                {
                    seedNum = 10;
                }
            }
        }

        /// <summary>
        /// Hpが全損すると死んでしまうよ！ｗ
        /// </summary>
        public float Hp { get; set; }

        private int maxSeedNum = 10;

        private Animator animator;

        // Use this for initialization
        void Start()
        {
            isPlay = true;
            //isPlay = false;

            this.playeRigidbody = gameObject.GetComponent<Rigidbody2D>();

            animator = GetComponent<Animator>();
        }
        
        void FixedUpdate()
        {
            if (!this.isPlay)
            {
                return;
            }
            var jumpVelocity = new Vector3(0, JumpPower, 0);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (isGround)
                {
                    this.playeRigidbody.transform.position = this.playeRigidbody.transform.position + new Vector3(0.1f, 0, 0);
                    this.playeRigidbody.transform.right = new Vector3(1, 0, 0);
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
                    this.playeRigidbody.transform.right = new Vector3(-1, 0, 0);
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
                if (isGround)
                {
                    this.isGround = !this.isGround;

                    this.playeRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
                Debug.Log("Jump!!!");
            }

            animator.SetBool("jump", !isGround);

            //球を発射
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShootSeed();
            }
        }

        bool HasSheeds()
        {
            return 0 < this.SheedNum ? true : false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.isPlay)
            {
                return;
            }
        }

        const int Seedmax = 50;//多量生成で重くならないように
        int seedCount;
        void ShootSeed()
        {
            if(Seedmax <= seedCount) { return; }
            seedCount++;
            GameObject sheed = Instantiate(this.sheed2Create, this.transform.position, Quaternion.identity);
            sheed.transform.right = transform.right;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            var layerName = LayerMask.LayerToName(col.gameObject.layer);
            if (layerName == "GroundLayer")
            {
                this.isGround = true;
            }

            if (col.transform.CompareTag("Seed"))
            {
                if (this.SheedNum < 10)
                {
                    this.SheedNum += 1;
                    Destroy(col.transform.gameObject);
                }
            }
        }
    }
}
