using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_StoryReader : MonoBehaviour
{

    [SerializeField] private TextAsset textAsset;
    [SerializeField] private Text textUI;
    [SerializeField] private GridLayoutGroup groupLayout;
    [SerializeField] private GameObject prefabButton;

    private Story story;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(textAsset.text);
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Next()
    {
        if(story.canContinue)
        {
            string content = story.Continue().Trim();
            textUI.text = content;
        }
        else if(story.currentChoices.Count > 0)
        {
            DisplayChoices(story.currentChoices);
        }
    }

    private void DisplayChoices(List<Choice> choicesParameter)
    {
        foreach(Transform child in groupLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Choice choice in choicesParameter)
        {
            GameObject bouton = Instantiate(prefabButton, groupLayout.transform);
            bouton.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }
    public Story GetStory() => story; 
    
    private void OnChoiceSelected(Choice choixSelectionneParameter)
    {
        story.ChooseChoiceIndex(choixSelectionneParameter.index);
        Next();
    }
}
