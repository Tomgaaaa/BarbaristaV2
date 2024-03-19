using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SCR_PopUp : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textInfo;
    private bool startTimer;
    private float timeToActivate;
   
    private void Update()
    {
        gameObject.transform.position = Input.mousePosition;
    }
    public void Activate(string popInfoTxt)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = Input.mousePosition;
        textInfo.text = popInfoTxt;
    }
    public void Desactivate()
    {
        gameObject.SetActive(false);
        startTimer = false;
    }
}
