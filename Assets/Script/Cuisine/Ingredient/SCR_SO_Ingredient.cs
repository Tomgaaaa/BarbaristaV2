using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SCR_Etagere;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Ingredient", order = 1)]
public class SCR_SO_Ingredient : ScriptableObject, ISerializationCallbackReceiver
{

    #region pour les dicos
    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, int> { };
    public Dictionary<enumResistance, int> dicoResistance;

    [System.Serializable] public class dicoIngredientTransfoClass : TemplateDico<enumEtatIgredient, SCR_SO_Ingredient> { };
    public Dictionary<enumEtatIgredient, SCR_SO_Ingredient> dicoIngredientTransfo;

    #endregion





    public enumAllIgredient myEnumIngredientSO;
    public Sprite mySpriteSO;
    public enumEtatIgredient actualStateSO;
    [SerializeField] private List<dicoResistanceClass> listDicoResistance;
    public int stockSO;
    [SerializeField] private List<dicoIngredientTransfoClass> listDicoIngredientTransfo;

    public void OnAfterDeserialize()
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
