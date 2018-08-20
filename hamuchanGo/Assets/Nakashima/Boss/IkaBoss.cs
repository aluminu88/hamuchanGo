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
        Transform leftHand;
        [SerializeField]
        GameObject normalIka;
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
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            firstLeftHandLocalPos = leftHand.localPosition;
            normalMesh = spriteMeshInstance.spriteMesh;
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

        IEnumerator DeadCoroutine()
        {
            spriteMeshInstance.spriteMesh = damagedMesh;
            spriteMeshInstance.color = damagedColor;
            IsPlaying = false;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        IEnumerator DamagedCoroutine()
        {
            spriteMeshInstance.spriteMesh = damagedMesh;
            spriteMeshInstance.color = damagedColor;
            const float DamageMeshTime = 0.5f;
            yield return new WaitForSeconds(DamageMeshTime);
            spriteMeshInstance.spriteMesh = normalMesh;
            spriteMeshInstance.color = Color.white;
        }

        void Update()
        {
            if (!IsPlaying) { return; }

            //ダメージを受けて一定以内なら腕を戻す
            if (0 < damagedTimer)
            {
                damagedTimer -= Time.deltaTime;
                Vector2 vec = player.position - leftHand.position;
                vec.Normalize();
                leftHand.right = vec;
                leftHand.transform.Translate(-vec * handSpeed * Time.deltaTime, Space.World);
                if(firstLeftHandLocalPos.x < leftHand.localPosition.x)//初期位置より右にはいかない
                if (damagedTimer <= 0) { damagedTimer = 0;}
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