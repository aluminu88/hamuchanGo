using System.Collections;
using System.Collections.Generic;
using Neno.Scripts;
using UnityEngine;

namespace Neno.Scripts
{
    public class GameRuleManager : Singleton<GameRuleManager>
    {

        [SerializeField] private int maxSheedNum = 10;


        //ハムスターから諸所値が飛んできた履
        public int SheedNum { get; set; }

        public int MaxSheedNum
        {
            get { return maxSheedNum; }
        }

        //トータルのスコア
        public int TotalScore { get; set; }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


