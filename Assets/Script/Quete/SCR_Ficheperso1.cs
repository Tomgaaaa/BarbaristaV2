using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;



public class SCR_Ficheperso1 : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] GameObject FullPic;
    [SerializeField] SpriteRenderer profilMini;
    [SerializeField] SpriteRenderer profilMaxi;
    [SerializeField] GameObject Maxi;
    [SerializeField] TextMeshProUGUI nomPerso;
    [SerializeField] Canvas canvas;

    private bool canMove = true;
    [SerializeField] SO_Personnage perso;
    private SortingGroup spriteRender;
    [SerializeField] SCR_HexagoneStat hexStat;

    private Vector3 initialScale;
    Tweener startTweener;


    [SerializeField] private Transform shadow;
    private Tweener tweenerScale;
  
    private SCR_QueteTableau queteHoverRaycast= null;
    private List<Collider2D> lastFichePerso= new List<Collider2D>(); // list des ficher persos ou on passe dessus, pour pouvoir re activer leur collider
    [SerializeField] private SpriteRenderer outlineRenderer;
    [SerializeField] private LayerMask layerMaskDrag;
    private Collider2D myCollider;


    private Vector3 diffMousePos;
    private Vector2 ScaleRayCast = new Vector3(21f,29f);
    private Vector2 initialScaleRayCast = new Vector3(21f,29f);




    private void Awake()
    {
        
        spriteRender = GetComponent<SortingGroup>();
        myCollider = GetComponent<Collider2D>();
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


        //RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition), float.MaxValue,layerMaskDrag ); // cast pour avoir la world position de la souris

        /*if (rayHit)
        {
            if (rayHit.transform.GetComponent<SCR_QueteTableau>())
            {

                


                queteHoverRaycast = rayHit.transform.GetComponent<SCR_QueteTableau>();


                if (queteHoverRaycast.GetDicoPerso()[0] == null && queteHoverRaycast.GetHigher()|| queteHoverRaycast.GetDicoPerso()[1] == null && queteHoverRaycast.GetHigher())
                {
                    transform.SetParent(rayHit.transform, false);

                    MakeSmall(true);
                    transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x  , mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);

                }
                else
                {
                    queteHoverRaycast = null;

                    transform.SetParent(null, false);
                    MakeSmall(false);

                    transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);
                }
                
            }
            else if(rayHit.transform.GetComponent<SCR_Ficheperso1>())   
            {

                SCR_Ficheperso1 ficheDessous = rayHit.transform.GetComponent<SCR_Ficheperso1>();

                Collider2D colliderFicheDessous = ficheDessous.GetComponent<Collider2D>();

                if(colliderFicheDessous != null)
                {
                    Debug.Log(colliderFicheDessous);
                    lastFichePerso.Add(colliderFicheDessous);

                    colliderFicheDessous.enabled = false;
                }
                


            }
            else 
            {

                queteHoverRaycast = null;

                transform.SetParent(null, false);

                MakeSmall(false);

                transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);


            }
            

        }*/


        Collider2D squareCollider = Physics2D.OverlapBox(transform.position, ScaleRayCast,0,layerMaskDrag) ;
        if(squareCollider)
        {
            if (squareCollider.transform.GetComponent<SCR_QueteTableau>())
            {




                queteHoverRaycast = squareCollider.transform.GetComponent<SCR_QueteTableau>();


                if (queteHoverRaycast.GetDicoPerso()[0] == null && queteHoverRaycast.GetHigher() || queteHoverRaycast.GetDicoPerso()[1] == null && queteHoverRaycast.GetHigher())
                {
                    transform.SetParent(squareCollider.transform, false);

                    MakeSmall(false);
                    transform.localScale = new Vector3(initialScale.x * 0.7f, initialScale.y * 0.7f, initialScale.z);
                    ScaleRayCast = new Vector2(initialScaleRayCast.x * 1.25f, initialScaleRayCast.y * 1.25f);

                    transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);

                }
                else
                {
                    queteHoverRaycast = null;

                    transform.SetParent(null, false);
                    //MakeSmall(false);

                    transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);
                }

            }
            /*else if (squareCollider.transform.GetComponent<SCR_Ficheperso1>())
            {
                transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);

                SCR_Ficheperso1 ficheDessous = squareCollider.transform.GetComponent<SCR_Ficheperso1>();

                BoxCollider2D colliderFicheDessous = ficheDessous.GetComponent<BoxCollider2D>();

                if (!lastFichePerso.Contains(colliderFicheDessous))
                {
                    lastFichePerso.Add(colliderFicheDessous);

                    colliderFicheDessous.enabled = false;
                }



            }*/
            else // si ya ni quete ni fiche perso
            {

                queteHoverRaycast = null;

                transform.SetParent(null, false);

                MakeSmall(false);

                ScaleRayCast = new Vector2(initialScaleRayCast.x, initialScaleRayCast.y);


                transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x - diffMousePos.x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y - diffMousePos.y, 0);


            }
        }
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position, new Vector2(ScaleRayCast.x,ScaleRayCast.y));
    }

