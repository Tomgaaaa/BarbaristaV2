using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_PlayerController : MonoBehaviour
{

    [SerializeField] SCR_StoryReader storyReader;
    [SerializeField] SCR_MasterCompendium masterCompendium;

    SCR_PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = GetComponentInChildren<SCR_PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {





            if(SceneManager.GetActiveScene().buildIndex == 1 && masterCompendium.GetIsOpen())
            {
                masterCompendium.CloseComp();
                
            }
            else if (pauseMenu.GetInOption())
            {
                pauseMenu.LeaveOption();
            }
            else
            {
                pauseMenu.Pause();
            }

            //pause
        }



        #region VN
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 4)  
        {
            storyReader.Next();
        }
        if (Input.GetKey(KeyCode.LeftControl) && SceneManager.GetActiveScene().buildIndex == 4)
        {
            storyReader.Next();
        }
        #endregion


        #region Compendium
        if (Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (masterCompendium.GetIsOpen())
            {
                masterCompendium.CloseComp();
            }
            else
            {
                masterCompendium.OpenComp();

            }
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) && SceneManager.GetActiveScene().buildIndex == 1 && masterCompendium.GetIsOpen())
        { 
            masterCompendium.NextPage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            masterCompendium.PrevPage();
        }
        #endregion
    }
}
