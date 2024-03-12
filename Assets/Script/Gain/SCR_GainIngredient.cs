using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SCR_QueteManager;

public class SCR_GainIngredient : MonoBehaviour, ISerializationCallbackReceiver
{
    #region dico
    [System.Serializable] public class dicoIngredientSpriteClass : TemplateDico<enumAllIgredient, Sprite> { };
    private Dictionary<enumAllIgredient, Sprite> dicoIngredientSprite; 
    [SerializeField] private List<dicoIngredientSpriteClass> listDicoIngredientSprite;
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
      

        for (int i = 0 ; i< listDailyIngredient.Count;i++)
        {
            SpriteRenderer ingredientSpawn = Instantiate(prefabIngredient,transforCadrillage);
            ingredientSpawn.sprite = dicoIngredientSprite[listDailyIngredient[i]];
            ingredientSpawn.transform.localPosition = new Vector3 ((i- offSetX) * spacingXIngredient ,-spacingY,0);

            if(i == colonne -1 )
            {
                spacingY += spacingYIngredient;
                offSetX = i +1;
            }

            yield return new WaitForSeconds(delaySpawnIngredient);
        }
        SpawnIngredientRecompense();

        yield return null;
    }


    private IEnumerator SpawnDaTaIngredient()
    {

        yield return new WaitForSeconds(1f);

        float spacingY = 0f;
        float offSetX = 0f;


        for (int i = 0; i < listDataIngredient.Count; i++)
        {
            SpriteRenderer ingredientSpawn = Instantiate(prefabIngredient, transforCadrillageRecompense);
            ingredientSpawn.sortingOrder = 15;
            ingredientSpawn.sprite = dicoIngredientSprite[listDataIngredient[i].myEnumIngredientSO];
            ingredientSpawn.transform.localPosition = new Vector3((i - offSetX) * spacingXIngredient, -spacingY, 0);

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
        cageotRecompense.DOMoveY(emplacementCageotRecompense.position.y, 1, true).SetEase(curveCageot).OnComplete(CallCoroutine);
        listDataIngredient = SCR_DATA.instanceData.GetListIngredientGagne();

    }

    public void CallCoroutine()
    {
        transforCadrillage.gameObject.SetActive(false);

        StartCoroutine(SpawnDaTaIngredient());
    }

}
