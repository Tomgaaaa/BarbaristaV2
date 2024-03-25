using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Mortier : SCR_Ustensile
{
    [Header("Specialement Mortier")]

    [SerializeField] private SCR_Pilon refPilon; // reference au pilon qu'on manipule
    [SerializeField] private float tempsNecessaireBoyage; // en seconde, temps qu'il faut où le pilon et le mortier restent en contact
    [SerializeField] private float velocityNecessaireBroyage; // velocité necessaire poour considerer que le pilon broie l'ingrédient
    [SerializeField] private Collider2D colliderPilon; // collider du pilon a activer au moment du drop
    [SerializeField] private GameObject colliderBordDMortier; // collider des bords du mortier 
    [SerializeField] private GameObject devantMortier;
    SCR_Ingredient ingredientActuel;

    public override void Start()
    {
        base.Start();
        colliderPilon.enabled = false;
        colliderBordDMortier.SetActive(false);
        //InitPilon(); // transmet les informations (velocite et temps necessaire) au pilon 



    }

    public override void OnMouseOver()
    {
    }
    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {

        ingredientActuel = ingredientDropParameter;
        devantMortier.SetActive(false);
        base.OnDrop(ingredientDropParameter);
        colliderPilon.enabled = true; // on active les collider de manipulation
        colliderBordDMortier.SetActive(true); // pareil

        refPilon.SetManipulation(true);
        ingredientCollider.isTrigger = true;
        InitPilon();
        ingredientDropParameter.transform.rotation = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
    }

  

    public override void FinishManipulation()
    {
        
        base.FinishManipulation();
        colliderPilon.enabled = false; // désactive les colliders de manipulation
        AudioManager.instanceAM.Play("Completion Mortier");
        sparkleVFX.Play();
        ingredientActuel.transform.rotation = new Quaternion(0, 0, 0, 1);
    }

    public void InitPilon()
    {
        refPilon.SetTimer(tempsNecessaireBoyage,velocityNecessaireBroyage,this,myVFX,ingredientDrop.GetCR_SO_Ingredient().TrancheTasse); // transmet les infos au pilon et le mortier comme reference

    }


    public override void OnMouseEnter()
    {
        
        base.OnMouseEnter();

  
    }

    public override void PickUpFromContenant()
    {
        colliderPilon.enabled = false; // désactive les colliders de manipulation
        colliderBordDMortier.SetActive(false); // pareil

        ingredientCollider.isTrigger = false;
        base.PickUpFromContenant();
        devantMortier.SetActive(true);
    }

    public void LockIngredient()
    {
         ingredientDrop.SetHasBeenTransformed(true);
        ingredientCollider.enabled = false;

    }

}

    
