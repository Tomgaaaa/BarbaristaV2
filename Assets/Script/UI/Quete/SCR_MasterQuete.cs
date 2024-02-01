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
        fiche.transform.position = P1.position;
    }
}
