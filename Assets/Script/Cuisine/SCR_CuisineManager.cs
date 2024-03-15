using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_CuisineManager : MonoBehaviour
{

    public static SCR_CuisineManager instanceCM;



    #region bouilloire
    [Header("Transition Boulloire")]
    [SerializeField] private Transform bambooShade; // transform du volet pour le deplacer
    [SerializeField] private Vector3 emplacementbambooShade; // l'emplacement que doit prendre le volet 
    private Vector3 startPositionBambooShade; // position initial du volet
    [SerializeField] private Transform allUstensile; // Reference des ustensiles pour les faire sortir de l'écran
    private Vector3 startPositionAllUstensile; //  position initial des ustensiles
    [SerializeField] private SCR_Bouilloire boulloire;
    [SerializeField] private Transform emplacementBoulloire;

    [SerializeField] private SCR_HexagoneStat hexagone;
    [SerializeField] private Transform emplacementHexagone;
    private Vector3 startPositionHexagone; //  position initial des ustensiles




    #endregion


    [Header("Autre")]
    [SerializeField] private SCR_Tasse refTasse;
    [SerializeField] private SCR_HexagoneStat refHexagone;

    [SerializeField] private SCR_QueteCuisine queteCuisine; // quete a gauche de l'ecran

    [SerializeField] private GameObject buttonValideBoisson;

    private Camera mainCam;
    private Vector3 startPoseCam;
    [SerializeField] private Vector3 emplacementCam;

    [SerializeField] private GameObject canvasUI;

    private void Awake()
    {
        if (instanceCM == null)
            instanceCM = this;
        else
            Destroy(gameObject);

    }



    // Start is called before the first frame update
    void Start()
    {
        startPositionBambooShade = bambooShade.position; // stock la position initial du volet
        startPositionHexagone = hexagone.transform.position; // stock la position initial du volet
        startPositionAllUstensile = allUstensile.position;


        queteCuisine.SetCurrentQuete(SCR_DATA.instanceData.GetCurrentQuest()); // update la quete a afficher avec celle qui est en cours


        mainCam = Camera.main;
        startPoseCam = new Vector3( mainCam.transform.position.x, mainCam.transform.position.y, 5.5f);

    }


    public void TransitionBouilloire() 
    {
        AudioManager.instanceAM.Play("Transibouilloire");
        bambooShade.DOLocalMove(emplacementbambooShade, 1f); // déplace le volet jusqu'a son emplacement

        allUstensile.DOLocalMove(new Vector3(-10, 0, 0), 1f);

        boulloire.transform.DOLocalMove(emplacementBoulloire.position, 1f);

        hexagone.transform.DOLocalMove(emplacementHexagone.position, 1f);


        mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, mainCam.transform.position.z), 1);
        mainCam.DOOrthoSize(emplacementCam.z, 1);

    }


    public void UnLockIngredient() 
    {
        bambooShade.DOLocalMove(startPositionBambooShade, 1f);

        allUstensile.DOLocalMove(startPositionAllUstensile, 1f);

        boulloire.transform.DOLocalMove(new Vector3 (-20,0,0), 1f);

        hexagone.gameObject.SetActive(false);
        hexagone.transform.DOLocalMove(startPositionHexagone, 1f);


        mainCam.transform.DOMove(new Vector3(startPoseCam.x, startPoseCam.y, mainCam.transform.position.z), 1);
        mainCam.DOOrthoSize(5.5f, 1);

    }

    public void hasFinishPreparation() // fonction appeller quand on a finit de mettre de l'eau dans la tasse
    {
        buttonValideBoisson.gameObject.SetActive(true); // bouton qui permet de passer a la prochaine boisson / quete
    }



    
    public void NextBoisson()// fonction appeller par le bouton qui s'affiche quand on a finit de preparer une boisson
    {
        buttonValideBoisson.gameObject.SetActive(false);

        AudioManager.instanceAM.Play("TransiDeuxiemeBoisson");



        SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Insert(0,refTasse.GetBoissonSo()) ; // ajoute la boisson preparer a la list des boissons servis de la quete




        if (SCR_DATA.instanceData.GetEtapePerso() == 0) // si on vient de servir le premier perso
        {
            SCR_DATA.instanceData.EtapePersoUp(); // alors on passe au persos d'apres
            queteCuisine.ChangePerso();

        }
        else if(SCR_DATA.instanceData.GetEtapePerso() == 1) // si on vient de servir le deuxieme perso
        {
            // salors on passe a la partie VN
            AudioManager.instanceAM.Play("PreparationFinish");
            //AudioManager.instanceAM.FadeOut("CuisineAlatea", 0, 4.5f);
            AudioManager.instanceAM.Pause("CuisineAlatea");
            AudioManager.instanceAM.Play("BarAlatea");
            SceneManager.LoadScene("SCE_VisualNovel");

        }

        UnLockIngredient();
        ResetBoisson();
        //eteCuisine.SetCurrentQuete(SCR_DATA.instanceData.GetCurrentQuest()); // met a jour la quete sur le tableau de la cuisine
    }

    public void ResetBoisson()
    {


        refTasse.ResetBoisson();


        Dictionary<enumResistance, float> dicoStatBoisson = new Dictionary<enumResistance, float>()
        {
            { enumResistance.Cryogenique, 0 },
            { enumResistance.Thermique, 0 },
            { enumResistance.Electrique, 0 },
            { enumResistance.Toxique, 0 },
            { enumResistance.Hemorragique, 0 },
            { enumResistance.Lethargique, 0 },

        };
        refHexagone.UpdateStat(dicoStatBoisson,true);
       

    }

    
    public void ZoomUstensile(bool isZooming)
    {
        if(isZooming)
        {
            canvasUI.SetActive(false);
        }
        else
        {
            canvasUI.SetActive(true);

        }
    }

}
