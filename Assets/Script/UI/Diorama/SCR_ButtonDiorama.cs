using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof( EventTrigger))]
[RequireComponent(typeof( SCR_CustomButton))]
public class SCR_ButtonDiorama : MonoBehaviour
{
    [Header("Ancien")]
    [SerializeField] GameObject canvasVN;
    [SerializeField] GameObject canvasCuisine;


    [Header("Nouveau zoom")]
    [SerializeField] private Vector3 zoomPosition;
    private Vector3 initialPosition;
    [SerializeField] private Vector3 zoomScale;
    private Vector3 initialScale;

    [SerializeField] private Image blur;

    private bool isClick = false;
    private int siblingIndex;


    [Header("Transition")]
    [SerializeField] private RectTransform transitionBG;
    [SerializeField] private RectTransform emplacementGoCuisine;
    [SerializeField] private RectTransform emplacementGoVN;


    private void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;

        siblingIndex = transform.GetSiblingIndex();


       
    }

    public void OnClick(/*GameObject obj*/ bool isSamuel)
    {
        /*gameObject.SetActive(false);
        obj.SetActive(true);*/




        if(isClick)
        {
            transform.SetSiblingIndex(siblingIndex);
            

            blur.DOFade(0, 1f);
            transform.DOMove(initialPosition, 1f);
            transform.DOScale(initialScale, 1f);

        }
        else
        {
            transform.SetAsLastSibling();
            


            blur.DOFade(0.22f, 1f);
            transform.DOMove(zoomPosition, 1f);
            transform.DOScale(zoomScale, 1f);
        }

        isClick = !isClick;
        


    }



    public void GoCuisine()
    {
        transitionBG.position = emplacementGoVN.position;
        transitionBG.DOMove(emplacementGoCuisine.position, 2.5f);

        Invoke("ChangeSceneCuisine", 0.8f);

    }
    public void GoVN()
    {
        transitionBG.position = emplacementGoCuisine.position;
        transitionBG.DOMove(emplacementGoVN.position, 2.5f);


        Invoke("ChangeSceneVN", 0.8f);


    }


    private void ChangeSceneVN()
    {
        canvasCuisine.SetActive(false);
        canvasVN.SetActive(true);
    }

    private void ChangeSceneCuisine()
    {
        canvasCuisine.SetActive(true);
        canvasVN.SetActive(false);
    }
}
