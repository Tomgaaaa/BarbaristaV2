using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "InfoPopUp",menuName = "ScriptableObjects/PopUp")]

public class SO_PopUp : ScriptableObject
{
   public string textInfo;
    public Vector2 position;
    public Vector2 dimension;

}
