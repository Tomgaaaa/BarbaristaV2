using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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



    #endregion


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

    }


    public void TransitionBouilloire() 
    {
        bambooShade.DOLocalMove(emplacementbambooShade, 1f); // déplace le volet jusqu'a son emplacement

        startPositionAllUstensile = allUstensile.position; 
        allUstensile.DOLocalMove(new Vector3(-10, 0, 0), 1f);

        boulloire.transform.DOLocalMove(emplacementBoulloire.position, 1f);
        boulloire.UnlockBouilloire();
    }


    public void UnLockIngredient() 
    {
        bambooShade.DOLocalMove(startPositionBambooShade, 1f);

        allUstensile.DOLocalMove(startPositionAllUstensile, 1f);

        boulloire.transform.DOLocalMove(new Vector3 (-20,0,0), 1f);
    }



 

}
