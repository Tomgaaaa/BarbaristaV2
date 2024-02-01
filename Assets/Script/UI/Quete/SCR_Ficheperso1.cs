using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ficheperso1 : MonoBehaviour
{
    Camera mainCamera;
    
    [SerializeField] Transform P1;
    Transform P2;

    bool P1assigned;




    void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnMouseDrag()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        transform.position = new Vector3 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,0) ;
    }

    private void OnMouseUp()
    {

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose
       // Debug.Log(rayHit.transform.name);
        if (rayHit.transform.GetComponent<SCR_MasterQuete>())
        {
            rayHit.transform.GetComponent<SCR_MasterQuete>().OnDrop(this);
        }
    }
}
