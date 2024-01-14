using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SCR_Etagere;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Ingredient", order = 1)]
public class SCR_SO_Ingredient : ScriptableObject, ISerializationCallbackReceiver
{

    #region pour les dicos

    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, int> { };
    public Dictionary<enumResistance, int> dicoResistance; // dictionnaire des resistances, on associe une resistance à un int, pas un float car y'a jamais de virgule


    [System.Serializable] public class dicoIngredientTransfoClass : TemplateDico<enumEtatIgredient, SCR_SO_Ingredient> { };
    public Dictionary<enumEtatIgredient, SCR_SO_Ingredient> dicoIngredientTransfo; // dicitonnaire qui permet de referencer le SO correspondant à l'etat transformé de cet ingrédient

    #endregion





    public enumAllIgredient myEnumIngredientSO; // definit il s'agit de quel ingrédient dans la list de tout les ingrédients
    public Sprite mySpriteSO; // le sprite associé à l'ingrédient / ingrédient transformé
    public enumEtatIgredient actualStateSO; // définit l'état actuel de l'ingrédient 
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor
    public int stockSO; // c'est le stock... ça me parait évident, int psk y'aura pas de demi-ingrédient 
    [SerializeField] private List<dicoIngredientTransfoClass> listDicoIngredientTransfo; // permet de visualiser le dico qui référencie les ingrédient transfo, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    public void OnAfterDeserialize() // fonction qui permet d'associer les listes (qu'on voit) aux dictionaires (qu'on ne voit pas)
    {

        dicoResistance = new Dictionary<enumResistance, int>();
        foreach (dicoResistanceClass item in listDicoResistance)
        {
            if (!dicoResistance.ContainsKey(item.key))
            {
                dicoResistance.Add(item.key, item.value);
            }
        }


        dicoIngredientTransfo = new Dictionary<enumEtatIgredient, SCR_SO_Ingredient>();
        foreach (dicoIngredientTransfoClass item in listDicoIngredientTransfo)
        {
            if (!dicoIngredientTransfo.ContainsKey(item.key))
            {
                dicoIngredientTransfo.Add(item.key, item.value);
            }
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
