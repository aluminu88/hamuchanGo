using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naka
{
    public class SeedsStack : MonoBehaviour
    {
        [SerializeField, Range(1, 30), Tooltip("種の最大数を指定")]
        int seedMax = 10;
        [SerializeField,Tooltip("見た感じの種の感覚")]
        float seedSpriteInterval;
        [SerializeField]
        int seedNum;//現在表示している数
        [SerializeField, Tooltip("この下の階層の種オブジェクトを指定")]
        Image seed;
        [SerializeField]
        Sprite filledSeedSprite;
        [SerializeField]
        Sprite emptySeedSprite;

        Image[] uiImages;
        void Start()
        {
            uiImages = new Image[seedMax];
            uiImages[0] = seed;
            Vector2 instPos = seed.transform.localPosition;
            for(int i = 1;i < seedMax; i++){
                instPos += Vector2.right * seedSpriteInterval;
                var instSeed = Instantiate(seed, transform).GetComponent<Image>();
                instSeed.transform.localPosition = instPos;
                uiImages[i]=instSeed;
            }
            for (int i = 0; i < seedNum; i++)
            {
                uiImages[i].sprite = filledSeedSprite;
            }
        }

        public void SetSeeds(int seedNum)
        {
            this.seedNum = seedNum;
            UpdateSeedSprite();
        }

        public void PushSeed(int num = 1)
        {
            seedNum += num;
            UpdateSeedSprite();
        }
        public void PopSeed(int num = 1)
        {
            seedNum -= num;
            UpdateSeedSprite();
        }

        /// <summary>
        /// 種画像の更新
        /// </summary>
        void UpdateSeedSprite()
        {
            for (int i = 0; i < uiImages.Length; i++)
            {
                if (i + 1 <= seedNum)
                {
                    uiImages[i].sprite = filledSeedSprite;
                }
                else
                {
                    uiImages[i].sprite = emptySeedSprite;
                }
            }
        }
    }
}