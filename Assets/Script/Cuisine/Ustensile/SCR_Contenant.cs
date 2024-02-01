using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Contenant : MonoBehaviour // script parent de bol et ustensile, parce que on d�pose les ingr�dients dans ces �l�ments et qu'ils ont des comportements similaire
{
    [SerializeField] private protected int maxIngredientDrop = 1; // le nombre maximum que l'on peut d�poser dans le contenant, concretement y a juste la boisson ou l'on peut en mettre 3
    private protected int nmbIngredientIn; // le nombre actuelle qu'il y a d'ingr�dient dans le contenant

    private protected SCR_Ingredient ingredientDrop; // ref de l'ingr�dient actuellement dans le contenant
    private protected Collider2D ingredientCollider; // collider de l'ingr�dient pour le passer en trigger, on le stock pour eviter de cast lors du depot et lors du retrait
    private Rigidbody2D ingredientRB; // meme chose pour le rigidBody de l'ingr�dient

    [SerializeField] private Transform emplacementIngredient; // emplacement o� doit se situer l'ingr�dient lorsqu'on le d�pose
    [SerializeField] private Vector3 multiplicateurScaleIngredient; // valeur qui va multiplier le scale de l'ingr�dient lorsqu'on le d�pose
    private protected Vector3 startScaleIngredient; // scale initiale de l'ingr�dient avant le d�pot


    [SerializeField] private protected List<Renderer> rendererOutline;
    private protected List<Material> outlineMaterial = new List<Material>();

    // Start is called before the first frame update
    public virtual void Start()
    {
        /*foreach(Renderer renderer in rendererOutline)
       {
           outlineMaterial.Add(renderer.material);
       }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void OnDrop(SCR_Ingredient ingredientDropParameter) // fonction appel� lorsque l'ingr�dient est relache au dessus d'un contenant
    {

        if(nmbIngredientIn < maxIngredientDrop) // verifie que le contenant n'est pas plein / que le nombre d'ingr�dient actuel est inf�rieur au maximum que ce le contenant peut contenir
        {
            nmbIngredientIn++; // ajoute 1 au nombre d'ingr�dient pr�sent dans le contenant
            ingredientDrop = ingredientDropParameter; // stock l'ingr�dient qui est actuellement dans le contenant
            ingredientDropParameter.SetInUstensileAndUstensile(true, this); // passe la variable inContenant de l'ingredient a vrai et renseigne ce contenant 

            ingredientCollider = ingredientDropParameter.GetComponent<Collider2D>(); // stock le collider de l'ingr�dient drop 
            //ingredientCollider.isTrigger = true; // passe le collider en trigger pour empecher les collisions mais qu'il puisse recevoir les cast quand meme

            ingredientRB = ingredientDropParameter.GetComponent<Rigidbody2D>(); // stock le rigidBody de l'ingr�dient drop
            ingredientRB.gravityScale = 0; // retire la gravit� 
            ingredientRB.velocity = Vector3.zero; // retire la v�locit� que l'ingr�dient avait
            ingredientRB.angularVelocity = 0; // retire la velocit� de rotation de l'ingr�dient

            startScaleIngredient = ingredientDropParameter.transform.localScale; // stock le scale initial de l'ingr�dient

           /* if(emplacementIngredient.transform.position.x == 0 || emplacementIngredient.transform.position.y == 0) 
            {
                emplacementIngredient.position = new Vector3(ingredientsDrop.transform.position.x, ingredientsDrop.transform.position.y, ingredientsDrop.transform.position.z);
            }*/

            if (multiplicateurScaleIngredient.x == 0 || multiplicateurScaleIngredient.y == 0) // permet d'empecher de faire disparaitre l'ingr�dient si le scale n'a pas ete renseign�
            {
                multiplicateurScaleIngredient = Vector3.one; // Vector3.one pour que le scale soit multipli� par 1, ce que ne changera pas le scale de l'ingr�dient
            }

            ingredientDropParameter.transform.DOMove(new Vector3(emplacementIngredient.position.x, emplacementIngredient.position.y,0),1f); // d�place l'ingr�dient � l'endroit voulus

            // creer un vector3 qui sera le scale de l'ingr�dient apres multiplication
            Vector3 newScale = new Vector3(multiplicateurScaleIngredient.x * ingredientDropParameter.transform.localScale.x, multiplicateurScaleIngredient.y * ingredientDropParameter.transform.localScale.y, multiplicateurScaleIngredient.z * ingredientDropParameter.transform.localScale.z);
            ingredientDropParameter.transform.DOScale(newScale, 0.2f); // resize l'ingr�dient avec le nouvea scla (scale de l'ingr�dient X le multiplicateur)
        }
    }

    public virtual void PickUpFromContenant( ) // fonction appell� lorsqu'un ingr�dient est cliqu� et qu'il est dans un contenant
    {
        
        ingredientRB.gravityScale = 1; // repasse la gravit� � sa valeur initiale
        ingredientCollider.isTrigger = false; // re active la collision de l'ingr�dient
        ingredientDrop.transform.DOScale(startScaleIngredient, 0.5f); // resize l'ingr�dient avec son scale initial


        nmbIngredientIn--; // retire 1 au nombre d'ingr�dient pr�sent dans le contenant 
        ingredientDrop = null;  // reset les variables de stockages
        ingredientCollider = null; // pareil
        ingredientRB = null; // �galement
    }



    public virtual void ShowOutline(bool needShowOutline, SCR_Ingredient ingredientDragParameter)
    {
        if (needShowOutline)
        {

            for (int i = 0; i < outlineMaterial.Count; i++)
            {
                outlineMaterial[i].SetFloat("_Thickness", 0.04f);

            }

        }
        else
        {
            for (int i = 0; i < outlineMaterial.Count; i++)
            {
                outlineMaterial[i].SetFloat("_Thickness", 0);

            }
        }

    }
}
