using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Boisson : ScriptableObject 
{
    public List<SCR_Ingredient> listIngredientsUtilises = new List<SCR_Ingredient>(); // liste des ingrédients transformés utilisés pour créer la boisson 

    public Sprite screenBoisson; // ça on verra plus tard pour l'affichage dans l'historique

    public GameObject tasseVN;


    public Dictionary<enumResistance, float> dicoResistanceBoisson = new Dictionary<enumResistance, float> // dico des resistances de la boisson, on l'initialise a 0 
    {
        { enumResistance.Electrique , 0f},
        { enumResistance.Hemorragique , 0f},
        { enumResistance.Toxique , 0f},
        { enumResistance.Lethargique , 0f},
        { enumResistance.Cryogenique , 0f},
        { enumResistance.Thermique , 0f}
    
    
    
    }; 

    public void CreateBoisson(List<SCR_Ingredient> listIngredientParameter, Dictionary<enumResistance, float> dicoResistanceParameter) // fonction qui permet de creer la boisson (obviously)
    {

        listIngredientsUtilises = listIngredientParameter; // on recupere la liste des ingredient utilises

        dicoResistanceBoisson = dicoResistanceParameter; // et le dico des resistances associés
    }

   
    public Dictionary<enumResistance, float> GetStat() => dicoResistanceBoisson; // fonction pour recuperer les stats de la boisson
   
    public void SetTasseVN(GameObject obj) => tasseVN = obj;
}
