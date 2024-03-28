using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SCR_GainIngredient : MonoBehaviour, ISerializationCallbackReceiver
{
    #region dico
    [System.Serializable] public class dicoIngredientSpriteClass : TemplateDico<enumAllIgredient, Sprite> { };
    private Dictionary<enumAllIgredient, Sprite> dicoIngredientSprite; 
    [SerializeField] private List<dicoIngredientSpriteClass> listDicoIngredientSprite;

    [System.Serializable] public class dicoIngredientStockClass : TemplateDico<int, List<enumAllIgredient>> { };
    private Dictionary<int, List<enumAllIgredient>> dicoStock;
    [SerializeField] private List<dicoIngredientStockClass> dicoStockList;

    [System.Serializable] public class dicoEnumSoIngredientClass : TemplateDico<enumAllIgredient, SCR_SO_Ingredient> { };
    private Dictionary<enumAllIgredient, SCR_SO_Ingredient> dicoEnumSoIngredient;
    [SerializeField] private List<dicoEnumSoIngredientClass> dicoEnumIngredientList;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoIngredientSprite = new Dictionary<enumAllIgredient, Sprite>();

        foreach (dicoIngredientSpriteClass item in listDicoIngredientSprite)
        {
            if (!dicoIngredientSprite.ContainsKey(item.key))
            {
                dicoIngredientSprite.Add(item.key, item.value);
            }
        }

        dicoStock = new Dictionary<int, List<enumAllIgredient>>();

        foreach (dicoIngredientStockClass item in dicoStockList)
        {
            if (!dicoStock.ContainsKey(item.key))
            {
                dicoStock.Add(item.key, item.value);
            }
        }


        dicoEnumSoIngredient = new Dictionary<enumAllIgredient, SCR_SO_Ingredient>();

        foreach (dicoEnumSoIngredientClass item in dicoEnumIngredientList)
        {
            if (!dicoEnumSoIngredient.ContainsKey(item.key))
            {
                dicoEnumSoIngredient.Add(item.key, item.value);
            }
        }
    }

    
   
    #endregion

    [SerializeField] private List<enumAllIgredient> listDailyIngredient;

    [SerializeField] private List<SCR_SO_Ingredient> listDataIngredient;
    [SerializeField] Transform transforCadrillage;
    [SerializeField] float delaySpawnIngredient;
    [SerializeField] float spacingXIngredient;
    [SerializeField] float spacingYIngredient;
    [SerializeField] int colonne;
    [SerializeField] SpriteRenderer prefabIngredient;

    [SerializeField] private Transform cageotRecompense;
    [SerializeField] Transform transforCadrillageRecompense;
    [SerializeField] private Transform emplacementCageotRecompense;
    [SerializeField] private AnimationCurve curveCageot;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    [ContextMenu("Spawn")]
    public void SpawnIngredient()
    {
        StartCoroutine(SpawnDailyIngredient());
    }


    private IEnumerator SpawnDailyIngredient()
    {
        yield return new WaitForSeconds(1f);

        float spacingY = 0f;
        float offSetX = 0f;
        
       
        for (int i = 0; i < dicoStockList[SCR_DATA.instanceData.GetJour()].value.Count ; i++)
        {
            SpriteRenderer ingredientSpawn = Instantiate(prefabIngredient, transforCadrillage);

            enumAllIgredient ingredientDico = dicoStockList[SCR_DATA.instanceData.GetJour()].value[i];
            ingredientSpawn.sprite = dicoIngredientSprite[ingredientDico];
            

            ingredientSpawn.transform.localPosition = new Vector3((i - offSetX) * spacingXIngredient, -spacingY, 0);
            AudioManager.instanceAM.Play("SpawnIngrédients");
            if (i == colonne - 1)
            {
                spacingY += spacingYIngredient;
                offSetX = i + 1;
            }

            yield return new WaitForSeconds(delaySpawnIngredient);

        }
       
        /*  version avant qu'on rajoute le dico d'ingrédient
        for (int i = 0; i < listDailyIngredient.Count; i++)
        {
            SpriteRenderer ingredientSpawn = Instantiate(prefabIngredient, transforCadrillage);
            ingredientSpawn.sprite = dicoIngredientSprite[listDailyIngredient[i]];
            ingredientSpawn.transform.localPosition = new Vector3((i - offSetX) * spacingXIngredient, -spacingY, 0);
            AudioManager.instanceAM.Play("SpawnIngrédients");
            if (i == colonne - 1)
            {
                spacingY += spacingYIngredient;
                offSetX = i + 1;
            }

            yield return new WaitForSeconds(delaySpawnIngredient);
        }
        */
        SpawnIngredientRecompense();

        yield return null;
    }


    private IEnumerator SpawnDaTaIngredient()
    {

        yield return new WaitForSeconds(0.5f);

        float spacingY = 0f;
        float offSetX = 0f;


        for (int i = 0; i < listDataIngredient.Count; i++)
        {
            SpriteRenderer ingredientSpawn = Instantiate(prefabIngredient, transforCadrillageRecompense);
            ingredientSpawn.sortingOrder = 15;
            ingredientSpawn.sprite = dicoIngredientSprite[listDataIngredient[i].myEnumIngredientSO];
            ingredientSpawn.transform.localPosition = new Vector3((i - offSetX) * spacingXIngredient, -spacingY, 0);
            AudioManager.instanceAM.Play("SpawnIngrédients");
            if (i == colonne - 1)
            {
                spacingY += spacingYIngredient;
                offSetX = i + 1;
            }

            yield return new WaitForSeconds(delaySpawnIngredient);
        }


        yield return null;
    }


    public void SpawnIngredientRecompense()
    {

        if (SCR_DATA.instanceData.GetListIngredientGagne().Count == 0)  // si on a pas gagne de recompense on fait pas l'anim
            return;

        AudioManager.instanceAM.Play("CaisseFalling");
        cageotRecompense.DOMoveY(emplacementCageotRecompense.position.y, 1, true).SetEase(curveCageot).OnComplete(CallCoroutine);
        AudioManager.instanceAM.Play("ItemBonus");
        listDataIngredient = SCR_DATA.instanceData.GetListIngredientGagne();

    }


    public  List<SCR_SO_Ingredient> GetIngredientGain()
    {
        List<SCR_SO_Ingredient> listToSend = new List<SCR_SO_Ingredient>();

        for (int i = 0; i< dicoStock[SCR_DATA.instanceData.GetJour()+1].Count; i++)
        {
            enumAllIgredient ingredientAjoute = dicoStock[SCR_DATA.instanceData.GetJour()+1][i];
            listToSend.Add(dicoEnumSoIngredient[ingredientAjoute]);
        }

        return listToSend;
    } 

    public void CallCoroutine()
    {
        transforCadrillage.gameObject.SetActive(false);

        StartCoroutine(SpawnDaTaIngredient());
    }

}
