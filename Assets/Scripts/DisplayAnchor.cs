using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    public class DisplayAnchor : MonoBehaviour
    {
        public string vnTag => inkTag;

        [SerializeField] string inkTag;

        private void Start()
        {
            GameObjectUtility.FindObjectOfType<VNEngine>()?.Add(this);
            gameObject.name = "Anchor_" + inkTag;
        }

    }
}