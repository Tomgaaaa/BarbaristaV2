using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Tasse : SCR_Contenant
{

    [SerializeField] private List<SCR_Ingredient> listIngredientsUtilises;
    private Dictionary<enumResistance, int> dicoStatBoisson = new Dictionary<enumResistance, int>();

    // Start is called before the first frame update
    void Start()
    {
        ResetBoisson();
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        listIngredientsUtilises.Add(ingredientDropParameter);
        ingredientDrop.gameObject.SetActive(false);

        CalCulStat(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance);
    }

    private void CalCulStat(Dictionary<enumResistance,int> statIngredientParameter) 
    {


        foreach (KeyValuePair<enumResistance,int> resistance in statIngredientParameter)
        {
            dicoStatBoisson[resistance.Key] += statIngredientParameter[resistance.Key];
           
        }

        foreach (KeyValuePair<enumResistance, int> resistance in dicoStatBoisson)
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }

    }

    public void ResetBoisson()
    {

        dicoStatBoisson.Add(enumResistance.Hemorragique,0);
        dicoStatBoisson.Add(enumResistance.Cryogenique,0);
        dicoStatBoisson.Add(enumResistance.Thermique,0);
        dicoStatBoisson.Add(enumResistance.Toxique,0);
        dicoStatBoisson.Add(enumResistance.Electrique,0);
        dicoStatBoisson.Add(enumResistance.Lethargique,0);
    }
}
