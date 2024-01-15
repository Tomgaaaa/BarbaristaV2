using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Compendium : MonoBehaviour
{
    [SerializeField] private List<GameObject> _page;
    [SerializeField]private int _currentPage;

    public virtual void NextPage()
    {
        _page[_currentPage].SetActive(false);
        _page[_currentPage + 1].SetActive(true);
    }

    public virtual void PreviewPage()
    {
        _page[_currentPage - 1].SetActive(true);
    }

    public virtual void GoToPage(int page)
    {

    }
}
