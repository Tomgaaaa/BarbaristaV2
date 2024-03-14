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


    public override void Start()
    {
        base.Start();
        colliderPilon.enabled = false;
        colliderBordDMortier.SetActive(false);
        InitPilon(); // transmet les informations (velocite et temps necessaire) au pilon 



    }

    public override void OnMouseOver()
    {
    }
    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        devantMortier.SetActive(false);
        base.OnDrop(ingredientDropParameter);
        colliderPilon.enabled = true; // on active les collider de manipulation
        colliderBordDMortier.SetActive(true); // pareil

        refPilon.SetManipulation(true);
        ingredientCollider.isTrigger = true;
    }

  

    public override void FinishManipulation()
    {
        devantMortier.SetActive(true);
        base.FinishManipulation();
        colliderPilon.enabled = false; // désactive les colliders de manipulation
        AudioManager.instanceAM.Play("Completion Mortier");


    }

    public void InitPilon()
    {
        refPilon.SetTimer(tempsNecessaireBoyage,velocityNecessaireBroyage,this); // transmet les infos au pilon et le mortier comme reference
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

    }

    public void LockIngredient()
    {
         ingredientDrop.SetHasBeenTransformed(true);
        ingredientCollider.enabled = false;

    }

}

    
