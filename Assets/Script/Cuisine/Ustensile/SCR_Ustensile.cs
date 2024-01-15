using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ustensile : MonoBehaviour // script parent de tout les ustensiles (ceux qui permettent de modifier un etat)
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation; // �tat de l'ingr�dient apres la transformation 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public virtual void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appel� lorsqu'un ingr�dient est lach� par dessus un ustensile
    {
        Debug.Log("touch� un sustensile");

    }

    public enumEtatIgredient GetEtat() { return etatApresTransformation; } // renvoie l'etat de transformation
}
