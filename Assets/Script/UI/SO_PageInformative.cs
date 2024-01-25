using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_PageInformative : ScriptableObject
{
    public enum enumTypePage
    {
        none, pageSimple
    };

    public string titre;
    public  Sprite illustration;
    public SO_PageInformative pagePrecedante;
    public SO_PageInformative pageSuivante;
    public Color colorContour;

    


    
}
