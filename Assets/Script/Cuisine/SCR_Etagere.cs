using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DictionaryLesson;





public class SCR_Etagere : MonoBehaviour, ISerializationCallbackReceiver
{

    [System.Serializable] public class dicoOutOfStockIngredientClass : TemplateDico<enumAllIgredient, GameObject> { };
    [System.Serializable] public class dicoTextStockClass : TemplateDico<enumAllIgredient, Text> { };
    [System.Serializable] public class dicoPositionIngredient : TemplateDico<SCR_SO_Ingredient, Vector3> { };


    [SerializeField] private List<dicoOutOfStockIngredientClass> listDicoOutOfStock;
    [SerializeField] private List<dicoTextStockClass> listDicoText;
    [SerializeField] private List<dicoPositionIngredient> listDicoPosition;

    private Dictionary<enumAllIgredient, GameObject> dicoOutOfStock;
    private Dictionary<enumAllIgredient, Text> dicoTextStock;
    private Dictionary<SCR_SO_Ingredient, Vector3> dicoposition;

    [SerializeField] private SCR_Pool refPool;




    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnAfterDeserialize()
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


        dicoposition = new Dictionary<SCR_SO_Ingredient, Vector3>();
        foreach (dicoPositionIngredient item in listDicoPosition)
        {
            if (!dicoposition.ContainsKey(item.key))
            {
                dicoposition.Add(item.key, item.value);
            }
        }

    }


    public void AddIngredient(SCR_SO_Ingredient ingredientAdd)
    {
        if(ingredientAdd.stockSO == 0)
        {
            SCR_PoolItem poolItem = refPool.Instantiate();
            SCR_Ingredient poolIngredient = poolItem.GetComponent<SCR_Ingredient>();
            poolIngredient.SetSoIngredient(ingredientAdd, this);
            poolIngredient.Init(refPool);
            poolIngredient.transform.position = dicoposition[ingredientAdd];

            GameObject etiquetteOutOfStock = dicoOutOfStock[ingredientAdd.myEnumIngredientSO];
            etiquetteOutOfStock.SetActive(false);
        }

        ingredientAdd.stockSO++;
        UpdateStockIngredient(ingredientAdd);

        

    }


    public void UpdateStockIngredient(SCR_SO_Ingredient IngredientPris )
    {
        dicoTextStock[IngredientPris.myEnumIngredientSO].text = "x" + IngredientPris.stockSO; ;
        if (IngredientPris.stockSO == 0)
        {
            GameObject etiquetteOutOfStock =  dicoOutOfStock[IngredientPris.myEnumIngredientSO];
            etiquetteOutOfStock.SetActive(true);

            // animation 
            Vector3 ancienScale = etiquetteOutOfStock.transform.localScale;
            etiquetteOutOfStock.transform.localScale= ancienScale * 2f;
            etiquetteOutOfStock.transform.DOScale(ancienScale, 0.3f);
        }
    }

    public void OnBeforeSerialize()
    {
    }



    public void IngredientPick()
    {
        :
    }
}
