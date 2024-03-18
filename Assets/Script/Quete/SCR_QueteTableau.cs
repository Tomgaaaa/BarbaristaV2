using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SCR_QueteTableau : MonoBehaviour
{
    [SerializeField] private protected SO_Quete myQueteSo;

    [SerializeField] private Text titre;
    [SerializeField] private Text description;
    [SerializeField] protected Text infoEventTexte;

    [SerializeField] private Image illu;

    [SerializeField] private Transform reward;
    private List<Image> listRewardInstance = new List<Image>();

    [SerializeField] private Transform diff;
    private protected List<Image> listDifficultyInstance = new List<Image>();

    [SerializeField] private Transform P1;
    [SerializeField] private Transform P2;

    public Dictionary<int, SCR_Ficheperso1> posQuete = new Dictionary<int, SCR_Ficheperso1>() { { 0,null},{ 1, null} };

    private bool isSelected = false;
    private bool isHigher = false;
    private bool inChoixPerso = false;

    [SerializeField] private GameObject selectedTamp;
    [SerializeField] private GameObject greyMask;

    [SerializeField] private List<GameObject> listHighlight;

    Sequence sequenceScale;
    private Vector3 initiaHighLightlScale;



    private void Awake()
    {
        if(myQueteSo != null)
        {
            InitialisationQuete();
            
        }
        initiaHighLightlScale = listHighlight[0].transform.localScale;
        

    }


    public virtual void InitialisationQuete()
    {
        foreach(GameObject go in listHighlight)
        {
            go.SetActive(false);
        }


        titre.text = myQueteSo.titre;
        description.text = myQueteSo.description;
        illu.sprite = myQueteSo.illustration;
        infoEventTexte.text = myQueteSo.infoEvenement;


        for (int i = 0; i < myQueteSo.difficulty.Count; i++)
        {
            listDifficultyInstance.Add( Instantiate<Image>(myQueteSo.difficulty[0], diff));

        }

        /*for (int i = 0; i < myQueteSo.reward.Count; i++)
        {

            listRewardInstance.Add(Instantiate<Image>(myQueteSo.reward[i], reward));

        }*/
    }



    private void OnMouseDown()
    {
        if (inChoixPerso)
            return;



        if (SCR_DATA.instanceData.GetJour() > 2 && SCR_QueteManager.instanceQueteManager.GetQueteCount() < 2 && !isSelected || SCR_DATA.instanceData.GetJour() <= 2 && SCR_QueteManager.instanceQueteManager.GetQueteCount() < 1)
        {
            isSelected = true;
            selectedTamp.SetActive(true);
            SCR_QueteManager.instanceQueteManager.AddCurrentQuete(this);
            
        }
        else if (isSelected)
        {

            isSelected = false;
            selectedTamp.SetActive(false);
            SCR_QueteManager.instanceQueteManager.AddCurrentQuete(this, true);
        }
        AudioManager.instanceAM.Play("Selection");

    }

    public void ShowMask(bool showParameter)
    {
        if(showParameter)
        {
            greyMask.SetActive(true);
        }
        else
        {
            greyMask.SetActive(false);

        }
    }


    private void testPourNoah()
    {
        AudioManager.instanceAM.Play("PulseQuest");

    }


    public void ShowHighLight(bool needToShow)
    {
   

        if (needToShow)
        {

            sequenceScale = DOTween.Sequence();

            sequenceScale.Append(listHighlight[0].transform.DOScale(new Vector3(initiaHighLightlScale.x * 1.1f, initiaHighLightlScale.y * 1.1f, initiaHighLightlScale.z * 1.1f),1f));
            sequenceScale.Join(listHighlight[1].transform.DOScale(new Vector3(initiaHighLightlScale.x * 1.1f, initiaHighLightlScale.y * 1.1f, initiaHighLightlScale.z * 1.1f),1f));
            sequenceScale.Join(listHighlight[0].GetComponent<SpriteRenderer>().DOFade(1, 1f));
            sequenceScale.Join(listHighlight[1].GetComponent<SpriteRenderer>().DOFade(1, 1f).OnComplete(testPourNoah));


            sequenceScale.Append(listHighlight[0].transform.DOScale(new Vector3(initiaHighLightlScale.x / 1.1f, initiaHighLightlScale.y / 1.1f, initiaHighLightlScale.z / 1.1f),1f));
            sequenceScale.Join(listHighlight[1].transform.DOScale(new Vector3(initiaHighLightlScale.x / 1.1f, initiaHighLightlScale.y / 1.1f, initiaHighLightlScale.z / 1.1f),1f));
            sequenceScale.Join(listHighlight[0].GetComponent<SpriteRenderer>().DOFade(0, 1f));
            sequenceScale.Join(listHighlight[1].GetComponent<SpriteRenderer>().DOFade(0, 1f));
            sequenceScale.SetLoops(-1);

            if (posQuete[0]== null)
                listHighlight[1].SetActive(true);

            if (posQuete[1] == null)
                listHighlight[0].SetActive(true);
        }
        else
        {
            foreach(GameObject go in listHighlight)
            {
                go.SetActive(false);
            }
        }


    }

   

    #region pour le cote tableau
    public void OnDrop(SCR_Ficheperso1 fiche)
    {
        if (!isHigher)
            return;

        if (posQuete[0] == null)
        {
            fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            fiche.transform.SetParent(transform);
            fiche.MakeSmall(true);
            fiche.transform.position = new Vector3(P1.position.x, P1.position.y, 0);
            posQuete[0] = fiche;

            myQueteSo.persosEnvoyes.Add(fiche.GetSoPerso());

            SCR_QueteManager.instanceQueteManager.AddRemovePersosUtilise(fiche, true);


        }
        else if (posQuete[1] == null)
        {
            fiche.GetComponent<SortingGroup>().sortingOrder = 6;
            fiche.transform.SetParent(transform);
            fiche.MakeSmall(true);
            fiche.transform.position = new Vector3(P2.position.x, P2.position.y, -1);
            posQuete[1] = fiche;

            myQueteSo.persosEnvoyes.Add(fiche.GetSoPerso());


            SCR_QueteManager.instanceQueteManager.AddRemovePersosUtilise(fiche, true);

        }

         
       
    }

    public void pickUp(SCR_Ficheperso1 fiche)
    {
       

        SCR_QueteManager.instanceQueteManager.AddRemovePersosUtilise(fiche,false);

        if (posQuete[0] == fiche)
        {
            fiche.GetComponent<SortingGroup>().sortingOrder = 11;

            fiche.transform.SetParent(null);

            fiche.MakeSmall(false);
            posQuete[0] = null;

            myQueteSo.persosEnvoyes.Remove(fiche.GetSoPerso());


        }
        else if (posQuete[1] == fiche)
        {
            fiche.GetComponent<SortingGroup>().sortingOrder = 11;

            fiche.transform.SetParent(null);

            fiche.MakeSmall(false);
            posQuete[1] = null;

            myQueteSo.persosEnvoyes.Remove(fiche.GetSoPerso());

        }



    }
    #endregion


    public void ResetPerso()
    {

        foreach(KeyValuePair<int,SCR_Ficheperso1> pair in posQuete)
        {
            if (pair.Value != null)
            {
                pair.Value.transform.DORotate(Vector3.zero, 1);
                pair.Value.GetComponent<SortingGroup>().sortingOrder = 11;
                pair.Value.transform.SetParent(null);
                pair.Value.MakeSmall(false);
                pair.Value.SetCanMove(true);

            }


        }

        posQuete[0] = null;
        posQuete[1] = null;

        myQueteSo.persosEnvoyes.Clear();

    }

    public SO_Quete GetQuete() { return myQueteSo; }
    public void SetCurrentQuete(SO_Quete currentQueteParameter) 
    {
        myQueteSo = currentQueteParameter;
        InitialisationQuete();
        
    }
    public void SetInChoixPerso(bool inChoixPersoParameter) { inChoixPerso = inChoixPersoParameter; }

    public void SetHigher(bool isHigerParameter) => isHigher = isHigerParameter;
    public bool GetHigher()
    {
        return isHigher;
    }
}
