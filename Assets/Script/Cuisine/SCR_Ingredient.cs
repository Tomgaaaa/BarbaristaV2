using DG.Tweening;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;


public class SCR_Ingredient : SCR_PoolItem // script de l'ingrédient et de l'ingrédient transformé
{
    [SerializeField] private SCR_SO_Ingredient myIngredient; // besoin d'un SO pour savoir de quel ingrédient il s'agit
    private SpriteRenderer mySpriteRenderer; // stock le sprite REnderer car on a besoin de changer le sprite lorsqu'on "instancie" ou quand on transforme
    private Rigidbody2D myRB;
    private Camera mainCamera; // stock la camera pour pouvoir l'utiliser dans les RayCast

    #region Drag
    [Header("Drag Parametre")]
    private TargetJoint2D myTargetJoint; // component qui est rajouté lorsqu'on clique sur l'ingrédient
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de réetablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est réduit a chaque fréquence
    #endregion


    private bool inEtagere = true; // permet de savoir si l'ingrédient est dans l'etagere ou non
    [SerializeField] private SCR_Etagere refEtagere; // reference de l'etagere, utile pour calculer la distance 

    private bool inContenant; // permet de savoir si l'ingrédient est dans un contenant ou pas
    private SCR_Contenant refLastContenant; // ref du contenant pour appeler des fonctions lorsqu'on prend l'ingrédient depuis un contenant

    private bool leaveEtagere = false; // bool qui permet de savoir si l'ingrédient a deja quitté la zone de l'etagere au moins une fois 

    private bool isMaintenu = false;

    private Camera mainCam;
    private Vector2 startPosCam;
    private bool hasBeenTransformed = false;

    private Material outlineMaterial;
    private SCR_Contenant lastRefContenantOutline;

    private void Start()
    {
        outlineMaterial = GetComponent<Renderer>().material;
        refEtagere.UpdateStockIngredient(myIngredient); // update le texte de stock 

        Init(refPool); //initialise les ingrédients qui passe pas par le pool
        mainCam = Camera.main;
        startPosCam = mainCam.transform.position;
    }

    private void OnEnable() // dans le OnEnable car c'est avant le start
    {

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        leaveEtagere = false;

        
        
    }

    public override void Init(SCR_Pool basePool) // fonction qui est dans pool item 
    {
        base.Init(basePool);
        UpdateSprite(); // voir ci-dessous

      
    }

    public void UpdateSprite() // update le sprite lorsqu'on l'"instancie"
    {
        gameObject.name = myIngredient.name; // change le nom de l'objet avec le nom du SO renseigné
        mySpriteRenderer.sprite = myIngredient.mySpriteSO; // change le sprite avec le sprite du SO ingrédient renseigné
       // GetComponent<BoxCollider2D>().size = mySpriteRenderer.size;

       
    }


    public void OnMouseEnter()
    {

        if (SCR_PauseMenu.instancePauseMenu.GetIsPause() || SCR_MasterCompendium.instanceMComp.GetIsOpen())
            return;

        if (!inContenant || !hasBeenTransformed || hasBeenTransformed && myIngredient.actualStateSO!=enumEtatIgredient.Nature)
        {

            SCR_Cursor.instanceCursor.ChangeHoverOff(true);

            /*Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOff");
            Cursor.SetCursor(cursorHover, new Vector2(80f, 50f), CursorMode.Auto);*/

            outlineMaterial.SetFloat("_Thickness", 0.04f);

        }
        
    }


    private void OnMouseExit()
    {

        if(!isMaintenu)
        {

            outlineMaterial.SetFloat("_Thickness", 0f);
           // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto );

            SCR_Cursor.instanceCursor.ChangeClickOff(false);


        }

    }

