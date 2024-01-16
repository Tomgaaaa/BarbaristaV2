using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SCR_UST_Presse : SCR_Ustensile
{


    [SerializeField] private Transform roueCrante;

    private float startRotation;

    private float endRotation;
    private int nmbDeTour;
    [SerializeField] private int nombreDeTourNecessaire;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        etatApresTransformation = enumEtatIgredient.Presse; // vus que c'est le script Presse, l'etat de transformation sera pressé 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        startRotation = roueCrante.localRotation.z;
    }

    public override void OnMouseDrag()
    {
        if(inManipulation)
        {
            base.OnMouseDrag();

            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDirection = mousePos - roueCrante.position;
            float rotZ = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion queternionEuler = Quaternion.Euler(0, 0, rotZ);



            //roueCrante.rotation = Quaternion.Euler(0,0, rotZ);
            /*if(roueCrante.eulerAngles.z < endRotation + 10 && roueCrante.eulerAngles.z > endRotation )
            {
                endRotation = roueCrante.eulerAngles.z;
                roueCrante.rotation = Quaternion.Euler(0,0, rotZ);
            }*/


            if (queternionEuler.eulerAngles.z < endRotation + 10 && queternionEuler.eulerAngles.z > endRotation || endRotation > 355 && queternionEuler.eulerAngles.z > 0)
            {
                roueCrante.rotation = Quaternion.Euler(0, 0, rotZ);
                endRotation = roueCrante.eulerAngles.z;

                if (endRotation > 355)
                {
                    endRotation = 0;
                    nmbDeTour++;

                    if (nmbDeTour >= nombreDeTourNecessaire)
                    {
                        FinishManipulation();
                        nmbDeTour = 0;
                        endRotation = 0;
                        roueCrante.rotation = Quaternion.Euler(0, 0, 0);

                    }
                }
            }
        }
        


    }

    public override void FinishManipulation()
    {
        
        base.FinishManipulation();

        // fauddra reset la position du bras dela presse

    }

    public static float Remap( float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
