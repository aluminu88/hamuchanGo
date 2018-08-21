using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naka
{
    public class IkaBoss : MonoBehaviour
    {
        [SerializeField, Tooltip("プレイヤーに接触した時に奪う種の量")]
        int steelSeedNum =1;
        [SerializeField, Tooltip("手の動くスピード")]
        float handSpeed;
        [SerializeField]
        int hp = 10;
        [SerializeField]
        float enemyInstCycle = 3;
        [SerializeField]
        float sumiInstCycle = 3;
        [SerializeField]
        Transform leftHand;
        [SerializeField]
        GameObject normalIka;
        [SerializeField]
        GameObject ikaSumi;
        [SerializeField, Tooltip("ハムちゃんにダメージを与えたときのエフェクト")]
        GameObject steelParticle;
        [SerializeField]
        Color damagedColor;
        [SerializeField]
        Anima2D.SpriteMeshInstance spriteMeshInstance;
        [SerializeField]
        Anima2D.SpriteMesh damagedMesh;


        Transform player;
        float damagedTimer = 0;
        public bool IsPlaying;
        Vector2 firstLeftHandLocalPos;
        Anima2D.SpriteMesh normalMesh;
        Animator animator;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            firstLeftHandLocalPos = leftHand.localPosition;
            normalMesh = spriteMeshInstance.spriteMesh;
            animator = GetComponent<Animator>();
        }

        public void LeftHandTriggerEnter(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Neno.Scripts.Player>().SheedNum -= steelSeedNum;
                var pos = collision.transform.position;
                Instantiate(steelParticle, pos, Quaternion.identity);
            }
            if (collision.tag == "Seed")
            {
                const float DamagedNockBack = 5f;
                damagedTimer += DamagedNockBack;
                Destroy(collision.gameObject);
            }
        }

        public void HeadTriggerEnter(Collider2D collision)
        {
            print("headHit");
            if (collision.tag == "Seed")
            {
                const int Damage = 1;
                hp -= Damage;
                Destroy(collision.gameObject);
                if (hp <= 0) { StartCoroutine(DeadCoroutine());return; }
                StartCoroutine(DamagedCoroutine());
            }
        }

        [ContextMenu("Kill")]
        void Kill()
        {
            StartCoroutine(DeadCoroutine());
        }

        IEnumerator DeadCoroutine()
        {
            animator.SetTrigger("Dead");
            spriteMeshInstance.spriteMesh = damagedMesh;
            spriteMeshInstance.color = damagedColor;
            IsPlaying = false;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        bool damagedCorutineRunning;
        IEnumerator DamagedCoroutine()
        {
            if (damagedCorutineRunning) { yield break; }
            damagedCorutineRunning = true;
            spriteMeshInstance.spriteMesh = damagedMesh;
            Color firstColor = spriteMeshInstance.color;
            spriteMeshInstance.color = damagedColor;
            const float DamageMeshTime = 0.5f;
            yield return new WaitForSeconds(DamageMeshTime);
            spriteMeshInstance.spriteMesh = normalMesh;
            spriteMeshInstance.color = firstColor;
            damagedCorutineRunning = false;
        }

        void Update()
        {
            if (!IsPlaying) { return; }
            InstEnemy();
            InstSumi();
        }

        float enemyTimer = 0;
        void InstEnemy()
        {
            enemyTimer += Time.deltaTime;
            if (enemyInstCycle <= enemyTimer)
            {
                enemyTimer = 0f;
                Instantiate(normalIka, transform.position, normalIka.transform.rotation);
            }
        }

        float sumiTimer = 0;
        void InstSumi()
        {
            sumiTimer += Time.deltaTime;
            if (enemyInstCycle <= sumiTimer)
            {
                sumiTimer = 0f;
                Instantiate(ikaSumi, transform.position, ikaSumi.transform.rotation);
            }
        }
        void HandMove()
        {
            //ダメージを受けて一定以内なら腕を戻す
            if (0 < damagedTimer)
            {
                damagedTimer -= Time.deltaTime;
                Vector2 vec = player.position - leftHand.position;
                vec.Normalize();
                leftHand.right = vec;
                leftHand.transform.Translate(-vec * handSpeed * Time.deltaTime, Space.World);
                if (leftHand.localPosition.x < firstLeftHandLocalPos.x) { damagedTimer = 0f; }//初期位置より右にはいかない
                if (damagedTimer <= 0) { damagedTimer = 0; }
            }
            else//それ以外なら腕を進める
            {
                Vector2 vec = player.position - leftHand.position;
                vec.Normalize();
                leftHand.right = vec;
                leftHand.transform.Translate(vec * handSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}