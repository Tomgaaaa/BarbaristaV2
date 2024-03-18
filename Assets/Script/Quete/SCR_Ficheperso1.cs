using DG.Tweening;
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
    Tweener startTweener;


    [SerializeField] private Transform shadow;
    private Tweener tweenerScale;
  

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
    
    private void Update()
    {
        hexStat.UpdateLine();
    }

    private void OnMouseDrag()
    {
        if (!canMove) { return; }


        SCR_Cursor.instanceCursor.ChangeHoverOn(true);

        hexStat.UpdateLine();
        transform.position = new Vector3 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,0) ;
        

    }

    private void OnMouseEnter()
    {
        SCR_Cursor.instanceCursor.ChangeHoverOff(true);
    }

    private void OnMouseExit()
    {
        SCR_Cursor.instanceCursor.ChangeClickOff(false);
    }


    private void OnMouseDown()
    {
        if (!canMove) { return; }

        SCR_Cursor.instanceCursor.ChangeHoverOn(true);
        //tweenerScale = transform.DOScale(new Vector3(initialScale.x * 1.1f, initialScale.y * 1.1f, initialScale.z * 1.1f), 1f);
        SCR_QueteManager.instanceQueteManager.ShowHiglightForAllQuest(true);

        shadow.DOLocalMove(new Vector3(1,-1,0), 0.5f);


        startTweener.Kill();
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
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //tweenerScale = transform.DOScale(new Vector3(initialScale.x, initialScale.y, initialScale.z), 1f);

        SCR_QueteManager.instanceQueteManager.ShowHiglightForAllQuest(false);


        shadow.DOLocalMove(Vector3.zero, 0.5f);


        spriteRender.sortingOrder = spriteRender.sortingOrder-1;
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose
       
        if (rayHit.transform.GetComponent<SCR_QueteTableau>())
        {
            tweenerScale.Kill();

            SCR_QueteTableau mQuete = rayHit.transform.GetComponent<SCR_QueteTableau>();
            mQuete.OnDrop(this);
            hexStat.UpdateLine();
            AudioManager.instanceAM.Play("FichePersoLacher");



        }
       

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void MakeSmall(bool etat)
    {


        if (etat)
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

    public void SetTweener(Tweener tweenerParameter) => startTweener = tweenerParameter;
    public Tweener GetTweener()
    {
        return startTweener;
    }
    public void SetCanMove(bool canMoveParameter) { canMove = canMoveParameter; }
    public SO_Personnage GetSoPerso() { return perso; }
}
