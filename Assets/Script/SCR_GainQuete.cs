using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GainQuete : MonoBehaviour
{

    public float moyennePerso1;
    public float moyennePerso2;


    public SO_Personnage personnage1;
    public SO_Personnage personnage2;
    public List<SCR_Ingredient> ingredientsUtilises;
    public SO_Quete queteUtilise;
    SO_Boisson boisson1R;
    SO_Boisson boisson2R;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


#if UNITY_EDITOR
    [ContextMenu("Calcule Quete")]
    private void CalculQuete()
    {
        boisson1R = ScriptableObject.CreateInstance<SO_Boisson>();
        boisson2R = ScriptableObject.CreateInstance<SO_Boisson>();

       Dictionary<enumResistance, float> statBoisson1 =  new Dictionary<enumResistance, float> {
            { enumResistance.Thermique, 100 } ,
            { enumResistance.Hemorragique, 100 } ,
            { enumResistance.Toxique, 100 } ,
            { enumResistance.Cryogenique, 100 } ,
            { enumResistance.Electrique, 100 } ,
            { enumResistance.Lethargique, 100 }
        };

        Dictionary<enumResistance, float> statBoisson2 = new Dictionary<enumResistance, float> {
            { enumResistance.Thermique, 100 } ,
            { enumResistance.Hemorragique, 100 } ,
            { enumResistance.Toxique, 100 } ,
            { enumResistance.Cryogenique, 100 } ,
            { enumResistance.Electrique, 100 } ,
            { enumResistance.Lethargique, 100 }
        };

        boisson1R.CreateBoisson(ingredientsUtilises, statBoisson1);
        boisson2R.CreateBoisson(ingredientsUtilises, statBoisson2);

        CalculeChanceQuete(queteUtilise,true);
    }

#endif


    public void CalculeChanceQuete(SO_Quete queteEffectueParameter, bool fromUi = false)
    {
        SO_Personnage perso1;
        SO_Personnage perso2;

        SO_Boisson boisson1 = boisson1R;
        SO_Boisson boisson2 = boisson2R;

        
    
            perso1 = personnage1;
            perso2 = personnage2;
        
       
        

     

        Dictionary<enumResistance,float> statCalcule = new Dictionary<enumResistance, float>();
            
        foreach(KeyValuePair<enumResistance, float> enumR in perso1.dicoResistance)
        {
            statCalcule[enumR.Key] = (perso1.dicoResistance[enumR.Key] + boisson1.dicoResistanceBoisson[enumR.Key])* 100/ queteUtilise.dicoResistanceDifficulte[enumR.Key];
            moyennePerso1 += statCalcule[enumR.Key];
        }
        moyennePerso1 = moyennePerso1 / 6;
    }

}
