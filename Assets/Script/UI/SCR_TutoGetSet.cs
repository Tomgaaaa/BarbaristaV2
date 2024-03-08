using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SCR_TutoGetSet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI explication;
    [SerializeField] SpriteRenderer imageExp;
    public SO_Tuto tuto;
    // Start is called before the first frame update
    void Awake()
    {
        explication.text = tuto.txtExplication;
        imageExp.sprite = tuto.imageExplicative;
    }

 
}
