using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Mortier : SCR_Ustensile
{
    [Header("Specialement Mortier")]

    [SerializeField] private SCR_Pilon refPilon;
    [SerializeField] private float tempsNecessaireBoyage;
    [SerializeField] private float velocityNecessaireBroyage;
    [SerializeField] private Collider2D colliderPilon;
    [SerializeField] private GameObject colliderBordDMortier;

    public override void Start()
    {
        base.Start();
        colliderPilon.enabled = false;
        colliderBordDMortier.SetActive(false);
        InitPilon();


    }


    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);
        colliderPilon.enabled = true;
        colliderBordDMortier.SetActive(true);
    }

    public override void PickUpFromContenant()
    {
        base.PickUpFromContenant();

        colliderPilon.enabled = false;
        colliderBordDMortier.SetActive(false);

    }

    public void InitPilon()
    {
        refPilon.SetTimer(tempsNecessaireBoyage,velocityNecessaireBroyage,this);
    }

}

    
