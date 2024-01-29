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
    Frejal, Kleck, Phylliul, Scolk, Shembo, Siarym
}

public class SCR_MasterCompendium : MonoBehaviour, ISerializationCallbackReceiver
{
  
    private List<GameObject> listPage;
    [SerializeField] private enumPage actualPage;
    [System.Serializable] public class dicoPageClass : TemplateDico<enumPage, GameObject> { };

    [SerializeField] List<dicoPageClass> dicoPageList;

    public Dictionary<enumPage, GameObject> dicoPage;

    public static SCR_MasterCompendium instanceMComp;

    private GameObject pageToDestroy;

    private void Awake() // singleton toi meme tu sais
    {
        if (instanceMComp == null)
            instanceMComp = this;
        else
            Destroy(gameObject);

        pageToDestroy = Instantiate(dicoPage[enumPage.Sommaire], transform);
    }

    

    public void PrevPage()
    {
        if (pageToDestroy != null)
            Destroy(pageToDestroy);
        actualPage--;
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);

    }

    public void NextPage()
    {
        if (pageToDestroy != null)
            Destroy(pageToDestroy);
        actualPage++;
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
    }

    public void GoToPage(GameObject gActualPage)
    {
        if (pageToDestroy != null)
            Destroy(pageToDestroy);
        actualPage = GetKeyFromValue(gActualPage);
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        
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

}
