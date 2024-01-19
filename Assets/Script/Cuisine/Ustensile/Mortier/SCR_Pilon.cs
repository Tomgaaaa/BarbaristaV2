using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SCR_Pilon : MonoBehaviour
{

    private Camera mainCam;
    private Rigidbody2D rb;

    private float velocityNecessairePilon;


    private float tempsNecessaireBoyagePilon;
    public float currentTempsBroyage;

    [SerializeField] private Transform aimPositionMortier;


    #region Drag
    private TargetJoint2D myTargetJoint;
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de réetablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est réduit a chaque fréquence
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        rb.centerOfMass = new Vector3 (0,-0.8f,0);
    }

    private void OnMouseDown()
    {
        SetTargetJointOnAnotherObject(false); // ajoute le component TargetJoint, parametre false car on n'a pas besoin de reset le joint
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // on passe l'objet sur ce layer pour qu'il garde ces collisions mais pas les Cast



    }



    private void OnMouseDrag()
    {
        //rb.MoveRotation(rb.rotation * 0);

        /*Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // recupere la world position du curseur
        Vector3 mouseDirection = mousePos - aimPositionMortier.position; // calcule le vecteur de direction entre la roue et le curseur
        float rotZ = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg ; // calcule l'angle necessaire
        Quaternion rotationVirtuelle = Quaternion.Euler(0, 0, rotZ); // Vector 3 avec pour Z la rotation de la roue qui vise le curseur
        */





        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Input.mousePosition)); // cast pour avoir la world position de la souris

        if (rayHit)// si le cast touche quelque chose
        {
            myTargetJoint.target = rayHit.point; // indique au TargetJoint que la target est la position de la souris
        }

    }

    private void OnMouseUp()
    {
        SetTargetJointOnAnotherObject(true); // retire le component TarGetJoint, parametre a vrai car cette fois on reset le joint
        gameObject.layer = LayerMask.NameToLayer("DragObject"); // repasse l'objet sur ce layer pour recevoir les Cast

        rb.velocity = Vector3.zero;
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

    public void SetTimer(float dureeBroyageParameter, float velociteParameter,SCR_Mortier refMortierParameter)
    {
        tempsNecessaireBoyagePilon = dureeBroyageParameter;
        velocityNecessairePilon = velociteParameter;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SCR_Mortier>() != null)
        {

            if (Mathf.Abs(rb.velocity.x) > velocityNecessairePilon || Mathf.Abs(rb.velocity.y) > velocityNecessairePilon)
            {
                currentTempsBroyage += Time.deltaTime;
            }




            if (currentTempsBroyage >= tempsNecessaireBoyagePilon)
            {
                Debug.Log("Transfo");
            }

        }
    }
    

}
