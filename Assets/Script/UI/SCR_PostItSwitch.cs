using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_PostItSwitch : MonoBehaviour
{
    
    [SerializeField] GameObject _pp1;
    [SerializeField] GameObject _pp2;

    [Header("List P1")]
    [SerializeField] List<Image> _pointP1Feu;
    [SerializeField] List<Image> _pointP1Toxique;
    [SerializeField] List<Image> _pointP1Glace;
    [SerializeField] List<Image> _pointP1Elec;
    [SerializeField] List<Image> _pointP1Hemo;
    [SerializeField] List<Image> _pointP1Leta;

    [Header("List P2")] 
    [SerializeField] List<Image> _pointP2Feu;
    [SerializeField] List<Image> _pointP2Toxique;
    [SerializeField] List<Image> _pointP2Glace;
    [SerializeField] List<Image> _pointP2Elec;
    [SerializeField] List<Image> _pointP2Hemo;
    [SerializeField] List<Image> _pointP2Leta;

    public Dictionary<string, List<Image>> FindValue = new Dictionary<string, List<Image>>();
  

    private void Start()
    {
        FindValue.Add("_FeuP1", _pointP1Feu);
        FindValue.Add("_ToxiqueP1", _pointP1Toxique);
        FindValue.Add("_GlaceP1", _pointP1Glace);
        FindValue.Add("_ElecP1", _pointP1Elec);
        FindValue.Add("_HemoP1", _pointP1Hemo);
        FindValue.Add("_LetaP1", _pointP1Leta);

        FindValue.Add("_FeuP2", _pointP2Feu);
        FindValue.Add("_ToxiqueP2", _pointP2Toxique);
        FindValue.Add("_GlaceP2", _pointP2Glace);
        FindValue.Add("_ElecP2", _pointP2Elec);
        FindValue.Add("_HemoP2", _pointP2Hemo);
        FindValue.Add("_LetaP2", _pointP2Leta);
    }
    public void SwitchPostIt(int _pp)
    {
        //Change le post it en dessous de la quête.
        if(_pp == 1)
        {
            _pp1.SetActive(true);
            _pp2.SetActive(false);
           
        }
        else if(_pp == 2)
        {
            _pp1.SetActive(false);
            _pp2.SetActive(true);
            
        }
    }

    public void SelectStats(int _id)
    {
        List<Image> listSelec = new List<Image>();
        Color _color = Color.white;

        if(_id <= 5)
        {
            foreach (Image _image in _pointP1Feu)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id, _pointP1Feu, Color.red);
        }
        else if(_id > 5 && _id <= 10)
        {
            foreach (Image _image in _pointP1Toxique)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id-5, _pointP1Toxique, new Color32(197, 25, 191,255));
        }
        else if(_id > 10 && _id <= 15)
        {
            foreach (Image _image in _pointP1Hemo)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 10, _pointP1Hemo, new Color32(161,10,10,255));
        }
        else if(_id > 15 && _id <= 20)
        {
            foreach (Image _image in _pointP1Glace)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 15, _pointP1Glace, Color.blue);
        }
        else if (_id > 20 && _id <= 25)
        {
            foreach (Image _image in _pointP1Elec)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 20, _pointP1Elec, Color.yellow);
        }
        else if (_id > 25 && _id <= 30)
        {
            foreach (Image _image in _pointP1Leta)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 25, _pointP1Leta, new Color32(9,10,136,255));
        }

        /*
                switch(_id)
                {
                    case "thermique": listSelec = _pointP1Feu;
                        _color = Color.red;
                        break;
                    case "cryo": listSelec = _pointP1Glace;
                        _color = Color.blue;
                        break;
                    case "toxic": listSelec = _pointP1Toxique;
                        _color = new Color(197, 25, 191, 255) ;
                        break;
                    case "Leta": listSelec = _pointP1Leta;
                        _color = Color.red;
                        break;
                    case "Hemo": listSelec = _pointP1Hemo;
                        _color = Color.red;
                        break;
                    case "Elec": listSelec = _pointP1Elec;
                        _color = Color.red;
                        break;
        */



    }

    public void SelectStatsP2(int _id)
    {
        List<Image> listSelec = new List<Image>();
        Color _color = Color.white;

        if (_id <= 5)
        {
            foreach (Image _image in _pointP2Feu)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id, _pointP2Feu, Color.red);
        }
        else if (_id > 5 && _id <= 10)
        {
            foreach (Image _image in _pointP2Toxique)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 5, _pointP2Toxique, new Color32(197, 25, 191, 255));
        }
        else if (_id > 10 && _id <= 15)
        {
            foreach (Image _image in _pointP2Hemo)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 10, _pointP2Hemo, new Color32(161, 10, 10, 255));
        }
        else if (_id > 15 && _id <= 20)
        {
            foreach (Image _image in _pointP2Glace)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 15, _pointP2Glace, Color.blue);
        }
        else if (_id > 20 && _id <= 25)
        {
            foreach (Image _image in _pointP2Elec)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 20, _pointP2Elec, Color.yellow);
        }
        else if (_id > 25 && _id <= 30)
        {
            foreach (Image _image in _pointP2Leta)
            {
                _image.color = Color.white;
            }
            ChangeColoButton(_id - 25, _pointP2Leta, new Color32(9, 10, 136, 255));
        }
    }

    private void ChangeColoButton(int _idButton, List<Image> listSelect, Color color)
    {
        for(int i = 0; i< _idButton;i ++)
        {
            listSelect[i].color = color;
        }
    }

    public void ResetColor(string _list)
    {
        foreach (Image _image in FindValue[_list])
        {
            _image.color = Color.white;
        }
    }

}
