using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum enumPage
{

    Sommaire, SommaireBiome, BiomeGlace, SousBGrotte, SousBTaiga, SousBMontagne,  BiomeFeu,  SousBAride, SousBRocheuse, SousBBasalte, BiomeElec, SousBSoufre, SousBHelium, SousBFoudre,  BiomeForet, SousBJungle, SousBBiolumi, SousBMarecage, 

    SommaireBestiaire,
    MagastalMontagne, FrengMontagne, SregolGrotte, FrengGrotte,  ChokaroTaiga, MagastalTaiga,
    SregolBasalte, DeshrogBasalte, CarabosDune, EvisparDune, CarabosRocheux,
    KerusculaJungle, PhacoliereJungle, EvisparMarais, PhacoliereMarais,  EvisparBiolumi,
    KerusculaFoudre, DeshrogFoudre, CresholdSoufre, CresholdHelium, ChokaroHelium, 
   

    SommaireIngredient,
    Shembo, Frejal, Kleck, Siarym, Phylliul, Scolk ,

    SommaireAlmanach,

    FichePersoElira,
    FichePersoSamuel,
    FichePersoSaori,
    FichePersoVikram,
    Sociagramme,
}

public class SCR_MasterCompendium : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private RectTransform infoBulle;
    [SerializeField] private enumPage actualPage;
    [System.Serializable] public class dicoPageClass : TemplateDico<enumPage, GameObject> { };

    [SerializeField] List<dicoPageClass> dicoPageList;

    public Dictionary<enumPage, GameObject> dicoPage;

    public static SCR_MasterCompendium instanceMComp;

    private GameObject pageToDestroy;

    [SerializeField] private GameObject BGClickable;
    [SerializeField] private GameObject ButtonCompendium;

    private bool isOpen;

    [SerializeField] SpriteRenderer outlineMaterial;


    public static event Action<SO_Tuto> clickOnCompendium;

    [SerializeField] private SO_Tuto tutoCompendium;



    private void Awake() // singleton toi meme tu sais
    {
        if (instanceMComp == null)
            instanceMComp = this;
        else
            Destroy(gameObject);

        // pageToDestroy = Instantiate(dicoPage[enumPage.Sommaire], transform);
        SCR_TutoManager.instanceTuto.CallPersistentTuto(tutoCompendium, SCR_TutoManager.enumEmplacement.right, clickOnCompendium);


    }



    public void PrevPage()
    {
        if (actualPage == enumPage.Sommaire)
            return;

        if (pageToDestroy != null)
            Destroy(pageToDestroy);
        actualPage--;
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        AudioManager.instanceAM.Play("SwiftPage");

    }

    public void NextPage()
    {
        
       
        
        if (pageToDestroy != null)
            Destroy(pageToDestroy);

        

        actualPage++;
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        AudioManager.instanceAM.Play("SwiftPage");
    }

    public void GoToPage(GameObject gActualPage)
    {
     
        if (pageToDestroy != null)
            Destroy(pageToDestroy);
        actualPage = GetKeyFromValue(gActualPage);
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        AudioManager.instanceAM.Play("SwiftPage");

    }

    public enumPage GetKeyFromValue(GameObject goPage)
    {
        foreach (enumPage keyVar in dicoPage.Keys)
        {
            if (dicoPage[keyVar] == goPage)
            {
                return keyVar;
            }
        }
        return enumPage.Sommaire;
    }
    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        dicoPage = new Dictionary<enumPage, GameObject>();
        foreach (dicoPageClass item in dicoPageList)
        {
            if (!dicoPage.ContainsKey(item.key))
            {
                dicoPage.Add(item.key, item.value);
            }
        }


    }

    public void OpenComp()
    {
        clickOnCompendium?.Invoke(tutoCompendium);

        isOpen = true;
        BGClickable.SetActive(true);
        AudioManager.instanceAM.Play("SesameOuvretoi");
        ButtonCompendium.SetActive(false);
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        
    }
    public void CloseComp()
    {
        outlineMaterial.material.SetFloat("_Thickness", 0f);


        isOpen = false;
        BGClickable.SetActive(false);
        AudioManager.instanceAM.Play("SesameFermetoi");
        ButtonCompendium.SetActive(true);

        if (pageToDestroy != null)
            Destroy(pageToDestroy);
    }

    public void PopUpInfo(SO_PopUp info)
    {
        infoBulle.gameObject.SetActive(true);
        infoBulle.GetComponentInChildren<Text>().text = info.textInfo;        
        infoBulle.transform.localPosition = info.position;
        infoBulle.sizeDelta = info.dimension;
        AudioManager.instanceAM.Play("PopUpFiche");
    }
    public void PopUpEnd()
    {
        infoBulle.gameObject.SetActive(false);
    }

   

    public void HighLight(bool onHoover)
    {
        if (SCR_PauseMenu.instancePauseMenu.GetIsPause() || isOpen)
            return;

        if (onHoover)
        {
            outlineMaterial.material.SetFloat("_Thickness", 0.09f);
            AudioManager.instanceAM.Play("CompendiumOverride");

        }
        else
            outlineMaterial.material.SetFloat("_Thickness", 0f);
        


    }


    public bool GetIsOpen() => isOpen;
}
