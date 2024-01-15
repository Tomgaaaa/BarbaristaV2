using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_UST_Rape : SCR_Ustensile // script specifique a la rape, hérite d'ustensile pour avoir acces aux fonctions OnDrop()... 
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        etatApresTransformation = enumEtatIgredient.Rape; // vus que c'est le script Rape, l'etat de transformation sera rapé 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
