using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neno.Scripts
{
    public abstract class Singleton<T> where T : new()
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        /// <summary>
        /// Instanceを新しく作り直します
        /// </summary>
        public void ResetInstance()
        {
            instance = new T();
        }
    }

}