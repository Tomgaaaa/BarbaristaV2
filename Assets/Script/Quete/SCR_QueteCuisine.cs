using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_QueteCuisine : SCR_QueteTableau
{

    [Header("Special Cuisine")]
    [SerializeField] private Image imageP1;
    [SerializeField] private Image imageP2;

    SO_Personnage perso;

    [SerializeField] SCR_HexagoneStat hexa;
    public override void InitialisationQuete()
    {
        /*
        foreach(Image ImageDiff in listDifficultyInstance)
        {
            Destroy(ImageDiff.gameObject);
        }
       listDifficultyInstance.Clear();
        */
        //base.InitialisationQuete();
        perso = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[SCR_DATA.instanceData.GetEtapePerso()];

        hexa.UpdateStat(perso.dicoResistance,false,true);



        infoEventTexte.text = myQueteSo.infoEvenement;
        
        imageP1.sprite = myQueteSo.persosEnvoyes[0].profil;

        //imageP2.sprite = myQueteSo.persosEnvoyes[1].profil;

      

    }
    public void UpdateStatWhenDrop(Dictionary<enumResistance,float> dicoIngredient)
    {
        foreach (KeyValuePair<enumResistance, float> resistance in dicoIngredient) // c'est juste pour debug
        {
            perso.dicoResistance[resistance.Key] += resistance.Value;
        }
        hexa.UpdateStat(perso.dicoResistance, false, true);
    }

}
