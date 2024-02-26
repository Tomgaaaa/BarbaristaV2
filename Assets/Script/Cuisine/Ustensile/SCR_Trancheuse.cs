using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Trancheuse : SCR_Ustensile
{

    [SerializeField] private Transform couteau; // reference au couteau qu'on rotate

    [SerializeField] private float rotationMin = 0f;
    [SerializeField] private float rotationMax = 100f;


    // pour la partie drag
    private Vector3 lastMousePos;
    private float mouseVelocity;
    private float forceRotation;
    [SerializeField] private float initialForceRotation = 10;
    private float totalRotation;
    private Tweener tweenRotationDrag;

    //private float lagSpeed = 1f; // vitesse de lag a laquelle le couteau suit le curseur

    [SerializeField] private int nombreDeCoupeNecessaire; // pour realiser la transformation
    private int currentNombreCoupe; // le nombre de coup mis actuellement
    private bool needReset ;  // si le couteau est revenu a sa rotation d'origine 



    public override void OnMouseDown()
    {
        base.OnMouseDown();
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        AudioManager.instanceAM.Play("Grab_1");

    }




    public override void OnMouseDrag()
    {
        if(inManipulation)
        {
            

            base.OnMouseDrag();

            mouseVelocity = 0f;
            
            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // calcul le vecteur direction entre la derniere position de la souris et sa position actuelle
            mouseVelocity += diffMousePos.x + diffMousePos.y; // valeur positive si on va a droite ou haut et negative si on va a gauche ou bas
            float mouseClamp = Mathf.Clamp(mouseVelocity, -1, 0); // clamp la valeur pour trancher d'un coup si on fait un mouvement rapide
            mouseVelocity = mouseClamp; // permet que mouseVelocity est la valeur max du clamp


            #region ancienne version
            /* pour la V1 float rotationXRemap = Remap(valueRotation, 0, -10, -51, -5); // passe la valeur entre 0 et -1 a une valeur entre -51 et -5 qui sont la rotation minimum et maximum du couteau


            if(couteau.rotation.eulerAngles.z - 360 < -25) // si le couteau est au début de sa rotation
            {
                lagSpeed = 0.5f; // pas trop de lag car il n'est pas au niveau de l'ingrédient
               
            }
            else
            {
                lagSpeed = 1.2f; // si il arrive au niveau de l'ingrédient, le couteau met + de temps a atteindre sa rotation cible, donne un effet de forcage 
            }

            // lerp la rotation entre sa rotation actuelle et sa rotation souhaité divisé par le lag pour la durée
            //couteau.rotation = Quaternion.Lerp(couteau.rotation,  Quaternion.Euler(0, 0, rotationXRemap), Time.deltaTime / lagSpeed);

            */
            #endregion

            if (couteau.rotation.eulerAngles.z < 40) // si le couteau est au début de sa rotation
            {
                forceRotation = initialForceRotation; // pas trop de lag car il n'est pas au niveau de l'ingrédient

            }
            else
            {
                forceRotation = initialForceRotation/2.5f; // si il arrive au niveau de l'ingrédient, le couteau met + de temps a atteindre sa rotation cible, donne un effet de forcage 
            }




            if (totalRotation > -rotationMax && !needReset)
            {
                totalRotation -= mouseVelocity * -forceRotation;
                couteau.Rotate(new Vector3(0, 0, mouseVelocity * -forceRotation));
            }



            // -50 = le plus haut/ rotation initial
            if (couteau.rotation.eulerAngles.z <= rotationMin && needReset) // si le couteau est revenu à sa rotation initial, il peut effectuer une nouvelle decoupe
            {
                needReset = false;
                

            }

            // -10 = position de fin / rotation finale
            if(couteau.eulerAngles.z > rotationMax && !needReset ) // si le couteau est arrivé a la fin de sa course, il doit revenir a sa rotation initial, pour pas juste faire des petits accoups
            {
                Debug.Log("ici");
                AudioManager.instanceAM.Play("Trancheuse_1");
                needReset = true; // empeche de rester en bas de la rotation et de spam des petits accoups
                currentNombreCoupe++; // ajoute 1 au nombre de ecoupe effectue
                
                totalRotation = 0f;

                if (currentNombreCoupe == nombreDeCoupeNecessaire) // si on a atteint le nombre de coupe necessaire
                {
                    FinishManipulation(); // alors on a fini de manipuler
                }
            }
            

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }
    }
   

    public override void OnMouseUp()
    {

        base.OnMouseUp();
        tweenRotationDrag.Kill();
        //baseTrancheuse.rotation = Quaternion.Euler(0, 0, -51f);

        totalRotation = 0f;


        //tweenRotationDrag = couteau.DORotate(new Vector3(0, 0, -51), 0.8f) ; // si le joueur relache le clique, le couteau se repositionne à sa rotation intial
        tweenRotationDrag = couteau.DORotate(new Vector3(0, 0, 0), 0.8f) ; // si le joueur relache le clique, le couteau se repositionne à sa rotation intial
        mouseVelocity = 0; // reset la valeur pour pas que quand on clique a nouveau, le couteau reprenne sa position ou on l'a lache
    }
  

    public override void FinishManipulation()
    {
        base.FinishManipulation();
        currentNombreCoupe = 0; // reset le nombre de coup effectué
        AudioManager.instanceAM.Play("CompletionTrancheuse");
    }

}
