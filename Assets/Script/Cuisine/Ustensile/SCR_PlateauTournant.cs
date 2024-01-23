using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlateauTournant : MonoBehaviour
{

    private Vector3 lastMousePos; // derniere position de la souris
    public float RotZ; // valeur qui permet de manipuler si on va dans la bonne direction
    private Camera mainCam;

    private Tween tweenRotation;
    public bool canRotate = false;
    public float targetRotation;



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




        float e = 0;
        e = (transform.rotation.eulerAngles.z + RotZ * 400);
        targetRotation = e % 360;
   
       
       

    }

    private void Update()
    {
        if (canRotate)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(64, 0,targetRotation), Time.deltaTime );

        
        }

    }

  

}
