using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_QueteCuisine : SCR_QueteTableau
{

    [Header("Special Cuisine")]
    [SerializeField] private Image imageP1;

    private SO_Personnage perso;
    [SerializeField] SCR_HexagoneStat hexa;
    Dictionary<enumResistance, float> dicoCurrentResistance = new Dictionary<enumResistance, float>
        {
            { enumResistance.Electrique , 0f},
            { enumResistance.Hemorragique , 0f},
            { enumResistance.Toxique , 0f},
            { enumResistance.Lethargique , 0f},
            { enumResistance.Cryogenique , 0f},
            { enumResistance.Thermique , 0f}
        };



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



        dicoCurrentResistance[enumResistance.Cryogenique] = perso.dicoResistance[enumResistance.Cryogenique];
        dicoCurrentResistance[enumResistance.Thermique] = perso.dicoResistance[enumResistance.Thermique];
        dicoCurrentResistance[enumResistance.Electrique] = perso.dicoResistance[enumResistance.Electrique];
        dicoCurrentResistance[enumResistance.Toxique] = perso.dicoResistance[enumResistance.Toxique];
        dicoCurrentResistance[enumResistance.Lethargique] = perso.dicoResistance[enumResistance.Lethargique];
        dicoCurrentResistance[enumResistance.Hemorragique] = perso.dicoResistance[enumResistance.Hemorragique];
        //imageP2.sprite = myQueteSo.persosEnvoyes[1].profil;

      

    }
    public void UpdateStatWhenDrop(Dictionary<enumResistance,float> dicoIngredient)
    {

     






        foreach (KeyValuePair<enumResistance, float> resistance in dicoIngredient) // c'est juste pour debug
        {
            //dicoTemp[resistance.Key] += perso.dicoResistance[resistance.Key] + resistance.Value;
            dicoCurrentResistance[resistance.Key] += resistance.Value;
        } 
        hexa.UpdateStat(dicoCurrentResistance, false, true);
    }


    public void ChangePerso()
    {
        perso = SCR_DATA.instanceData.GetCurrentQuest().persosEnvoyes[1];
        hexa.UpdateStat(perso.dicoResistance, false, true);
        imageP1.sprite = perso.profil;


        dicoCurrentResistance[enumResistance.Cryogenique] = perso.dicoResistance[enumResistance.Cryogenique];
        dicoCurrentResistance[enumResistance.Thermique] = perso.dicoResistance[enumResistance.Thermique];
        dicoCurrentResistance[enumResistance.Electrique] = perso.dicoResistance[enumResistance.Electrique];
        dicoCurrentResistance[enumResistance.Toxique] = perso.dicoResistance[enumResistance.Toxique];
        dicoCurrentResistance[enumResistance.Lethargique] = perso.dicoResistance[enumResistance.Lethargique];
        dicoCurrentResistance[enumResistance.Hemorragique] = perso.dicoResistance[enumResistance.Hemorragique];



    }

}
