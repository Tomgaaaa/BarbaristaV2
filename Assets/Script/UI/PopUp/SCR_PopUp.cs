using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SCR_PopUp : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI textInfo;
    private bool startTimer;
    private float timeToActivate;

    private Vector3 offset;
   
    private void Update()
    {
        gameObject.transform.position = Input.mousePosition + offset;
        
    }

    public void ActivateFromBiome(string popInfoText)
    {
        Activate(popInfoText, Vector3.zero);

    }

    public void Activate(string popInfoTxt, Vector3 offsetParamater)
    {
        if (Input.mousePosition.x > Screen.width/2)
        {
            offset = offsetParamater;
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);
           // CreateSprite(gameObject.GetComponent<Image>().sprite, new Vector2(1, 1));
        }
        else
        {
            offset = offsetParamater;
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
            // CreateSprite(gameObject.GetComponent<Image>().sprite, new Vector2(0, 1));
        }
        gameObject.SetActive(true);
        gameObject.transform.position = Input.mousePosition + offset;
        textInfo.text = popInfoTxt;
    }
    public void Desactivate()
    {
        gameObject.SetActive(false);
    }

    public void CallPopUp(string infoDiff)
    {
        SCR_QueteManager.instanceQueteManager.PopUpReader(infoDiff);
    }

    void CreateSprite(Sprite actualSprite, Vector2 pivot)
    {
        Sprite.Create(actualSprite.texture, actualSprite.rect, pivot);
    }
}
