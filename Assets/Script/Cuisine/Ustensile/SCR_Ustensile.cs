using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ustensile : SCR_Contenant // script parent de tout les ustensiles (ceux qui permettent de modifier un etat)
{

    [SerializeField] private protected enumEtatIgredient etatApresTransformation; // état de l'ingrédient apres la transformation 
    private protected Camera mainCam;
    [SerializeField] private Vector3 emplacementCam;

    private protected SCR_Ingredient ingredientDrop;


    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCam = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);

        ingredientDrop = ingredientDropParameter;


        Rigidbody2D ingredientRB = ingredientDropParameter.GetComponent<Rigidbody2D>();
        ingredientRB.gravityScale = 0;
        ingredientRB.velocity = Vector3.zero;
        ingredientRB.angularVelocity = 0;

        mainCam.transform.DOMove(new Vector3(emplacementCam.x, emplacementCam.y, -2), 1f);
        mainCam.DOOrthoSize(emplacementCam.z, 1f);


    }


    public virtual void OnMouseDrag()
    {
    }

    public enumEtatIgredient GetEtat() { return etatApresTransformation; } // renvoie l'etat de transformation
}
