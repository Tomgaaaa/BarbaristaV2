using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    public class Character : Display2D
    {
        public SOCharacter details => definition;

        [SerializeField] SOCharacter definition;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            if (!definition)
            {
                Debug.LogError("No definition set !", this.gameObject);
                return;
            }

            inkTag = definition.tag;
            gameObject.name = "Character_" + definition.tag;
            mainImage.sprite = definition.GetDefaultFace();
            GameObjectUtility.FindObjectOfType<VNEngine>()?.Add(this);
        }

        public void SetEmotion(string face)
        {
            mainImage.sprite = definition.GetFace(face);
        }
    }
}
