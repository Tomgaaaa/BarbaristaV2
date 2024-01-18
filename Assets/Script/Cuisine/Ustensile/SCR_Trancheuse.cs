using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Trancheuse : SCR_Ustensile
{

    [SerializeField] private Transform baseTrancheuse;


    private Vector3 lastMousePos;
    private float RotZ;
    private Tween tweenRotationDrag;

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
            RotZ += diffMousePos.x + diffMousePos.y;
            float rotzClamp = Mathf.Clamp(RotZ, -1, 0);
            RotZ = rotzClamp;
            float rotationXRemap = Remap(rotzClamp, 0, -1, -51, -5);

            //baseTrancheuse.rotation = Quaternion.Euler(0, 0, rotationXRemap);
            //Mathf.Lerp(baseTrancheuse.rotation.eulerAngles.z, Quaternion.Euler(0, 0, rotationXRemap), baseTrancheuse.rotation.eulerAngles.z);
            tweenRotationDrag = baseTrancheuse.DORotate(new Vector3(0,0,rotationXRemap), 1f);

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }
    }


    private void OnMouseUp()
    {
        tweenRotationDrag.Kill();
        //baseTrancheuse.rotation = Quaternion.Euler(0, 0, -51f);

        tweenRotationDrag = baseTrancheuse.DORotate(new Vector3(0, 0, -51), 1.2f) ;

    }

    public override void FinishManipulation()
    {
        base.FinishManipulation();

    }

}
