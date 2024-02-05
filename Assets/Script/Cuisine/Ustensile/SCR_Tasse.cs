using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Tasse : SCR_Contenant
{

    [SerializeField] private List<SCR_Ingredient> listIngredientsUtilises; // liste des ingr�dients qui ont ete drop dans la tasse, sera utilise pour l'historique
    private Dictionary<enumResistance, float> dicoStatBoisson = new Dictionary<enumResistance, float>()
    {
        { enumResistance.Cryogenique, 0 },
        { enumResistance.Thermique, 0 },
        { enumResistance.Electrique, 0 },
        { enumResistance.Toxique, 0 },
        { enumResistance.Hemorragique, 0 },
        { enumResistance.Lethargique, 0 },

    }; // dico des stats de la boisson

    [SerializeField] private SCR_Bouilloire refBouilloire; // ref a la bouilloire pour la d�bloquer lorsqu'il y a 3 ingr�dients dans la tasse

    #region Visuelle
    [Header("Presse")]
    [SerializeField] private SpriteRenderer SR_liquide;

    [Header("Tranche")]
    [SerializeField] private List<SpriteRenderer> listSrTranche;
    private List<SpriteRenderer> listTrancheDejaUtilise = new List<SpriteRenderer>();

    [Header("Broye")]
    [SerializeField] private List<SpriteRenderer> listSrBroye;
    private List<SpriteRenderer> listBroyeDejaUtilise = new List<SpriteRenderer>();

    [Header("Rape")]
    [SerializeField] private List<SpriteRenderer> listSrRape;
    private List<SpriteRenderer> listRapeDejaUtilise = new List<SpriteRenderer>();

    #endregion


    [SerializeField] private SCR_HexagoneStat refHexagone;

    [SerializeField] private GameObject allVisuelle;

    [SerializeField] private Text textNmbIngredient;

    // Start is called before the first frame update
    public override void Start()
    {

        allVisuelle.SetActive(false);
    }

    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        textNmbIngredient.text = nmbIngredientIn + " /3";

        listIngredientsUtilises.Add(ingredientDropParameter); // ajoute l'ingr�dient drop sur la tasse a la liste des ingr�dients utilis�s pour la boisson
        AudioManager.instanceAM.Play("DropItemTasse");
        ingredientDrop.gameObject.SetActive(false); // d�sactive l'ingr�dient drop, faudra le renvoyer dans le pool plutot

        CalCulStat(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance); // met a jour les stats de la boisson avec l'ingr�dient qui a ete drop

        // a mettre quand l'eau a ete vers�e
        UpdateVisuelle(ingredientDrop.GetCR_SO_Ingredient());


        SCR_Cursor.instanceCursor.ChangeClickOff(true);

        if(listIngredientsUtilises.Count == 3) // si il y a 3 ingr�dient dans la tasse 
        {
            SCR_CuisineManager.instanceCM.TransitionBouilloire(false);
            refBouilloire.UnlockBouilloire(); // alors on d�bloque le fait de pouvoir manipuler la bouilloire
        }
    }

    private void CalCulStat(Dictionary<enumResistance,float> statIngredientParameter) // fonction qui permet de calculer les stats de la boisson en prennant en parametre le dico de stat de l'ingr�dient
    {


        foreach (KeyValuePair<enumResistance,float> resistance in statIngredientParameter) // pour chaque paire (quand il y a une cl� associ� a une valeur),on fait ce qu'il y a ci dessous
        {
            dicoStatBoisson[resistance.Key] += statIngredientParameter[resistance.Key]; // on prend la meme cl� dans le dico de la boisson que la cl� de l'ingr�dient et on ajoute la valuer de l'ingr�dient
           
        }


      /*  foreach (KeyValuePair<enumResistance, float> resistance in dicoStatBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }*/

    }


    public void FinishBoisson()
    {
        refHexagone.UpdateStat(dicoStatBoisson);
        allVisuelle.SetActive(true);

        AudioManager.instanceAM.Play("Preparationfini");



    }
    private void UpdateVisuelle(SCR_SO_Ingredient SoParameter)
    {


        if(SoParameter.actualStateSO == enumEtatIgredient.Presse)
        {

            switch (listIngredientsUtilises.Count)
            {
                case 1: SR_liquide.color = SoParameter.colorSO; break; 

                case 2:
                    Color MixedColor = new Color((listIngredientsUtilises[0].GetCR_SO_Ingredient().colorSO.r + SoParameter.colorSO.r) / 2, (listIngredientsUtilises[0].GetCR_SO_Ingredient().colorSO.g + SoParameter.colorSO.g) / 2, (listIngredientsUtilises[0].GetCR_SO_Ingredient().colorSO.b + SoParameter.colorSO.b) / 2, 255);
                    SR_liquide.color = MixedColor;
                    break; 

                case 3: SR_liquide.color = SoParameter.colorSO; break; 
            }
        }

        else if(SoParameter.actualStateSO == enumEtatIgredient.Tranche)
        {
            SpriteRenderer randomTranche = getRandomSr(listSrTranche,listTrancheDejaUtilise);
            listTrancheDejaUtilise.Add(randomTranche);

            randomTranche.color = SoParameter.colorSO;
            randomTranche.gameObject.SetActive(true);

        }

        else if (SoParameter.actualStateSO == enumEtatIgredient.Broye)
        {
            SpriteRenderer randomBroye = getRandomSr(listSrBroye, listBroyeDejaUtilise);
            listBroyeDejaUtilise.Add(randomBroye);

            randomBroye.color = SoParameter.colorSO;
            randomBroye.gameObject.SetActive(true);
        }

        else if(SoParameter.actualStateSO == enumEtatIgredient.Rape)
        {
            SpriteRenderer randomRape = getRandomSr(listSrRape, listRapeDejaUtilise);
            listRapeDejaUtilise.Add(randomRape);

            randomRape.color = SoParameter.colorSO;
            randomRape.gameObject.SetActive(true);
        }



    }


    private SpriteRenderer getRandomSr(List<SpriteRenderer> listGlobalParameter, List<SpriteRenderer> listDejaUtilliseParameter)
    {
        SpriteRenderer randomTranche = listGlobalParameter[Random.Range(0, listGlobalParameter.Count)];

        if(listDejaUtilliseParameter.Contains(randomTranche))
            randomTranche = getRandomSr(listGlobalParameter,listDejaUtilliseParameter);



        return randomTranche;
    }
    
    public void ResetBoisson()
    {

            dicoStatBoisson[enumResistance.Hemorragique] = 0;
            dicoStatBoisson[enumResistance.Cryogenique] = 0;
            dicoStatBoisson[enumResistance.Thermique] = 0;
            dicoStatBoisson[enumResistance.Toxique] = 0;
            dicoStatBoisson[enumResistance.Electrique] = 0;
            dicoStatBoisson[enumResistance.Lethargique] = 0;
        



        allVisuelle.SetActive(false);
        nmbIngredientIn = 0;
        textNmbIngredient.text = nmbIngredientIn + " /3";

    }
}
