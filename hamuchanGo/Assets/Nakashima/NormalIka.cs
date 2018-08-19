using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka {
    public class NormalIka : MonoBehaviour {
        [SerializeField]
        float moveSpeed;
        [SerializeField,Tooltip("プレイヤーに接触した時に奪う種の量")]
        int steelSeedNum;
        [SerializeField]
        GameObject steelParticle;

        Transform player;
        void Start() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update() {
            Vector2 vec = player.position - transform.position;
            vec.Normalize();
            transform.Translate(vec * moveSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Neno.Scripts.Player>().SheedNum -= steelSeedNum;
                var pos = collision.transform.position;
                Instantiate(steelParticle, pos, Quaternion.identity);
            }
        }
    }
}