using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class SCR_QueteManager : MonoBehaviour
{

    public static SCR_QueteManager instanceQueteManager;

    [SerializeField] private List<SO_Quete> listAllQuete;
    [SerializeField] private List<SCR_MasterQuete> listQuetePropose;

    [SerializeField] private List<Transform> listTransformSpawn;
    [SerializeField] private SCR_MasterQuete prefabQuete;


    [SerializeField] private List<SO_Quete> listCurrentQueteInstance;
    private int etapeQuete = 0;
    private List<SO_Quete> listAncienneQuete;



    [SerializeField] private GameObject buttonConfirmation;
    [SerializeField] private GameObject buttonRetour;
    private Dictionary<SCR_MasterQuete, Vector3> backupTransformQuete = new Dictionary<SCR_MasterQuete, Vector3>();
    [SerializeField] private Transform positionOffQuete;


    private void Awake()
    {
        if (instanceQueteManager == null)
            instanceQueteManager = this;
        else
            Destroy(gameObject);


        SpawnQuete();
        
        
    }

#if UNITY_EDITOR
    [ContextMenu("SpawnQuete")]
    private void SpawnQuete()
    {
        for (int i = 0; i < 4; i++)
        {
            SCR_MasterQuete ms = Instantiate(prefabQuete,transform);
            ms.SetCurrentQuete(listAllQuete[Random.Range(0, listAllQuete.Count)]);
            ms.transform.position = listTransformSpawn[i].position;
            listQuetePropose.Add(ms);
            backupTransformQuete.Add(listQuetePropose[i], listQuetePropose[i].transform.position);
        }
    }
#endif


  

    public void SwitchToChoixPerso()
    {
        buttonRetour.SetActive(true);

        buttonConfirmation.SetActive(false);


        for (int i = 0 ; i < listQuetePropose.Count ; i++)
        {
            listQuetePropose[i].transform.DOMove(positionOffQuete.position, 2);




            //Destroy(listQuetePropose[i].gameObject);
        }
    }
    public void RetourChoixQuete()
    {
        buttonRetour.SetActive(false);
        buttonConfirmation.SetActive(true);


        for (int i = 0; i < listQuetePropose.Count; i++)
        {
            listQuetePropose[i].transform.DOMove(backupTransformQuete[listQuetePropose[i]], 2);
            //Destroy(listQuetePropose[i].gameObject);
        }
    }

    public void AddCurrentQuete(SCR_MasterQuete currentQueteParameter, bool removeParameter = false) 
    {
        if (!removeParameter)
        {
            listCurrentQueteInstance.Add(currentQueteParameter.GetQuete());
            listQuetePropose.Remove(currentQueteParameter);

            if(listCurrentQueteInstance.Count == 2)
            {
                buttonConfirmation.SetActive(true);


            }
        }
        else
        {
            if (listCurrentQueteInstance.Count == 2)
            {
                buttonConfirmation.SetActive(false);

            }
            listCurrentQueteInstance.Remove(currentQueteParameter.GetQuete());
            listQuetePropose.Add(currentQueteParameter);

        }

    }  

    public void AddBoisson(SO_Boisson boissonParameter)
    {
        listCurrentQueteInstance[etapeQuete].boissonsServis.Add(boissonParameter);
    }


    public SO_Quete GetCurrentQuete() { return listCurrentQueteInstance[etapeQuete]; }

    public int GetQueteCount() { return listCurrentQueteInstance.Count;}




}
