using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "Tuto", menuName = "ScriptableObjects/Tutoriel")]

public class SO_Tuto : ScriptableObject
{
    public string txtExplication;
    public Sprite imageExplicative;
    public VideoPlayer videoGIF;

}
