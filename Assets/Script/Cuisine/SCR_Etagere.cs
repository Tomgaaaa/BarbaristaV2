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
    private Dictionary<enumAllIgredient, GameObject> dicoOutOfStock; // le GameOject est le sprite OutOfSTock a afficher quand il n'y a plus d'ingrédient


    [System.Serializable] public class dicoTextStockClass : TemplateDico<enumAllIgredient, Text> { };
    [SerializeField] private List<dicoTextStockClass> listDicoText; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumAllIgredient, Text> dicoTextStock; // renseigne le text qui affiche le stock d'un ingrédient


    [System.Serializable] public class dicoPositionIngredient : TemplateDico<SCR_SO_Ingredient, Vector3> { };
    [SerializeField] private List<dicoPositionIngredient> listDicoPosition; // juste pour visualiser le dico ci-dessous
    private Dictionary<SCR_SO_Ingredient, Vector3> dicoPosition; // renseigne la position des ingrédients dans l'etagere


    [System.Serializable] public class dicoOutlineMaterialClass : TemplateDico<enumAllIgredient, Material> { };
    [SerializeField] private List<dicoOutlineMaterialClass> listOutlineMaterial; // juste pour visualiser le dico ci-dessous
    private Dictionary<enumAllIgredient, Material> dicoOutlineMaterial; 

    #endregion



    [SerializeField] private SCR_Pool refPool; // besoin du pool quand on veut rajouter un ingrédient dans le stock et que le stock est à 0

   



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


    public void AddIngredient(SCR_SO_Ingredient ingredientAddParameter) // fonction appelé par les boutons, permet de rajouter 1 au stock d'un ingrédient
    {
        if(ingredientAddParameter.stockSO == 0) // si le stock est a 0 il y a quelque truc a faire en +
        {
            

            SpawnIngredient(ingredientAddParameter,true); // force le spawn d'un ingrédient car on veut en rajouter un
            


            GameObject etiquetteOutOfStock = dicoOutOfStock[ingredientAddParameter.myEnumIngredientSO]; // recupere quel objet OutOfStock il faut désactiver
            etiquetteOutOfStock.SetActive(false); // désactive le OutOfStock car on rajouter 1 au stock dans il n'est plus "out of stock"
        }

        ingredientAddParameter.stockSO++; // rajoute 1 au stock
        UpdateStockIngredient(ingredientAddParameter); // fonction expliqué ci-dessous

        

    }

    public void SpawnIngredient(SCR_SO_Ingredient ingredientRemoveParameter, bool forceSpawnParameter = false) // fonction appelé lorsqu'on clique sur l'ingrédient et qu'il etait sur l'etagere, forceSpawn permet de forcer le spawn meme si il n'y a plus de stock
    {
        if (ingredientRemoveParameter.stockSO > 0 || forceSpawnParameter) // s'il y a encore du stock ou qu'on force le spawn d'un ingrédient
        {
            SCR_PoolItem poolItem = refPool.Instantiate(); // on "instancie" un pool item depuis le pool
           SCR_Ingredient poolIngredient = poolItem.GetComponent<SCR_Ingredient>(); // on cast le pool item en ingrédient
           poolIngredient.SetSoIngredient(ingredientRemoveParameter, this); // définit quel est l'ingrédient 
           poolIngredient.Init(refPool); // permet d'avoir la reference du pool
           poolIngredient.transform.position = dicoPosition[ingredientRemoveParameter]; // positionne l'ingrédient à la position necessaire dans l'etagere, cette position est recuperé grace au dico position

           poolIngredient.GetComponent<Renderer>().material = dicoOutlineMaterial[ingredientRemoveParameter.myEnumIngredientSO];
        }
        else // si on a plus de stock
        {
            Debug.Log("out of stock");
            AudioManager.instanceAM.Play("OutOfStock");
        }

    }

    public void UpdateStockIngredient(SCR_SO_Ingredient IngredientPris ) // fonction qui met a jour le text de stock d'un ingrédient, appelé lorsqu'un ingrédient est pris
    {
        dicoTextStock[IngredientPris.myEnumIngredientSO].text = "x" + IngredientPris.stockSO; ; // recupere quel text on doit update grace au dico, le texte affiché est juste un X et me int du stock de l'ingrédient 

        if (IngredientPris.stockSO == 0) // si le stock d'un ingrédient qu'on a pris arrive a 0, il faut faire quelques trucs
        {
            GameObject etiquetteOutOfStock =  dicoOutOfStock[IngredientPris.myEnumIngredientSO]; // recupere le OutOfStock associé a l'ingrédient pris
            etiquetteOutOfStock.SetActive(true); //active l'objet, qui est désactiver de base

            // animation 
            Vector3 ancienScale = etiquetteOutOfStock.transform.localScale; // stock le scale d'origine 
            etiquetteOutOfStock.transform.localScale= ancienScale * 2f; // change le scale de l'objet en X2
            etiquetteOutOfStock.transform.DOScale(ancienScale, 0.3f); // change le scale de l'objet, il passe de X2 à son ancien scale (X1) en 0.3 secondes
        }
    }


    


    public void OnBeforeSerialize()
    {
    }




}
