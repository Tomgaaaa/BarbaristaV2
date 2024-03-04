using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GainQuete : MonoBehaviour
{

    public float moyennePerso1;
    public float moyennePerso2;


    //public SO_Personnage personnage1;
    //public SO_Personnage personnage2;
    //public List<SCR_Ingredient> ingredientsUtilises;
    //public SO_Quete queteUtilise;
    //SO_Boisson boisson1R;
    //SO_Boisson boisson2R;

    private Dictionary<enumResistance,float> dicoRes = new Dictionary<enumResistance, float>() // juste pour parcourir les enum
    {
        {enumResistance.Electrique, 0 },
        {enumResistance.Cryogenique, 0 },
        {enumResistance.Hemorragique, 0 },
        {enumResistance.Toxique, 0 },
        {enumResistance.Thermique, 0 },
        {enumResistance.Lethargique, 0 },
    };

    // Start is called before the first frame update
    void Start()
    {
        CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[0], false);
        CalculeChanceQuete(SCR_DATA.instanceData.GetListCurrentQuest()[1], false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


#if UNITY_EDITOR
    [ContextMenu("Calcule Quete")]
   /* public void CalculQuete()
    {
        boisson1R = ScriptableObject.CreateInstance<SO_Boisson>();
        boisson2R = ScriptableObject.CreateInstance<SO_Boisson>();

       Dictionary<enumResistance, float> statBoisson1 =  new Dictionary<enumResistance, float> {
            { enumResistance.Thermique, 100 } ,
            { enumResistance.Hemorragique, 100 } ,
            { enumResistance.Toxique, 100 } ,
            { enumResistance.Cryogenique, 50 } ,
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
    }*/

#endif


    public void CalculeChanceQuete(SO_Quete queteEffectueParameter, bool fromUi = false)
    {

        float ReussiteMission = 0; // % de chance de reussite de la mission


        SO_Personnage perso1;
        perso1 = queteEffectueParameter.persosEnvoyes[0];
        SO_Personnage perso2;
        perso2 = queteEffectueParameter.persosEnvoyes[1];

        SO_Boisson boisson1 = queteEffectueParameter.boissonsServis[0];
        SO_Boisson boisson2 = queteEffectueParameter.boissonsServis[1];

        
    
            /*perso1 = personnage1;  // a gerter
            perso2 = personnage2;*/




        int nombreStatUtilise = 0; // le nombre de stat qui est utilise par la mission, utile (diviseur) de la moyenne des stats

        float moyenneAllStatPerso1 = 0; // moyenne de toute les stats qui sont necessaire à la mission, pour le perso 1
        Dictionary<enumResistance,float> statCalculePerso1 = new Dictionary<enumResistance, float>(); // dictionnaire de % de chance de reussite par resistance pour le perso 1


        float moyenneAllStatPerso2 = 0; // moyenne de toute les stats qui sont necessaire à la mission, pour le perso 2
        Dictionary<enumResistance,float> statCalculePerso2 = new Dictionary<enumResistance, float>(); // dictionnaire de % de chance de reussite par resistance pour le perso 2



        foreach (KeyValuePair<enumResistance, float> enumR in perso1.dicoResistance)// in dicoPerso1 c'est juste pour lister toutes les enums qui existent
        {
            // si la quete contient la resistance ET que la resistance et # de 0 alors on doit faire le calcule
            if(queteEffectueParameter.dicoResistanceDifficulte.ContainsKey(enumR.Key) && queteEffectueParameter.dicoResistanceDifficulte[enumR.Key] != 0)
            {
                nombreStatUtilise++; // incremente le nombre de stat utilise car on est en train de calculer une stat


                //partie pour calculer la moyenne d'une stat pour le perso 1
                
                statCalculePerso1.Add(enumR.Key, 0); // ajoute une clé de resistance dans le dico de % de reussite du perso 
                statCalculePerso1[enumR.Key] = ((perso1.dicoResistance[enumR.Key] + boisson1.dicoResistanceBoisson[enumR.Key]) * 100) / queteEffectueParameter.dicoResistanceDifficulte[enumR.Key];

                if (statCalculePerso1[enumR.Key] > 100) // limite le % max a 100 pour pas avoir une reussite de 200% dans une stat qui carry les autres stats
                    statCalculePerso1[enumR.Key] = 100;
                else if(statCalculePerso1[enumR.Key] < 0) // limite le % min a 100 pour les memes raisons que ci dessus
                    statCalculePerso1[enumR.Key] = 0;

                //partie pour calculer la moyenne de toutes les stats du perso 1
                moyenneAllStatPerso1 = (moyenneAllStatPerso1 +  statCalculePerso1[enumR.Key]) / nombreStatUtilise ;




                
                statCalculePerso2.Add(enumR.Key, 0);// ajoute une clé de resistance dans le dico de % de reussite du perso 
                statCalculePerso2[enumR.Key] = ((perso2.dicoResistance[enumR.Key] + boisson2.dicoResistanceBoisson[enumR.Key]) * 100) / queteEffectueParameter.dicoResistanceDifficulte[enumR.Key];

                if (statCalculePerso2[enumR.Key] > 100) // limite le % max a 100 pour pas avoir une reussite de 200% dans une stat qui carry les autres stats
                    statCalculePerso2[enumR.Key] = 100;
                else if (statCalculePerso2[enumR.Key] < 0) // limite le % min a 100 pour les memes raisons que ci dessus
                    statCalculePerso2[enumR.Key] = 0;

                //partie pour calculer la moyenne de toutes les stats du perso 2
                moyenneAllStatPerso2 = (moyenneAllStatPerso2 + statCalculePerso2[enumR.Key]) / nombreStatUtilise;

            }

        }


        // calcule du reussite de la mission en prenant en compte le % de reussite du perso 1 et du perso 2, divisé par 2 car ya 2 personnes par quete
        ReussiteMission = (moyenneAllStatPerso1 + moyenneAllStatPerso2) / 2;



            Debug.Log("reussite de mission perso 1 = " + moyenneAllStatPerso1);
            Debug.Log("reussite de mission perso 2 = " + moyenneAllStatPerso2);
            Debug.Log("reussite de mission = " + ReussiteMission);

        float RandomPick = UnityEngine.Random.Range(0, 100);
        bool hasWinMission;

        Debug.Log("Nombre Random Pick = " + RandomPick);

        if (RandomPick <= ReussiteMission)
        {
            hasWinMission = true;
            AjoutXPRelation(queteEffectueParameter, ReussiteMission, hasWinMission);

            Debug.Log("Tu as reussi la mission, BRAVO" + hasWinMission);

        }
        else
        {
            hasWinMission = false;
            Debug.Log("Tu n'as pas réussi la mission " + hasWinMission);
        }



    }


    private void AjoutXPRelation(SO_Quete queteUtiliseXP, float percentReussiteParameter, bool hasWinMissionParameter)
    {
        
        SO_Personnage perso1;
        perso1 = queteUtiliseXP.persosEnvoyes[0];
        //perso1 = personnage1; // a gerter

        SO_Personnage perso2;
        perso2 = queteUtiliseXP.persosEnvoyes[1];
        //perso2 = personnage2; // a gerter


        #region Affection
        //partie gain d'affection 
        if (hasWinMissionParameter)
        {
            perso1.dicoRelationPerso[perso2.myEnumPerso] += 1;
            perso2.dicoRelationPerso[perso1.myEnumPerso] += 1;

        }
        else
        {
            perso1.dicoRelationPerso[perso2.myEnumPerso] -= 1;
            perso2.dicoRelationPerso[perso1.myEnumPerso] -= 1;
        }


        /* foreach(KeyValuePair<enumPerso,int> persoEach in dicoRes)
         {
             Debug.Log(perso1+"a tant de relation "  + personnage1.dicoRelationPerso[persoEach.Key] + " avec " + persoEach.Key);
         }*/
        #endregion



        #region XP


        CalculeXP(perso1,queteUtiliseXP);
        CalculeXP(perso2, queteUtiliseXP);
        /*for (int i = 0; i< queteUtilise.persosEnvoyes.Count; i++)
        {
            CalculeXP(queteUtilise.persosEnvoyes[i]);
        }*/

        

    }


    private void CalculeXP(SO_Personnage persoUtilise,SO_Quete queteEffectueCalculParameter)
    {

        foreach (KeyValuePair<enumResistance, float> enumR in dicoRes)// in dicoPerso1 c'est juste pour lister toutes les enums qui existent
        {
            if (queteEffectueCalculParameter.dicoResistanceDifficulte.ContainsKey(enumR.Key) && queteEffectueCalculParameter.dicoResistanceDifficulte[enumR.Key] != 0)
            {

                float differenceStatPerso2 = queteEffectueCalculParameter.dicoResistanceDifficulte[enumR.Key] - persoUtilise.dicoResistance[enumR.Key];
                float xpGagnePerso2 = 0;

                if (differenceStatPerso2 > 0 && differenceStatPerso2 < 250)
                {
                    xpGagnePerso2 = differenceStatPerso2 * 0.5f;
                }
                else if (differenceStatPerso2 >= 250 && differenceStatPerso2 < 500)
                {
                    xpGagnePerso2 = 125;
                }
                else if (differenceStatPerso2 >= 500 && differenceStatPerso2 < 800)
                {
                    xpGagnePerso2 = ((differenceStatPerso2 - 500) * 0.5f) + 125;
                }
                else if (differenceStatPerso2 >= 850 && differenceStatPerso2 < 1000)
                {
                    xpGagnePerso2 = 300;
                }
                else if (differenceStatPerso2 >= 1000 && differenceStatPerso2 < 1250)
                {
                    xpGagnePerso2 = ((differenceStatPerso2 - 1000) * 0.8f) + 300;
                }
                else if (differenceStatPerso2 >= 1250 && differenceStatPerso2 <= 1500)
                {
                    xpGagnePerso2 = 500;
                }

                persoUtilise.dicoResistance[enumR.Key] += xpGagnePerso2;
                Debug.Log("il y a une difference de " + differenceStatPerso2 +" "+ persoUtilise.myEnumPerso+ " gagne " + xpGagnePerso2 + "d'xp dans la stat " +enumR.Key );
            }
        }


                
    }


            #endregion

}
