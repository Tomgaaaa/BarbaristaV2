using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DATA : MonoBehaviour
{

    public static SCR_DATA instanceData;

    [SerializeField] private List<SO_Quete> listCurrentQuete;

    private int etapeQuete; // index indiquant laquelle des 2 quetes on est en train de preparer
    private int etapePerso; // index indiquant quel perso on est en train de servir

    private int jour = 1; // le jour actuelle


    private void Awake() // sinbgleton + don't destroy pour qu'il recupere / garde les datas a travers les scenes
    {
        if (instanceData == null)
            instanceData = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void SetListCurrentQuest(SO_Quete SoQueteParameter) { listCurrentQuete.Add(SoQueteParameter);} // fonction appeller lorsquu'on valide le choix des persos, jsp pk j'ai pas pus transferer de liste doncj'appelle cette fonction 2 fois

    public SO_Quete GetCurrentQuest() { return listCurrentQuete[etapeQuete]; } // fonction pour avoir la quete actuelle entre les 2 choisis
    public List<SO_Quete> GetListCurrentQuest() { return listCurrentQuete; } // fonction pour recuperer les 2 quetes choisis
    public int GetEtapeQuest() {  return etapeQuete; } // fonction pour recuperer l'etape de la quete, si on est en train de faire la premiere ou la deuxieme quete
    public int GetEtapePerso() {  return etapePerso; } // fonction pour recuperer  l'etape du perso, si on sert le premier ou le deuxieme client de la quete
    public int GetJour() {  return jour; } // fonction pour recuperer le jour actuelle
    public void JourUP() {   jour++; }// fonction pour passer au jour suivant
    public void EtapePersoUp() // fonction appellé quand on a finit une boisson pour mettre a jour l'etape perso
    { 
        if(etapePerso == 0) // si on a servis la premiere personne alors on incremente l'etape
        {
            etapePerso++;
        }
        else if(etapePerso == 1)  // si on a servis la deuxiemme personne alors on reset l'etape
        {
            etapePerso = 0; 
        }
    }

    public void EtapeQueteUp() // fonction appellé quand on a finit une boisson pour mettre a jour l'etape de quete 
    {
        if (etapeQuete == 0) // meme principe que pour l'etape perso
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
