using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ingredient : SCR_PoolItem // script de l'ingr�dient et de l'ingr�dient transform�
{
    [SerializeField] private SCR_SO_Ingredient myIngredient; // besoin d'un SO pour savoir de quel ingr�dient il s'agit
    private SpriteRenderer mySpriteRenderer; // stock le sprite REnderer car on a besoin de changer le sprite lorsqu'on "instancie" ou quand on transforme
    private Camera mainCamera; // stock la camera pour pouvoir l'utiliser dans les RayCast

    #region Drag
    [Header("Drag Parametre")]
    private TargetJoint2D myTargetJoint; // component qui est rajout� lorsqu'on clique sur l'ingr�dient
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de r�etablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est r�duit a chaque fr�quence
    #endregion


    private bool inEtagere = true; // permet de savoir si l'ingr�dient est dans l'etagere ou non
    [SerializeField] private SCR_Etagere refEtagere; // reference de l'etagere, utile pour calculer la distance 


    private void Start()
    {
        Init(refPool); //initialise les ingr�dients qui passe pas par le pool
        refEtagere.UpdateStockIngredient(myIngredient); // update le texte de stock 


    }

    private void OnEnable() // dans le OnEnable car c'est avant le start
    {
        
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
       
    }

    public override void Init(SCR_Pool basePool) // fonction qui est dans pool item 
    {
        base.Init(basePool);
        UpdateSprite(); // voir ci-dessous
    }

    public void UpdateSprite() // update le sprite lorsqu'on l'"instancie"
    {
        gameObject.name = myIngredient.name; // change le nom de l'objet avec le nom du SO renseign�
        mySpriteRenderer.sprite = myIngredient.mySpriteSO; // change le sprite avec le sprite du SO ingr�dient renseign�
    }

    private void OnMouseDown() // fonction appel� lorsqu'on clique sur l'ingr�dient
    {
        if (inEtagere) // verifie si l'objet est dans l'etagere ou non 
        {
            inEtagere = false; // l'objet n'est plus dans l'etagere car on l'a pris
            myIngredient.stockSO--; // retire 1 au stock
            refEtagere.SpawnIngredient(myIngredient); // fait spawn un ingr�dient dans l'etagere
            //SpawnIngredient(); // fait spawn un ingr�dient pour remplacer celui qu'on a pris
            refEtagere.UpdateStockIngredient(myIngredient); // met a jour le texte de stock 


        }

        SetTargetJointOnAnotherObject(false); // ajoute le component TargetJoint, parametre false car on n'a pas besoin de reset le joint
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // on passe l'objet sur ce layer pour qu'il garde ces collisions mais pas les Cast
        mySpriteRenderer.sortingOrder = 10; // fait passer l'objet devant tout le reste 
    }

    private void OnMouseUp() // fonction appel� lorsqu'on relache le clique (et qu'on avait clique sur l'objet avant, pas lorsqu'on relache le clique n'importe ou)
    {


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // cr�er un Cast pour savoir si on a relach� l'ingr�dient sur quelque chose

        if (rayHit.transform.GetComponent<SCR_Ustensile>()) // si on a relach� l'objet sur un ustensile 
        {
            SCR_Ustensile ustensileDrop = rayHit.transform.GetComponent<SCR_Ustensile>(); // stock l'ustensile dans une var
            ustensileDrop.OnDrop(this); // appele la fonction OnDrop de l'ustensile
            //Transformation(ustensileDrop.GetEtat()); // transforme l'ingr�dient dans l'etat de l'ustensile
        }




        SetTargetJointOnAnotherObject(true); // retire le component TarGetJoint, parametre a vrai car cette on reset le joint
        gameObject.layer = LayerMask.NameToLayer("DragObject"); // repasse l'objet sur ce layer pour recevoir les Cast
        mySpriteRenderer.sortingOrder = 5; // repasse l'objet au meme niveau qu'il a de base
    }

    public void OnMouseDrag() // fonction appel� lorsuq'on maintiens le clique sur l'ingr�dient
    {
       


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // cast pour avoir la world position de la souris

        if (rayHit)// si le cast touche quelque chose
        {
            myTargetJoint.target = rayHit.point; // indique au TargetJoint que la target est la position de la souris
        }
    }

   

    public void Transformation(enumEtatIgredient newEtatParameter) // fonction appel� lorsqu'on drop un ingr�dient sur un ustensile
    {
        myIngredient =  myIngredient.dicoIngredientTransfo[newEtatParameter]; // remplace le SO avec celui associ� au nouvel etat
        UpdateSprite(); // change le sprite avec le nouveau sprite de l'ingr�dient transform�
    }


    public void SetTargetJointOnAnotherObject(bool needReset = false) // fonction qui ajoute / retire le component TargetJoint, �l�ment principal pour Drag un objet 
    {
        if(!needReset) // si on pas besoin de reset le component = quand on commence a drag l'ingr�dient
        {
            myTargetJoint = gameObject.AddComponent<TargetJoint2D>(); // ajoute le component a l'objet
            myTargetJoint.frequency = frequenceJoint; //met a jour les parametre du component avec les parametres qu'on a d�finis 
            myTargetJoint.dampingRatio = dampingJoint; // voir juste au dessus

        }
        else // si on veut reset le component = quand on a finis de drag l'ingr�dient
        {
            Destroy(myTargetJoint); // on retire le component 
            myTargetJoint = null; // passe � null pour eviter les erreurs de missingObject machin trucs
        }
    }

    


   
    

    public void SetSoIngredient(SCR_SO_Ingredient parameter_SOingredient, SCR_Etagere etagereParameter) { myIngredient = parameter_SOingredient; refEtagere = etagereParameter; } 
}
