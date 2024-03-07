using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VNsup
{
    [RequireComponent(typeof(Button))]
    public class ChoiceButton : MonoBehaviour
    {
        protected Button button { get; set; }
        protected TMP_Text content { get; set; }
        protected Choice currentChoice { get; set; }

        private void Awake()
        {
            button = GetComponent<Button>();
            content = button.GetComponentInChildren<TMP_Text>();
        }

        public virtual void SetChoice(Choice choice)
        {
            currentChoice = choice;
            content.text = choice.text.Trim();
        }

        public virtual void SetConfig(ChoiceDisplay.ChoiceClicked callback)
        {
            button.onClick.AddListener(() => callback(currentChoice));
        }
    }
}
