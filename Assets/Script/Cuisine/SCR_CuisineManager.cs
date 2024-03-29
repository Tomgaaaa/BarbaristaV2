using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SCR_CuisineManager : MonoBehaviour
{

    public static SCR_CuisineManager instanceCM;



    #region bouilloire
    [Header("Transition Boulloire")]
    [SerializeField] private Transform bambooShade; // transform du volet pour le deplacer
    [SerializeField] private Vector3 emplacementbambooShade; // l'emplacement que doit prendre le volet 
    private Vector3 startPositionBambooShade; // position initial du volet
    [SerializeField] private Transform allUstensile; // Reference des ustensiles pour les faire sortir de l'�cran
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


    private Camera mainCam;
    private Vector3 startPoseCam;
    [SerializeField] private Vector3 emplacementCam;

    [SerializeField] private GameObject canvasUI;

    [SerializeField] private List<SCR_Ustensile> listUstensile;
    private List<Vector3> listTransformUstensile = new List<Vector3>();

    [SerializeField] private TextMeshProUGUI compteurBoisson;
    [SerializeField] private GameObject nextBoissonButton;
    private Text textNextBoisson;
    private CanvasGroup canvasGroupNextBoisson;


    [SerializeField] SCR_PopUp popUpIngredient;


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

        if (SCR_TutoManager.instanceTuto != null)
            SCR_TutoManager.instanceTuto.gameObject.SetActive(true);

        nextBoissonButton.SetActive(false);
        textNextBoisson = nextBoissonButton.GetComponentInChildren<Text>();
        canvasGroupNextBoisson = nextBoissonButton.GetComponentInChildren<CanvasGroup>();


    }


    public void TransitionBouilloire() 
    {
        AudioManager.instanceAM.Play("Transibouilloire");
        bambooShade.DOLocalMove(emplacementbambooShade, 1f); // d�place le volet jusqu'a son emplacement

        allUstensile.DOLocalMove(new Vector3(-10, 0, 0), 1f);

        boulloire.transform.DOLocalMove(emplacementBoulloire.position, 1f);

        hexagone.transform.DOLocalMove(emplacementHexagone.position, 1f);


        mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, mainCam.transform.position.z), 1);
        mainCam.DOOrthoSize(emplacementCam.z, 1);

        ZoomUstensile(true, boulloire);

    }


    public void UnLockIngredient() 
    {
        bambooShade.DOLocalMove(startPositionBambooShade, 1f);

        allUstensile.DOLocalMove(startPositionAllUstensile, 1f);

        boulloire.transform.DOLocalMove(new Vector3 (-20,0,0), 1f);

        boulloire.SetEauVerser();
        

        hexagone.gameObject.SetActive(false);
        hexagone.transform.DOLocalMove(startPositionHexagone, 1f);


        mainCam.transform.DOMove(new Vector3(startPoseCam.x, startPoseCam.y, mainCam.transform.position.z), 1);
        mainCam.DOOrthoSize(5.5f, 1);

    }



    public void FinisshBouilloire()
    {
        nextBoissonButton.gameObject.SetActive(true);
        textNextBoisson.text = "Boisson de " + SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[SCR_DATA.instanceData.GetEtapePerso()].namePerso + " pr�par�e";
        canvasGroupNextBoisson.DOFade(1, 1);
        AudioManager.instanceAM.Play("CompletionBoissonServi");
    }

    
    public void NextBoisson()// fonction appeller par le bouton qui s'affiche quand on a finit de preparer une boisson
    {

        canvasGroupNextBoisson.DOFade(0, 0);
        nextBoissonButton.SetActive(false);
        ZoomUstensile(false, boulloire);



        AudioManager.instanceAM.Play("TransiDeuxiemeBoisson");



        SCR_DATA.instanceData.GetCurrentQuest().boissonsServis.Insert(0,refTasse.GetBoissonSo()) ; // ajoute la boisson preparer a la list des boissons servis de la quete




        if (SCR_DATA.instanceData.GetEtapePerso() == 0) // si on vient de servir le premier perso
        {
            SCR_DATA.instanceData.EtapePersoUp(); // alors on passe au persos d'apres
            queteCuisine.ChangePerso();

            compteurBoisson.text = "1/2";


        }
        else if(SCR_DATA.instanceData.GetEtapePerso() == 1) // si on vient de servir le deuxieme perso
        {
            // salors on passe a la partie VN
            AudioManager.instanceAM.Play("PreparationFinish");
            //AudioManager.instanceAM.FadeOut("CuisineAlatea", 0, 4.5f);
            AudioManager.instanceAM.Pause("CuisineAlatea");
            AudioManager.instanceAM.Play("BarAlatea");

            if (SCR_TutoManager.instanceTuto != null)
                SCR_TutoManager.instanceTuto.gameObject.SetActive(false);

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

    
    public void ZoomUstensile(bool isZooming, SCR_Ustensile ustensileUsed)
    {
        if(isZooming)
        {
            /*listTransformUstensile.Clear();

            int index = listUstensile.IndexOf(ustensileUsed);

            for(int i = 0; i < listUstensile.Count; i++)
            {
                if(i!= index)
                {
                    listTransformUstensile.Add(listUstensile[i].transform.localPosition);
                    listUstensile[i].transform.DOLocalMove(new Vector3(listUstensile[i].transform.position.x - 30f, listUstensile[i].transform.position.y, listUstensile[i].transform.position.z), 1f);
                }

            }*/


            SCR_Cursor.instanceCursor.ZoomCamera();
            canvasUI.SetActive(false);
        }
        else
        {

           /* int index = listUstensile.IndexOf(ustensileUsed);

            for (int i = 0; i < listUstensile.Count; i++)
            {
                if (i != index)
                    listUstensile[i].transform.DOLocalMove(new Vector3(listTransformUstensile[i].x, listTransformUstensile[i].y, listTransformUstensile[i].z), 1f);



            }*/


            SCR_Cursor.instanceCursor.DeZoomCamera();
            canvasUI.SetActive(true);

        }
    }





    public void PopUp(bool show, string ingredientName)
    {
        if (show)
        {
            popUpIngredient.Activate(ingredientName, new Vector3(30, -40,0));
        }
        else
        {
            popUpIngredient.Desactivate();

        }
    }

}
