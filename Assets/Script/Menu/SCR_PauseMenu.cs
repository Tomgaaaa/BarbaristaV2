using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_PauseMenu : MonoBehaviour
{
    public static SCR_PauseMenu instancePauseMenu;



    bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI, OptionUI, CreditUI;
    [SerializeField] GameObject pauseFirstButton, optionsFirstButton, creditFirtButton;
    GameObject lastSelected;

    [SerializeField] SCR_Options options;
    bool inOption = false;


    private void Awake()
    {
        if (instancePauseMenu == null)
            instancePauseMenu = this;
        else
            Destroy(gameObject);




    }

    public void Pause()
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);


        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }



    public void Option()
    {
        inOption = true;
        options.UpdateValue();
        pauseMenuUI.SetActive(false);
        OptionUI.gameObject.SetActive(true);
        lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);

    }

    public void Credit()
    {
        pauseMenuUI.SetActive(false);
        CreditUI.gameObject.SetActive(true);
        lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(creditFirtButton);
    }

    public void LeaveOption()
    {
        inOption = false;
        EventSystem.current.SetSelectedGameObject(lastSelected);
        OptionUI.SetActive(false);
        CreditUI.SetActive(false);
        pauseMenuUI.SetActive(true);

       

    }

    




    public void GoMenu()
    {

        Time.timeScale = 1f;
        AudioManager.instanceAM.FadeOut("BarAlatea", 0, 4.5f);
        AudioManager.instanceAM.FadeOut("CuisineAlatea", 0, 4.5f);
        AudioManager.instanceAM.Play("Menu");

        if (SCR_TutoManager.instanceTuto != null)
            SCR_TutoManager.instanceTuto.gameObject.SetActive(false);

        SceneManager.LoadScene("SCE_Menu");
    }


    public bool GetInOption() {  return inOption; }
    public bool GetIsPause() {  return GameIsPaused; }


}
