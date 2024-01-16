using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Compendium_Child : MonoBehaviour
{
    SCR_Compendium parent;

    private void Start()
    {
        parent = GetComponentInParent<SCR_Compendium>();
        
    }

    public  void NextPageChild()
    {
        parent.NextPage();
    }

    public void PreviewPageChild()
    {
        parent.PreviewPage();
    }

    public void GoToChild(int page)
    {
        parent.GoToPage(page);
    }

}
