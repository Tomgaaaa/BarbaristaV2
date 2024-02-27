using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_QueteCuisine : SCR_QueteTableau
{

    [Header("Special Cuisine")]
    [SerializeField] private Image imageP1;
    [SerializeField] private Image imageP2;


    public override void InitialisationQuete()
    {
        base.InitialisationQuete();

        imageP1.sprite = myQueteSo.persosEnvoyes[0].profil;
        imageP2.sprite = myQueteSo.persosEnvoyes[1].profil;
    }


}
