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
    public List<Image> difficulty;
    public Sprite illustration; 
    public string description;
    public string infoEvenement;
    public List<Image> reward;


    public List<SO_Personnage> persosEnvoyes = new List<SO_Personnage>();
    public List<SO_Boisson> boissonsServis = new List<SO_Boisson>();

    public TextAsset myQueteInk;
    


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

    public void Init(Dictionary<enumResistance, float> dicoResistanceDifficulteParameter,string titreParameter, List<Image> difficultyParameter, Sprite illustrationParameter, string descriptionParameter, string infoEvenementParameter, List<Image> rewardParameter,TextAsset textInkParameter)
    {
        dicoResistanceDifficulte = dicoResistanceDifficulteParameter;
        titre = titreParameter;
        difficulty = difficultyParameter;
        illustration = illustrationParameter;
        description = descriptionParameter;
        infoEvenement = infoEvenementParameter;
        reward = rewardParameter;
        myQueteInk = textInkParameter;

    }
    

}
