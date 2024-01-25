using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum enumPage
{

    Sommaire,SommaireBiome,BiomeGlace, BiomeFeu, BiomeElec, BiomeForet,SousBMontagne, SousBGrotte, SousBTaiga, SousBVolcan, SousBDune, SousBRocheuse,SousBFoudre, SousBHelium, SousBSoufre, SousBJungle, SousBMarecage, SousBBiolumi,

    SommaireBestiaire,
    FrengMontagne, FrengGrotte, MagastalMontagne, MagastalTaiga, SregolGrotte, ChokaroTaiga, SregolBasalte, DeshrogBasalte, CarabosDune,
    EvisparDune, CarabosRocheux, KerusculaFoudre, CresholdSoufre, CresholdHelium, ChokaroHelium, KerusculaJungle, PhacoliereJungle, PhacoliereMarais,
    EvisparMarais, EvisparBiolumi,

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
        dicoPage[actualPage].SetActive(false);
        actualPage--;
        dicoPage[actualPage].SetActive(true);

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
        pageToDestroy = Instantiate(dicoPage[actualPage], transform);
        actualPage = GetKeyFromValue(gActualPage);
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
