using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Trancheuse : SCR_Ustensile
{

    [SerializeField] private Transform baseTrancheuse;


    private Vector3 lastMousePos;
    private float RotZ;

   

    public override void OnMouseDrag()
    {
        if(inManipulation)
        {

            base.OnMouseDrag();



            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
            RotZ += diffMousePos.x + diffMousePos.y;
            float rotzClamp = Mathf.Clamp(RotZ, 0, 1);
            RotZ = rotzClamp;
            Debug.Log(RotZ);
            //float rotationXRemap = Remap(rotzClamp, 0, 1, 5, -25);

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }
    }

}
