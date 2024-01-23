using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enumPage { 

    Sommaire,

    SommaireBiome,
    BiomeGlace, BiomeFeu, BiomeElec, BiomeForet,
    SousBMontagne, SousBGrotte, SousBTaiga, SousBVolcan, SousBDune, SousBRocheuse,
    SousBFoudre, SousBHelium, SousBSoufre,SousBJungle, SousBMarecage, SousBBiolumi,

    SommaireBestiaire,
    FrengMontagne, FrengGrotte, MagastalMontagne, MagastalTaiga, SregolGrotte, ChokaroTaiga, SregolBasalte, DeshrogBasalte, CarabosDune,
    EvisparDune, CarabosRocheux, KerusculaFoudre, CresholdSoufre, CresholdHelium, ChokaroHelium, KerusculaJungle, PhacoliereJungle, PhacoliereMarais,
    EvisparMarais, EvisparBiolumi,

    SommaireIngredient,
    Frejal, Kleck, Phylliul, Scolk, Shembo, Siarym }

public class SCR_Compendium : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private List<GameObject> _page;
    [SerializeField]private int _currentPage;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _simpleFlipPage;

    [System.Serializable] public class dicoPageClass : TemplateDico<enumPage, GameObject> { };
    [SerializeField] List<dicoPageClass> dicoPageList;

    private Dictionary<enumPage, GameObject> dicoPage;
    public virtual void NextPage()
    {
        _audio.PlayOneShot(_simpleFlipPage);

        _page[_currentPage].SetActive(false);
        _page[_currentPage + 1].SetActive(true);
        _currentPage++;
        
    }

    public virtual void PreviewPage()
    {
        _audio.PlayOneShot(_simpleFlipPage);
        _page[_currentPage].SetActive(false);
        _page[_currentPage - 1].SetActive(true);
        _currentPage--;
    }

    public virtual void GoToPage(int page)
    {
        _audio.PlayOneShot(_simpleFlipPage);
        _page[_currentPage].SetActive(false);
        _page[page].SetActive(true);
        _currentPage = page;
    }

    public void OnBeforeSerialize()
    {
       
    }

    public void OnAfterDeserialize()
    {
        dicoPage = new Dictionary<enumPage, GameObject>();
        foreach(dicoPageClass item in dicoPageList)
        {
            if(!dicoPage.ContainsKey(item.key))
            {
                dicoPage.Add(item.key, item.value);
            }
        }
        
    }
}
