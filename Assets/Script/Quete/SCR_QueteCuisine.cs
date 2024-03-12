using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_QueteCuisine : SCR_QueteTableau
{

    [Header("Special Cuisine")]
    [SerializeField] private Image imageP1;

    public SO_Personnage perso;
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
        infoEventTexte.text = myQueteSo.infoEvenement;


        perso = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[0];
        hexa.UpdateStat(perso.dicoResistance,false,true);
        imageP1.sprite = myQueteSo.persosEnvoyes[0].profil;

        //imageP2.sprite = myQueteSo.persosEnvoyes[1].profil;

      

    }
    public void UpdateStatWhenDrop(Dictionary<enumResistance,float> dicoIngredient)
    {


        Dictionary<enumResistance,float> dicoTemp = new Dictionary<enumResistance, float>();

        foreach (KeyValuePair<enumResistance, float> resistance in dicoIngredient) // c'est juste pour debug
        {
            dicoTemp[resistance.Key] = perso.dicoResistance[resistance.Key] + resistance.Value;
        } 
        hexa.UpdateStat(dicoTemp, false, true);
    }


    public void ChangePerso()
    {
        perso = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[1];
        hexa.UpdateStat(perso.dicoResistance, false, true);
        imageP1.sprite = perso.profil;

    }

}
