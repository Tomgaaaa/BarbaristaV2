using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TutoManager : MonoBehaviour, ISerializationCallbackReceiver
{

    public enum enumEmplacement{haut,gauche, centre, right }

    public static SCR_TutoManager instanceTuto;

    [SerializeField] private SCR_Tuto prefabTuto;
    [SerializeField] private Transform gridAllHelp;
    [SerializeField] private GameObject buttonCloseHelp;

    SCR_Tuto persistentTuto;

    #region Dico



    [System.Serializable] public class dicoTutoBoolClass : TemplateDico<SO_Tuto, bool> { };
    private Dictionary<SO_Tuto, bool> dicoTutoBool; // dico listant les persos a afficher selon le jour
    [SerializeField] private List<dicoTutoBoolClass> listDicoTutoBool;


    [System.Serializable] public class dicoenumEmplacementClass : TemplateDico<enumEmplacement, RectTransform> { };
    private Dictionary<enumEmplacement, RectTransform> dicoEnumEmplacement; // dico listant les persos a afficher selon le jour
    [SerializeField] private List<dicoenumEmplacementClass> listDicoEnumEmplacement;
    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoTutoBool = new Dictionary<SO_Tuto, bool>();

        foreach (dicoTutoBoolClass item in listDicoTutoBool)
        {
            if (!dicoTutoBool.ContainsKey(item.key))
            {
                dicoTutoBool.Add(item.key, item.value);
            }
        }



        dicoEnumEmplacement = new Dictionary<enumEmplacement, RectTransform>();

        foreach (dicoenumEmplacementClass item in listDicoEnumEmplacement)
        {
            if (!dicoEnumEmplacement.ContainsKey(item.key))
            {
                dicoEnumEmplacement.Add(item.key, item.value);
            }
        }

    }
    #endregion



    private void Awake()
    {
        if (instanceTuto == null)
            instanceTuto = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void Calltuto(SO_Tuto tutoToShow, enumEmplacement emplacementParameter,bool forceTuto)
    {
        if (!dicoTutoBool[tutoToShow]) // verifie si le tuto n'a pas deja ete montre
        {
            SCR_Tuto tuto = Instantiate(prefabTuto, dicoEnumEmplacement[emplacementParameter]);
            tuto.Initialisation(tutoToShow,forceTuto);
        }
    }
    public void CallPersistentTuto(SO_Tuto tutoToShow ,enumEmplacement emplacementParameter, Action<SO_Tuto> eventEndPersistent)
    {
        persistentTuto = Instantiate(prefabTuto, dicoEnumEmplacement[emplacementParameter]);
        persistentTuto.Initialisation(tutoToShow,true);

        SCR_MasterCompendium.clickOnCompendium += ValidTutoPersistent;

    }



    public void ValidTuto(SO_Tuto tutoParameter)
    {
        dicoTutoBool[tutoParameter] = true;

    }

    public void ValidTutoPersistent(SO_Tuto tutoParameter)
    {
        dicoTutoBool[tutoParameter] = true;
        persistentTuto.ButtonClose();
    }


    public void ButtonHelp()
    {
        buttonCloseHelp.SetActive(true);

        foreach (KeyValuePair<SO_Tuto,bool> pair in dicoTutoBool)
        {
            if (pair.Value)
            {
                SCR_Tuto tuto = Instantiate(prefabTuto, gridAllHelp );
                tuto.Initialisation(pair.Key,false);
            }
        }
    }


    public void CloseButtonHelp()
    {
        foreach(Transform children in gridAllHelp)
        {
            Destroy(children.gameObject);
        }
        
        buttonCloseHelp.SetActive(false);
    }

    public Dictionary<SO_Tuto, bool> GetDicoTuto() { return dicoTutoBool; }

}
