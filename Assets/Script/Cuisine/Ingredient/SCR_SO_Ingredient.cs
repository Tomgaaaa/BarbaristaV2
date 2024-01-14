using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SCR_Etagere;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Ingredient", order = 1)]
public class SCR_SO_Ingredient : ScriptableObject, ISerializationCallbackReceiver
{

    #region pour les dicos

    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, int> { };
    public Dictionary<enumResistance, int> dicoResistance; // dictionnaire des resistances, on associe une resistance � un int, pas un float car y'a jamais de virgule


    [System.Serializable] public class dicoIngredientTransfoClass : TemplateDico<enumEtatIgredient, SCR_SO_Ingredient> { };
    public Dictionary<enumEtatIgredient, SCR_SO_Ingredient> dicoIngredientTransfo; // dicitonnaire qui permet de referencer le SO correspondant � l'etat transform� de cet ingr�dient

    #endregion





    public enumAllIgredient myEnumIngredientSO; // definit il s'agit de quel ingr�dient dans la list de tout les ingr�dients
    public Sprite mySpriteSO; // le sprite associ� � l'ingr�dient / ingr�dient transform�
    public enumEtatIgredient actualStateSO; // d�finit l'�tat actuel de l'ingr�dient 
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des r�sistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en �ditor
    public int stockSO; // c'est le stock... �a me parait �vident, int psk y'aura pas de demi-ingr�dient 
    [SerializeField] private List<dicoIngredientTransfoClass> listDicoIngredientTransfo; // permet de visualiser le dico qui r�f�rencie les ingr�dient transfo, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en �ditor

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
