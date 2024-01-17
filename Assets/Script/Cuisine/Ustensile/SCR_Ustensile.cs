using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ustensile : SCR_Contenant // script parent de tout les ustensiles (ceux qui permettent de modifier un etat)
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation; // �tat de l'ingr�dient apres la transformation 

    private protected Camera mainCam; // reference a la cam pour la transition camera 
    private Vector3 startPositionCam; // stock la position initial de la cam
    [SerializeField] private Vector3 emplacementCam; // position que devra prendre la camera apres transition

    [SerializeField] private Collider2D colliderManipulation; // reference au collider utile � la manipulation
    [SerializeField] private Collider2D colliderDrop; // reference au collider qui permet le OnDrop
    private protected bool inManipulation = false; // empeche de manipuler l'ingr�dient si il n'y pas d'ingr�dient

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



    public override void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appell� lorsuq'un ingr�dient est drop sur l'ustensile
    {
        if(nmbIngredientIn < maxIngredientDrop) // v�rifie si il n'y a pas d�ja un ingr�dient dans l'ustensile
        {
            base.OnDrop(ingredientDropParameter); // fait le OnDrop du script contenant


            colliderManipulation.enabled = true; // active le collider de manipulation 
            colliderDrop.enabled = false; // d�sactive le collider qui permet de detecter le OnDrop
            inManipulation = true; // on passe en mode manipulation

            ingredientCollider.enabled = false; // d�sactive le collider de l'ingr�dient car on ne peut pas toucher l'ingr�dient lorsqu'on le manipule

            mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, -2), 1f); // d�place la camera centr� sur l'ustensile
            mainCam.DOOrthoSize(emplacementCam.z, 1f); // change le zoom de la camera, emplacement.z car on est en 2D donc inutile le Z, �a �vite de recreer une variable
        }

        


    }


    public virtual void OnMouseDrag()
    {
    }


    public virtual void FinishManipulation() // fonction appell� lorsqu'on a finis la manipulation
    {
        colliderManipulation.enabled = false; // d�sactive le collider de manipulation vus qu'on a finis
        inManipulation = false; // on ne manipule plus l'ingr�dient 


        ingredientDrop.Transformation(etatApresTransformation); // indique � l'ingr�dient qu'on le transforme en l'etat que transforme l'ustensile
        ingredientCollider.enabled = true; // re active le collider de l'ingr�dient pour pouvoir le reprendre

        mainCam.transform.DOMove(new Vector3(startPositionCam.x, startPositionCam.y, -2), 1f); // reposition la camera sa position intial
        mainCam.DOOrthoSize(5.7f, 1f); // remet le zoom de la camera a sa valeur intial





    }

    public override void PickUpFromContenant()
    {
        base.PickUpFromContenant();
        colliderDrop.enabled = true; // r�active le collider pour permettre de re drop des ingr�dients sur l'ustensile, on le reactive que lorsque l'ingr�dient est repris


    }

    public enumEtatIgredient GetEtat() { return etatApresTransformation; } // renvoie l'etat de transformation
}
