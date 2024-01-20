using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Tasse : SCR_Contenant
{

    [SerializeField] private List<SCR_Ingredient> listIngredientsUtilises; // liste des ingrédients qui ont ete drop dans la tasse, sera utilise pour l'historique
    private Dictionary<enumResistance, int> dicoStatBoisson = new Dictionary<enumResistance, int>(); // dico des stats de la boisson

    [SerializeField] private SCR_Bouilloire refBouilloire; // ref a la bouilloire pour la débloquer lorsqu'il y a 3 ingrédients dans la tasse

    // Start is called before the first frame update
    void Start()
    {
        ResetBoisson(); // initialise le dictionnaire de stat
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        listIngredientsUtilises.Add(ingredientDropParameter); // ajoute l'ingrédient drop sur la tasse a la liste des ingrédients utilisés pour la boisson
        ingredientDrop.gameObject.SetActive(false); // désactive l'ingrédient drop, faudra le renvoyer dans le pool plutot

        CalCulStat(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance); // met a jour les stats de la boisson avec l'ingrédient qui a ete drop

        if(listIngredientsUtilises.Count == 3) // si il y a 3 ingrédient dans la tasse 
        {
            refBouilloire.UnlockBouilloire(); // alors on débloque le fait de pouvoir manipuler la bouilloire
        }
    }

    private void CalCulStat(Dictionary<enumResistance,int> statIngredientParameter) // fonction qui permet de calculer les stats de la boisson en prennant en parametre le dico de stat de l'ingrédient
    {


        foreach (KeyValuePair<enumResistance,int> resistance in statIngredientParameter) // pour chaque paire (quand il y a une clé associé a une valeur),on fait ce qu'il y a ci dessous
        {
            dicoStatBoisson[resistance.Key] += statIngredientParameter[resistance.Key]; // on prend la meme clé dans le dico de la boisson que la clé de l'ingrédient et on ajoute la valuer de l'ingrédient
           
        }

        foreach (KeyValuePair<enumResistance, int> resistance in dicoStatBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }

    }

    public void ResetBoisson()
    {
        if(dicoStatBoisson.Count != 6) // sera effectué au start pour initialiser le dico
        {
            dicoStatBoisson.Add(enumResistance.Hemorragique, 0);
            dicoStatBoisson.Add(enumResistance.Cryogenique, 0);
            dicoStatBoisson.Add(enumResistance.Thermique, 0);
            dicoStatBoisson.Add(enumResistance.Toxique, 0);
            dicoStatBoisson.Add(enumResistance.Electrique, 0);
            dicoStatBoisson.Add(enumResistance.Lethargique, 0);
        }
        else // si le dico est deja intialisé, passe juste toutes les stats à 0
        {
            dicoStatBoisson[enumResistance.Hemorragique] = 0;
            dicoStatBoisson[enumResistance.Cryogenique] = 0;
            dicoStatBoisson[enumResistance.Thermique] = 0;
            dicoStatBoisson[enumResistance.Toxique] = 0;
            dicoStatBoisson[enumResistance.Electrique] = 0;
            dicoStatBoisson[enumResistance.Lethargique] = 0;
        }
        
    }
}
