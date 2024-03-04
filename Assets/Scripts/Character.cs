using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Display2D
{
    public SOCharacter details => definition;

    [SerializeField] SOCharacter definition;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = definition.tag;
        mainImage.sprite = definition.GetDefaultFace();
    }
}
