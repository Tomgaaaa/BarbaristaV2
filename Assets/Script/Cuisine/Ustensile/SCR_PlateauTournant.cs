using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class SCR_PlateauTournant : MonoBehaviour
{

    private Vector3 lastMousePos; // derniere position de la souris
    public float RotZ; // valeur qui permet de manipuler si on va dans la bonne direction
    private Camera mainCam;

    private Tween tweenRotation;
    public bool canRotate = false;
    private float targetRotation;
    private float targetTour;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

    }


    private void OnMouseDown()
    {
        canRotate = false;
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        tweenRotation.Kill();
    }


    private void OnMouseDrag()
    {
        RotZ = 0;
        Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // vecteur de direction entre la derniere position de la souris et sa position actuelle
        RotZ += diffMousePos.x + diffMousePos.y; // positif quand on va vers le haut ou droite et negatif quand on va a gauche ou en bas
        


        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        //tweenRotation = transform.DORotate(new Vector3(64, transform.rotation.y,transform.rotation.z + RotZ*90), 11 - 10 * MathF.Abs(RotZ));
        canRotate = true;



            
        targetTour = (transform.rotation.eulerAngles.z + RotZ * 200) / 360;


        /*if(RotZ > 0)
        {
            targetRotation = (transform.rotation.eulerAngles.z + RotZ * 200) % 360;

        }
        else
        {
            targetRotation = (transform.rotation.eulerAngles.z - (360 -RotZ) * 200) % 360;


        }*/

        Debug.Log(targetRotation);

    }

    private void Update()
    {
        if (canRotate)
        {

            if(targetTour > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(64, 0,targetRotation), Time.deltaTime );

            }
        }

    }

}
