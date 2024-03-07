using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VNsup
{
    public class CharacterNameView : UIDisplay
    {
        [SerializeField] TMP_Text nameText;

        public virtual void SetName(string characterName, Color characterColor)
        {
            nameText.text = characterName;
            nameText.color = characterColor;
        }
    }

}
