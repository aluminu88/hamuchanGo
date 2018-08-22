using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace Neno.Scripts
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// 発射するための種
        /// </summary>
        [SerializeField] private GameObject sheed2Create;

        //c#でスネークケースはだめぇぇ
        [SerializeField] private GameObject hamu_throwing_cut;

        [SerializeField] private AudioClip jumpSE;

        [SerializeField] private AudioClip seedthrowSE;

        [SerializeField] private AudioClip seedgetSE;

        [SerializeField] private AudioClip EatingSE;

        [SerializeField] private AudioClip DamagedSE;

        [SerializeField] private AudioClip GameOverSE;


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

        [SerializeField] ContactFilter2D filter2d;

        [SerializeField] private GameObject jumpEffect;

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
        void Awake()
        {

            isPlay = false;

            this.playeRigidbody = gameObject.GetComponent<Rigidbody2D>();

            animator = GetComponent<Animator>();

            this.SheedNum = PlayerStatusModel.Instance.SeedNum;
            this.Hp = PlayerStatusModel.Instance.PlayerHp;
            this.maxSeedNum = PlayerStatusModel.Instance.MaxSeedNum;

            PlayerStatusModel.Instance.PlayerMaxHp = this.hpMax;

            //SceneManager.sceneLoaded += (scene, mode) =>
            //{
            //    this.SheedNum = PlayerStatusModel.Instance.SeedNum;
            //    this.Hp = PlayerStatusModel.Instance.PlayerHp;
            //    this.maxSeedNum =
            //};

            //SceneManager.sceneUnloaded += scene =>
            //{
            //    PlayerStatusModel.Instance.SeedNum = this.SheedNum;
            //    PlayerStatusModel.Instance.PlayerHp = this.Hp;
            //};
        }

        /// <summary>
        /// iosとandroidの両方に対応している操作に関する関数。
        /// </summary>
        void MobileControl()
        {
            if (!this.isPlay)
            {
                return;
            }

            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

            var jumpVelocity = new Vector3(0, JumpPower, 0);
            var isTouched = this.playeRigidbody.IsTouching(filter2d);

            //ジョイスティックの入力が小さすぎたら無視
            if (0.1f < Mathf.Abs(horizontal))
            {
                horizontal /= 10;
                float jumpVelocityHorizontal = horizontal * 5;

                jumpVelocity.x = jumpVelocityHorizontal;

                Vector3 addPos = new Vector3(horizontal, 0f, 0f);

                if (isGround)
                {
                    this.playeRigidbody.transform.position = this.playeRigidbody.transform.position + new Vector3(horizontal, 0, 0);

                    this.playeRigidbody.transform.right = new Vector3(horizontal, 0, 0);
                    animator.SetBool("stop", isTouched);
                }
                else
                {
                    if (0 < horizontal)
                    {
                        if (playeRigidbody.velocity.x < 10)
                        {
                            this.playeRigidbody.velocity += new Vector2(0.2f, 0);
                        }
                    }
                    else if (0 > horizontal)
                    {
                        if (-10 < playeRigidbody.velocity.x)
                        {
                            this.playeRigidbody.velocity += new Vector2(-0.2f, 0);

                        }
                    }
                }
            }else{
                animator.SetBool("stop", isTouched);
            }


            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                if (isTouched)
                {
                    if(isGround){
                        this.isGround = false;
                        Instantiate(jumpEffect, (Vector2)transform.position - 0.7f * Vector2.up, jumpEffect.transform.rotation);
                        this.playeRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                        GetComponent<AudioSource>().PlayOneShot(jumpSE);
                    }
                }
            }


            animator.SetBool("jump", !isTouched);

            //球を発射
            if (CrossPlatformInputManager.GetButtonDown("Attack"))
            {
                ShootSeed();
                hamu_throwing_cut.SetActive(true);
            }
            else{
                hamu_throwing_cut.SetActive(false);
            }

        }

        void FixedUpdate()
        {
#if UNITY_IOS
            MobileControl();
#elif UNITY_ANDROID
            MobileControl();
#else

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
                var isTouched = this.playeRigidbody.IsTouching(filter2d);
                if (isTouched)
                {
                    this.isGround = !this.isGround;
                    Instantiate(jumpEffect, (Vector2)transform.position - 0.7f * Vector2.up, jumpEffect.transform.rotation);
                    this.playeRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                    GetComponent<AudioSource>().PlayOneShot(jumpSE);
                }
            }

            animator.SetBool("jump", !isGround);



            if (Input.GetKey(KeyCode.Z) )
            {
                hamu_throwing_cut.SetActive(true);
            }
            else hamu_throwing_cut.SetActive(false);

            animator.SetBool("stop", isGround && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)));

            //球を発射
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShootSeed();
            }
#endif
        }//fixedUpdate

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
            this.Hp -= this.DecreaseHpSpeed * Time.deltaTime;
            if (this.Hp <= 0)
            {
                if (HasSheeds())
                {
                    this.Hp = this.hpMax;
                    GetComponent<AudioSource>().PlayOneShot(EatingSE);
                    this.SheedNum--;
                }
                else
                {
                    //gameOver
                    GetComponent<AudioSource>().PlayOneShot(GameOverSE);
                    Camera.main.GetComponent<AudioSource>().Stop();
                    animator.SetTrigger("GameOver");
                    stageManager.GameOver();
                }
            }
            stageManager.ChangeHp(this.Hp);
        }

        public void SavePlayerStatus()
        {
            PlayerStatusModel.Instance.SeedNum = this.SheedNum;
            PlayerStatusModel.Instance.PlayerHp = this.Hp;
        }

        void ShootSeed()
        {
            if (this.SheedNum > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(seedthrowSE);
                this.SheedNum--;
                GameObject sheed = Instantiate(this.sheed2Create, this.transform.position, Quaternion.identity);
                sheed.transform.right = transform.right;
            }
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
                    GetComponent<AudioSource>().PlayOneShot(seedgetSE);
                    this.SheedNum += 1;
                    Destroy(col.transform.gameObject);
                }
            }
        }

        public void damade(float damagepower,Transform enemytransform)
        {
            animator.SetTrigger("damaged");
            GetComponent<AudioSource>().PlayOneShot(DamagedSE);
            var damagevector = (this.transform.position - enemytransform.position).normalized;
            this.playeRigidbody.AddForce(damagevector * damagepower, ForceMode2D.Impulse);
        }

    }
}
