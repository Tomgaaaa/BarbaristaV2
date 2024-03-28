using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Pilon : MonoBehaviour
{
    public ParticleSystem pilonVfx;
    public Sprite ingredientVfx;

    private Camera mainCam;
    private Rigidbody2D rb;

    private float velocityNecessairePilon;


    private float tempsNecessaireBoyagePilon;
    public float currentTempsBroyage;
    private bool inManipulation;

    [SerializeField] private Transform aimPositionMortier; // pour la partie ou le pilon vise la base du mortier

    private bool inMortier;
    private bool contactMortier;
    private SCR_Mortier refMortier;

    private Vector3 initialPosition;
    private Vector3 ddd;

    private Vector3 lastMousePos; // derniere position de la souris

    private bool isMaintenu;

    [SerializeField] private SpriteRenderer outlinePilonRenderer;
    private Material outlinePilonMaterial;

    #region Drag
    private TargetJoint2D myTargetJoint;
    [SerializeField, Range(0f, 1000f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de réetablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est réduit a chaque fréquence
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        rb.centerOfMass = new Vector3 (0,-0.8f,0); // change le centre de masse du pilon pour le mettre en bas du pilon

        outlinePilonMaterial = outlinePilonRenderer.material;
    }

    private void OnMouseDown()
    {

        isMaintenu = true;

        SetTargetJointOnAnotherObject(false); // ajoute le component TargetJoint, parametre false car on n'a pas besoin de reset le joint
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // on passe l'objet sur ce layer pour qu'il garde ces collisions mais pas les Cast
        AudioManager.instanceAM.Play("GrabPilon");

        refMortier.LockIngredient();

        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOn");
        SCR_Cursor.instanceCursor.ChangeHoverOn(true);

    }


    private void OnMouseOver()
    {
        outlinePilonMaterial.SetFloat("_OutlineDensity", 1f);


        if (!isMaintenu)
        {
            Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOff");
            SCR_Cursor.instanceCursor.ChangeHoverOff(true);


        }
    }

    private void OnMouseExit()
    {
        outlinePilonMaterial.SetFloat("_OutlineDensity", 0f);


        if (!isMaintenu)
        {
            SCR_Cursor.instanceCursor.ChangeClickOff(false);



        }
    }


    private void OnMouseDrag()
    {
        //rb.MoveRotation(rb.rotation * 0); // pour que le pilon vise toujours le bas
        

        if (inManipulation)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Input.mousePosition)); // cast pour avoir la world position de la souris
            outlinePilonMaterial.SetFloat("_OutlineDensity", 1f);


            if (rayHit)// si le cast touche quelque chose
            {
                Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // recupere la world position du curseur
                ddd = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // vecteur de direction entre la derniere position de la souris et sa position actuelle
                
                if (contactMortier && rayHit.point.y < transform.position.y)
                {
                    Vector3 mouseDirection = mousePos - transform.position; // calcule le vecteur de direction entre la roue et le curseur
                    float distance = Mathf.Abs(mouseDirection.x) + Mathf.Abs(mouseDirection.y);
                    float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg + 90; // calcule l'angle necessaire


                    float distanceRemap = Mathf.Clamp(distance,0,5) * angle / 5;


                    rb.MoveRotation(distanceRemap);

                    if (Mathf.Abs(ddd.x) * 5f > velocityNecessairePilon)
                    {
                        currentTempsBroyage += Time.deltaTime; // alors on ajoute le temps que l'on passe en collision

                    }

                }
                else
                {



                    Vector3 mouseDirection = aimPositionMortier.position - mousePos; // calcule le vecteur de direction entre la roue et le curseur
                    float rotZ = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg + 90; // calcule l'angle necessaire


                    
                    transform.position = rayHit.point;
                    transform.rotation =  Quaternion.Euler(0, 0, rotZ);


                    //rb.MoveRotation(rotZ); // pour que le pilon vise la base du mortier


                    //myTargetJoint.target = rayHit.point; // indique au TargetJoint que la target est la position de la souris

                }


                if(inMortier && Mathf.Abs(ddd.y) * 10 > velocityNecessairePilon)
                {
                    currentTempsBroyage += Time.deltaTime; // alors on ajoute le temps que l'on passe en collision

                    pilonVfx.Play();
                    pilonVfx.textureSheetAnimation.SetSprite(0, ingredientVfx);
                }

               




                if (currentTempsBroyage >= tempsNecessaireBoyagePilon) // si le temps actuelle de broyage et supérieur à celle necessaire alors on transforme l'ingrédient
                {
                    refMortier.FinishManipulation();
                    
                    inManipulation = false;
                    transform.DOMove(initialPosition, 0.5f);
                    transform.DORotate(Vector3.zero, 0.5f);
                    currentTempsBroyage = 0;
                    
                }

            }

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);


        }





    }

    private void OnMouseUp()
    {
        isMaintenu =false;



        SetTargetJointOnAnotherObject(true); // retire le component TarGetJoint, parametre a vrai car cette fois on reset le joint
        gameObject.layer = LayerMask.NameToLayer("DragObject"); // repasse l'objet sur ce layer pour recevoir les Cast

        rb.velocity = Vector3.zero; // a voir

        outlinePilonMaterial.SetFloat("_OutlineDensity", 0f);

    }

    public void SetTargetJointOnAnotherObject(bool needReset = false) // fonction qui ajoute / retire le component TargetJoint, élément principal pour Drag un objet 
    {
        if (!needReset) // si on pas besoin de reset le component = quand on commence a drag l'ingrédient
        {
            myTargetJoint = gameObject.AddComponent<TargetJoint2D>(); // ajoute le component a l'objet
            myTargetJoint.frequency = frequenceJoint; //met a jour les parametre du component avec les parametres qu'on a définis 
            myTargetJoint.dampingRatio = dampingJoint; // voir juste au dessus

        }
        else // si on veut reset le component = quand on a finis de drag l'ingrédient
        {
            Destroy(myTargetJoint); // on retire le component 
            myTargetJoint = null; // passe à null pour eviter les erreurs de missingObject machin trucs
        }
    }

    public void SetTimer(float dureeBroyageParameter, float velociteParameter,SCR_Mortier refMortierParameter, ParticleSystem particle, Sprite ingredient) // recupere les informations du bol de mortier pour initialiser les parametres
    {
        ingredientVfx = ingredient;
        pilonVfx = particle;
        tempsNecessaireBoyagePilon = dureeBroyageParameter;
        velocityNecessairePilon = velociteParameter;
        refMortier = refMortierParameter;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInParent<SCR_Mortier>())
        {
            contactMortier = true;
            
            AudioManager.instanceAM.Play("Mortier");
        }
        


    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponentInParent<SCR_Mortier>())
        { 
            contactMortier = false;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SCR_Mortier>() != null) // si on reste en collision avec le mortier 
        {
            inMortier =true;

            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SCR_Mortier>() != null) // si on reste en collision avec le mortier 
        {
            inMortier = false;
        }
    }

    public virtual float Remap(float value, float from1, float to1, float from2, float to2) // je le garde psk j'en ai eu besoin pendant un test et que je galere a retrouver le nom remap a chaque fois
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void SetManipulation(bool manipulationParameter){inManipulation = manipulationParameter;}
    

}
