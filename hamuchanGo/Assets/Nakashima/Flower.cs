using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class Flower : MonoBehaviour
    {

        void Start()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                print("FlowerHit");
            }
        }
    }
}