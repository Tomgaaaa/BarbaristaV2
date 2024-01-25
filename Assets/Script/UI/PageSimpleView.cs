using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageSimpleView : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text titre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetPage(SO_PageInformative pageInfo)
    {
        image.sprite = pageInfo.illustration;
        titre.text = pageInfo.titre;
    }
}
