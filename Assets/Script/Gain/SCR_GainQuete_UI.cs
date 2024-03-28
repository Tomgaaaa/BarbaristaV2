using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SCR_GainIngredient;

public class SCR_GainQuete_UI : MonoBehaviour, ISerializationCallbackReceiver
{

    #region Dico

    private Dictionary<enumAllIgredient, Sprite> dicoIngredientSprite;
    [SerializeField] private List<dicoIngredientSpriteClass> listDicoIngredientSprite;
    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoIngredientSprite = new Dictionary<enumAllIgredient, Sprite>();

        foreach (dicoIngredientSpriteClass item in listDicoIngredientSprite)
        {
            if (!dicoIngredientSprite.ContainsKey(item.key))
            {
                dicoIngredientSprite.Add(item.key, item.value);
            }
        }
    }

    #endregion


    [SerializeField] private Transform curseurAmitie;
    private Vector3 initialPositionCurseur;
    [SerializeField] private List<Transform> listAmitieAB;

    [SerializeField] private SpriteRenderer spritePerso1;
    [SerializeField] private SpriteRenderer spritePerso2;

    [SerializeField] private TextMeshProUGUI textPerso1;
    [SerializeField] private TextMeshProUGUI textPerso2;

    [SerializeField] private TextMeshProUGUI textNmbQuete;

    [SerializeField] private SCR_HexagoneStat hexagone1;
    [SerializeField] private SCR_HexagoneStat hexagone2;

    [SerializeField] private List<GameObject> listButton;
    [SerializeField] private GameObject CanvasResumeQuete;
    [SerializeField] private Image prefabImageRien;


    [SerializeField] private Transform gridLayout;   

    private SCR_GainQuete gainQuete;
    [SerializeField] private SCR_GainIngredient gainIngredient;

    [SerializeField] private GameObject stampWin;
    [SerializeField] private GameObject stampLose;
    private Vector3 initialScaleStamp;


    private Tweener tweenerAmitie;

    private void Start()
    {

        initialScaleStamp = stampWin.transform.localScale;

        Loadpage(SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[1],true);
        UpdateAmitie(SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[1], false);
        UpdateReward(SCR_DATA.instanceData.GetListCurrentQuest()[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].hasWinMission);



        if (SCR_DATA.instanceData.GetJour() < 3) 
        {
            textNmbQuete.text = "1/1";
            listButton[0].SetActive(false);
            listButton[2].SetActive(true);
        }
    }


    public void Loadpage(SO_Personnage Perso1Parameter, SO_Personnage Perso2Parameter,bool loadFirstQuest)
    {
        initialPositionCurseur = curseurAmitie.position;


        spritePerso1.sprite = Perso1Parameter.profil;
        spritePerso2.sprite = Perso2Parameter.profil;

        textPerso1.text = Perso1Parameter.namePerso;
        textPerso2.text = Perso2Parameter.namePerso;

        hexagone1.UpdateStat(Perso1Parameter.dicoResistance, false);
        hexagone2.UpdateStat(Perso2Parameter.dicoResistance, false);


        stampWin.SetActive(false); // je les desactive pour pouvoir les reactiver apres
        stampLose.SetActive(false);

        if (loadFirstQuest) // pour savoir si la page chargé montre la premiere quete ou la deuxieme
        {
            if (SCR_DATA.instanceData.GetListCurrentQuest()[0].hasWinMission) // si la mission 1 est reussite
            {
                stampWin.SetActive(true);
                stampWin.transform.localScale = initialScaleStamp * 1.2f;
                stampWin.transform.DOScale(initialScaleStamp, 0.5f);


            }
            else // si la mission 1 est perdue
            {
                stampLose.SetActive(true);
                stampLose.transform.localScale = initialScaleStamp * 1.2f;
                stampLose.transform.DOScale(initialScaleStamp, 0.5f);

            }
            

        }
        else
        {
            if (SCR_DATA.instanceData.GetListCurrentQuest()[1].hasWinMission) // si la mission 2 est reussite
            {
                stampWin.SetActive(true);
                stampWin.transform.localScale = initialScaleStamp * 1.2f;
                stampWin.transform.DOScale(initialScaleStamp, 0.5f);


            }
            else // si la mission 2 est perdue
            {
                stampLose.SetActive(true);
                stampLose.transform.localScale = initialScaleStamp * 1.2f;
                stampLose.transform.DOScale(initialScaleStamp, 0.5f);

            }
        }




    }

    public void UpdateAmitie(SO_Personnage Perso1Parameter, SO_Personnage Perso2Parameter, bool instantDeplacement)
    {
        float distanceMinMax = ((listAmitieAB[1].position.x - listAmitieAB[0].position.x) / 6) * (3 + Perso1Parameter.dicoRelationPerso[Perso2Parameter.myEnumPerso]);
        curseurAmitie.position = initialPositionCurseur;

        if(instantDeplacement)
        {
            curseurAmitie.position = new Vector3 (listAmitieAB[0].position.x + distanceMinMax,curseurAmitie.position.y,curseurAmitie.position.z);
        }
        else
        {
            tweenerAmitie = curseurAmitie.DOMoveX(listAmitieAB[0].position.x + distanceMinMax,2f);
        }
    }


    public void UpdateReward(SO_Quete queteUtiliseParameter, bool winMission)
    {
        foreach(Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }



        if (winMission)
        {
            for (int i = 0; i < queteUtiliseParameter.reward.Count; i++)
            {
                Image imag = Instantiate<Image>(prefabImageRien, gridLayout);
                imag.sprite = dicoIngredientSprite[queteUtiliseParameter.reward[i].myEnumIngredientSO];
            }

        }
        else
        {
            Instantiate<Image>(prefabImageRien, gridLayout);
        }

    }

    public void ChangeQuete()
    {
        tweenerAmitie.Kill();
            

        listButton[0].SetActive(false);
        listButton[1].SetActive(true);
        listButton[2].SetActive(true);

        textNmbQuete.text = "2/2";
        //gainQuete.CalculQuete2();
        Loadpage(SCR_DATA.instanceData.GetListCurrentQuest()[1].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[1].persosEnvoyes[1],false);
        UpdateAmitie(SCR_DATA.instanceData.GetListCurrentQuest()[1].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[1].persosEnvoyes[1],false);
        UpdateReward(SCR_DATA.instanceData.GetListCurrentQuest()[1],SCR_DATA.instanceData.GetListCurrentQuest()[1].hasWinMission);
        //gainQuete.CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[1]);
    }

    public void RetourQuetePrecedente()
    {
        tweenerAmitie.Kill();


        listButton[0].SetActive(true);
        listButton[1].SetActive(false);
        listButton[2].SetActive(false);
        textNmbQuete.text = "1/2";


        Loadpage(SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[1],true);
        UpdateAmitie(SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].persosEnvoyes[1], false);
        UpdateReward(SCR_DATA.instanceData.GetListCurrentQuest()[0], SCR_DATA.instanceData.GetListCurrentQuest()[0].hasWinMission);
        //gainQuete.CalculQuete();
        //gainQuete.CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[0]);
    }

    public void PassResumeQuete()
    {
        CanvasResumeQuete.SetActive(false);
        gainIngredient.gameObject.SetActive(true);
        gainIngredient.SpawnIngredient();

        stampLose.gameObject.SetActive(false);
        stampWin.gameObject.SetActive(false);

        List<SCR_SO_Ingredient> listIngredientGagne = new List<SCR_SO_Ingredient>();



        foreach (SO_Quete queteFaite in SCR_DATA.instanceData.GetListCurrentQuest())
        {

            if (queteFaite.hasWinMission)
            {
                foreach (SCR_SO_Ingredient ingredient in SCR_DATA.instanceData.GetListCurrentQuest()[0].reward)
                {
                    listIngredientGagne.Add(ingredient);
                }
            }

        }


        SCR_DATA.instanceData.SetListIngredientGagne(listIngredientGagne);

    }
    public void GoQuete()
    {
        List<SCR_SO_Ingredient> dailyIngredient = gainIngredient.GetIngredientGain();

        for(int i = 0; i < dailyIngredient.Count; i++)
        {
            SCR_DATA.instanceData.GetListIngredientGagne().Add(dailyIngredient[i]);

        }

        
        SCR_DATA.instanceData.ClearDay();


        SCR_DATA.instanceData.EtapeQueteUp();
        SCR_DATA.instanceData.EtapePersoUp();
        SCR_DATA.instanceData.JourUP();
        SceneManager.LoadScene("SCE_Quete");
    }

    public void SetGainQuete(SCR_GainQuete gainQueteParameter ) => gainQuete = gainQueteParameter;

}
