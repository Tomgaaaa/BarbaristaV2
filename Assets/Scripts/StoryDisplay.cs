using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class StoryDisplay : MonoBehaviour
{
    public StoryReader reader { get; set; }

    [Header("Character")]
    [SerializeField] TMP_Text characterName;
    [SerializeField] RectTransform characterBox;
    [Header("Story")]
    [SerializeField] TMP_Text storyText;
    [SerializeField] RectTransform storyBox;

    string content;
    string lastCharacter;

    // Start is called before the first frame update
    void Start()
    {
        lastCharacter = "";
    }

    public virtual void ShowStory(string text)
    {
        content = text;
        if (IsACharacter())
        {
            string id = CharacterID();
            content = content.Substring(id.Length + 1);
            if (!string.IsNullOrEmpty(id) && id != lastCharacter)
            {
                lastCharacter = id;
                ChangeCharacter(id);
            }

            SetCharacterBoxActive(true);
        }
        else
            SetCharacterBoxActive(false);

        storyText.text = content;
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

    public void SetCharacterBoxActive(bool active)
    {
        if (active)
            ShowCharacterBox();
        else
            HideCharacterBox();
    }

    protected virtual void ChangeCharacter(string id)
    {
        VNEngine engine = reader.vnEngine;
        SOCharacter charac = engine.FindCharacterDefinition(id);

        if (charac != null)
        {
            characterName.color = charac.characterColor;
            characterName.text = charac.characterName;

        }
        else
        {
            characterName.color = Color.white;
            characterName.text = "? ? ?";
        }
    }

    protected virtual void HideCharacterBox()
    {
        characterBox.gameObject.SetActive(false);
    }

    protected virtual void ShowCharacterBox()
    {
        characterBox.gameObject.SetActive(true);
    }

}
