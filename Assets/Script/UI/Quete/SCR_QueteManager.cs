using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_QueteManager : MonoBehaviour
{

    public static SCR_QueteManager instanceQueteManager;

    private SO_Quete currentQuete;

    private List<SO_Quete> listAncienneQuete;

    [SerializeField] private GameObject buttonConfirmation;

    private void Awake()
    {
        if (instanceQueteManager == null)
            instanceQueteManager = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }


    public void UnlockConfirmButton(bool unlockParameter)
    {
        if(unlockParameter)
        {
            buttonConfirmation.SetActive(true);
        }
        else
        {
            buttonConfirmation.SetActive(false);
        }
    }

    public void SetCurrentQuete(SO_Quete currentQueteParameter) { currentQuete = currentQueteParameter; }  

}
