using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_MasterQuete : MonoBehaviour
{
    [SerializeField] SO_Quete myQueteSo;

    [SerializeField] Text titre;
    [SerializeField] Text description;
    [SerializeField] Text infoEventTexte;

    [SerializeField] Image illu;

    [SerializeField] Transform reward;
    [SerializeField] Transform diff;
    [SerializeField] Transform P1;
    [SerializeField] Transform P2;

    public Dictionary<int, SCR_Ficheperso1> posQuete = new Dictionary<int, SCR_Ficheperso1>() { { 0,null},{ 1, null} };

    public bool posP1;
    public bool posP2;

    private void Awake()
    {
        if(myQueteSo != null)
        {
            InitialisationQuete();
            
        }
    }


    private void InitialisationQuete()
    {
        titre.text = myQueteSo.titre;
        description.text = myQueteSo.description;
        illu.sprite = myQueteSo.illustration;




        for (int i = 0; i < myQueteSo.difficulty.Count; i++)
        {
            Instantiate<Image>(myQueteSo.difficulty[0], diff);

        }

        for (int i = 0; i < myQueteSo.reward.Count; i++)
        {

            Instantiate<Image>(myQueteSo.reward[i], reward);

        }
    }

    #region pour le cote tableau
    public void OnDrop(SCR_Ficheperso1 fiche)
    {
        if(posQuete[0]==null)
        {
            fiche.MakeSmall(true);
            fiche.transform.position = new Vector3(P1.position.x, P1.position.y,-1);
            posQuete[0] = fiche;
            
        }
        else if(posQuete[1] == null)
        {
            fiche.MakeSmall(true);
            fiche.transform.position = new Vector3(P2.position.x, P2.position.y, -1);
            posQuete[1] = fiche;
        }

        if (posQuete[0]!=null && posQuete[1]!=null)
        {
            SCR_QueteManager.instanceQueteManager.UnlockConfirmButton(true);
        }
       
    }

    public void pickUp(SCR_Ficheperso1 fiche)
    {
        if (posQuete[0] != null && posQuete[1] != null)
        {
            SCR_QueteManager.instanceQueteManager.UnlockConfirmButton(false);
        }


        if (posQuete[0] == fiche)
        {
            fiche.MakeSmall(false);
            posQuete[0] = null;

        }
        else if (posQuete[1] == fiche)
        {
            fiche.MakeSmall(false);
            posQuete[1] = null;
        }
        
        
        
    }
    #endregion

    public void GetCurrentQuete(SO_Quete currentQueteParameter) 
    {
        myQueteSo = currentQueteParameter;
        InitialisationQuete();
    }
}
