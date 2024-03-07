using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VNsup
{
    public class StoryBoxView : UIDisplay
    {
        [SerializeField] TMP_Text lineText;
        [SerializeField] Button nextButton;

        public virtual void SetLine(string content)
        {
            lineText.text = content;
        }

        public void SetInteractable(bool active)
        {
            nextButton.interactable = active;
        }

    }
}
