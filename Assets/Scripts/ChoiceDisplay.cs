using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VNsup
{
    public class ChoiceDisplay : UIDisplay
    {
        public delegate void ChoiceClicked(Choice selection);

        LayoutGroup layoutGroup;
        [SerializeField] ChoiceButton buttonPrefab;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            layoutGroup = GetComponent<LayoutGroup>();
            Clear();
        }

        public void ResetChoices(List<Choice> choices, ChoiceClicked callback)
        {
            Clear();

            foreach (Choice c in choices)
            {
                ChoiceButton btn = Instantiate(buttonPrefab, layoutGroup.transform, false);
                SetButton(btn, c, callback);
                
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
        }

        public virtual void SetButton(ChoiceButton btn, Choice choice, ChoiceClicked callback)
        {
            btn.SetChoice(choice);
            btn.SetConfig(callback);
        }

        public void Clear()
        {
            foreach (Transform t in layoutGroup.transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
