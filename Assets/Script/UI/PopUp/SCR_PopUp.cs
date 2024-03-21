using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SCR_PopUp : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI textInfo;
    private bool startTimer;
    private float timeToActivate;

    private Vector3 offset;
   
    private void Update()
    {
        gameObject.transform.position = Input.mousePosition + offset;
    }

    public void ActivateFromBiome(string popInfoText)
    {
        Activate(popInfoText, Vector3.zero);

    }

    public void Activate(string popInfoTxt, Vector3 offsetParamater)
    {
        offset = offsetParamater;
        gameObject.SetActive(true);
        gameObject.transform.position = Input.mousePosition + offset;
        textInfo.text = popInfoTxt;
    }
    public void Desactivate()
    {
        gameObject.SetActive(false);
    }

    public void CallPopUp(string infoDiff)
    {
        SCR_QueteManager.instanceQueteManager.PopUpReader(infoDiff);
    }
}
