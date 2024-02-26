using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
using UnityEngine.Rendering;

public class SCR_QueteManager : MonoBehaviour
{

    public static SCR_QueteManager instanceQueteManager;

    [SerializeField] private List<SO_Quete> listAllQuete;
    private List<SCR_MasterQuete> listQuetePropose = new List<SCR_MasterQuete>();

    [SerializeField] private List<Transform> listTransformSpawn;

    [Header("Spawn")]
    [SerializeField] private SCR_MasterQuete prefabQuete;
    [SerializeField] private SCR_Ficheperso1 prefabFichePerso;
    [SerializeField] private Transform parentFichePerso;
    private List<SCR_Ficheperso1> listFichepersoPropose = new List<SCR_Ficheperso1>();
    private List<SCR_Ficheperso1> listFichepersoUtilise = new List<SCR_Ficheperso1>();
    [SerializeField] private List<SO_Personnage> listPerso;
    private List<Vector3> startPositionPerso = new List<Vector3>();


    private List<SCR_MasterQuete> listCurrentQueteInstance = new List<SCR_MasterQuete>();
    private int etapeQuete = 0;
    private bool firstIsHigher = true;
    private List<SO_Quete> listAncienneQuete;


    [Header("Boutons Confirmation")]
    [SerializeField] private GameObject buttonConfirmation;
    [SerializeField] private GameObject buttonRetour;
    [SerializeField] private GameObject buttonChangerSens;
    [SerializeField] private GameObject buttonConfirmationPersos;


    [Header("Animation")]
    private Dictionary<SCR_MasterQuete, Vector3> backupTransformQuete = new Dictionary<SCR_MasterQuete, Vector3>();
    [SerializeField] private Transform positionOffQuete;
    [SerializeField] private Transform positionSelectQuete;


    private void Awake()
    {
        if (instanceQueteManager == null)
            instanceQueteManager = this;
        else
            Destroy(gameObject);


        SpawnQuete();
        SpawnPerso();

        
        
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

    private void SpawnPerso()
    {
        for (int i = 0; i < 4; i++)
        {
            SCR_Ficheperso1 fichePerso = Instantiate(prefabFichePerso, parentFichePerso);
            listFichepersoPropose.Add(fichePerso);
            fichePerso.SetSoPerso(listPerso[i]); 
            startPositionPerso.Add(fichePerso.transform.position);

        }
    }


    public void SwitchToChoixPerso()
    {
        buttonRetour.SetActive(true);
        buttonConfirmation.SetActive(false);
        buttonChangerSens.SetActive(true);


        //permet que pendant l'animation de decalage la page ne traverse pas celle du dessous
        listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 1);


        for (int i = 0 ; i < listQuetePropose.Count ; i++)
        {
            listQuetePropose[i].SetInChoixPerso(true);
            if (!listCurrentQueteInstance.Contains(listQuetePropose[i]))
            {
                listQuetePropose[i].transform.DOMove(positionOffQuete.position, 2);

            }

        }

        for (int i = 0 ; i < 4; i++)
        {
            listFichepersoPropose[i].transform.DOMove(listTransformSpawn[i].position,2f);
        }


        listCurrentQueteInstance[1].transform.DOMove(new Vector3(positionSelectQuete.position.x, positionSelectQuete.position.y,0), 2);
        listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 35), 1);
        listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
        listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 0;


        listCurrentQueteInstance[0].transform.DOMove(positionSelectQuete.position, 2);
        listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 5;
        listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 5;


    }
    public void RetourChoixQuete()
    {
        buttonRetour.SetActive(false);
        buttonConfirmation.SetActive(true);
        buttonChangerSens.SetActive(false);
        buttonConfirmationPersos.SetActive(false);


        for (int i = 0; i < listQuetePropose.Count; i++)
        {
            listQuetePropose[i].SetInChoixPerso(false);

            listQuetePropose[i].transform.DOMove(backupTransformQuete[listQuetePropose[i]], 2);
            listQuetePropose[i].transform.DORotate(Vector3.zero, 1);

            listQuetePropose[i].ResetPerso();
            //Destroy(listQuetePropose[i].gameObject);
        }


        listFichepersoUtilise.Clear();
        for (int i = 0; i < 4; i++)
        {
            
            listFichepersoPropose[i].transform.DOMove(startPositionPerso[i], 2f);
        }
    }


    
    public void ChangeShowedQuete()
    {
        if(firstIsHigher)
        {
            firstIsHigher = false;
            listCurrentQueteInstance[0].transform.position = new Vector3(listCurrentQueteInstance[0].transform.position.x, listCurrentQueteInstance[0].transform.position.y, 1);
            listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 0;
            listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 0;


            foreach(SCR_Ficheperso1 fiche in listCurrentQueteInstance[0].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(false);
                fiche.GetComponent<SortingGroup>().sortingOrder = 4;
            }


            listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 0);
            listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 5;
            listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 5;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[1].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(true);
                fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            }

            listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 0), 1);
            listCurrentQueteInstance[0].transform.DORotate(new Vector3(0, 0, 35), 1);

        }
        else
        {
            firstIsHigher = true;
            listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 1);

            listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 0;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[1].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(false);
                fiche.GetComponent<SortingGroup>().sortingOrder = 4;
            }


            listCurrentQueteInstance[0].transform.position = new Vector3(listCurrentQueteInstance[0].transform.position.x, listCurrentQueteInstance[0].transform.position.y, 0);

            listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 5;
            listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 5;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[0].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(true);
                fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            }

            listCurrentQueteInstance[0].transform.DORotate(new Vector3(0, 0, 0), 1);
            listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 35), 1);
        }
        



    }

    public void AddRemovePersosUtilise(SCR_Ficheperso1 ficherPersoParameter, bool addPerso)
    {

        if(addPerso)
        {
            listFichepersoUtilise.Add(ficherPersoParameter);

        }
        else
        {
            listFichepersoUtilise.Remove(ficherPersoParameter);

        }


        if(listFichepersoUtilise.Count == 4)
        {

            buttonConfirmationPersos.SetActive(true);
        }
        else
        {
            buttonConfirmationPersos.SetActive(false);
        }

    }


    public void ConfirmSelectionPersos()
    {
        Debug.Log("Go cuisine");
    }

    public void AddCurrentQuete(SCR_MasterQuete currentQueteParameter, bool removeParameter = false) 
    {
        if (!removeParameter)
        {
            listCurrentQueteInstance.Add(currentQueteParameter);

            if(listCurrentQueteInstance.Count == 2)
            {
                buttonConfirmation.SetActive(true);

                for(int i = 0 ; i < listQuetePropose.Count ; i++)
                {
                    if (!listCurrentQueteInstance.Contains(listQuetePropose[i]))
                    {
                        listQuetePropose[i].ShowMask(true);
                    }
                }


            }
        }
        else
        {
            if (listCurrentQueteInstance.Count == 2)
            {
                buttonConfirmation.SetActive(false);

                for (int i = 0; i < listQuetePropose.Count; i++)
                {
                    if (!listCurrentQueteInstance.Contains(listQuetePropose[i]))
                    {
                        listQuetePropose[i].ShowMask(false);
                    }
                }
            }
            listCurrentQueteInstance.Remove(currentQueteParameter);

        }

    }  

    public void AddBoisson(SO_Boisson boissonParameter)
    {
        listCurrentQueteInstance[etapeQuete].GetQuete().boissonsServis.Add(boissonParameter);
    }


    public SO_Quete GetCurrentQuete() { return listCurrentQueteInstance[etapeQuete].GetQuete(); }

    public int GetQueteCount() { return listCurrentQueteInstance.Count;}




}
