using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Contenant : MonoBehaviour
{
    [SerializeField] private int maxIngredientDrop = 1;
    private int actualIngredientIn;

    [SerializeField] private Transform emplacementIngredient;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void OnDrop(SCR_Ingredient ingredientDropParameter)
    {

        if(actualIngredientIn < maxIngredientDrop)
        {
            actualIngredientIn++;
            ingredientDropParameter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ingredientDropParameter.transform.DOMove(emplacementIngredient.position,1f);
            ingredientDropParameter.transform.DOScale(new Vector3(0.14f,0.14f,1f),1f);
        }
    }


}
