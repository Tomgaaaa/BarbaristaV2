using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Quete", menuName = "ScriptableObjects/QueteInfo")]
public class SO_Quete : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, float> { };
    public Dictionary<enumResistance, float> dicoResistance; // dictionnaire des resistances, on associe une resistance � un float, float psk pour bouger dans l'hexagone il faut un float
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des r�sistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en �ditor

    public string titre;
    public List<Image> difficulty;
    public Sprite illustration; 
    public string description;
    public List<Image> reward;

    public void OnAfterDeserialize() // fonction qui permet d'associer les listes (qu'on voit) aux dictionaires (qu'on ne voit pas)
    {

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
