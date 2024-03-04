using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCR_StoryReader : MonoBehaviour
{

    [SerializeField] private TextAsset textAsset;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GridLayoutGroup groupLayout;
    [SerializeField] private GameObject prefabButton;

    private Story story;


    // Start is called before the first frame update
    void Start()
    {


        if (SCR_DATA.instanceData == null)
        {
            story = new Story(textAsset.text);
            story.BindExternalFunction("FinishDialogue", (string name) => { ChangeScene(); });

            story.ChoosePathString("Apresquete");


        }
        else
        {
            story = new Story(SCR_DATA.instanceData.GetCurrentQuest().myQueteInk.text);
            story.BindExternalFunction("FinishDialogue", (string name) => { ChangeScene(); });

            if (SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
            {
                story.ChoosePathString("Apresquete");
            }
            else
            {
                story.ChoosePathString("Avantquete");
            }
        }


            Next();
        


       
    }

    
    
    private void ChangeScene()
    {
        if (SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 0)
        {
            SceneManager.LoadScene("SCE_Cuisine");
        }
        else if(SCR_DATA.instanceData.GetEtapeQuest() == 0 && SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
        {
            SCR_DATA.instanceData.EtapeQueteUp();
            SCR_DATA.instanceData.EtapePersoUp();

            story = new Story( SCR_DATA.instanceData.GetCurrentQuest().myQueteInk.text);
            story.BindExternalFunction("FinishDialogue", (string name) => { ChangeScene(); });

            story.ChoosePathString("Avantquete");
            //Next();

        }
        else if(SCR_DATA.instanceData.GetEtapeQuest() == 1 && SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
        {
            SCR_DATA.instanceData.EtapeQueteUp();
            SCR_DATA.instanceData.EtapePersoUp();
            SCR_DATA.instanceData.JourUP();

            SceneManager.LoadScene("SCE_GainQuete");

        }
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
        foreach (Transform child in groupLayout.transform)
        {
            Destroy(child.gameObject);
        }
        story.ChooseChoiceIndex(choixSelectionneParameter.index);
        Next();
    }
}