    private void OnMouseDown() // fonction appelé lorsqu'on clique sur l'ingrédient
    {
        if (SCR_PauseMenu.instancePauseMenu.GetIsPause() || SCR_MasterCompendium.instanceMComp.GetIsOpen())
            return;


        isMaintenu = true;

        SCR_Cursor.instanceCursor.ChangeHoverOn(true);


        /* Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOn");
         Cursor.SetCursor(cursorHover, new Vector2(80f, 50f), CursorMode.Auto);*/


        outlineMaterial.SetFloat("_Thickness", 0f);


        if (inEtagere) // verifie si l'objet est dans l'etagere ou non 
        {
            inEtagere = false; // l'objet n'est plus dans l'etagere car on l'a pris
            myIngredient.stockSO--; // retire 1 au stock
            AudioManager.instanceAM.Play("PickItem");
            refEtagere.SpawnIngredient(myIngredient); // fait spawn un ingrédient dans l'etagere
            //SpawnIngredient(); // fait spawn un ingrédient pour remplacer celui qu'on a pris
            refEtagere.UpdateStockIngredient(myIngredient); // met a jour le texte de stock 


        }


        if(inContenant) // si on clique sur l'ingrédient et qu'il est dans un contenant
        {
            inContenant = false; // on le sort du contenant
            refLastContenant.PickUpFromContenant(); // informe le contenant que l'on a pris l'ingrédient

            mainCam.transform.DOMove(new Vector3(startPosCam.x, startPosCam.y, -2), 1f); // reposition la camera sa position intial
            AudioManager.instanceAM.Play("Transitionback");
            mainCam.DOOrthoSize(5.7f, 1f); // remet le zoom de la camera a sa valeur intial

            if (refLastContenant.GetComponent<SCR_Ustensile>())
            {
                SCR_Cursor.instanceCursor.DeZoomCamera();

            }




        }

        SetTargetJointOnAnotherObject(false); // ajoute le component TargetJoint, parametre false car on n'a pas besoin de reset le joint
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // on passe l'objet sur ce layer pour qu'il garde ces collisions mais pas les Cast
        refEtagere.gameObject.layer = LayerMask.NameToLayer("Default"); // change le layere de l'etagere afin qu'il puisse recevoir les raycast afin de ranger les ingrédients
        mySpriteRenderer.sortingOrder = 10; // fait passer l'objet devant tout le reste 
    }

    private void OnMouseUp() // fonction appelé lorsqu'on relache le clique (et qu'on avait clique sur l'objet avant, pas lorsqu'on relache le clique n'importe ou)
    {

        if (SCR_PauseMenu.instancePauseMenu.GetIsPause() || SCR_MasterCompendium.instanceMComp.GetIsOpen())
            return;
        if(lastRefContenantOutline!=null)
            lastRefContenantOutline.ShowGrey(false, this);



        mySpriteRenderer.sortingOrder = 5; // repasse l'objet au meme niveau qu'il a de base
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        isMaintenu = false;

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose

        if (rayHit.transform.GetComponent<SCR_Ustensile>()) // si on a relaché l'objet sur un ustensile 
        {
            SCR_Ustensile ustensileDrop = rayHit.transform.GetComponent<SCR_Ustensile>(); // stock l'ustensile dans une var

            if (myIngredient.actualStateSO == enumEtatIgredient.Nature) // verifie que notre ingrédient n'est pas transformé
            {
                ustensileDrop.OnDrop(this); // appele la fonction OnDrop de l'ustensile

            }


        }


        if (rayHit.transform.GetComponent<SCR_Tasse>() && myIngredient.actualStateSO!=enumEtatIgredient.Nature) // si on a relaché l'objet sur un ustensile 
        {
            SCR_Contenant contenantDrop = rayHit.transform.GetComponent<SCR_Contenant>(); // stock le contenant dans une var
            contenantDrop.OnDrop(this); // appelle la fonction onDrop du contenant

        }



        /*if (rayHit.transform.GetComponent<SCR_Contenant>() && rayHit.transform.GetComponent<SCR_Ustensile>() == null) // si on a relaché l'objet sur un contenant et qu'il ne s'agit pas d'un ustensile
        {
            SCR_Contenant contenantDrop = rayHit.transform.GetComponent<SCR_Contenant>(); // stock le contenant dans une var
            contenantDrop.OnDrop(this); // appelle la fonction onDrop du contenant

        }*/



        if (rayHit.transform.GetComponent<SCR_Etagere>() && leaveEtagere) // fonction qui permet de ranger un ingrédient dans l'etagere, si on relache au niveau de l'etagere et que l'ingrédedient a deja quitté la zone d'etagere au moins une fois
        {
            if(myIngredient.actualStateSO != enumEtatIgredient.Nature) // si l'ingrédient est nature, il faut le transformer en nature, sinon pas besoin
            {
                Transformation(enumEtatIgredient.Nature); // transforme l'ingrédient dans sa version nature, c'est lui qui a les stats de stock
            }

            Back(); // renvoie l'ingrédient dans le pool
            refEtagere.AddIngredient(myIngredient); // ajoute un ingrédient au stock et met a jour le texte de stock
        }




        SetTargetJointOnAnotherObject(true); // retire le component TarGetJoint, parametre a vrai car cette fois on reset le joint
        gameObject.layer = LayerMask.NameToLayer("DragObject"); // repasse l'objet sur ce layer pour recevoir les Cast
        refEtagere.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // change le layere de l'etagere pour pas qu'il interfere avec les ray cast de l'ingrédients
      
    }

