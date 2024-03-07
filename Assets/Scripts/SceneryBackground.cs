using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    public class SceneryBackground : Display2D
    {
        protected override void Awake()
        {
            base.Awake();
            GameObjectUtility.FindObjectOfType<VNEngine>()?.Add(this);
            gameObject.name = "Background_" + inkTag;
        }
    }
}
