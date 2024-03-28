using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bouilloire : SCR_Ustensile
{

    [SerializeField] private Transform contenantBouilloire; // ref de l'objet qui rotate
    private Vector3 startRotationBouilloire; // rotation initial de la bouilloire

    private Vector3 lastMousePos; // derniere position de la souris
    private float RotZ; // valeur qui permet de manipuler si on va dans la bonne direction

    [SerializeField] private float quantiteEauNecessaire; // quantite necessaire pour realiser la manipulation
    private float eauVerse; // la valeur actuelle de l'eau qu'on a versé

    [SerializeField] private SCR_Tasse refTasse;

    private Tweener tweenerRotation;

    [SerializeField] private Animator waterAnimator;

    [SerializeField] private GameObject waterImg;
    [SerializeField] private float waterStart;
    [SerializeField] private float waterEnd;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startRotationBouilloire = transform.rotation.eulerAngles;
    }


    public override void OnMouseOver()
    {
        base.OnMouseOver();
        outlineMaterial.SetFloat("_OutlineDensity", 1f);

    }
    public override void OnMouseExit()
    {
        base.OnMouseExit();
        outlineMaterial.SetFloat("_OutlineDensity", 0f);


    }
    public override void OnMouseDown()
    {
        isMaintenu = true;
        lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        AudioManager.instanceAM.Play("BouilloireVersement");

        /*Texture2D cursorHover = Resources.Load<Texture2D>("Cursor_HoverOn");

        Cursor.SetCursor(cursorHover, new Vector2(80f, 50f), CursorMode.Auto);*/

        SCR_Cursor.instanceCursor.ChangeHoverOn(true);

        tweenerRotation.Kill();


    }

    public override void OnMouseDrag()
    {
        if(inManipulation)
        {
            outlineMaterial.SetFloat("_OutlineDensity", 1f);

            Vector3 diffMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePos; // vecteur de direction entre la derniere position de la souris et sa position actuelle
            RotZ += diffMousePos.x + diffMousePos.y; // positif quand on va vers le haut ou droite et negatif quand on va a gauche ou en bas
            float rotzClamp = Mathf.Clamp(RotZ, 0, 1); // clamp cette valeur à 1 car on veut on mouvement ver le haut ou droite pour effectuer la manipulation
            RotZ = rotzClamp; // RotZ prend la valeur max du clamp, pour que quand on soustrait, on soustrait depuis le max et pas du "surplus"
            float rotationXRemap = Remap(rotzClamp, 0, 1, 5, -25); // remap cette valeur avec les rotation min et max de la bouilloire

            if(rotationXRemap < -10) // si la bouilloire atteint une certaines rotation, l'eau coule
            {

                waterAnimator.SetBool("endAnimation", false);
                waterAnimator.SetBool("startAnimation",true);

                waterAnimator.SetFloat("flowSpeed", RotZ);

                eauVerse  += Remap(rotzClamp,0.5f,1,0,1);// remap la rotation min qui permet de verser de l'eau et le max, si la bouilloire est + penche, elle verse + d'eau

                float percentReussite = eauVerse / quantiteEauNecessaire;


                float poidsY = Mathf.Lerp(-0.323f, 0.495f, percentReussite);
                waterImg.transform.localPosition = new Vector3(waterImg.transform.localPosition.x, poidsY, waterImg.transform.localPosition.z);// fait monter l'eau dans la tasse

                if (eauVerse >= quantiteEauNecessaire) // si on atteint la quati d'eau necessaire on a finit de manipuler
                {
                    waterAnimator.SetBool("endAnimation", true);
                    waterAnimator.SetBool("startAnimation", false);

                    FinishManipulation();
                }
            }
            else
            {
                waterAnimator.SetBool("endAnimation",true);
                waterAnimator.SetBool("startAnimation", false);


            }

            contenantBouilloire.transform.rotation = Quaternion.Euler(0, 0, rotationXRemap); // update la rotation de la bouilloire 

            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);



            
        }
        


    }

    public override void OnMouseUp()
    {

        base.OnMouseUp();
        outlineMaterial.SetFloat("_OutlineDensity", 0f);

        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);



        tweenerRotation = contenantBouilloire.transform.DOLocalRotate(startRotationBouilloire, 1f); // reset la rotation de la bouilloire àa sa rotation initial
        RotZ = 0f; // permet que quand on clique a nouveau sur la bouilloire la rotation reprenne a 0
    }


    public override void FinishManipulation()
    {
 
        OnMouseUp(); // reset la rotation de la bouilloire

        refTasse.FinishBoisson();



        Invoke("CallNextBoisson", 1);
    }

    public void CallNextBoisson()
    {
        SCR_CuisineManager.instanceCM.FinisshBouilloire();
        eauVerse = 0f; // reset la valeur d'eau versé


    }

    public void UnlockBouilloire() // debloque le fait de pouvoir manipuler la bouilloire
    {
        
        colliderManipulation.enabled = true;
        inManipulation = true;
    }
    

    public void SetEauVerser()
    {
        eauVerse = 0;
        waterImg.transform.position = new Vector3(waterImg.transform.position.x, -0.323f, waterImg.transform.position.z);//reset la position du liquide dans la bouilloire
    }
    
}
