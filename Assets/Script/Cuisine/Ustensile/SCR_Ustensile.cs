using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ustensile : SCR_Contenant // script parent de tout les ustensiles (ceux qui permettent de modifier un etat)
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation; // état de l'ingrédient apres la transformation 
    private protected Camera mainCam;
    private Vector3 startPositionCam;
    [SerializeField] private Vector3 emplacementCam;

    [SerializeField] private Collider2D colliderManipulation;
    [SerializeField] private Collider2D colliderDrop;
    private protected bool inManipulation = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCam = Camera.main;
        startPositionCam = mainCam.transform.position;
        colliderManipulation.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        if(nmbIngredientIn < maxIngredientDrop)
        {
            base.OnDrop(ingredientDropParameter);

            ingredientDrop = ingredientDropParameter;
            ingredientDrop.GetComponent<Collider2D>().enabled = false;

            colliderManipulation.enabled = true;
            colliderDrop.enabled = false;
            inManipulation = true;


            mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, -2), 1f);
            mainCam.DOOrthoSize(emplacementCam.z, 1f);
        }

        


    }


    public virtual void OnMouseDrag()
    {
    }


    public virtual void FinishManipulation()
    {
        colliderManipulation.enabled = false;
        colliderDrop.enabled = true;
        inManipulation = false;


        ingredientDrop.Transformation(etatApresTransformation);
        ingredientDrop.SetInUstensileAndUstensile(true, this);
        ingredientDrop.GetComponent<Collider2D>().enabled = true;

        mainCam.transform.DOMove(new Vector3(startPositionCam.x, startPositionCam.y, -2), 1f);
        mainCam.DOOrthoSize(5.7f, 1f);





    }

    public enumEtatIgredient GetEtat() { return etatApresTransformation; } // renvoie l'etat de transformation
}
