using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class BossLeftHand : MonoBehaviour
    {
        [SerializeField]
        IkaBoss boss;

        void Start()
        {
        }

        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            boss.LeftHandTriggerEnter(collision);
        }
    }
}