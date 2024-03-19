using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VNsup;

public class SCR_PlayerController : MonoBehaviour
{

    [SerializeField] StoryReader storyReader;
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



            if (SceneManager.GetActiveScene().name == "SCE_Cuisine")
            {
                if (masterCompendium.GetIsOpen())
                {
                    masterCompendium.CloseComp();

                }

            }


            if (pauseMenu.GetInOption())
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
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 2)  
        {
            storyReader.Next();
        }
        if (Input.GetKey(KeyCode.LeftControl) && SceneManager.GetActiveScene().buildIndex == 2)
        {
            storyReader.Next();
        }
        #endregion


        #region Compendium
        if (Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().name  == "SCE_Cuisine")
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




        if (SceneManager.GetActiveScene().name == "SCE_Cuisine" && Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); // créer un Cast pour savoir si on a relaché l'ingrédient sur quelque chose


            if (masterCompendium.GetIsOpen())
            {
                masterCompendium.CloseComp();
            }
            else if (rayHit.transform.GetComponent<SCR_Ingredient>())
            {
                Debug.Log("touche ingredient");
                SCR_Ingredient ingredientClick = rayHit.transform.GetComponent<SCR_Ingredient>();
                //masterCompendium.GoToPage()
            }


            masterCompendium.CloseComp();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && SceneManager.GetActiveScene().name == "SCE_Cuisine" && masterCompendium.GetIsOpen())
        { 
            masterCompendium.NextPage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneManager.GetActiveScene().name == "SCE_Cuisine")
        {
            masterCompendium.PrevPage();
        }
        #endregion
    }
}
