using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_ButtonChoice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SkipVN()
    {
        if(SCR_DATA.instanceData.GetEtapeQuete() == 1)
        {
            SceneManager.LoadScene(0);
            SCR_DATA.instanceData.EtapeQueteUp();
            SCR_DATA.instanceData.EtapePersoUp();
        }
        else
        {
            SceneManager.LoadScene(1);
            SCR_DATA.instanceData.EtapeQueteUp();
            SCR_DATA.instanceData.EtapePersoUp();
            SCR_DATA.instanceData.JourUP();
        }


        
    }

   
}
