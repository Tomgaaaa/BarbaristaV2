using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_SceneManager : MonoBehaviour
{
    



    public void ChangeScene(int idSceneParameter)
    {
        SceneManager.LoadScene(idSceneParameter);
    }
}
