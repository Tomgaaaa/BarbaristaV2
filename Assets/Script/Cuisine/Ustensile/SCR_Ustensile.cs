using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ustensile : SCR_Contenant // script parent de tout les ustensiles (ceux qui permettent de modifier un etat)
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation; // �tat de l'ingr�dient apres la transformation 

    private protected Camera mainCam; // reference a la cam pour la transition camera 
    [SerializeField] private protected Vector3 emplacementCam; // position que devra prendre la camera apres transition

    [SerializeField] private protected Collider2D colliderManipulation; // reference au collider utile � la manipulation
    [SerializeField] private Collider2D colliderDrop; // reference au collider qui permet le OnDrop
    private protected bool inManipulation = false; // empeche de manipuler l'ingr�dient si il n'y pas d'ingr�dient
    private protected bool isMaintenu;

    [SerializeField] private SO_Tuto myTutoOnDrop;

    [SerializeField] private Transform newPosPostTransfo;

    [SerializeField] private protected ParticleSystem myVFX;
    [SerializeField] private protected ParticleSystem sparkleVFX;

    [SerializeField] private protected List<SCR_Ustensile> listUstensil;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        mainCam = Camera.main;
        colliderManipulation.enabled = false;

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public virtual void OnMouseDown()
    {
        isMaintenu = true;
        if (inManipulation)
        {

            ingredientDrop.SetHasBeenTransformed(true);
            ingredientCollider.enabled = false;

            /*Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOn");

            Cursor.SetCursor(cursorHover, new Vector2(80f, 50f), CursorMode.Auto);*/


            SCR_Cursor.instanceCursor.ChangeHoverOn(true);

        }
    }

    public virtual void OnMouseUp()
    {
        isMaintenu = false;

        if (inManipulation)
        {
            SCR_Cursor.instanceCursor.ChangeClickOff(false);

           // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

    }
    public virtual void OnMouseOver()
    {
        if (inManipulation && !isMaintenu)
        {
            /*Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOff");

            Cursor.SetCursor(cursorHover, new Vector2(80f, 50f), CursorMode.Auto);*/


            SCR_Cursor.instanceCursor.ChangeHoverOff(true);

        }
    }

    public virtual void OnMouseEnter()
    {
        
    }

    public virtual void OnMouseExit()
    {
        if (inManipulation && !isMaintenu)
        {
            SCR_Cursor.instanceCursor.ChangeClickOff(false);

            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    public virtual void OnMouseDrag()
    {
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appell� lorsuq'un ingr�dient est drop sur l'ustensile
    {
        if(nmbIngredientIn < maxIngredientDrop) // v�rifie si il n'y a pas d�ja un ingr�dient dans l'ustensile
        {

            base.OnDrop(ingredientDropParameter); // fait le OnDrop du script contenant

            ingredientDropParameter.GetComponent<SpriteRenderer>().sortingOrder = 2;

            colliderManipulation.enabled = true; // active le collider de manipulation 
            colliderDrop.enabled = false; // d�sactive le collider qui permet de detecter le OnDrop
            inManipulation = true; // on passe en mode manipulation

           // ingredientCollider.enabled = false; // d�sactive le collider de l'ingr�dient car on ne peut pas toucher l'ingr�dient lorsqu'on le manipule

            mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, -2), 1f); // d�place la camera centr� sur l'ustensile
            AudioManager.instanceAM.Play("Transition");
            mainCam.DOOrthoSize(emplacementCam.z, 1f); // change le zoom de la camera, emplacement.z car on est en 2D donc inutile le Z, �a �vite de recreer une variable
            SCR_Cursor.instanceCursor.ZoomCamera();

            SCR_TutoManager.instanceTuto.Calltuto(myTutoOnDrop, SCR_TutoManager.enumEmplacement.gauche,true); // appelle le tuto manager pour afficher le tuto associ� � l'ustensile


            SCR_CuisineManager.instanceCM.ZoomUstensile(true, this);



        }

        foreach(var item in listUstensil)
        {
            if(this != item)
            {
                item.gameObject.SetActive(false);
            }
        }


    }


    

    public virtual void FinishManipulation() // fonction appell� lorsqu'on a finis la manipulation
    {
        OnMouseUp();

        colliderManipulation.enabled = false; // d�sactive le collider de manipulation vus qu'on a finis
        inManipulation = false; // on ne manipule plus l'ingr�dient 


        ingredientDrop.Transformation(etatApresTransformation); // indique � l'ingr�dient qu'on le transforme en l'etat que transforme l'ustensile
        ingredientCollider.enabled = true; // re active le collider de l'ingr�dient pour pouvoir le reprendre
        ingredientDrop.transform.position = newPosPostTransfo.position;
        

    }

    public override void PickUpFromContenant()
    {
        base.PickUpFromContenant();
        colliderDrop.enabled = true; // r�active le collider pour permettre de re drop des ingr�dients sur l'ustensile, on le reactive que lorsque l'ingr�dient est repris

        SCR_CuisineManager.instanceCM.ZoomUstensile(false, this);

        foreach (var item in listUstensil)
        {
            if (this != item)
            {
                item.gameObject.SetActive(true);
            }
        }

    }

    public virtual float Remap(float value, float from1, float to1, float from2, float to2) // je le garde psk j'en ai eu besoin pendant un test et que je galere a retrouver le nom remap a chaque fois
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    public enumEtatIgredient GetEtat() { return etatApresTransformation; } // renvoie l'etat de transformation
    public Collider2D GetDropCollider() { return colliderDrop; } // renvoie l'etat de transformation
}
