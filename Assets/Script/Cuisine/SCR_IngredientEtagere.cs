using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_IngredientEtagere : MonoBehaviour
{

    [SerializeField] private SCR_SO_Ingredient mySoIngredient;
    [SerializeField] private SCR_Pool refPool;

    public float timerHover;

    // Start is called before the first frame update
    void Start()
    {

    }



    private void OnMouseDown()
    {
        Debug.Log("clique sur l'ingrédient");
        SCR_PoolItem poolItem =  refPool.Instantiate();
        SCR_Ingredient poolIngredient = poolItem.GetComponent<SCR_Ingredient>();
        poolIngredient.SetSoIngredient(mySoIngredient);
        poolIngredient.Init(refPool);
        poolIngredient.transform.position = this.transform.position;
    }

 

    private void OnMouseExit()
    {
        timerHover = 0;
    }
    private void OnMouseOver()
    {
        timerHover += Time.deltaTime;
        
    }
}
