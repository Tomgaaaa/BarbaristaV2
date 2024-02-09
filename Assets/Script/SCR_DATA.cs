using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DATA : MonoBehaviour
{

    public static SCR_DATA instanceData;

    [SerializeField] private List<SO_Quete> listCurrentQuete;


    private void Awake()
    {
        if (instanceData == null)
            instanceData = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void SetListCurrentQuest(List<SO_Quete> listParameter)
    {
        listCurrentQuete = listParameter;
    }
}