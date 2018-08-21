using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{

    public class StatusInitializer : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            PlayerStatusModel.Instance.StatusInit();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
