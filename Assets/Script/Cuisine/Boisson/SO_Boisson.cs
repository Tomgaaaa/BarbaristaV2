using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Boisson : ScriptableObject 
{
    public List<SCR_SO_Ingredient> listIngredientsUtilises = new List<SCR_SO_Ingredient>(); // liste des ingr�dients transform�s utilis�s pour cr�er la boisson 

    public List<int> spriteRendererUtilise = new List<int>();


    public Dictionary<enumResistance, float> dicoResistanceBoisson = new Dictionary<enumResistance, float> // dico des resistances de la boisson, on l'initialise a 0 
    {
        { enumResistance.Electrique , 0f},
        { enumResistance.Hemorragique , 0f},
        { enumResistance.Toxique , 0f},
        { enumResistance.Lethargique , 0f},
        { enumResistance.Cryogenique , 0f},
        { enumResistance.Thermique , 0f}
    
    
    
    }; 

    public void CreateBoisson(List<SCR_SO_Ingredient> listIngredientParameter, Dictionary<enumResistance, float> dicoResistanceParameter, List<int> listSpriteRendererParameter) // fonction qui permet de creer la boisson (obviously)
    {

        listIngredientsUtilises = listIngredientParameter; // on recupere la liste des ingredient utilises

        dicoResistanceBoisson = dicoResistanceParameter; // et le dico des resistances associ�s

        spriteRendererUtilise = listSpriteRendererParameter;
    }

   
    public Dictionary<enumResistance, float> GetStat() => dicoResistanceBoisson; // fonction pour recuperer les stats de la boisson
   
}
