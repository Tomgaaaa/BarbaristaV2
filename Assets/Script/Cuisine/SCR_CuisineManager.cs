using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SCR_CuisineManager : MonoBehaviour
{

    public static SCR_CuisineManager instanceCM;



    #region bouilloire
    [Header("Transition Boulloire")]
    [SerializeField] private Transform bambooShade; // transform du volet pour le deplacer
    [SerializeField] private Vector3 emplacementbambooShade; // l'emplacement que doit prendre le volet 
    private Vector3 startPositionBambooShade; // position initial du volet
    [SerializeField] private Transform allUstensile; // Reference des ustensiles pour les faire sortir de l'écran
    private Vector3 startPositionAllUstensile; //  position initial des ustensiles
    [SerializeField] private SCR_Bouilloire boulloire;
    [SerializeField] private Transform emplacementBoulloire;

    [SerializeField] private SCR_HexagoneStat hexagone;
    [SerializeField] private Transform emplacementHexagone;
    private Vector3 startPositionHexagone; //  position initial des ustensiles




    #endregion

    [SerializeField] private SCR_Tasse refTasse;
    [SerializeField] private SCR_HexagoneStat refHexagone;

    private void Awake()
    {
        if (instanceCM == null)
            instanceCM = this;
        else
            Destroy(gameObject);

    }



    // Start is called before the first frame update
    void Start()
    {
        startPositionBambooShade = bambooShade.position; // stock la position initial du volet
        startPositionHexagone = hexagone.transform.position; // stock la position initial du volet
        startPositionAllUstensile = allUstensile.position;

    }


    public void TransitionBouilloire(bool resetPositionParameter) 
    {
        AudioManager.instanceAM.Play("Transibouilloire");
        bambooShade.DOLocalMove(emplacementbambooShade, 1f); // déplace le volet jusqu'a son emplacement

        allUstensile.DOLocalMove(new Vector3(-10, 0, 0), 1f);

        boulloire.transform.DOLocalMove(emplacementBoulloire.position, 1f);

        hexagone.transform.DOLocalMove(emplacementHexagone.position, 1f);

    }


    public void UnLockIngredient() 
    {
        bambooShade.DOLocalMove(startPositionBambooShade, 1f);

        allUstensile.DOLocalMove(startPositionAllUstensile, 1f);

        boulloire.transform.DOLocalMove(new Vector3 (-20,0,0), 1f);

        hexagone.gameObject.SetActive(false);
        hexagone.transform.DOLocalMove(startPositionHexagone, 1f).OnComplete(ResetBoisson);

    }

    public void ResetBoisson()
    {


        refTasse.ResetBoisson();


        Dictionary<enumResistance, float> dicoStatBoisson = new Dictionary<enumResistance, float>()
        {
            { enumResistance.Cryogenique, 0 },
            { enumResistance.Thermique, 0 },
            { enumResistance.Electrique, 0 },
            { enumResistance.Toxique, 0 },
            { enumResistance.Hemorragique, 0 },
            { enumResistance.Lethargique, 0 },

        };
        refHexagone.UpdateStat(dicoStatBoisson);
        hexagone.gameObject.SetActive(true);

    }



}
