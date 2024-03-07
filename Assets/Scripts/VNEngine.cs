using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNsup
{
    public class VNEngine : MonoBehaviour
    {
        [SerializeField] List<Character> characters;
        [SerializeField] List<SOCharacter> charactersDefinitions;
        [SerializeField] List<SceneryBackground> backgrounds;
        [SerializeField] List<DisplayAnchor> anchors;

        protected SceneryBackground currentBg { get; set; }

        // Start is called before the first frame update
        void Awake()
        {
            currentBg = null;
        }

        public void Add(DisplayAnchor a)
        {
            if (a && !anchors.Contains(a))
                anchors.Add(a);
        }

        public void Add(Character c)
        {
            if (c && c.details && !characters.Contains(c))
            {
                charactersDefinitions.Add(c.details);
                characters.Add(c);
            }
        }

        public void Add(SceneryBackground sb)
        {
            if (sb && !backgrounds.Contains(sb))
                backgrounds.Add(sb);
        }

        public DisplayAnchor FindAnchor(string objName)
        {
            objName = objName.ToLower();
            DisplayAnchor a = anchors.Find(x => (x?.vnTag.ToLower().Equals(objName) ?? false));
            if (a == null)
                throw new UnassignedReferenceException("'" + objName + "' anchor not found");

            return a;
        }

        public SceneryBackground FindBackground(string bg)
        {
            bg = bg.ToLower();

            SceneryBackground bgObj = backgrounds.Find(x => (x?.vnTag.ToLower().Equals(bg) ?? false));

            if (bgObj == null)
                throw new UnassignedReferenceException("'" + bg + "' background not found");

            return bgObj;
        }

        public SOCharacter FindCharacterDefinition(string id)
        {
            id = id.ToLower();
            return charactersDefinitions.Find(x => x.tag.ToLower().Equals(id));
        }

        public Character FindCharacter(string id)
        {
            id = id.ToLower();
            Character c = characters.Find(x => (x?.vnTag.ToLower().Equals(id) ?? false));

            if (c == null)
                throw new UnassignedReferenceException("'" + id + "' character not found");

            return c;
        }

        public virtual void FadeToBackground(string name, float duration)
        {
            if (currentBg != null)
            {
                currentBg.FadeOut(duration);
            }

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

        public virtual void DisplayChar(string item, bool display, float duration)
        {
            Character c = FindCharacter(item);

            if (display)
                c?.FadeIn(duration);
            else
                c?.FadeOut(duration);
        }

        public void SetEmotion(string character, string name)
        {
            Character c = FindCharacter(character);
            c?.SetEmotion(name);
        }

        public void MoveTo(string character, string name, float duration)
        {
            Character c = FindCharacter(character);
            DisplayAnchor a = FindAnchor(name);

            if (duration > 0)
                c.Move(a.transform, duration);
            else
                c.transform.position = a.transform.position;
        }
    }
}
