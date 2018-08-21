using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpIka : MonoBehaviour {
    [SerializeField, Tooltip("プレイヤーに接触した時に奪う種の量")]
    int steelSeedNum;
    [SerializeField, Tooltip("時機に接触した時消えるかどうか")]
    bool hitDestroy;
    [SerializeField, Tooltip("タネに当たった時のエフェクト")]
    GameObject damagedEffect;
    [SerializeField, Tooltip("ハムちゃんにダメージを与えたときのエフェクト")]
    GameObject steelParticle;

    [SerializeField, Tooltip("ジャンプサイクル")]
    float cycleTime = 1;
    [SerializeField, Tooltip("ジャンプのタイミングではねる確率")]
    float jumpProbability = 0.5f;
    [SerializeField, Tooltip("ジャンプする角度")]
    float jumpRange = 90f;
    [SerializeField, Tooltip("ジャンプ力")]
    float jumpImpulse = 1f;

    float timer = 0f;
    Rigidbody2D rb;
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (cycleTime <= timer)
        {
            timer = 0f;
            Jump();
        }
    }

    void Jump()
    {
        print("jump");
        float rand = Random.Range(0f, 1f);
        if ( rand <= jumpProbability)
        {
            float minAngle = 90f - jumpRange / 2;
            float maxAngle = 90f + jumpRange / 2;
            float randomAngle = Random.Range(minAngle, maxAngle);
            Vector2 jumpVector = new Vector2();
            jumpVector.x= Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            jumpVector.y= Mathf.Sin(randomAngle * Mathf.Deg2Rad);
            jumpVector.Normalize();
            rb.velocity = Vector2.zero;
            rb.AddForce(jumpImpulse * jumpVector,ForceMode2D.Impulse);
        }
    }
}
