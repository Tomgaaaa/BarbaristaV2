using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VNsup;
using static SCR_QueteManager;

public class SCR_PlayerController : MonoBehaviour, ISerializationCallbackReceiver
{

    [SerializeField] StoryReader storyReader;
    [SerializeField] SCR_MasterCompendium masterCompendium;

    SCR_PauseMenu pauseMenu;


    #region dico 

    [System.Serializable] public class dicoIngredientPrefabClass : TemplateDico<enumAllIgredient, GameObject> { };
    private Dictionary<enumAllIgredient, GameObject> dicoIngredientPrefab; // dico listant les persos a afficher selon le jour
    [SerializeField] private List<dicoIngredientPrefabClass> listDicoIngredientPrefab;


    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoIngredientPrefab = new Dictionary<enumAllIgredient, GameObject>();

        foreach (dicoIngredientPrefabClass item in listDicoIngredientPrefab)
        {
            if (!dicoIngredientPrefab.ContainsKey(item.key))
            {
                dicoIngredientPrefab.Add(item.key, item.value);
            }
        }
    }

    #endregion

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
                SCR_Ingredient ingredientClick = rayHit.transform.GetComponent<SCR_Ingredient>();

                masterCompendium.OpenComp();
                masterCompendium.GoToPage(dicoIngredientPrefab[ingredientClick.GetCR_SO_Ingredient().myEnumIngredientSO]);
            }


        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && SceneManager.GetActiveScene().name == "SCE_Cuisine" && masterCompendium.GetIsOpen())
        { 
            masterCompendium.NextPage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneManager.GetActiveScene().name == "SCE_Cuisine" && masterCompendium.GetIsOpen())
        {
            masterCompendium.PrevPage();
        }
        #endregion
    }

   
}
