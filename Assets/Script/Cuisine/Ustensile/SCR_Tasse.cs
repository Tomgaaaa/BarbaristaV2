using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Tasse : SCR_Contenant
{

    [SerializeField] private List<SCR_Ingredient> listIngredientsUtilises; // liste des ingr�dients qui ont ete drop dans la tasse, sera utilise pour l'historique
    private Dictionary<enumResistance, int> dicoStatBoisson = new Dictionary<enumResistance, int>(); // dico des stats de la boisson

    [SerializeField] private SCR_Bouilloire refBouilloire; // ref a la bouilloire pour la d�bloquer lorsqu'il y a 3 ingr�dients dans la tasse

    // Start is called before the first frame update
    void Start()
    {
        ResetBoisson(); // initialise le dictionnaire de stat
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        listIngredientsUtilises.Add(ingredientDropParameter); // ajoute l'ingr�dient drop sur la tasse a la liste des ingr�dients utilis�s pour la boisson
        ingredientDrop.gameObject.SetActive(false); // d�sactive l'ingr�dient drop, faudra le renvoyer dans le pool plutot

        CalCulStat(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance); // met a jour les stats de la boisson avec l'ingr�dient qui a ete drop

        if(listIngredientsUtilises.Count == 3) // si il y a 3 ingr�dient dans la tasse 
        {
            refBouilloire.UnlockBouilloire(); // alors on d�bloque le fait de pouvoir manipuler la bouilloire
        }
    }

    private void CalCulStat(Dictionary<enumResistance,int> statIngredientParameter) // fonction qui permet de calculer les stats de la boisson en prennant en parametre le dico de stat de l'ingr�dient
    {


        foreach (KeyValuePair<enumResistance,int> resistance in statIngredientParameter) // pour chaque paire (quand il y a une cl� associ� a une valeur),on fait ce qu'il y a ci dessous
        {
            dicoStatBoisson[resistance.Key] += statIngredientParameter[resistance.Key]; // on prend la meme cl� dans le dico de la boisson que la cl� de l'ingr�dient et on ajoute la valuer de l'ingr�dient
           
        }

        foreach (KeyValuePair<enumResistance, int> resistance in dicoStatBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }

    }

    public void ResetBoisson()
    {
        if(dicoStatBoisson.Count != 6) // sera effectu� au start pour initialiser le dico
        {
            dicoStatBoisson.Add(enumResistance.Hemorragique, 0);
            dicoStatBoisson.Add(enumResistance.Cryogenique, 0);
            dicoStatBoisson.Add(enumResistance.Thermique, 0);
            dicoStatBoisson.Add(enumResistance.Toxique, 0);
            dicoStatBoisson.Add(enumResistance.Electrique, 0);
            dicoStatBoisson.Add(enumResistance.Lethargique, 0);
        }
        else // si le dico est deja intialis�, passe juste toutes les stats � 0
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
