using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PostItSwitch : MonoBehaviour
{
    
    [SerializeField] GameObject _pp1;
    [SerializeField] GameObject _pp2;


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
}
