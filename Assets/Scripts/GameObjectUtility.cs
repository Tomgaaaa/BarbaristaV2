using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    public static class GameObjectUtility
    {
        public static T FindObjectOfType<T>() where T : MonoBehaviour
        {
#if UNITY_2023_2_OR_NEWER
        return GameObject.FindFirstObjectByType<T>();
#else
            return GameObject.FindObjectOfType<T>();
#endif
        }
    }
}
