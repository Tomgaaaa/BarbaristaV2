using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Contenant : MonoBehaviour
{
    [SerializeField] private protected int maxIngredientDrop = 1;
    private protected int nmbIngredientIn;

    public SCR_Ingredient ingredientDrop;
    private Collider2D ingredientCollider;
    private Rigidbody2D ingredientRB;

    [SerializeField] private Transform emplacementIngredient;
    [SerializeField] private Vector3 multiplicateurScaleIngredient;
    private protected Vector3 startScaleIngredient;

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

        if(nmbIngredientIn < maxIngredientDrop)
        {
            nmbIngredientIn++;
            ingredientDrop = ingredientDropParameter;
            ingredientDrop.SetInUstensileAndUstensile(true, this);

            ingredientCollider = ingredientDrop.GetComponent<Collider2D>();
            ingredientCollider.isTrigger = true;

            ingredientRB = ingredientDrop.GetComponent<Rigidbody2D>();
            ingredientRB.gravityScale = 0;
            ingredientRB.velocity = Vector3.zero;
            ingredientRB.angularVelocity = 0;

            startScaleIngredient = ingredientDrop.transform.localScale;

            if(emplacementIngredient.transform.position.x == 0 || emplacementIngredient.transform.position.y == 0)
            {
                emplacementIngredient.position = new Vector3(ingredientDrop.transform.position.x, ingredientDrop.transform.position.y, ingredientDrop.transform.position.z);
            }

            if (multiplicateurScaleIngredient.x == 0 || multiplicateurScaleIngredient.y == 0)
            {
                multiplicateurScaleIngredient = Vector3.one;
            }

            ingredientDrop.transform.DOMove(emplacementIngredient.position,1f);

            Vector3 newScale = new Vector3(multiplicateurScaleIngredient.x * ingredientDrop.transform.localScale.x, multiplicateurScaleIngredient.y * ingredientDrop.transform.localScale.y, multiplicateurScaleIngredient.z * ingredientDrop.transform.localScale.z);
            ingredientDrop.transform.DOScale(newScale, 0.5f);
        }
    }

    public virtual void PickUpFromContenant( )
    {

        ingredientRB.gravityScale = 1;
        ingredientCollider.isTrigger = false;
        ingredientDrop.transform.DOScale(startScaleIngredient, 0.5f);


        nmbIngredientIn--;
        ingredientDrop = null; 
        ingredientCollider = null;
        ingredientRB = null;
    }
}
