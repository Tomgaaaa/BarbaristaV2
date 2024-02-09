using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class SCR_Ficheperso1 : MonoBehaviour
{
    Camera mainCamera;
    bool OnQuest = false;
    [SerializeField] GameObject FullPic;
    [SerializeField] SpriteRenderer profilMini;
    [SerializeField] SpriteRenderer profilMaxi;
    [SerializeField] GameObject Maxi;
    bool cantPlace = false;
    [SerializeField] SO_Personnage perso;
    private SortingGroup spriteRender;
    [SerializeField] SCR_HexagoneStat hexStat;
   

    private void Awake()
    {
       
      

        spriteRender = GetComponent<SortingGroup>();
    }

    void Start()
    {
        hexStat.UpdateStat(perso.dicoResistance,true);
        
        mainCamera = Camera.main;
    }



    private void OnMouseDrag()
    {
        hexStat.UpdateLine();
        transform.position = new Vector3 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y,0) ;


    }

    private void OnMouseDown()
    {

        spriteRender.sortingOrder = spriteRender.sortingOrder + 1;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

  

        if(rayHit.transform.GetComponent<SCR_MasterQuete>())
        {
            SCR_MasterQuete mQuete = rayHit.transform.GetComponent<SCR_MasterQuete>();
            
            mQuete.pickUp(this);
        }
  
    }

    private void OnMouseUp()
    {
        spriteRender.sortingOrder = spriteRender.sortingOrder-1;
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose
       
        if (rayHit.transform.GetComponent<SCR_MasterQuete>())
        {
            SCR_MasterQuete mQuete = rayHit.transform.GetComponent<SCR_MasterQuete>();

            if (OnQuest == true )
            {      
                    mQuete.OnDrop(this);
                hexStat.UpdateLine();
            }
            else if(OnQuest != true && cantPlace == false)
            {
                
                OnQuest = true;
                   
                 mQuete.OnDrop(this);
                hexStat.UpdateLine();
            }
            else
            {
                Debug.Log("CantPlace");
            }
            
        }
        else
        {
            if(OnQuest == true)
            {
                OnQuest = false;
                
            }
            
        }

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void MakeSmall(bool etat)
    {
        if(etat == true)
        {
            gameObject.transform.localScale = gameObject.transform.localScale / 2.5f;
            FullPic.SetActive(true);
            Maxi.SetActive(false);
        }
        else
        {
            gameObject.transform.localScale = gameObject.transform.localScale * 2.5f;
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


}
