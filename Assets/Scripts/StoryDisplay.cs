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
        [SerializeField] protected CharacterNameView characterBoxCenter;
        [SerializeField] protected CharacterNameView characterBoxLeft;
        [SerializeField] protected CharacterNameView characterBoxRight;
        CharacterNameView currentNameBox;
        [Header("Story")]
        [SerializeField] protected StoryBoxView storyBoxCenter;
        [SerializeField] protected StoryBoxView storyBoxLeft;
        [SerializeField] protected StoryBoxView storyBoxRight;
        StoryBoxView currentStoryBox;
        [Header("Choices")]
        [SerializeField] protected ChoiceDisplay choicesView;

        protected string content { get; set; }
        protected string lastCharacter { get; set; }


        private string Perso1Name;
        private string Perso2Name;
        public void SetStringPerso(string perso1Parameter, string perso2Parameter)
        {
            Perso1Name = perso1Parameter; 
            Perso2Name = perso2Parameter;

        }

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            lastCharacter = "";

            #region rajouter
            currentStoryBox = storyBoxCenter;
            currentNameBox = characterBoxCenter;
            storyBoxLeft.Hide();
            storyBoxRight.Hide();
            characterBoxLeft.Hide();
            characterBoxRight.Hide();
            
#endregion
            if (!characterBoxCenter)
                throw new MissingFieldException("StoryDisplay", "characterBox");
            if (!storyBoxCenter)
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
                else if (string.IsNullOrEmpty(id))
                {
                    content = content.Substring(1);
                }


                SetCharacterBoxActive(true);
            }
            else
                SetCharacterBoxActive(false);

            currentStoryBox.SetLine(content);
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
                currentStoryBox.Show();
                choicesView.Hide();
            }
            else
            {
                choicesView.Show();
                currentStoryBox.Hide();
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
            storyBoxCenter.SetInteractable(state);
        }

        public void SetCharacterBoxActive(bool active)
        {
            currentNameBox.SetActive(active);
        }

        public void SetStoryBoxActive(bool active)
        {
            //faut faire truc ici
            currentStoryBox.SetActive(active);
        }

        protected virtual void ChangeCharacter(string id)
        {
            // ou ici

            SOCharacter charac = engine?.FindCharacterDefinition(id);

            currentStoryBox.Hide();
            currentNameBox.Hide();


            if (charac.tag == "Sigg")
            {
                currentStoryBox = storyBoxCenter;
                currentNameBox = characterBoxCenter;

            }
            else if(id == Perso1Name)
            {
                currentStoryBox = storyBoxLeft;
                currentNameBox = characterBoxLeft;

            }
            else if(id == Perso2Name) 
            {
                currentStoryBox = storyBoxRight;
                currentNameBox = characterBoxRight;

            }
            currentStoryBox.Show();

            if (charac != null)
            {
                currentNameBox.SetName(charac.characterName, charac.characterColor);
            }
            else
            {
                Debug.LogWarning("Definition '" + id + "' not found.");
                currentNameBox.SetName("? ? ?", Color.white * 0.5f);
            }
        }

    }
}
