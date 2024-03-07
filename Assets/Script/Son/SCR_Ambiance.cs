using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Ambiance : MonoBehaviour
{
    [SerializeField] string sound;
    // Start is called before the first frame update
    void Start()
    {

        AudioManager.instanceAM.Play(sound);
        AudioManager.instanceAM.Play(sound);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
