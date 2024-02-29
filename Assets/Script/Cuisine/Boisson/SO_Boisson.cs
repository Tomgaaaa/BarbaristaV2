using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Boisson : ScriptableObject 
{
    public List<SCR_Ingredient> listIngredientsUtilises = new List<SCR_Ingredient>();

    public Sprite screenBoisson;

    public Dictionary<enumResistance, float> dicoResistanceBoisson = new Dictionary<enumResistance, float>
    {
        { enumResistance.Electrique , 0f},
        { enumResistance.Hemorragique , 0f},
        { enumResistance.Toxique , 0f},
        { enumResistance.Lethargique , 0f},
        { enumResistance.Cryogenique , 0f},
        { enumResistance.Thermique , 0f}
    
    
    
    }; // dictionnaire des resistances, on associe une resistance à un float, float psk pour bouger dans l'hexagone il faut un float

    public void CreateBoisson(List<SCR_Ingredient> listIngredientParameter, Dictionary<enumResistance, float> dicoResistanceParameter)
    {

        listIngredientsUtilises = listIngredientParameter;

        dicoResistanceBoisson = dicoResistanceParameter;
    }

   
    public Dictionary<enumResistance, float> GetStat() => dicoResistanceBoisson;
   

}
