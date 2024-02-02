using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_MasterQuete : MonoBehaviour
{
    [SerializeField] SO_Quete queteInfo;

    [SerializeField] Text titre;
    [SerializeField] Text description;

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
        
        titre.text = queteInfo.titre;
        description.text = queteInfo.description;
        illu.sprite = queteInfo.illustration;

        foreach(Image etoile in queteInfo.difficulty)
        {
            Instantiate<Image>(queteInfo.difficulty[0], diff);
        }

        for (int i = 0; i < queteInfo.reward.Count;i++)
        {
            
            Instantiate<Image>(queteInfo.reward[i], reward);
            
        }
    }

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

    }

    public void pickUp(SCR_Ficheperso1 fiche)
    {
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
}
