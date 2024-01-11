using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryLesson : MonoBehaviour, ISerializationCallbackReceiver
{

    [System.Serializable] public class dicoItem : TemplateDico<string, float> { };
    [System.Serializable] public class dicoBoisson : TemplateDico<string, bool> { };
    

    

    [SerializeField] List <dicoItem> dico;
    [SerializeField] List <dicoItem> dicoBool;
    private Dictionary<string, float> dicoQuete;

    public void OnAfterDeserialize()
    {
        dicoQuete = new Dictionary<string, float>();
        foreach (dicoItem item in dico)
        {
            if (!dicoQuete.ContainsKey(item.key))
            {
                dicoQuete.Add(item.key, item.value);
            }
        }
    }

    public void OnBeforeSerialize()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
