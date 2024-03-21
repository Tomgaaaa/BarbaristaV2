using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Quete", menuName = "ScriptableObjects/QueteInfo")]
public class SO_Quete : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, float> { };
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    public Dictionary<enumResistance, float> dicoResistanceDifficulte; // dictionnaire des resistances, on associe une resistance à un float, float psk pour bouger dans l'hexagone il faut un float
    public string titre;
    public GameObject difficultySprite;
    [Range(1,3)]
    public int difficultyInt = 1;
    public Sprite illustration;
    [TextArea(6,10)]
    public string description;
    [TextArea(6,10)]
    public string Evenement;
    public string Temps;
    public string Meteo;
    public string Altitude;
    public string Biome;
    public string SBiome;


    //public List<Image> reward;
    public List<SCR_SO_Ingredient> reward;


    public List<SO_Personnage> persosEnvoyes = new List<SO_Personnage>();
    public List<SO_Boisson> boissonsServis = new List<SO_Boisson>();

    public TextAsset myQueteInk;

    public bool hasWinMission;
    


    public void OnAfterDeserialize() // fonction qui permet d'associer les listes (qu'on voit) aux dictionaires (qu'on ne voit pas)
    {
        persosEnvoyes.Clear();
        boissonsServis.Clear();


        dicoResistanceDifficulte = new Dictionary<enumResistance, float>();
        foreach (dicoResistanceClass item in listDicoResistance)
        {
            if (!dicoResistanceDifficulte.ContainsKey(item.key))
            {
                dicoResistanceDifficulte.Add(item.key, item.value);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void Init(Dictionary<enumResistance, float> dicoResistanceDifficulteParameter,string titreParameter,int difficultyIntParameter, GameObject difficultyParameter, Sprite illustrationParameter, string descriptionParameter, string infoEvenementParameter,string biomeParameter, string sBiomeParameter, string tempsParameter, string meteoParameter, string altitudeParameter, List<SCR_SO_Ingredient> rewardParameter,TextAsset textInkParameter)
    {
        dicoResistanceDifficulte = dicoResistanceDifficulteParameter;
        titre = titreParameter;
        difficultySprite = difficultyParameter;
        difficultyInt = difficultyIntParameter;
        illustration = illustrationParameter;
        description = descriptionParameter;
        Evenement = infoEvenementParameter;
        Biome = biomeParameter;
        SBiome = sBiomeParameter;
        Temps = tempsParameter;
        Meteo = meteoParameter;
        Altitude = altitudeParameter;
        reward = rewardParameter;
        myQueteInk = textInkParameter;

    }

  
}
