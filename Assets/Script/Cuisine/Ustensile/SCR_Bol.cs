using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;
using static SCR_Etagere;

public class SCR_Bol : SCR_Contenant, ISerializationCallbackReceiver
{
    #region dico
    [System.Serializable] public class dicoEtiquetteUstensileClass : TemplateDico<enumEtatIgredient, Sprite> { };
    [SerializeField] private List<dicoEtiquetteUstensileClass> listDicoEtiquetteUstensile; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumEtatIgredient, Sprite> dicoEtiquetteUstensile;
    #endregion

    [SerializeField] private SpriteRenderer etiquette;
    [SerializeField] private List<SCR_Bol> listOtherBol;
    [SerializeField] private SCR_Etagere refEtagere;

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

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        if(ingredientDropParameter.GetCR_SO_Ingredient().actualStateSO != enumEtatIgredient.Nature)
        {
            base.OnDrop(ingredientDropParameter);

            etiquette.sprite = dicoEtiquetteUstensile[ingredientDrop.GetCR_SO_Ingredient().actualStateSO];
            etiquette.gameObject.SetActive(true);


            if (listOtherBol[0].GetNombreIngredient() == 1 && listOtherBol[1].GetNombreIngredient() == 1 )
            {
                UnlockBouilloire();
            }

        }



    }

    public override void PickUpFromContenant()
    {
        base.PickUpFromContenant();
        etiquette.gameObject.SetActive(false);
    }



    public void UnlockBouilloire()
    {
        Debug.Log("passage a la bouilloire");
        refEtagere.LockIngredient();
    }

    public int GetNombreIngredient() { return nmbIngredientIn; }
}
