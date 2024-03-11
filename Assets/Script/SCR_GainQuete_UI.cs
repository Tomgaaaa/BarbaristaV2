using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCR_GainQuete_UI : MonoBehaviour
{
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


    private Tweener tweenerAmitie;

    public void Loadpage(SO_Personnage Perso1Parameter, SO_Personnage Perso2Parameter)
    {
        initialPositionCurseur = curseurAmitie.position;


        spritePerso1.sprite = Perso1Parameter.profil;
        spritePerso2.sprite = Perso2Parameter.profil;

        textPerso1.text = Perso1Parameter.namePerso;
        textPerso2.text = Perso2Parameter.namePerso;

        hexagone1.UpdateStat(Perso1Parameter.dicoResistance, false);
        hexagone2.UpdateStat(Perso2Parameter.dicoResistance, false);


    }

    public void UpdateAmitie(SO_Personnage Perso1Parameter, SO_Personnage Perso2Parameter, bool instantDeplacement)
    {
        float distanceMinMax = (listAmitieAB[1].position.x - listAmitieAB[0].position.x) / 6;
        curseurAmitie.position = initialPositionCurseur;

        if(instantDeplacement)
        {
            curseurAmitie.position = new Vector3 (curseurAmitie.position.x + (+Perso1Parameter.dicoRelationPerso[Perso2Parameter.myEnumPerso] * distanceMinMax * distanceMinMax),curseurAmitie.position.y,curseurAmitie.position.z);
        }
        else
        {
            tweenerAmitie = curseurAmitie.DOMoveX(curseurAmitie.position.x + (+Perso1Parameter.dicoRelationPerso[Perso2Parameter.myEnumPerso] * distanceMinMax * distanceMinMax),2f);
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
                Instantiate<Image>(queteUtiliseParameter.reward[i], gridLayout);
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
        gainQuete.CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[1]);
    }

    public void RetourQuetePrecedente()
    {
        tweenerAmitie.Kill();


        listButton[0].SetActive(true);
        listButton[1].SetActive(false);
        listButton[2].SetActive(false);
        textNmbQuete.text = "1/2";
        //gainQuete.CalculQuete();
        gainQuete.CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[0]);
    }

    public void PassResumeQuete()
    {
        CanvasResumeQuete.SetActive(false);
    }
    public void GoQuete()
    {
        SceneManager.LoadScene("SCE_Quete");
    }

    public void SetGainQuete(SCR_GainQuete gainQueteParameter ) => gainQuete = gainQueteParameter;

}