#endif

    private void OnMouseEnter()
    {
        SCR_Cursor.instanceCursor.ChangeHoverOff(true);
        outlineRenderer.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        SCR_Cursor.instanceCursor.ChangeClickOff(false);
        outlineRenderer.gameObject.SetActive(false);
    }


    private void OnMouseDown()
    {
        if (!canMove) { return; }

        Vector3 distanceMousePosition;
        distanceMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diffMousePos = distanceMousePosition ;

        SCR_Cursor.instanceCursor.ChangeHoverOn(true);
        //tweenerScale = transform.DOScale(new Vector3(initialScale.x * 1.1f, initialScale.y * 1.1f, initialScale.z * 1.1f), 1f);
        SCR_QueteManager.instanceQueteManager.ShowHiglightForAllQuest(true);

        shadow.DOLocalMove(new Vector3(1,-1,0), 0.5f);


        startTweener.Kill();
        spriteRender.sortingOrder = spriteRender.sortingOrder + 1;
        canvas.sortingOrder = canvas.sortingOrder + 1;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));
        AudioManager.instanceAM.Play("FichePerso");
        myCollider.enabled = false;

        if (rayHit.transform.GetComponent<SCR_QueteTableau>())
        {
            SCR_QueteTableau mQuete = rayHit.transform.GetComponent<SCR_QueteTableau>();
           
            mQuete.pickUp(this);
            
        }
  
    }

    private void OnMouseUp()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SCR_Cursor.instanceCursor.ChangeHoverOff(true);

        //tweenerScale = transform.DOScale(new Vector3(initialScale.x, initialScale.y, initialScale.z), 1f);

        SCR_QueteManager.instanceQueteManager.ShowHiglightForAllQuest(false);

    
            


        shadow.DOLocalMove(Vector3.zero, 0.5f);


        spriteRender.sortingOrder = spriteRender.sortingOrder-1;
        canvas.sortingOrder = canvas.sortingOrder - 1;

        AudioManager.instanceAM.Play("FichePersoLacher");
        //RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose

        Collider2D rayHit = Physics2D.OverlapBox(transform.position, ScaleRayCast, 0, layerMaskDrag);


        if (rayHit.transform.GetComponent<SCR_QueteTableau>())
        {
            tweenerScale.Kill();

            SCR_QueteTableau mQuete = rayHit.transform.GetComponent<SCR_QueteTableau>();
            mQuete.OnDrop(this);
            hexStat.UpdateLine();
            AudioManager.instanceAM.Play("FichePersoLacher");

            ScaleRayCast = new Vector2(initialScale.x * 5, initialScale.y * 7);

        }
        
        


        myCollider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("FichePerso");
    }


   

    public void MakeSmall(bool isSmall)
    {


        if (isSmall)
        {
            // transform.localScale = initialScale / 2.5f;
            transform.localScale = new Vector3(0.23f, 0.23f, 0);
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
        nomPerso.text = perso.namePerso;
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
