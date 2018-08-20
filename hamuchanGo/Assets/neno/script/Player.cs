using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Neno.Scripts
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// 発射するための種
        /// </summary>
        [SerializeField] private GameObject sheed2Create;

        //[SerializeField] private int SheedMaxNum = 10;
        private int SheedMaxNum = 10;

        [SerializeField]
        private StageManager stageManager;

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
                stageManager.ChangePlayersSeeds(seedNum);
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
            isPlay = false;

            this.playeRigidbody = gameObject.GetComponent<Rigidbody2D>();

            animator = GetComponent<Animator>();

            this.SheedNum = PlayerStatusModel.Instance.SeedNum;
            this.Hp = PlayerStatusModel.Instance.PlayerHp;
            this.maxSeedNum = PlayerStatusModel.Instance.MaxSeedNum;
            
            //SceneManager.sceneLoaded += (scene, mode) =>
            //{
            //    this.SheedNum = PlayerStatusModel.Instance.SeedNum;
            //    this.Hp = PlayerStatusModel.Instance.PlayerHp;
            //    this.maxSeedNum = 
            //};

            SceneManager.sceneUnloaded += scene =>
            {
                PlayerStatusModel.Instance.SeedNum = this.SheedNum;
                PlayerStatusModel.Instance.PlayerHp = this.Hp;
            };
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

        }

        bool HasSheeds()
        {
            return 0 <= this.SheedNum ? true : false;
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
                    SceneManager.LoadScene("GameOver");
                }
            }

            //球を発射
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShootSeed();
            }
        }

        void ShootSeed()
        {
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
