using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNEngine : MonoBehaviour
{
    [SerializeField] List<Character> characters;
    [SerializeField] List<SOCharacter> charactersDefinitions;
    [SerializeField] List<SceneryBackground> backgrounds;
    [SerializeField] List<Transform> anchors;

    SceneryBackground currentBg;

    // Start is called before the first frame update
    void Start()
    {
        currentBg = null;

        foreach(Character c in characters)
        {
            if (c && c.details)
                charactersDefinitions.Add(c.details);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform FindAnchor(string objName)
    {
        objName = objName.ToLower();
        return anchors.Find(x => (x?.gameObject.name.ToLower().Equals(objName) ?? false));
    }

    public SceneryBackground FindBackground(string bg)
    {
        bg = bg.ToLower();
        return backgrounds.Find(x => (x?.gameObject.name.ToLower().Equals(bg) ?? false));
    }

    public SOCharacter FindCharacterDefinition(string id)
    {
        id = id.ToLower();
        return charactersDefinitions.Find(x => x.tag.ToLower().Equals(id));
    }

    public Character FindCharacter(string id)
    {
        id = id.ToLower();
        return characters.Find(x => (x?.gameObject.name.ToLower().Equals(id) ?? false));
    }

    public virtual void FadeToBackground(string name, float duration)
    {
        if (currentBg != null)
        {
            currentBg.FadeOut(duration);
        }
        else
            duration = 0;

        Debug.Log("Duration " + duration);

        currentBg = FindBackground(name);
        currentBg.FadeIn(duration);
    }

    public virtual void FlipXChar(string item)
    {
        Character c = FindCharacter(item);
        c?.FlipX();
    }

    public virtual void FlipYChar(string item)
    {
        Character c = FindCharacter(item);
        c?.FlipY();
    }

    public virtual void DisplayChar(string item, bool display)
    {
        Character c = FindCharacter(item);

        if (display)
            c.Show();
        else
            c.Hide();
    }
}
