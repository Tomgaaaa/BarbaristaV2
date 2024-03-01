using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        story.BindExternalFunction("FinishDialogue",(string name) => { ChangeScene(); });

        if(SCR_DATA.instanceData.GetCurrentQuete().boissonsServis.Count == 2 )
        {
            story.ChoosePathString("Apresquete");
        }
        else
        {
            story.ChoosePathString("Avantquete");
        }
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ChangeScene()
    {
        if (SCR_DATA.instanceData.GetCurrentQuete().boissonsServis.Count == 0)
        {
            SceneManager.LoadScene("SCE_Cuisine");
        }
        else if(SCR_DATA.instanceData.GetEtapeQuete() == 0 && SCR_DATA.instanceData.GetCurrentQuete().boissonsServis.Count == 2)
        {
            SCR_DATA.instanceData.EtapeQueteUp();
            SCR_DATA.instanceData.EtapePersoUp();

            story = new Story( SCR_DATA.instanceData.GetCurrentQuete().myQueteInk.text);
            story.ChoosePathString("Avantquete");
            Next();

        }
        else if(SCR_DATA.instanceData.GetEtapeQuete() == 1 && SCR_DATA.instanceData.GetCurrentQuete().boissonsServis.Count == 2)
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
