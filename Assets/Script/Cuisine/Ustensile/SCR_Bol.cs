using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bol : SCR_Contenant, ISerializationCallbackReceiver
{
    #region dico
    [System.Serializable] public class dicoEtiquetteUstensileClass : TemplateDico<enumEtatIgredient, Sprite> { };
    [SerializeField] private List<dicoEtiquetteUstensileClass> listDicoEtiquetteUstensile; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumEtatIgredient, Sprite> dicoEtiquetteUstensile; // dico o� on associe un etat d'ingr�dient � un sprite pour afficher le bon ustensile qui a �t� utilis�
    #endregion

    [SerializeField] private SpriteRenderer etiquette; // reference du spriteRenderer de l'etiquette
    [SerializeField] private List<SCR_Bol> listOtherBol; // liste des autres bols pour verifier si ils sont plein 
    [SerializeField] private SCR_Etagere refEtagere; // ref a l'etagere pour bloquer les ingr�dient lorsqu'on passa a la bouilloire, mais �a va pas rester la

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

    public override void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appell� lorsqu'un ingr�dient a �t� drop sur un bol
    {
        if(ingredientDropParameter.GetCR_SO_Ingredient().actualStateSO != enumEtatIgredient.Nature) // verifie que l'ingr�dient qui a ete drop, est un ingr�dient transform�
        {
            base.OnDrop(ingredientDropParameter);

            etiquette.sprite = dicoEtiquetteUstensile[ingredientDrop.GetCR_SO_Ingredient().actualStateSO]; // update l'etiquette pour afficher l'ustensile utilis� sur l'ingr�dient
            etiquette.gameObject.SetActive(true); // active l'etiquette qui est d�sactiv� de base


            if (listOtherBol[0].GetNombreIngredient() == 1 && listOtherBol[1].GetNombreIngredient() == 1 ) // verifie si les autres ingr�dients ont un ingr�dient
            {
                UnlockBouilloire(); // si tout les bols ont un ingr�dient alors on passe � la bouilloire
            }

        }



    }

    public override void PickUpFromContenant() // fonction appell� lorsqu'un ingr�dient est drag et qu'il est dans un bol
    {
        base.PickUpFromContenant();
        etiquette.gameObject.SetActive(false); // d�sactive l'etiquette car l'ingr�dient a �t� repris
    }



    public void UnlockBouilloire() // fonction appell� lorsque les 3 bols ont un 1 ingr�dient
    {
        SCR_CuisineManager.instanceCM.TransitionBouilloire(); // affiche le volet qui permet de bloquer le fait de prendre des ingr�dient, �a va changer
    }

    public int GetNombreIngredient() { return nmbIngredientIn; } // permet de recuperer le nombre d'ingr�dient present dans le bol pour verifier s'il y a un ingr�dient
}
