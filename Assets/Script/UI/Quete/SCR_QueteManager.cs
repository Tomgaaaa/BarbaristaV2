using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class SCR_QueteManager : MonoBehaviour
{

    public static SCR_QueteManager instanceQueteManager;

    public SO_Quete currentQuete;

    private List<SO_Quete> listAncienneQuete;

    [SerializeField] private GameObject buttonConfirmation;

    [SerializeField] private List<SortingGroup> listMasterQueteLayer;

    private void Awake()
    {
        if (instanceQueteManager == null)
            instanceQueteManager = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }


    public void UpdateQueteLayer(SortingGroup queteDragParameter)
    {
        listMasterQueteLayer.Remove(queteDragParameter);
        listMasterQueteLayer.Insert(0, queteDragParameter);

        for (int i = 0; i < listMasterQueteLayer.Count; i++)
        {
            listMasterQueteLayer[i].sortingOrder = i;
            listMasterQueteLayer[i].GetComponentInChildren<Canvas>().sortingOrder = i;
        }
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

    public void AddBoisson(SO_Boisson boissonParameter)
    {
        currentQuete.boissonsServis.Add(boissonParameter);
    }




#if UNITY_EDITOR
    [ContextMenu("SayDicoStat")]
    private void SayStat()
    {
        foreach (KeyValuePair<enumResistance, float> resistance in currentQuete.boissonsServis[0].dicoResistanceBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }
    }
#endif
}
