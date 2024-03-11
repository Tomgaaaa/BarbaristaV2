using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;

namespace VNsup
{
    public class StoryReader : MonoBehaviour
    {
        public enum StoryReadState { NONE, READ, WAIT, RESUME };

        [System.Serializable] public class StoryEventTrigger : UnityEvent<StoryReader, object[]> { }
        [System.Serializable] public class VariableEventTrigger : UnityEvent<string, object> { }
        [System.Serializable]
        public class EventLink
        {
            public string eventName;
            public StoryEventTrigger onTrigger;
        }

        [System.Serializable]
        public class VariableLink
        {
            public string variableName;
            public VariableEventTrigger onTrigger;
        }

        public VNEngine vnEngine => engine;
        public Story story { get; private set; }
        public StoryDisplay display => storyDisplay;


        [SerializeField] bool startOnAwake;
        [SerializeField] TextAsset storyAsset;
        [SerializeField] StoryDisplay storyDisplay;
        [SerializeField] VNEngine engine;
        [SerializeField] List<EventLink> eventList;
        [SerializeField] List<VariableLink> variableList;

        public StoryReadState state { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            if (storyAsset == null)
            {
                gameObject.SetActive(false);
                throw new UnassignedReferenceException("storyAsset is empty");
            }

            story = new Story(storyAsset.text);
            state = StoryReadState.READ;

            SetupGlobalMethods();
            SetupCustomMethods();

            foreach (EventLink e in eventList)
            {
                story.BindExternalFunctionGeneral(e.eventName, (object[] args) => { e.onTrigger?.Invoke(this, args); return null; });
            }

            foreach (VariableLink v in variableList)
            {
                story.ObserveVariable(v.variableName, (string varName, object varValue) => v.onTrigger?.Invoke(varName, varValue));
            }

            storyDisplay.engine = engine;
            storyDisplay.ReadTags(story.globalTags, true);


        }

        private void OnEnable()
        {
            storyDisplay.OnChoiceSelected += SelectChoice;
        }

        private void OnDisable()
        {
            storyDisplay.OnChoiceSelected -= SelectChoice;
        }

        public virtual void Start()
        {

            story = new Story(SCR_DATA.instanceData.GetCurrentQuest().myQueteInk.text);

            story.variablesState["Perso1"] = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[0].namePerso;
            story.variablesState["Perso2"] = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[1].namePerso;
            storyDisplay.SetStringPerso(story.variablesState["Perso1"].ToString(), story.variablesState["Perso2"].ToString());

            SetupGlobalMethods();


            if (SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
            {
                story.ChoosePathString("Apresquete");
            }
            else
            {
                story.ChoosePathString("Avantquete");
            }



            if (startOnAwake)
                Next();
        }

        protected bool UpdateState()
        {
            if (state == StoryReadState.WAIT)
            {
                state = StoryReadState.RESUME;
                storyDisplay.WaitPlayer();
                return false;
            }
            else if (state == StoryReadState.RESUME)
            {
                storyDisplay.ComeBack();
            }

            return true;
        }

        public virtual void Next()
        {
            if (!UpdateState())
                return;

            if (story.canContinue)
            {
                string content = story.Continue().Trim();

                if (string.IsNullOrEmpty(content))
                    Next();

                if (story.currentTags.Count > 0)
                    storyDisplay.ReadTags(story.currentTags, false);

                storyDisplay.ShowStory(content);
            }
            else if (story.currentChoices?.Count > 0)
            {
                storyDisplay.ShowChoices(story.currentChoices);
            }
        }

        public virtual void SelectChoice(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
            Next();
        }

        public void CancelWait()
        {
            if (state == StoryReadState.WAIT)
            {
                state = StoryReadState.READ;
            }
        }

        public void ThenWaitPlayer()
        {
            state = StoryReadState.WAIT;
        }

        void SetupGlobalMethods()
        {
            story.BindExternalFunction("changeBg", (string name) => engine.FadeToBackground(name, 0));
            story.BindExternalFunction("fadeBg", (string name, float duration) => engine.FadeToBackground(name, duration));
            story.BindExternalFunction("flipX", (string name) => engine.FlipXChar(name));
            story.BindExternalFunction("flipY", (string name) => engine.FlipYChar(name));
            story.BindExternalFunction("show", (string name) => engine.DisplayChar(name, true));
            story.BindExternalFunction("hide", (string name) => engine.DisplayChar(name, false));
            story.BindExternalFunction("fadeIn", (string name, float duration) => engine.DisplayChar(name, true, duration));
            story.BindExternalFunction("fadeOut", (string name, float duration) => engine.DisplayChar(name, false, duration));
            story.BindExternalFunction("thenWaitPlayer", () => ThenWaitPlayer());
            story.BindExternalFunction("face", (string character, string name) => engine.SetEmotion(character, name));
            story.BindExternalFunction("moveTo", (string character, string name, float duration) => engine.MoveTo(character, name, duration));

            story.BindExternalFunction("FinishDialogue", (string name) => ChangeScene());
            story.BindExternalFunction("playSound", (string name) =>  AudioManager.instanceAM.Play(name));

        }

        protected virtual void SetupCustomMethods()
        {

        }

        private void ChangeScene()
        {
            if (SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 0)
            {
                AudioManager.instanceAM.Play("SwitchToCuisine");
                AudioManager.instanceAM.Pause("BarAlatea");
                
                AudioManager.instanceAM.Play("CuisineAlatea");
                SceneManager.LoadScene("SCE_Cuisine");
            }
            else if (SCR_DATA.instanceData.GetEtapeQuest() == 0 && SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
            {
                SCR_DATA.instanceData.EtapeQueteUp();
                SCR_DATA.instanceData.EtapePersoUp();

                story = new Story(SCR_DATA.instanceData.GetCurrentQuest().myQueteInk.text);
                SetupGlobalMethods();
                story.ChoosePathString("Avantquete");
                //Next();

            }
            else if (SCR_DATA.instanceData.GetEtapeQuest() == 1 && SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Count == 2)
            {
                SCR_DATA.instanceData.EtapeQueteUp();
                SCR_DATA.instanceData.EtapePersoUp();
                SCR_DATA.instanceData.JourUP();

                SceneManager.LoadScene("SCE_GainQuete");

            }
        }

    }
}
