using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DATA : MonoBehaviour
{

    public static SCR_DATA instanceData;

    [SerializeField] private List<SO_Quete> listCurrentQuete;

    public int etape;


    private void Awake()
    {
        if (instanceData == null)
            instanceData = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void SetListCurrentQuest(SO_Quete SoQueteParameter)
    {
        listCurrentQuete.Add(SoQueteParameter);
    }

    public List<SO_Quete> GetCurrentQuete() { return listCurrentQuete; }
    public int GetEtape() {  return etape; }
}
