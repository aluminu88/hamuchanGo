using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public class PlayerStatusModel : Singleton<PlayerStatusModel>
    {
        public int SeedNum { get; set; }
        public int MaxSeedNum { get; private set; }
        public float PlayerHp { get; set; }

        public PlayerStatusModel()
        {
            //初期値！
            MaxSeedNum = 10;
            SeedNum = 6;
            PlayerHp = 1000;
        }

        public void SetMaxSeedNum(int maxNum)
        {
            this.MaxSeedNum = maxNum;
        }

        public void StatusInit()
        {
            //初期値！
            MaxSeedNum = 10;
            SeedNum = 6;
            PlayerHp = 1000;
        }
    }
}
