using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCR_SceneManager : MonoBehaviour
{

    [SerializeField] private Dropdown dropdownUI;
    int sceneIndex;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    public void ChangeScene(int idSceneParameter)
    {
        SceneManager.LoadScene(idSceneParameter);
    }

    public void DropdownChangement()
    {
        switch (dropdownUI.value)
        {
            case 0:
                if (sceneIndex != 0)
                {
                    sceneIndex = 0;
                    SceneManager.LoadScene(0);
                }
                break;

            case 1:
                if (sceneIndex != 1)
                {
                    sceneIndex = 1;
                    SceneManager.LoadScene(1);
                }
                break;

            case 2:
                if (sceneIndex != 2)
                {
                    sceneIndex = 2;
                    SceneManager.LoadScene(2);
                }
                break;

            case 3:
                if (sceneIndex != 3)
                {
                    sceneIndex = 3;
                    SceneManager.LoadScene(3);
                }
                break;

        }
    }
}
