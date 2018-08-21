using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naka
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField, Tooltip("召喚周期")]
        float cycleTime = 1;
        [SerializeField, Tooltip("召喚するオブジェクト")]
        GameObject instantiateObject;
        [SerializeField,Tooltip ("場に存在できる最大数")]
        int maxObjectNum = 20;

        Queue<GameObject> instantiatedObjects = new Queue<GameObject>(); 
        float timer = 0;
        void Start()
        {

        }

        void Update()
        {
            timer += Time.deltaTime;
            if (cycleTime <= timer)
            {
                timer = 0f;
                var obj = Instantiate(instantiateObject, transform.position, transform.rotation);
                instantiatedObjects.Enqueue(obj);
                if(maxObjectNum < instantiatedObjects.Count)//最大数よりも多く存在したら古いのを削除
                {
                    var destroyObject = instantiatedObjects.Dequeue();
                    Destroy(destroyObject);
                }
            }
        }
    }
}