    public void OnMouseDrag() // fonction appelé lorsuq'on maintiens le clique sur l'ingrédient
    {
        if (SCR_PauseMenu.instancePauseMenu.GetIsPause() || SCR_MasterCompendium.instanceMComp.GetIsOpen())
            return;


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // cast pour avoir la world position de la souris

        if (rayHit)// si le cast touche quelque chose
        {
            myTargetJoint.target = rayHit.point; // indique au TargetJoint que la target est la position de la souris

            if(rayHit.transform.GetComponent<SCR_Ustensile>() && !hasBeenTransformed || rayHit.transform.GetComponent<SCR_Tasse>() && hasBeenTransformed)
            {
                lastRefContenantOutline = rayHit.transform.GetComponent<SCR_Contenant>();
                lastRefContenantOutline.ShowOutline(true,this);
            }
            else if(rayHit.transform.GetComponent<SCR_Contenant>())
            {
                lastRefContenantOutline = rayHit.transform.GetComponent<SCR_Contenant>();
                lastRefContenantOutline.ShowGrey(true, this);
            }
            else
            {
                if(lastRefContenantOutline != null)
                {
                    lastRefContenantOutline.ShowOutline(false,this);
                    lastRefContenantOutline.ShowGrey(false,this);
                    lastRefContenantOutline = null;
                }
            }
        }
    }

   

    public void Transformation(enumEtatIgredient newEtatParameter) // fonction appelé lorsqu'on drop un ingrédient sur un ustensile
    {
        myIngredient =  myIngredient.dicoIngredientTransfo[newEtatParameter]; // remplace le SO avec celui associé au nouvel etat
        UpdateSprite(); // change le sprite avec le nouveau sprite de l'ingrédient transformé
    }


    public void SetTargetJointOnAnotherObject(bool needReset = false) // fonction qui ajoute / retire le component TargetJoint, élément principal pour Drag un objet 
    {
        if(!needReset) // si on pas besoin de reset le component = quand on commence a drag l'ingrédient
        {
            myTargetJoint = gameObject.AddComponent<TargetJoint2D>(); // ajoute le component a l'objet
            myTargetJoint.frequency = frequenceJoint; //met a jour les parametre du component avec les parametres qu'on a définis 
            myTargetJoint.dampingRatio = dampingJoint; // voir juste au dessus



            myRB = GetComponent<Rigidbody2D>();
            myRB.interpolation = RigidbodyInterpolation2D.Interpolate;
            myRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else // si on veut reset le component = quand on a finis de drag l'ingrédient
        {
            Destroy(myTargetJoint); // on retire le component 
            myTargetJoint = null; // passe à null pour eviter les erreurs de missingObject machin trucs
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // fontion appellé quand on quitté la collision avec un objet en trigger
    {

        if (collision.GetComponent<SCR_Etagere>()) // verifie que c'est le trigger de l'etagere qu'on quitte
        {
            leaveEtagere = true; // passe le bool a vrai pour permettre de ranger l'ingrédient dans l'etagere
        }

    }


    public SCR_SO_Ingredient GetCR_SO_Ingredient() { return myIngredient; }


    public void SetHasBeenTransformed(bool newTransfoParameter) { hasBeenTransformed = newTransfoParameter; }
    public void SetInUstensileAndUstensile(bool inUstensileParameter, SCR_Contenant ustensileUseParameter) { inContenant = inUstensileParameter; refLastContenant = ustensileUseParameter; }
    public void SetSoIngredient(SCR_SO_Ingredient parameter_SOingredient, SCR_Etagere etagereParameter) { myIngredient = parameter_SOingredient; refEtagere = etagereParameter; } 
}
