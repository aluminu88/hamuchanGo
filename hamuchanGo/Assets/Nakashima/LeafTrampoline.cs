using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class LeafTrampoline : MonoBehaviour
    {

        [SerializeField]
        float bounciness = 1;

        [SerializeField]
        private AudioClip TrampolineSE;

        void Start()
        {

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Vector2 impluse = transform.up * bounciness;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = impluse;
                GetComponent<AudioSource>().PlayOneShot(TrampolineSE);
            }
        }
    }
}