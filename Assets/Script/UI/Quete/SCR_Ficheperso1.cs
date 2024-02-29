using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SCR_Ficheperso1 : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] GameObject FullPic;
    [SerializeField] SpriteRenderer profilMini;
    [SerializeField] SpriteRenderer profilMaxi;
    [SerializeField] GameObject Maxi;
    private bool canMove = true;
    [SerializeField] SO_Personnage perso;
    private SortingGroup spriteRender;
    [SerializeField] SCR_HexagoneStat hexStat;

    private Vector3 initialScale;
   

    private void Awake()
    {
        spriteRender = GetComponent<SortingGroup>();
        initialScale = transform.localScale;
    }

    void Start()
    {
        hexStat.UpdateStat(perso.dicoResistance,true);
        
        mainCamera = Camera.main;
    }



    private void OnMouseDrag()
    {
        if (!canMove) { return; }

        hexStat.UpdateLine();
        transform.position = new Vector3 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,0) ;
        

    }

    private void OnMouseDown()
    {
        if (!canMove) { return; }

        spriteRender.sortingOrder = spriteRender.sortingOrder + 1;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));
        AudioManager.instanceAM.Play("FichePerso");


        if (rayHit.transform.GetComponent<SCR_QueteTableau>())
        {
            SCR_QueteTableau mQuete = rayHit.transform.GetComponent<SCR_QueteTableau>();
           
            mQuete.pickUp(this);
            
        }
  
    }

    private void OnMouseUp()
    {
        spriteRender.sortingOrder = spriteRender.sortingOrder-1;
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose
       
        if (rayHit.transform.GetComponent<SCR_QueteTableau>())
        {
            SCR_QueteTableau mQuete = rayHit.transform.GetComponent<SCR_QueteTableau>();
            mQuete.OnDrop(this);
            hexStat.UpdateLine();
            AudioManager.instanceAM.Play("FichePersoLacher");


        }
       

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void MakeSmall(bool etat)
    {
        if(etat)
        {
            transform.localScale = initialScale / 2.5f;
            FullPic.SetActive(true);
            Maxi.SetActive(false);
        }
        else
        {
            transform.localScale  = initialScale;
            FullPic.SetActive(false);
            Maxi.SetActive(true);
        }
       
    }

    private void UpdatePerso()
    {
        profilMaxi.sprite = perso.profil;
        profilMini.sprite = perso.profil;
    }

    public void SetSoPerso(SO_Personnage persoParameter)
    {
        perso = persoParameter;
        UpdatePerso();
    }


    public void SetCanMove(bool canMoveParameter) { canMove = canMoveParameter; }
    public SO_Personnage GetSoPerso() { return perso; }
}
