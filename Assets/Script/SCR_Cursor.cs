using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Cursor : MonoBehaviour
{
    [SerializeField] private Sprite _sCursorHoverOff;
    [SerializeField] private Sprite _sCursorHoverON;
    [SerializeField] private Sprite _sCursorOff;
    [SerializeField] private Sprite _sCursorON;

    private Camera mainCam;
    private SpriteRenderer myRenderer;

    public static SCR_Cursor instanceCursor;

    private Vector2 cursorOffset = new Vector2(0.1f, -0.2f);

    public bool overrideUpdate = false;

    public float mutiplicateurZoom;

    private void Awake()
    {
        if (instanceCursor == null)
            instanceCursor = this;
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        mainCam = Camera.main;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Cursor.visible = false;
    }

    private void Update()
    {


        if(Input.GetKeyDown(KeyCode.Mouse0) && !overrideUpdate) 
        {
            myRenderer.sprite = _sCursorON;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !overrideUpdate)
        {
            myRenderer.sprite = _sCursorOff;
        }


        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Input.mousePosition)); // cast pour avoir la world position de la souris

        transform.position = rayHit.point + cursorOffset;
    }

    public void ZoomCamera()
    {
       Vector3 newScale = new Vector3(transform.localScale.x * mutiplicateurZoom, transform.localScale.y * mutiplicateurZoom, transform.localScale.z* mutiplicateurZoom);
       transform.DOScale(newScale, 1f);



        //transform.localScale  = new Vector3(transform.localScale.x * mutiplicateurZoom, transform.localScale.y * mutiplicateurZoom, transform.localScale.z * mutiplicateurZoom);
    }

    public void DeZoomCamera()
    {
        Vector3 newScale = new Vector3(transform.localScale.x / mutiplicateurZoom, transform.localScale.y / mutiplicateurZoom, transform.localScale.z / mutiplicateurZoom);
        transform.DOScale(newScale, 1f);



        //transform.localScale = new Vector3(transform.localScale.x / mutiplicateurZoom, transform.localScale.y / mutiplicateurZoom, transform.localScale.z / mutiplicateurZoom);

    }


    public void ChangeHoverOn(bool overrideParameter) { overrideUpdate = overrideParameter; myRenderer.sprite = _sCursorHoverON; }
    public void ChangeHoverOff(bool overrideParameter) { overrideUpdate = overrideParameter; myRenderer.sprite = _sCursorHoverOff; }
    public void ChangeClickOn() { myRenderer.sprite = _sCursorON; }
    public void ChangeClickOff(bool overrideParameter) { overrideUpdate = overrideParameter; myRenderer.sprite = _sCursorOff; }
}
