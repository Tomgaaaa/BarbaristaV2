using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Page_Perso : MonoBehaviour
{
    [SerializeField] private SO_Personnage persoPage;
    [SerializeField] private SCR_HexagoneStat hexagone;


    private void OnEnable()
    {
        hexagone.UpdateStat(persoPage.dicoResistance,false,true);
    }
}
