using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Tasse : SCR_Contenant
{

    [SerializeField] private List<SCR_SO_Ingredient> listIngredientsUtilises = new List<SCR_SO_Ingredient>(); // liste des ingrédients qui ont ete drop dans la tasse, sera utilise pour l'historique
    private Dictionary<enumResistance, float> dicoStatBoisson = new Dictionary<enumResistance, float>()
    {
        { enumResistance.Cryogenique, 0 },
        { enumResistance.Thermique, 0 },
        { enumResistance.Electrique, 0 },
        { enumResistance.Toxique, 0 },
        { enumResistance.Hemorragique, 0 },
        { enumResistance.Lethargique, 0 },

    }; // dico des stats de la boisson

    [SerializeField] private SCR_Bouilloire refBouilloire; // ref a la bouilloire pour la débloquer lorsqu'il y a 3 ingrédients dans la tasse

    #region Visuelle
    [Header("Presse")]
    [SerializeField] private SpriteRenderer SR_liquide;
    [SerializeField] private SpriteRenderer SR_eauMonte;

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

    [SerializeField] SCR_QueteCuisine cuisine;

    [SerializeField] private GameObject smokeVFX;

    private List <int> listSpriteRandomIndex = new List<int>();
    private int Etape = 0;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //allVisuelle.SetActive(false);

        smokeVFX.SetActive(false);
    }


    
    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        textNmbIngredient.text = nmbIngredientIn + " /3";

        listIngredientsUtilises.Add(ingredientDropParameter.GetCR_SO_Ingredient()); // ajoute l'ingrédient drop sur la tasse a la liste des ingrédients utilisés pour la boisson
        AudioManager.instanceAM.Play("DropItemTasse");
        ingredientDrop.gameObject.SetActive(false); // désactive l'ingrédient drop, faudra le renvoyer dans le pool plutot

        CalCulStat(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance); // met a jour les stats de la boisson avec l'ingrédient qui a ete drop

        cuisine.UpdateStatWhenDrop(ingredientDropParameter.GetCR_SO_Ingredient().dicoResistance); //met a jour les stats de l'hewagone fiche perso dans l'UI cuisine quand on drop un ingredient

        // a mettre quand l'eau a ete versée
        UpdateVisuelle(ingredientDrop.GetCR_SO_Ingredient(),false);


        SCR_Cursor.instanceCursor.ChangeClickOff(true);

        if(listIngredientsUtilises.Count == 3) // si il y a 3 ingrédient dans la tasse 
        {
            SCR_CuisineManager.instanceCM.TransitionBouilloire();
            
            refBouilloire.UnlockBouilloire(); // alors on débloque le fait de pouvoir manipuler la bouilloire

           
        }

        ingredientDropParameter.Back(); // renvoie l'ingrédient dans le pool
    }

    private void CalCulStat(Dictionary<enumResistance,float> statIngredientParameter) // fonction qui permet de calculer les stats de la boisson en prennant en parametre le dico de stat de l'ingrédient
    {


        foreach (KeyValuePair<enumResistance,float> resistance in statIngredientParameter) // pour chaque paire (quand il y a une clé associé a une valeur),on fait ce qu'il y a ci dessous
        {
            dicoStatBoisson[resistance.Key] += statIngredientParameter[resistance.Key]; // on prend la meme clé dans le dico de la boisson que la clé de l'ingrédient et on ajoute la valuer de l'ingrédient
           
        }


      /* foreach (KeyValuePair<enumResistance, float> resistance in dicoStatBoisson) // c'est juste pour debug
        {
            Debug.Log("Stat " + resistance.Key + " : " + resistance.Value);
        }*/

    }



    public void FinishBoisson()
    {
        refHexagone.UpdateStat(dicoStatBoisson,false);
        allVisuelle.SetActive(true);

        smokeVFX.SetActive(true);




        AudioManager.instanceAM.Play("Preparationfini");

    }

    public void RecreateBoisson(List<SCR_SO_Ingredient> listIngredientParameter)
    {

        listIngredientsUtilises = listIngredientParameter;
        UpdateVisuelle(listIngredientsUtilises[0],true);
        UpdateVisuelle(listIngredientsUtilises[1],true);
        UpdateVisuelle(listIngredientsUtilises[2],true);
        allVisuelle.SetActive(true);
    }

    public void UpdateVisuelle(SCR_SO_Ingredient SoParameter, bool recreateBoisson)
    {
        if(SoParameter.actualStateSO == enumEtatIgredient.Presse)
        {

            listSpriteRandomIndex.Add(0);


            switch (listIngredientsUtilises.Count)
            {
                case 1: SR_liquide.color = SoParameter.colorSO; SR_eauMonte.color = SoParameter.colorSO; break; 

                case 2:
                    Color MixedColor = new Color((listIngredientsUtilises[0].colorSO.r + SoParameter.colorSO.r) / 2, (listIngredientsUtilises[0].colorSO.g + SoParameter.colorSO.g) / 2, (listIngredientsUtilises[0].colorSO.b + SoParameter.colorSO.b) / 2, 255);
                    SR_liquide.color = MixedColor;
                    SR_eauMonte.color = MixedColor;
                    break; 

                case 3: SR_liquide.color = SoParameter.colorSO; SR_eauMonte.color = SoParameter.colorSO; break; 
            }
        }

        else if(SoParameter.actualStateSO == enumEtatIgredient.Tranche)
        {

            SpriteRenderer randomTranche = getRandomSr(listSrTranche,listTrancheDejaUtilise);

            listSpriteRandomIndex.Add(listTrancheDejaUtilise.IndexOf(randomTranche)); // nouveau
            if (recreateBoisson)
                randomTranche = listSrTranche[Etape];

            listTrancheDejaUtilise.Add(randomTranche);


            randomTranche.color = SoParameter.colorSO;
            randomTranche.gameObject.SetActive(true);

        }

        else if (SoParameter.actualStateSO == enumEtatIgredient.Broye)
        {

            SpriteRenderer randomBroye = getRandomSr(listSrBroye, listBroyeDejaUtilise);

            listSpriteRandomIndex.Add(listTrancheDejaUtilise.IndexOf(randomBroye)); // nouveau
            if (recreateBoisson)
                randomBroye = listSrBroye[Etape];

            listBroyeDejaUtilise.Add(randomBroye);

            randomBroye.color = SoParameter.colorSO;
            randomBroye.gameObject.SetActive(true);
        }

        else if(SoParameter.actualStateSO == enumEtatIgredient.Rape)
        {

            SpriteRenderer randomRape = getRandomSr(listSrRape, listRapeDejaUtilise);

            listSpriteRandomIndex.Add(listTrancheDejaUtilise.IndexOf(randomRape)); // nouveau
            if (recreateBoisson)
                randomRape = listSrRape[Etape];


            listRapeDejaUtilise.Add(randomRape);

            randomRape.color = SoParameter.colorSO;
            randomRape.gameObject.SetActive(true);
        }

        Etape++;


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


         foreach(SpriteRenderer spriteR in listBroyeDejaUtilise)
        {
            spriteR.gameObject.SetActive(false);
        }

        foreach (SpriteRenderer spriteR in listRapeDejaUtilise)
        {
            spriteR.gameObject.SetActive(false);
        }

        foreach (SpriteRenderer spriteR in listTrancheDejaUtilise)
        {
            spriteR.gameObject.SetActive(false);
        }

        listBroyeDejaUtilise.Clear();
        listRapeDejaUtilise.Clear();
        listTrancheDejaUtilise.Clear();

        listSpriteRandomIndex.Clear();
        Etape = 0;

        SR_liquide.color = Color.white;

        listIngredientsUtilises.Clear();
        allVisuelle.SetActive(false);
        smokeVFX.SetActive(false);

        nmbIngredientIn = 0;
        textNmbIngredient.text = nmbIngredientIn + " /3";

    }



    public SO_Boisson GetBoissonSo() 
    {
        SO_Boisson instanceSo = ScriptableObject.CreateInstance<SO_Boisson>();

        Dictionary<enumResistance, float> dicoTransfere = new Dictionary<enumResistance, float>();
        dicoTransfere[enumResistance.Cryogenique] = dicoStatBoisson[enumResistance.Cryogenique];
        dicoTransfere[enumResistance.Thermique] = dicoStatBoisson[enumResistance.Thermique];
        dicoTransfere[enumResistance.Toxique] = dicoStatBoisson[enumResistance.Toxique];
        dicoTransfere[enumResistance.Hemorragique] = dicoStatBoisson[enumResistance.Hemorragique];
        dicoTransfere[enumResistance.Electrique] = dicoStatBoisson[enumResistance.Electrique];
        dicoTransfere[enumResistance.Lethargique] = dicoStatBoisson[enumResistance.Lethargique];


        List<SCR_SO_Ingredient> listIngredientTransfere = new List<SCR_SO_Ingredient>();
        for (int i = 0; i < listIngredientsUtilises.Count; i++)
        {
            listIngredientTransfere.Add(listIngredientsUtilises[i]);
        }

        List<int> listIndexTransfere = new List<int>();
        for (int i = 0; i < listSpriteRandomIndex.Count; i++)
        {
            listIndexTransfere.Add(listSpriteRandomIndex[i]);
        }

        instanceSo.CreateBoisson(listIngredientTransfere, dicoTransfere, listIndexTransfere);
        
        return instanceSo;
    }
}
