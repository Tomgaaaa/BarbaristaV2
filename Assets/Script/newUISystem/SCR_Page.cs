using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Page : SCR_PoolItem
{

    [SerializeField] private List<Button> sociogrammeButton;


    public void Start()
    {
        foreach (Button button in sociogrammeButton)
        {
            button.interactable = false;

        }
    }

    private void OnEnable()
    {
        if(SCR_DATA.instanceData.GetJour() == 3 && sociogrammeButton !=null)
        {
            foreach(Button button in sociogrammeButton)
            {
                button.interactable = true;

            }

        }
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
