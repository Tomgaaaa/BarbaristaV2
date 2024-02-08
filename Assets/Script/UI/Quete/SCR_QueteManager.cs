using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class SCR_QueteManager : MonoBehaviour
{

    public static SCR_QueteManager instanceQueteManager;

    [SerializeField] SO_Quete currentQuete;
    private SO_Quete currentQueteInstance;

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


        currentQueteInstance = Instantiate(currentQuete);

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
        currentQueteInstance.boissonsServis.Add(boissonParameter);
    }


    public SO_Quete GetCurrentQuete() { return currentQueteInstance; }

#if UNITY_EDITOR
    [ContextMenu("SayDicoStat")]
    private void SayStat()
    {
        foreach (KeyValuePair<enumResistance, float> resistance in currentQueteInstance.boissonsServis[0].dicoResistanceBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }
    }
#endif
}
