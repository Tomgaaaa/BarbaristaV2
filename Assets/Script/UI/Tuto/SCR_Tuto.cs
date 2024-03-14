using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Tuto : MonoBehaviour
{

    [SerializeField] private SO_Tuto so_tuto;
    [SerializeField] private Image imageTuto; // en serialize car si je fais getComponenet ça me chope le BG
    [SerializeField] private Text textTuto;
    private Button buttonTuto;

    // Start is called before the first frame update
    void Start()
    {
        textTuto = GetComponentInChildren<Text>();
        buttonTuto = GetComponentInChildren<Button>();

        Initialisation(so_tuto,true);


    }


    public void Initialisation(SO_Tuto soTuto, bool callUnlock)
    {
        so_tuto = soTuto;
        imageTuto.sprite = so_tuto.imageExplicative;
        textTuto.text = so_tuto.txtExplication;

        if(callUnlock)
        {
            Invoke("UnlockButton", 1);
        }
    }

    private void UnlockButton() // fonction pour empecher de spam click
    {
        buttonTuto.interactable = true;
    }

    public void ButtonClose() // fonction appeller par le bouton omnipresent pour fermer le tuto
    {
        SCR_TutoManager.instanceTuto.ValidTuto(so_tuto);
        Destroy(gameObject);
    }
}
