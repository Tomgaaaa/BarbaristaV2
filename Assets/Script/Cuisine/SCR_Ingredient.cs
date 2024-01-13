using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Ingredient : SCR_PoolItem
{
    [SerializeField] private SCR_SO_Ingredient myIngredient;
    private SpriteRenderer mySpriteRenderer;
    private Camera mainCamera;

    #region Drag
    [Header("Drag Parametre")]
    private TargetJoint2D myTargetJoint;
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f;
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f;
    #endregion


    private bool inEtagere = true;
    [SerializeField] private SCR_Etagere refEtagere;
    public bool estMaintenue;


    private void Start()
    {
        Init(refPool);
        refEtagere.UpdateStockIngredient(myIngredient);

    }

    private void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
       
    }

    public override void Init(SCR_Pool basePool)
    {
        base.Init(basePool);
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        gameObject.name = myIngredient.name;
        mySpriteRenderer.sprite = myIngredient.mySpriteSO;
    }

    private void OnMouseDown()
    {
        if (inEtagere)
        {
            inEtagere = false;
            SpawnIngredient();
            myIngredient.stockSO--;
            refEtagere.UpdateStockIngredient(myIngredient);


        }

        estMaintenue = true;
        SetTargetJointOnAnotherObject(false);
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        mySpriteRenderer.sortingOrder = 10;
    }

    private void OnMouseUp()
    {
        estMaintenue = false;


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

        if (rayHit.transform.GetComponent<SCR_Ustensile>())
        {
            SCR_Ustensile ustensileDrop = rayHit.transform.GetComponent<SCR_Ustensile>();
            ustensileDrop.OnDrop(this);
            Transformation(ustensileDrop.GetEtat());
        }




        SetTargetJointOnAnotherObject(true);
        gameObject.layer = LayerMask.NameToLayer("DragObject");
        mySpriteRenderer.sortingOrder = 5;
    }

    public void OnMouseDrag()
    {
       


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

        if (rayHit)
        {
            myTargetJoint.target = rayHit.point;
        }
    }

   

    public void Transformation(enumEtatIgredient newEtatParameter)
    {
        myIngredient =  myIngredient.dicoIngredientTransfo[newEtatParameter];
        UpdateSprite();
    }


    public void SetTargetJointOnAnotherObject(bool needReset = false)
    {
        if(!needReset)
        {
            myTargetJoint = gameObject.AddComponent<TargetJoint2D>();
            myTargetJoint.frequency = frequenceJoint;
            myTargetJoint.dampingRatio = dampingJoint;

        }
        else
        {
            Destroy(myTargetJoint); 
            myTargetJoint = null;
        }
    }

    public void SpawnIngredient()
    {
        if (myIngredient.stockSO > 1)
        {
            SCR_PoolItem poolItem = refPool.Instantiate();
            SCR_Ingredient poolIngredient = poolItem.GetComponent<SCR_Ingredient>();
            poolIngredient.SetSoIngredient(myIngredient,refEtagere);
            poolIngredient.Init(refPool);
            poolIngredient.transform.position = transform.position;
        }
        else
        {
            Debug.Log("out of stock");
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<SCR_Etagere>() && !estMaintenue)
        {
            Back(); 

        }
    }
    

    public void SetSoIngredient(SCR_SO_Ingredient parameter_SOingredient, SCR_Etagere etagereParameter) { myIngredient = parameter_SOingredient; refEtagere = etagereParameter; }
}
