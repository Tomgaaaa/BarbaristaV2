using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Compendium : MonoBehaviour
{
    [SerializeField] private List<GameObject> _page;
    [SerializeField]private int _currentPage;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _simpleFlipPage;

    public virtual void NextPage()
    {
        _audio.PlayOneShot(_simpleFlipPage);

        _page[_currentPage].SetActive(false);
        _page[_currentPage + 1].SetActive(true);
        _currentPage++;
        
    }

    public virtual void PreviewPage()
    {
        _audio.PlayOneShot(_simpleFlipPage);
        _page[_currentPage].SetActive(false);
        _page[_currentPage - 1].SetActive(true);
        _currentPage--;
    }

    public virtual void GoToPage(int page)
    {
        _audio.PlayOneShot(_simpleFlipPage);
        _page[_currentPage].SetActive(false);
        _page[page].SetActive(true);
        _currentPage = page;
    }
}
