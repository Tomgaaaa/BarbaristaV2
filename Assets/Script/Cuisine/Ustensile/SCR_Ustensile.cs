using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SCR_Ustensile : MonoBehaviour
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public virtual void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        Debug.Log("touché un sustensile");

    }

    public enumEtatIgredient GetEtat() { return etatApresTransformation; }
}
