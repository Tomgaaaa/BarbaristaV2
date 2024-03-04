using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static DictionaryLesson;





public class SCR_Etagere : MonoBehaviour, ISerializationCallbackReceiver // script qui permet de gerer ce qui est visuel au niveau de l'etagere (logique), pas de data a stock ici
{
    #region tout ce qui est dico

    [System.Serializable] public class dicoOutOfStockIngredientClass : TemplateDico<enumAllIgredient, GameObject> { };
    [SerializeField] private List<dicoOutOfStockIngredientClass> listDicoOutOfStock; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumAllIgredient, GameObject> dicoOutOfStock; // le GameOject est le sprite OutOfSTock a afficher quand il n'y a plus d'ingr�dient


    [System.Serializable] public class dicoTextStockClass : TemplateDico<enumAllIgredient, Text> { };
    [SerializeField] private List<dicoTextStockClass> listDicoText; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumAllIgredient, Text> dicoTextStock; // renseigne le text qui affiche le stock d'un ingr�dient


    [System.Serializable] public class dicoPositionIngredient : TemplateDico<SCR_SO_Ingredient, Vector3> { };
    [SerializeField] private List<dicoPositionIngredient> listDicoPosition; // juste pour visualiser le dico ci-dessous
    private Dictionary<SCR_SO_Ingredient, Vector3> dicoPosition; // renseigne la position des ingr�dients dans l'etagere


    [System.Serializable] public class dicoOutlineMaterialClass : TemplateDico<enumAllIgredient, Material> { };
    [SerializeField] private List<dicoOutlineMaterialClass> listOutlineMaterial; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumAllIgredient, Material> dicoOutlineMaterial; 

    #endregion



    [SerializeField] private SCR_Pool refPool; // besoin du pool quand on veut rajouter un ingr�dient dans le stock et que le stock est � 0

   



    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnAfterDeserialize() // permet de faire le lien list (qu'on voit) -> dico (qu'on voit pas)
    {
        dicoOutOfStock = new Dictionary<enumAllIgredient, GameObject>();
        foreach (dicoOutOfStockIngredientClass item in listDicoOutOfStock)
        {
            if (!dicoOutOfStock.ContainsKey(item.key))
            {
                dicoOutOfStock.Add(item.key, item.value);
            }
        }


        dicoTextStock = new Dictionary<enumAllIgredient, Text>();
        foreach (dicoTextStockClass item in listDicoText)
        {
            if (!dicoTextStock.ContainsKey(item.key))
            {
                dicoTextStock.Add(item.key, item.value);
            }
        }


        dicoPosition = new Dictionary<SCR_SO_Ingredient, Vector3>();
        foreach (dicoPositionIngredient item in listDicoPosition)
        {
            if (!dicoPosition.ContainsKey(item.key))
            {
                dicoPosition.Add(item.key, item.value);
            }
        }

        dicoOutlineMaterial = new Dictionary<enumAllIgredient, Material>();
        foreach (dicoOutlineMaterialClass item in listOutlineMaterial)
        {
            if (!dicoOutlineMaterial.ContainsKey(item.key))
            {
                dicoOutlineMaterial.Add(item.key, item.value);
            }
        }


    }


    public void AddIngredient(SCR_SO_Ingredient ingredientAddParameter) // fonction appel� par les boutons, permet de rajouter 1 au stock d'un ingr�dient
    {
        if(ingredientAddParameter.stockSO == 0) // si le stock est a 0 il y a quelque truc a faire en +
        {
            

            SpawnIngredient(ingredientAddParameter,true); // force le spawn d'un ingr�dient car on veut en rajouter un
            


            GameObject etiquetteOutOfStock = dicoOutOfStock[ingredientAddParameter.myEnumIngredientSO]; // recupere quel objet OutOfStock il faut d�sactiver
            etiquetteOutOfStock.SetActive(false); // d�sactive le OutOfStock car on rajouter 1 au stock dans il n'est plus "out of stock"
        }

        ingredientAddParameter.stockSO++; // rajoute 1 au stock
        UpdateStockIngredient(ingredientAddParameter); // fonction expliqu� ci-dessous

        

    }

    public void SpawnIngredient(SCR_SO_Ingredient ingredientRemoveParameter, bool forceSpawnParameter = false) // fonction appel� lorsqu'on clique sur l'ingr�dient et qu'il etait sur l'etagere, forceSpawn permet de forcer le spawn meme si il n'y a plus de stock
    {
        if (ingredientRemoveParameter.stockSO > 0 || forceSpawnParameter) // s'il y a encore du stock ou qu'on force le spawn d'un ingr�dient
        {
            SCR_PoolItem poolItem = refPool.Instantiate(); // on "instancie" un pool item depuis le pool
           SCR_Ingredient poolIngredient = poolItem.GetComponent<SCR_Ingredient>(); // on cast le pool item en ingr�dient
           poolIngredient.SetSoIngredient(ingredientRemoveParameter, this); // d�finit quel est l'ingr�dient 
           poolIngredient.Init(refPool); // permet d'avoir la reference du pool
           poolIngredient.transform.position = dicoPosition[ingredientRemoveParameter]; // positionne l'ingr�dient � la position necessaire dans l'etagere, cette position est recuper� grace au dico position

           poolIngredient.GetComponent<Renderer>().material = dicoOutlineMaterial[ingredientRemoveParameter.myEnumIngredientSO];
        }
        else // si on a plus de stock
        {
            Debug.Log("out of stock");
            AudioManager.instanceAM.Play("OutOfStock");
        }

    }

    public void UpdateStockIngredient(SCR_SO_Ingredient IngredientPris ) // fonction qui met a jour le text de stock d'un ingr�dient, appel� lorsqu'un ingr�dient est pris
    {
        dicoTextStock[IngredientPris.myEnumIngredientSO].text = "x" + IngredientPris.stockSO; ; // recupere quel text on doit update grace au dico, le texte affich� est juste un X et me int du stock de l'ingr�dient 

        if (IngredientPris.stockSO == 0) // si le stock d'un ingr�dient qu'on a pris arrive a 0, il faut faire quelques trucs
        {
            GameObject etiquetteOutOfStock =  dicoOutOfStock[IngredientPris.myEnumIngredientSO]; // recupere le OutOfStock associ� a l'ingr�dient pris
            etiquetteOutOfStock.SetActive(true); //active l'objet, qui est d�sactiver de base

            // animation 
            Vector3 ancienScale = etiquetteOutOfStock.transform.localScale; // stock le scale d'origine 
            etiquetteOutOfStock.transform.localScale= ancienScale * 2f; // change le scale de l'objet en X2
            etiquetteOutOfStock.transform.DOScale(ancienScale, 0.3f); // change le scale de l'objet, il passe de X2 � son ancien scale (X1) en 0.3 secondes
        }
    }


    


    public void OnBeforeSerialize()
    {
    }




}
