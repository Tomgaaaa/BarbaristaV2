using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_UST_Rape : SCR_Ustensile // script specifique a la rape, hérite d'ustensile pour avoir acces aux fonctions OnDrop()... 
{

    [Header("Unique à la rape")]
    [SerializeField] private Transform refPlaque;

    private Vector3 lastMousePos; // derniere position de la souris
    private float PosX; // valeur qui permet de manipuler si on va dans la bonne direction

    [SerializeField] private float tempsNecessaire;
    private float currentTempsPasse;

    #region Drag
    private TargetJoint2D plaqueTargetJoint;
    [SerializeField, Range(0f, 100f)] float frequenceJoint = 5f; // frequence a laquel l'objet essaye de réetablir la distance avec la target
    [SerializeField, Range(0f, 100f)] float dampingJoint = 1f; // vitesse qui est réduit a chaque fréquence
    #endregion


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        etatApresTransformation = enumEtatIgredient.Rape; // vus que c'est le script Rape, l'etat de transformation sera rapé 
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

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        Destroy(plaqueTargetJoint); plaqueTargetJoint = null;
        refPlaque.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
    }
    public override void OnMouseDrag()
    {
        if(inManipulation)
        {
            base.OnMouseDrag();
            
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
                

                if (currentTempsPasse >= tempsNecessaire)
                {
                    FinishManipulation();
                    refPlaque.DOLocalMoveX(0, 0.5f);
                    currentTempsPasse = 0;
                    AudioManager.instanceAM.Play("Finish");
                }
            }


            lastMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        }


    }
}
