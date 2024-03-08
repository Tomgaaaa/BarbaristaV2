using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VNsup
{
    public class StoryDisplay : UIDisplay
    {
        public event Action<Choice> OnChoiceSelected;

        public VNEngine engine { get; set; }

        [Header("Character")]
        [SerializeField] protected CharacterNameView characterBox;
        [Header("Story")]
        [SerializeField] protected StoryBoxView storyBox;
        [Header("Choices")]
        [SerializeField] protected ChoiceDisplay choicesView;

        protected string content { get; set; }
        protected string lastCharacter { get; set; }

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            lastCharacter = "";

            if (!characterBox)
                throw new MissingFieldException("StoryDisplay", "characterBox");
            if (!storyBox)
                throw new MissingFieldException("StoryDisplay", "storyBox");
            if (!choicesView)
                throw new MissingFieldException("StoryDisplay", "choicesView");
        }

        public virtual void WaitPlayer()
        {
            SetActive(false);
        }

        public virtual void ComeBack()
        {
            SetActive(true);
        }

        public virtual void ReadTags(List<string> tags, bool global)
        {
            if (global && tags != null)
            {
                foreach(string t in tags)
                {
                    Debug.Log(t);
                }
            }
        }

        public virtual void ShowStory(string text)
        {
            SetStoryMode(true);
            SetNextButtonState(true);

            content = text;

            if (IsACharacter())
            {
                string id = CharacterID();
                if (!string.IsNullOrEmpty(id) && id != lastCharacter)
                {
                    lastCharacter = id;
                    content = CharacterContent();
                    ChangeCharacter(id);
                }

                SetCharacterBoxActive(true);
            }
            else
                SetCharacterBoxActive(false);

            storyBox.SetLine(content);
        }

        public virtual void ShowChoices(List<Choice> currentChoices)
        {
            SetStoryMode(false);

            SetCharacterBoxActive(false);
            SetNextButtonState(false);

            choicesView.ResetChoices(currentChoices, (c) => OnChoiceSelected?.Invoke(c));
        }

        public virtual void SetStoryMode(bool state)
        {
            if (state)
            {
                storyBox.Show();
                choicesView.Hide();
            }
            else
            {
                choicesView.Show();
                storyBox.Hide();
            }
        }

        public virtual bool IsACharacter()
        {
            if (content.StartsWith(':'))
                return true;
            else
            {
                Regex rx = new Regex("^[^: ]+:");
                return rx.IsMatch(content);
            }
        }

        public virtual string CharacterID()
        {
            
            return content.Substring(0, content.IndexOf(':'));
        }

        public virtual string CharacterContent()
        {
            return content.Substring(lastCharacter.Length + 1);
        }

        protected virtual void SetNextButtonState(bool state)
        {
            storyBox.SetInteractable(state);
        }

        public void SetCharacterBoxActive(bool active)
        {
            characterBox.SetActive(active);
        }

        public void SetStoryBoxActive(bool active)
        {
            //faut faire truc ici
            storyBox.SetActive(active);
        }

        protected virtual void ChangeCharacter(string id)
        {

            // ou ici

            SOCharacter charac = engine?.FindCharacterDefinition(id);

            if (charac != null)
            {
                characterBox.SetName(charac.characterName, charac.characterColor);
            }
            else
            {
                Debug.LogWarning("Definition '" + id + "' not found.");
                characterBox.SetName("? ? ?", Color.white * 0.5f);
            }
        }

    }
}
