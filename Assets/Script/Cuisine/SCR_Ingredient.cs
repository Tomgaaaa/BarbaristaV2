using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ingredient : SCR_PoolItem
{
    [SerializeField] private SCR_SO_Ingredient myIngredient;
    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Init(SCR_Pool basePool)
    {
        base.Init(basePool);
        gameObject.name = myIngredient.name;
        mySpriteRenderer.sprite = myIngredient.mySpriteSO;
        Debug.Log("Fait");
    }

    public void SetSoIngredient(SCR_SO_Ingredient parameter_SOingredient) { myIngredient = parameter_SOingredient; }
}
