using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_PostItSwitch : MonoBehaviour
{
    
    [SerializeField] GameObject _pp1;
    [SerializeField] GameObject _pp2;
    [SerializeField] List<Image> _pointP1Feu;
    [SerializeField] List<Image> _pointP1Toxique;
    [SerializeField] List<Image> _pointP1Glace;
    [SerializeField] List<Image> _pointP1Elec;
    [SerializeField] List<Image> _pointP1Hemo;
    [SerializeField] List<Image> _pointP1Leta;



    public void SwitchPostIt(int _pp)
    {
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
            ChangeColoButton(_id, _pointP1Feu, Color.red);
        }
        else if(_id > 5 && _id <= 10)
        {
            ChangeColoButton(_id-5, _pointP1Toxique, new Color(197, 25, 191, 255));
        }
        else if(_id > 10 && _id <= 15)
        {
            ChangeColoButton(_id - 10, _pointP1Hemo, new Color(161,10,10,255));
        }
        else if(_id > 15 && _id <= 20)
        {
            ChangeColoButton(_id - 15, _pointP1Glace, Color.blue);
        }
        else if (_id > 20 && _id <= 25)
        {
            ChangeColoButton(_id - 20, _pointP1Elec, Color.yellow);
        }
        else if (_id > 25 && _id <= 30)
        {
            ChangeColoButton(_id - 25, _pointP1Leta, new Color(9,10,136,255));
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

    private void ChangeColoButton(int _idButton, List<Image> listSelect, Color color)
    {
        for(int i = 0; i< _idButton;i ++)
        {
            listSelect[i].color = color;
        }
    }

}
