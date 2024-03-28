using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_UST_Rape : SCR_Ustensile // script specifique a la rape, hérite d'ustensile pour avoir acces aux fonctions OnDrop()... 
{
    int timeBase = 0;
    [Header("Unique à la rape")]
    [SerializeField] private Transform refPlaque;

    private Vector3 lastMousePos; // derniere position de la souris
    private float PosX; // valeur qui permet de manipuler si on va dans la bonne direction

    [SerializeField] private float tempsNecessaire;
    private float currentTempsPasse;

    [SerializeField] private GameObject Poid;
    #region Drag
    private TargetJoint2D plaqueTargetJoint;
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de réetablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est réduit a chaque fréquence
    #endregion

    private SCR_Ingredient IngredientIn;


    [SerializeField] private SpriteRenderer poigneeOutlineRenderer;
    private Material poigneeOutlineMaterial;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        etatApresTransformation = enumEtatIgredient.Rape; // vus que c'est le script Rape, l'etat de transformation sera rapé 

        poigneeOutlineMaterial = poigneeOutlineRenderer.material;
    }


    public override void OnMouseDown()
    {
        base.OnMouseDown();


        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        plaqueTargetJoint = refPlaque.gameObject.AddComponent<TargetJoint2D>();
        plaqueTargetJoint.frequency = frequenceJoint;
        plaqueTargetJoint.dampingRatio = dampingJoint;
        AudioManager.instanceAM.Play("Grab_2");
        
    }

    public override void OnMouseOver()
    {
        base.OnMouseOver();

        if(inManipulation)
            poigneeOutlineMaterial.SetFloat("_OutlineDensity", 1f);


    }



    public override void OnMouseExit()
    {
        base.OnMouseExit();

        if(inManipulation)
            poigneeOutlineMaterial.SetFloat("_OutlineDensity", 0f);

    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        Destroy(plaqueTargetJoint); plaqueTargetJoint = null;
        refPlaque.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        poigneeOutlineMaterial.SetFloat("_OutlineDensity", 0f);


    }
    public override void OnMouseDrag()
    {
        if(inManipulation)
        {
            base.OnMouseDrag();
            poigneeOutlineMaterial.SetFloat("_OutlineDensity", 1f);


            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // vecteur de direction entre la derniere position de la souris et sa position actuelle
            
            PosX += diffMousePos.x; // positif quand on va vers le haut ou droite et negatif quand on va a gauche ou en bas
            float posxClamp = Mathf.Clamp(PosX, -1, 1); // clamp cette valeur à 1 car on veut on mouvement ver le haut ou droite pour effectuer la manipulation
            PosX = posxClamp; // RotZ prend la valeur max du clamp, pour que quand on soustrait, on soustrait depuis le max et pas du "surplus"
            float positionXRemap = Remap(posxClamp, -1, 1, -0.2f, 0.2f); // remap cette valeur avec les rotation min et max de la bouilloire

            plaqueTargetJoint.target = new Vector2(transform.position.x + positionXRemap, refPlaque.position.y);
            /*AudioManager.instanceAM.Play("Raper");*/
            if (diffMousePos.x != 0)
            {
                
                currentTempsPasse += Mathf.Abs(diffMousePos.x /10);

                int timeCode = Mathf.RoundToInt(currentTempsPasse);
                

                if(timeCode > timeBase)
                {
                    ParticleSystem vfxParticle = Instantiate<ParticleSystem>(myVFX, transform);
                    vfxParticle.textureSheetAnimation.SetSprite(0, ingredientDrop.GetCR_SO_Ingredient().TrancheTasse);
                    timeBase = timeCode;
                }

                float percentReussite = currentTempsPasse / tempsNecessaire;
                

                float poidsY = Mathf.Lerp(1.3f, 0.9f, percentReussite);
                float posIngredient = Mathf.Lerp(emplacementIngredient.position.y, -1.318f, percentReussite);
                Poid.transform.localPosition = new Vector3(Poid.transform.localPosition.x, poidsY, Poid.transform.localPosition.z);
                IngredientIn.transform.position = new Vector3(IngredientIn.transform.position.x, posIngredient, 0);

                if (currentTempsPasse >= tempsNecessaire)
                {
                    FinishManipulation();
                    AudioManager.instanceAM.Play("RaperFini");
                    refPlaque.DOLocalMoveX(0, 0.5f);
                    currentTempsPasse = 0;
                    timeBase = 0;
                    sparkleVFX.Play();
                    Poid.transform.localPosition = new Vector3(Poid.transform.localPosition.x, 1.3f, Poid.transform.localPosition.z);
                    IngredientIn.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    IngredientIn.transform.rotation = new Quaternion(0, 0, 0, 1);
                }
            }


            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }


    }
    public override void OnDrop(SCR_Ingredient ingredientDropParameter)
    {
        base.OnDrop(ingredientDropParameter);
        IngredientIn = ingredientDropParameter;
        ingredientDropParameter.transform.DOMove(new Vector3(emplacementIngredient.position.x, emplacementIngredient.position.y, 0), 1f);
       
        ingredientDropParameter.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ingredientDropParameter.transform.rotation = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
        ingredientDropParameter.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

}
