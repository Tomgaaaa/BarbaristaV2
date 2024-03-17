using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SCR_QueteManager : MonoBehaviour, ISerializationCallbackReceiver
{

    public static SCR_QueteManager instanceQueteManager;

    [SerializeField] private List<SO_Quete> listAllQuete;


    #region Dico



    [System.Serializable] public class dicoJourQueteClass : TemplateDico<int, List<SO_Quete>> { };
    public  Dictionary<int, List<SO_Quete>> dicoJourQuete { get; private set; } // dico listant les quetes a afficher selon le jour
    [SerializeField] private List<dicoJourQueteClass> listDicoJourQuete;

    [System.Serializable] public class dicoJourPersoClass : TemplateDico<int, List<SO_Personnage>> { };
    private Dictionary<int, List<SO_Personnage>> dicoJourPerso; // dico listant les persos a afficher selon le jour
    [SerializeField] private List<dicoJourPersoClass> listDicoJourPerso;
    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoJourQuete = new Dictionary<int, List<SO_Quete>>();

        foreach (dicoJourQueteClass item in listDicoJourQuete)
        {
            if (!dicoJourQuete.ContainsKey(item.key))
            {
                dicoJourQuete.Add(item.key, item.value);
            }
        }




        dicoJourPerso = new Dictionary<int,List<SO_Personnage>>();

        foreach (dicoJourPersoClass item in listDicoJourPerso)
        {
            if (!dicoJourPerso.ContainsKey(item.key))
            {
                dicoJourPerso.Add(item.key, item.value);
            }
        }

    }

    #endregion


    // out dated
    private List<SCR_QueteTableau> listQuetePropose = new List<SCR_QueteTableau>(); // liste de toute les quetes disponibles

    [SerializeField] private List<Transform> listTransformSpawn; // liste des emplacements pour les quetes et les persos

    [Header("Spawn")]
    [SerializeField] private SCR_QueteTableau prefabQuete; // prefab de quete que pour le choix de quete
    [SerializeField] private SCR_Ficheperso1 prefabFichePerso; // prefab de fiche perso pour le choix des persos
    [SerializeField] private Transform parentFichePerso; // parent des fiches persos, pour quand on revient au choix de quete toutes les fiches persos bougent
    private List<SCR_Ficheperso1> listFichepersoPropose = new List<SCR_Ficheperso1>(); // list des persos spawn, pour differencier ceux qui sont utilises et ceux qui ne le sont pas
    private List<SCR_Ficheperso1> listFichepersoUtilise = new List<SCR_Ficheperso1>(); // lsit des fiches persos selectiones
    [SerializeField] private List<SO_Personnage> listSoPerso; // list de tout les SO persos que l'on peut proposer
    private List<SO_Personnage> listSoPersoInstancie = new List<SO_Personnage>(); // list de tout les SO persos que l'on a instancie
    private List<Vector3> startPositionPerso = new List<Vector3>(); // position de base des fiches persos


    private List<SCR_QueteTableau> listCurrentQueteInstance = new List<SCR_QueteTableau>(); // liste des quetes instanties / proposes
    private bool firstIsHigher = true; // bool qui permet de savoir si la premiere quete selectionne est celle du dessus


    [Header("Boutons Confirmation")]
    [SerializeField] private GameObject buttonConfirmation; // bouton de confirmation du choix des quetes 
    [SerializeField] private GameObject buttonRetour; // bouton retour pour revenir au choix des quetes 
    [SerializeField] private GameObject buttonChangerSens; // bouton pour changer la quete du dessus dans le choix des persos
    [SerializeField] private GameObject buttonConfirmationPersos; // bouton de confirmation dui choix des persos


    [Header("Animation")] // surement a virer quand on fera des vrai animations
    private Dictionary<SCR_QueteTableau, Vector3> backupTransformQuete = new Dictionary<SCR_QueteTableau, Vector3>();
    [SerializeField] private Transform positionOffQuete;
    [SerializeField] private Transform positionSelectQuete;



    private void Awake()
    {
        if (instanceQueteManager == null)
            instanceQueteManager = this;
        else
            Destroy(gameObject);


        
        
    }

    private void Start()
    {
        SpawnQuete(); //instantie des persos et des quetes par rapport a leur SO
        SpawnPerso();

    }


    private void SpawnQuete()
    {
        if(SCR_DATA.instanceData.GetLisNotSelectedQuest().Count != 0)
        {
            foreach (SO_Quete queteNotSelected in SCR_DATA.instanceData.GetLisNotSelectedQuest())
            {
                dicoJourQuete[SCR_DATA.instanceData.GetJour()].Add(queteNotSelected);

            }
            SCR_DATA.instanceData.SetListNotSelectedQuest(null, true);
        }

       



        for (int i = 0; i < dicoJourQuete[SCR_DATA.instanceData.GetJour()].Count; i++) // pour chaque quete presente, au jour actuelle, je fais ce qu'il y ci dessous
        {

            SCR_QueteTableau ms = Instantiate(prefabQuete,transform); // instantie un prefab de quete

            SO_Quete queteInstance = ScriptableObject.CreateInstance<SO_Quete>(); // on cree une instance de SO, pour l'instant le SO est vide
            SO_Quete queteChoisis = dicoJourQuete[SCR_DATA.instanceData.GetJour()][i]; // quete renseigne dans le dico quete / jour, on la recupere pour prendre ses infos
            
            // on transmet les infos de la quete selectionne a l'instance de SO, c'est l'equivalent de faire un copie coller de SO
            queteInstance.Init(queteChoisis.dicoResistanceDifficulte,queteChoisis.titre,queteChoisis.difficulty,queteChoisis.illustration,queteChoisis.description,queteChoisis.infoEvenement,queteChoisis.reward, queteChoisis.myQueteInk);


           // ms.SetCurrentQuete(dicoJourQuete[SCR_DATA.instanceData.GetJour()][i]);
            ms.SetCurrentQuete(queteInstance); // on attribue a la fiche quete le SO de quete qu'on a cree ci dessus
            ms.transform.position = listTransformSpawn[i].position; // on le positionne celon la liste de spawn
            listQuetePropose.Add(ms); // on l'ajoute a la liste des quetes qu'on propose
            backupTransformQuete.Add(listQuetePropose[i], listQuetePropose[i].transform.position);
        }
    }


    private void SpawnPerso()// a revoir
    {



        for (int i = 0; i < dicoJourPerso[SCR_DATA.instanceData.GetJour()].Count; i++)
        {
            SCR_Ficheperso1 fichePerso = Instantiate(prefabFichePerso, parentFichePerso);
            listFichepersoPropose.Add(fichePerso);


            if (SCR_DATA.instanceData.GetListPersos().Count == 0)
            {
                SO_Personnage personnageInstance = ScriptableObject.CreateInstance<SO_Personnage>();
               // SO_Personnage personnageChoisi = listSoPerso[i];
                SO_Personnage personnageChoisi = dicoJourPerso[SCR_DATA.instanceData.GetJour()][i];
                personnageInstance.Init(personnageChoisi.dicoResistance, personnageChoisi.dicoRelationPerso, personnageChoisi.namePerso, personnageChoisi.myEnumPerso,personnageChoisi.profil,personnageChoisi.characterPerso);

                fichePerso.SetSoPerso(personnageInstance);

                listSoPersoInstancie.Add(personnageInstance);


               if(i == 3)
                {
                    SCR_DATA.instanceData.SetListPersos(listSoPersoInstancie);
                }


            }
            else
            {
                fichePerso.SetSoPerso(SCR_DATA.instanceData.GetListPersos()[i]);
            }


            startPositionPerso.Add(fichePerso.transform.position);

        }


        
        
    }


    public void SwitchToChoixPerso()// fonction appeller par le bouton de confirmation du choix de quete
    {
        buttonRetour.SetActive(true);
        buttonConfirmation.SetActive(false);


        //permet que pendant l'animation de decalage la page ne traverse pas celle du dessous

        if(SCR_DATA.instanceData.GetJour()>2)
        {
            buttonChangerSens.SetActive(true);

            listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 4);

            // positionne la deuxieme quete derriere et legerement tourne et on la fait passer derriere la premiere quete
            listCurrentQueteInstance[1].transform.DOMove(new Vector3(positionSelectQuete.position.x, positionSelectQuete.position.y, 4), 2);
            listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 35), 1);
           // listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 0;
            listCurrentQueteInstance[1].SetHigher(false);

        }
        
        listCurrentQueteInstance[0].SetHigher(true);

        for (int i = 0 ; i < listQuetePropose.Count ; i++)
        {
            listQuetePropose[i].SetInChoixPerso(true);// on indique aux quetes qu'on est dans la partie choix de persos, pour empecher de les grabs

            if (!listCurrentQueteInstance.Contains(listQuetePropose[i])) 
            {
                listQuetePropose[i].transform.DOMove(positionOffQuete.position, 2); // on change la position de celle qui ne sont pas selectionnes, pour les mettre hors ecran
                
            }

        }

        for (int i = 0 ; i < dicoJourPerso[SCR_DATA.instanceData.GetJour()].Count; i++) // on fait bouger les fiches persos pour les afficher dans l'ecran
        {
            listFichepersoPropose[i].SetTweener( listFichepersoPropose[i].transform.DOMove(listTransformSpawn[i].position,2f));
           
        }

        

        // positionne la premiere quete devant la deuxieme 
        listCurrentQueteInstance[0].transform.DOMove(positionSelectQuete.position, 2);
        //listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 5;
        listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 5;

        AudioManager.instanceAM.Play("ValidationQuest");
        AudioManager.instanceAM.Play("SwitchWhoosh");

    }
    public void RetourChoixQuete() // fonction appeller par le bouton retour dans la partie choix de perso
    {
        buttonRetour.SetActive(false);
        buttonConfirmation.SetActive(true);
        buttonChangerSens.SetActive(false);
        buttonConfirmationPersos.SetActive(false);

        firstIsHigher = true; // reinitialise cette valeur au cas ou on est appuye sur le bouton de changer premiere quete et qu'on revient au choix de quete

        for (int i = 0; i < listQuetePropose.Count; i++) // repositionne les quetes dans l'ecran, on indique qu'on est plus dans choix persos et on reset les persos s'ils en avaient
        {
            listQuetePropose[i].SetInChoixPerso(false);

            listQuetePropose[i].transform.DOMove(backupTransformQuete[listQuetePropose[i]], 2);
            listQuetePropose[i].transform.DORotate(Vector3.zero, 1);

            listQuetePropose[i].ResetPerso();
            //Destroy(listQuetePropose[i].gameObject);

            AudioManager.instanceAM.Play("RetourChoixQuete");
            AudioManager.instanceAM.Play("Retourwhoosh");
        }


        listFichepersoUtilise.Clear();
        for (int i = 0; i < listSoPersoInstancie.Count; i++)
        {
            
            listFichepersoPropose[i].transform.DOMove(startPositionPerso[i], 2f);
            listFichepersoPropose[i].transform.parent = null;
        }
    }


    
    public void ChangeShowedQuete() // fonction pour changer laquelle des 2 quetes on veut attriber des persos
    {
        if(firstIsHigher) // si la premiere quete est sur le dessus alors on veut qu'elle passe derriere 
        {
            listCurrentQueteInstance[0].SetHigher(false);
            listCurrentQueteInstance[1].SetHigher(true);


            firstIsHigher = false;
            listCurrentQueteInstance[0].transform.position = new Vector3(listCurrentQueteInstance[0].transform.position.x, listCurrentQueteInstance[0].transform.position.y, 1);
            //listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 0;
            listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 0;


            foreach(SCR_Ficheperso1 fiche in listCurrentQueteInstance[0].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(false);
                fiche.GetComponent<SortingGroup>().sortingOrder = 4;
            }


            listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 0);
            //listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 5;
            listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 5;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[1].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(true);
                fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            }

            listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 0), 1);
            listCurrentQueteInstance[0].transform.DORotate(new Vector3(0, 0, 35), 1);

        }
        else // si la deuxieme quete est sur le dessus, on veut qu'elle passse derriere
        {
            listCurrentQueteInstance[0].SetHigher(true);
            listCurrentQueteInstance[1].SetHigher(false);


            firstIsHigher = true;
            listCurrentQueteInstance[1].transform.position = new Vector3(listCurrentQueteInstance[1].transform.position.x, listCurrentQueteInstance[1].transform.position.y, 1);

            //listCurrentQueteInstance[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            listCurrentQueteInstance[1].GetComponentInChildren<Canvas>().sortingOrder = 0;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[1].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(false);
                fiche.GetComponent<SortingGroup>().sortingOrder = 4;
            }


            listCurrentQueteInstance[0].transform.position = new Vector3(listCurrentQueteInstance[0].transform.position.x, listCurrentQueteInstance[0].transform.position.y, 0);

            //listCurrentQueteInstance[0].GetComponent<SpriteRenderer>().sortingOrder = 5;
            listCurrentQueteInstance[0].GetComponentInChildren<Canvas>().sortingOrder = 5;

            foreach (SCR_Ficheperso1 fiche in listCurrentQueteInstance[0].GetComponentsInChildren<SCR_Ficheperso1>())
            {
                fiche.SetCanMove(true);
                fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            }

            listCurrentQueteInstance[0].transform.DORotate(new Vector3(0, 0, 0), 1);
            listCurrentQueteInstance[1].transform.DORotate(new Vector3(0, 0, 35), 1);
        }

        AudioManager.instanceAM.Play("SwitchFiche");


    }

    public void AddRemovePersosUtilise(SCR_Ficheperso1 ficherPersoParameter, bool addPerso) // fonction pour rajouter / enlever des persos d'une quete
    {

        if(addPerso)
        {
            listFichepersoUtilise.Add(ficherPersoParameter);

        }
        else
        {
            listFichepersoUtilise.Remove(ficherPersoParameter);

        }


        if(listFichepersoUtilise.Count == dicoJourPerso[SCR_DATA.instanceData.GetJour()].Count) // a changer
        {

            buttonConfirmationPersos.SetActive(true);
        }
        else
        {
            buttonConfirmationPersos.SetActive(false);
        }

    }


    public void ConfirmSelectionPersos() // bouton appeller par le bouton de confirmation du choix de perso
    {

        foreach(SCR_QueteTableau masterQuete in listQuetePropose)
        {

            if(listCurrentQueteInstance.Contains(masterQuete))
            {
                SCR_DATA.instanceData.SetListCurrentQuest(masterQuete.GetQuete()); // on transfere les quetes selectionnes au data


            }
            else
            {
                SCR_DATA.instanceData.SetListNotSelectedQuest(masterQuete.GetQuete()); // on transfere les quetes non selectionnes au data

            }


        }

        AudioManager.instanceAM.Play("switchVN");
        AudioManager.instanceAM.Play("ConfirmSwitchVN");
        SceneManager.LoadScene("SCE_VisualNovel");
    }

    public void AddCurrentQuete(SCR_QueteTableau currentQueteParameter, bool removeParameter = false) // fonction appeler lorsqu'on clique sur une des quetes dans la partie choix de quete
    {
        if (!removeParameter) // si on veut rajouter la quete
        {
            listCurrentQueteInstance.Add(currentQueteParameter); // on l'ajoute a la liste des quetes selectiones

            if(listCurrentQueteInstance.Count == 2 || SCR_DATA.instanceData.GetJour() <= 2 &&listCurrentQueteInstance.Count == 1 ) // si on a selectionne 2 quetes,     a changer
            {
                buttonConfirmation.SetActive(true);

                for(int i = 0 ; i < listQuetePropose.Count ; i++)
                {
                    if (!listCurrentQueteInstance.Contains(listQuetePropose[i]))
                    {
                        listQuetePropose[i].ShowMask(true); // pour afficher le selected rouge
                    }
                }


            }
        }
        else
        {
            if (listCurrentQueteInstance.Count == 2 || SCR_DATA.instanceData.GetJour() == 1 && listCurrentQueteInstance.Count == 1 || SCR_DATA.instanceData.GetJour() == 2 && listCurrentQueteInstance.Count == 1)
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

   



    public int GetQueteCount() { return listCurrentQueteInstance.Count;}

    
}
