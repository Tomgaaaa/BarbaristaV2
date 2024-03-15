using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SCR_Menu : MonoBehaviour
{

    [SerializeField] GameObject Options;
    [SerializeField] GameObject Menu;




    [SerializeField] GameObject menuFirstButton, optionsFirstButton;

    [SerializeField] GameObject splashcreen;

    GameObject lastSelected;

    bool inOption;

    AudioManager audioManager;


    [SerializeField] GameObject Credit,creditButton;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        //EventSystem.current.SetSelectedGameObject(menuFirstButton);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SCE_Quete");
        AudioManager.instanceAM.Play("CliquerTableauQuete");
        AudioManager.instanceAM.Play("TransiTableauQuete");
        AudioManager.instanceAM.FadeOut("Menu",0, 2.5f);
        AudioManager.instanceAM.Play("ButtonPlay");
        // transition + changement de scene
    }



    //Code pour afficher les options.
    public void boutonOption()
    {
        Menu.SetActive(false);
        Options.SetActive(true);
        inOption = true;
        AudioManager.instanceAM.Play("ButtonOptions");
        
        lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        
    }


    public void Quit()
    {
        Application.Quit();
        AudioManager.instanceAM.Play("ButtonQuit");
        Debug.Log("J'ai quitté le jeu");
    }

    //Code pour le retour menu.
    public void Retourmenu()
    {

        inOption = false;
        Menu.SetActive(true);
        Options.SetActive(false);
        Credit.SetActive(false);
        AudioManager.instanceAM.Play("ButtonReturnMenu");
        EventSystem.current.SetSelectedGameObject(lastSelected);

    }


  


    public void CreditGame()
    {
        Credit.SetActive(true);
        Menu.SetActive(false);
        inOption = true; ;
        lastSelected = EventSystem.current.currentSelectedGameObject;
        AudioManager.instanceAM.Play("ButtonCredits");
        EventSystem.current.SetSelectedGameObject(creditButton);
    }


    public void SplashScreen()
    {
        splashcreen.SetActive(false);
        Menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirstButton);
    }

    public void Over()
    {
        AudioManager.instanceAM.Play("Random");
    }

}
