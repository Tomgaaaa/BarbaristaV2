using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Events;

public class StoryReader : MonoBehaviour
{
    [System.Serializable] public class EventLink
    {
        public string linkName;
        public UnityEvent trigger;
    }

    public VNEngine vnEngine => engine;

    [SerializeField] TextAsset storyAsset;
    [SerializeField] StoryDisplay storyDisplay;
    [SerializeField] VNEngine engine;
    [SerializeField] List<EventLink> eventList;
    [SerializeField] bool startOnAwake;

    Story story;

    // Start is called before the first frame update
    void Awake()
    {
        if (storyAsset == null)
        {
            gameObject.SetActive(false);
            throw new UnassignedReferenceException("storyAsset is empty");
        }
        story = new Story(storyAsset.text);

        SetupGlobalMethods();

        if (startOnAwake)
            Next();

        storyDisplay.reader = this;
    }

    public virtual void Next()
    {
        if (story.canContinue)
            storyDisplay.ShowStory(story.Continue().Trim());
    }

    void SetupGlobalMethods()
    {
        story.BindExternalFunction("changeBg", (string name) => engine.FadeToBackground(name, 0));
        story.BindExternalFunction("fadeBg", (string name, float duration) => engine.FadeToBackground(name, duration));
        story.BindExternalFunction("flipX", (string name) => engine.FlipXChar(name));
        story.BindExternalFunction("flipY", (string name) => engine.FlipYChar(name));
        story.BindExternalFunction("show", (string name) => engine.DisplayChar(name, true));
        story.BindExternalFunction("hide", (string name) => engine.DisplayChar(name, false));
    }

}
