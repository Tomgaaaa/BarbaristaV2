using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FichePerso : MonoBehaviour
{

    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnMouseDrag()
    {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));
        transform.position = rayHit.point;
    }
}
