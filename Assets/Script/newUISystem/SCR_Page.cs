using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Page : SCR_PoolItem
{
    
    public void Start()
    {
        
    }

    public void GoToPage(GameObject ePage)
    {
        SCR_MasterCompendium.instanceMComp.GoToPage(ePage);
        
    }
   public void NextPage()
    {
        SCR_MasterCompendium.instanceMComp.NextPage();
        
    }
    public void PrevPage()
    {
        SCR_MasterCompendium.instanceMComp.PrevPage();
    }
    public void QuitComp()
    {
        SCR_MasterCompendium.instanceMComp.CloseComp();
    }
}
