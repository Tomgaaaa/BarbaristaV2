using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_PauseMenu : MonoBehaviour
{

    bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI, OptionUI, CreditUI;
    [SerializeField] GameObject pauseFirstButton, optionsFirstButton, creditFirtButton;
    GameObject lastSelected;

    [SerializeField] SCR_Options options;
    bool inOption = false;

    AudioManager audioManager;

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
        //audioManager = FindObjectOfType<AudioManager>();
        //audioManager.Play("Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("SCE_Menu");
    }


    public bool GetInOption() {  return inOption; }


}
