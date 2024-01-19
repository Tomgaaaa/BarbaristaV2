using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Trancheuse : SCR_Ustensile
{

    [SerializeField] private Transform couteau;


    private Vector3 lastMousePos;
    private float RotZ;
    private Tweener tweenRotationDrag;

    private float lagSpeed = 1f;

    [SerializeField] private int nombreDeCoupeNecessaire;
    private int currentNombreCoupe;
    private bool needReset ; 

    private void OnMouseDown()
    {
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

    }


    public override void OnMouseDrag()
    {
        if(inManipulation)
        {

            base.OnMouseDrag();



            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
            Debug.Log(diffMousePos);
            RotZ += diffMousePos.x + diffMousePos.y;
            float rotzClamp = Mathf.Clamp(RotZ, -1, 0);
            RotZ = rotzClamp;
            float rotationXRemap = Remap(rotzClamp, 0, -1, -51, -5);



            if(couteau.rotation.eulerAngles.z - 360 < -25)
            {
                lagSpeed = 0.5f;
            }
            else
            {
                lagSpeed = 2f;
            }




            couteau.rotation = Quaternion.Lerp(couteau.rotation,  Quaternion.Euler(0, 0, rotationXRemap), Time.deltaTime / lagSpeed);
            
            //tweenRotationDrag = baseTrancheuse.DORotate(new Vector3(0,0,rotationXRemap), 1.5f);





            if(couteau.rotation.eulerAngles.z - 360 < -50 && needReset)
            {
                needReset = false;
            }

            if(couteau.eulerAngles.z - 360 > -10 && !needReset ) 
            {
                needReset = true;
                currentNombreCoupe++;


                if (currentNombreCoupe == nombreDeCoupeNecessaire)
                {
                    FinishManipulation();
                }
            }
            

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }
    }
   

    private void OnMouseUp()
    {
        tweenRotationDrag.Kill();
        //baseTrancheuse.rotation = Quaternion.Euler(0, 0, -51f);

        tweenRotationDrag = couteau.DORotate(new Vector3(0, 0, -51), 0.8f) ;
        RotZ = 0;

    }
  

    public override void FinishManipulation()
    {
        base.FinishManipulation();
        currentNombreCoupe = 0;
    }

}
