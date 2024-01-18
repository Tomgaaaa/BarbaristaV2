using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bouilloire : MonoBehaviour
{

    private Collider2D colliderManipulation;
    [SerializeField] private Transform contenantBouilloire;
    private Vector3 startRotationBouilloire;

    private Camera mainCam;
    private Vector3 lastMousePos;
    private float RotZ;

    [SerializeField] private float quantiteEauNecessaire;
    private float eauVerse;
    private bool inManipulation = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        startRotationBouilloire = transform.rotation.eulerAngles;

        colliderManipulation = GetComponent<Collider2D>();
    }




    private void OnMouseDown()
    {
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if(inManipulation)
        {
            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
            RotZ += diffMousePos.x + diffMousePos.y;
            float rotzClamp = Mathf.Clamp(RotZ, 0, 1);
            RotZ = rotzClamp;
            float rotationXRemap = Remap(rotzClamp, 0, 1, 5, -25);

            if(rotationXRemap < -10)
            {

                eauVerse  += Remap(rotzClamp,0.5f,1,0,1);
            }

            contenantBouilloire.transform.rotation = Quaternion.Euler(0, 0, rotationXRemap);

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);



            if (eauVerse >= quantiteEauNecessaire)
            {
                FinishManipulation();
            }
        }
        


    }

    private void OnMouseUp()
    {
        contenantBouilloire.transform.DOLocalRotate(startRotationBouilloire, 1f);
        RotZ = 0f;
    }


    private void FinishManipulation()
    {
        colliderManipulation.enabled = false;
        inManipulation = false;
        OnMouseUp();
    }

    public void UnlockBouilloire()
    {
        colliderManipulation.enabled = true;
        inManipulation = true;
    }
    
    public void LockBouilloire()
    {
        colliderManipulation.enabled = false;
    }


    public float Remap(float value, float from1, float to1, float from2, float to2) // je le garde psk j'en ai eu besoin pendant un test et que je galere a retrouver le nom remap a chaque fois
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
