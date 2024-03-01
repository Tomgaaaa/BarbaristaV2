using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DATA : MonoBehaviour
{

    public static SCR_DATA instanceData;

    [SerializeField] private List<SO_Quete> listCurrentQuete;

    private int etapeQuete; // index indiquant laquelle des 2 quetes on est en train de preparer
    private int etapePerso; // index indiquant quel perso on est en train de servir

    public int jour = 1;


    private void Awake()
    {
        if (instanceData == null)
            instanceData = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void SetListCurrentQuest(SO_Quete SoQueteParameter) { listCurrentQuete.Add(SoQueteParameter);}

    public SO_Quete GetCurrentQuete() { return listCurrentQuete[etapeQuete]; }
    public List<SO_Quete> GetListCurrentQuete() { return listCurrentQuete; }
    public int GetEtapeQuete() {  return etapeQuete; }
    public int GetEtapePerso() {  return etapePerso; }
    public int GetJour() {  return jour; }
    public void JourUP() {   jour++; }
    public void EtapePersoUp() // fonction appellé quand on a finit une boisson pour mettre a jour l'etape perso
    { 
        if(etapePerso == 0)
        {
            etapePerso++;
        }
        else if(etapePerso == 1)
        {
            etapePerso = 0; 
        }
    }

    public void EtapeQueteUp() // fonction appellé quand on a finit une boisson pour mettre a jour l'etape de quete 
    {
        if (etapeQuete == 0)
        {
            etapeQuete++;
        }
        else if(etapeQuete == 1 )
        {
            etapeQuete = 0;
        }
        else
            Debug.Log(" y a un bleme");
       
    }
}
