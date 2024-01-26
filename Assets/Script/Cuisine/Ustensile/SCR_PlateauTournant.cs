using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_PlateauTournant : MonoBehaviour
{

    private Vector3 lastMousePos; // derniere position de la souris
    public float RotZ; // valeur qui permet de manipuler si on va dans la bonne direction
    private Camera mainCam;

    public float targetRotation;
    public float currentRotation;
    public AnimationCurve curveLinear;
    public AnimationCurve curveEaseOut;
    public float speedRotation;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

    }


    private void OnMouseDown()
    {
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //tweenRotation.Kill();

        targetRotation = 0; currentRotation = 0;

        StopAllCoroutines();
    }


    private void OnMouseDrag()
    {
        RotZ = 0;
        Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // vecteur de direction entre la derniere position de la souris et sa position actuelle
        RotZ += diffMousePos.x ; // positif quand on va vers le haut ou droite et negatif quand on va a gauche ou en bas
        
        

        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {



        float e = 0;
        e = (transform.rotation.eulerAngles.z + RotZ * 400);
        targetRotation = e;

        currentRotation = targetRotation;

        RotZ = Mathf.Clamp(RotZ, -1.5f, 1.5f);
        speedRotation = Remap(Mathf.Abs(RotZ), 0, 1.5f, 3, 0.5f);

        StartCoroutine(LerpRotation());
       

    }




    private IEnumerator LerpRotation()
    {

        while(currentRotation != 0)
        {
            float _time = 0;

            Quaternion startRot = transform.rotation;
            Quaternion endRot;

            if (currentRotation > 90 || currentRotation < -90)
            {
                endRot = Quaternion.Euler(64, 0, transform.rotation.eulerAngles.z + Mathf.Sign(RotZ) * 90);

            }
            else
            {
                endRot = Quaternion.Euler(64, 0, transform.rotation.eulerAngles.z + Mathf.Abs(RotZ) * currentRotation);

            }




            while (_time < speedRotation)
            {
                if (currentRotation > 90 || currentRotation < -90)
                {
                    transform.rotation = Quaternion.Lerp(startRot, endRot, curveLinear.Evaluate(_time / speedRotation));

                }
                else
                {
                    transform.rotation = Quaternion.Lerp(startRot, endRot, curveEaseOut.Evaluate(_time / speedRotation));
                }

                _time += Time.deltaTime;
                yield return null;



            }


            if (currentRotation > 90 || currentRotation < -90)
            {
                currentRotation += Mathf.Sign(RotZ) * -90;

            }
            else
            {
                currentRotation = 0;

            }

           
        }
        

    }

    public virtual float Remap(float value, float from1, float to1, float from2, float to2) // je le garde psk j'en ai eu besoin pendant un test et que je galere a retrouver le nom remap a chaque fois
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
