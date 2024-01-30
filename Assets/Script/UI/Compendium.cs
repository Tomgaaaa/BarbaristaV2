using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{
    [SerializeField] SO_PageInformative actualPage;
    [SerializeField] PageSimpleView view;


    public void PrevPage()
    {
        actualPage = actualPage.pagePrecedante;
        view.SetPage(actualPage);
        AudioManager.instanceAM.Play("SwiftPage");
    }

    public void NextPage()
    {
        actualPage = actualPage.pageSuivante;
        view.SetPage(actualPage);
    }
    public void SetPage(SO_PageInformative newPage)
    {
        actualPage = newPage;
        view.SetPage(actualPage);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
