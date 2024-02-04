using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ButtonDiorama : MonoBehaviour
{
    [SerializeField] GameObject canvasVN;
    [SerializeField] GameObject canvasCuisine;
    public void OnClick(GameObject obj)
    {
        gameObject.SetActive(false);
        obj.SetActive(true);
    }
    
    public void GoCuisine()
    {
        canvasVN.SetActive(false);
        canvasCuisine.SetActive(true);
    }
    public void GoVN()
    {
        canvasCuisine.SetActive(false);
        canvasVN.SetActive(true);
    }
}
