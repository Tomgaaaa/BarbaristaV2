using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bol : SCR_Contenant, ISerializationCallbackReceiver
{
    #region dico
    [System.Serializable] public class dicoEtiquetteUstensileClass : TemplateDico<enumEtatIgredient, Sprite> { };
    [SerializeField] private List<dicoEtiquetteUstensileClass> listDicoEtiquetteUstensile; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumEtatIgredient, Sprite> dicoEtiquetteUstensile; // dico où on associe un etat d'ingrédient à un sprite pour afficher le bon ustensile qui a été utilisé
    #endregion

    [SerializeField] private SpriteRenderer etiquette; // reference du spriteRenderer de l'etiquette
    [SerializeField] private List<SCR_Bol> listOtherBol; // liste des autres bols pour verifier si ils sont plein 
    [SerializeField] private SCR_Etagere refEtagere; // ref a l'etagere pour bloquer les ingrédient lorsqu'on passa a la bouilloire, mais ça va pas rester la

    private void Start()
    {

    }

    public void OnAfterDeserialize()
    {
        dicoEtiquetteUstensile = new Dictionary<enumEtatIgredient, Sprite>();
        foreach (dicoEtiquetteUstensileClass item in listDicoEtiquetteUstensile)
        {
            if (!dicoEtiquetteUstensile.ContainsKey(item.key))
            {
                dicoEtiquetteUstensile.Add(item.key, item.value);
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appellé lorsqu'un ingrédient a été drop sur un bol
    {
        if(ingredientDropParameter.GetCR_SO_Ingredient().actualStateSO != enumEtatIgredient.Nature) // verifie que l'ingrédient qui a ete drop, est un ingrédient transformé
        {
            base.OnDrop(ingredientDropParameter);

            etiquette.sprite = dicoEtiquetteUstensile[ingredientDrop.GetCR_SO_Ingredient().actualStateSO]; // update l'etiquette pour afficher l'ustensile utilisé sur l'ingrédient
            etiquette.gameObject.SetActive(true); // active l'etiquette qui est désactivé de base


            if (listOtherBol[0].GetNombreIngredient() == 1 && listOtherBol[1].GetNombreIngredient() == 1 ) // verifie si les autres ingrédients ont un ingrédient
            {
                UnlockBouilloire(); // si tout les bols ont un ingrédient alors on passe à la bouilloire
            }

        }



    }

    public override void PickUpFromContenant() // fonction appellé lorsqu'un ingrédient est drag et qu'il est dans un bol
    {
        base.PickUpFromContenant();
        etiquette.gameObject.SetActive(false); // désactive l'etiquette car l'ingrédient a été repris
    }



    public void UnlockBouilloire() // fonction appellé lorsque les 3 bols ont un 1 ingrédient
    {
        SCR_CuisineManager.instanceCM.TransitionBouilloire(); // affiche le volet qui permet de bloquer le fait de prendre des ingrédient, ça va changer
    }

    public int GetNombreIngredient() { return nmbIngredientIn; } // permet de recuperer le nombre d'ingrédient present dans le bol pour verifier s'il y a un ingrédient
}
