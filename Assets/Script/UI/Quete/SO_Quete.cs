using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Quete", menuName = "ScriptableObjects/QueteInfo")]
public class SO_Quete : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, float> { };
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    public Dictionary<enumResistance, float> dicoResistance; // dictionnaire des resistances, on associe une resistance à un float, float psk pour bouger dans l'hexagone il faut un float
    public string titre;
    public List<Image> difficulty;
    public Sprite illustration; 
    public string description;
    public string infoEvenement;
    public List<Image> reward;


    public List<SO_Personnage> persosEnvoyes = new List<SO_Personnage>();
    public List<SO_Boisson> boissonsServis = new List<SO_Boisson>();
    public Dictionary<enumResistance, float> dicoResistanceJoueur; // dictionnaire des resistances, stat que le joueur a fait 


    public void OnAfterDeserialize() // fonction qui permet d'associer les listes (qu'on voit) aux dictionaires (qu'on ne voit pas)
    {
        persosEnvoyes.Clear();
        boissonsServis.Clear();

        dicoResistance = new Dictionary<enumResistance, float>();
        foreach (dicoResistanceClass item in listDicoResistance)
        {
            if (!dicoResistance.ContainsKey(item.key))
            {
                dicoResistance.Add(item.key, item.value);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
    

}
