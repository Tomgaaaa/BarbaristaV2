using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_UST_Presse : SCR_Ustensile
{


    [SerializeField] private Transform roueCrante;


    private float currentRotation; // valeur de rotation qui permet d'evaluer cmbien de tour la roue a fait
    private int nmbDeTour; // nombre de tour réalisé
    [SerializeField] private int nombreDeTourNecessaire; // nombre de tour necessaire avant de transfoirmer un ingredient
    private bool Isplayingsound = false;

    public Quaternion rotation;

    [SerializeField] private GameObject Poid;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        etatApresTransformation = enumEtatIgredient.Presse; // vus que c'est le script Presse, l'etat de transformation sera pressé 
        currentRotation = 360;
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);
        Poid.transform.localPosition = new Vector3(Poid.transform.localPosition.x, 2.05f, Poid.transform.localPosition.z);
    }

    public override void OnMouseDrag()
    {
        if(inManipulation) // verifie qu'on manipule bien l'ustensile
        {
            base.OnMouseDrag();

            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // recupere la world position du curseur
            Vector3 mouseDirection = mousePos - roueCrante.position; // calcule le vecteur de direction entre la roue et le curseur
            float rotZ = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90; // calcule l'angle necessaire
            Quaternion rotationVirtuelle = Quaternion.Euler(0, 0, rotZ); // Vector 3 avec pour Z la rotation de la roue qui vise le curseur


            rotation = rotationVirtuelle;
            //roueCrante.rotation = Quaternion.Euler(0,0, rotZ);
            /*if(roueCrante.eulerAngles.z < endRotation + 10 && roueCrante.eulerAngles.z > endRotation )
            {
                endRotation = roueCrante.eulerAngles.z;
                roueCrante.rotation = Quaternion.Euler(0,0, rotZ);
            }*/





            // si la rotation virtuelle est plus petite que la rotation actuelle + 10 mais qu'elle est supérieur a la rotation actuelle
            // ou si la rotation actuelle est superieur a 355 et que la rotation virtuelle est superieur a 0
            if (rotationVirtuelle.eulerAngles.z > currentRotation - 20 && rotationVirtuelle.eulerAngles.z < currentRotation )
            {
                if (!Isplayingsound)
                {

                 //AudioManager.instanceAM.Play("Pressoir");
                    Isplayingsound = true;

                }
                roueCrante.rotation = rotationVirtuelle; // alors la rotation de la roue = la rotation virtuelle
                currentRotation = roueCrante.eulerAngles.z; // la rotation se met a jour


                if (currentRotation < 5) // si la rotation est superieur a 355, un tour a ete realise
                {
                    

                    currentRotation = 360; // on reset la rotation 
                    nmbDeTour++; // on ajoute 1 au nombre de tour effectué

                    ParticleSystem vfxParticle = Instantiate<ParticleSystem>(myVFX, transform);
                    vfxParticle.textureSheetAnimation.SetSprite(0, ingredientDrop.GetCR_SO_Ingredient().TrancheTasse);

                    //float poidsY = Mathf.Lerp(2.05f, 1.113f, (nmbDeTour/nombreDeTourNecessaire)); 

                    Poid.transform.localPosition = new Vector3(Poid.transform.localPosition.x, Poid.transform.localPosition.y -0.3f , Poid.transform.localPosition.z);

                    AudioManager.instanceAM.Play("Presse");
                    if (nmbDeTour >= nombreDeTourNecessaire) // si on a realise le nombre de tour necessaire
                    {
                        FinishManipulation(); // alors on a finit de manipuler
                        nmbDeTour = 0; // on reset le nombre de tour pour le prochain ingrédient
                        currentRotation = 360;

                        roueCrante.rotation = Quaternion.Euler(0, 0, 0); // on reset la rotation de la roue
                    }

                }
                
            }
            else
            {
               


            }
        }
        


    }

    public override void FinishManipulation()
    {
        
        base.FinishManipulation();
        AudioManager.instanceAM.Play("Finish_Presse");
        // fauddra reset la position du bras de la presse
        sparkleVFX.Play();
       
    }
    public override void PickUpFromContenant()
    {
        base.PickUpFromContenant();
        Poid.transform.localPosition = new Vector3(Poid.transform.localPosition.x, 3.125f, Poid.transform.localPosition.z);
    }

}
