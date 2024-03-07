using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    [RequireComponent(typeof(RectTransform))]
    public class UIDisplay : MonoBehaviour
    {
        protected RectTransform rect;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetActive(bool active)
        {
            if (active)
                Show();
            else
                Hide();
        }
    }
}
