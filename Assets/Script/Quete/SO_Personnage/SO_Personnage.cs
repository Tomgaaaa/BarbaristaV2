using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Personnage", menuName = "ScriptableObjects/Perssonnage")]
public class SO_Personnage : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable] public class dicoResistanceClass : TemplateDico<enumResistance, float> { };
    public Dictionary<enumResistance, float> dicoResistance; // dictionnaire des resistances, on associe une resistance à un float, float psk pour bouger dans l'hexagone il faut un float
    [SerializeField] private List<dicoResistanceClass> listDicoResistance; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    [System.Serializable] public class dicoRelationPersoClass : TemplateDico<enumPerso, int> { };
    public Dictionary<enumPerso, int> dicoRelationPerso; 
    [SerializeField] private List<dicoRelationPersoClass> listDicoReltionPerso; 

    public string namePerso;
    public enumPerso myEnumPerso;
    public SOCharacter characterPerso;
    public Sprite profil;
    public Sprite pleinPied;

    public void OnAfterDeserialize() // fonction qui permet d'associer les listes (qu'on voit) aux dictionaires (qu'on ne voit pas)
    {

        dicoResistance = new Dictionary<enumResistance, float>();
        foreach (dicoResistanceClass item in listDicoResistance)
        {
            if (!dicoResistance.ContainsKey(item.key))
            {
                dicoResistance.Add(item.key, item.value);
            }
        }

        dicoRelationPerso = new Dictionary<enumPerso, int>();
        foreach (dicoRelationPersoClass item in listDicoReltionPerso)
        {
            if (!dicoRelationPerso.ContainsKey(item.key))
            {
                dicoRelationPerso.Add(item.key, item.value);
            }
        }

    }

    public void OnBeforeSerialize()
    {
        
    }


    public void Init(Dictionary<enumResistance,float> dicoResistanceParameter, Dictionary<enumPerso,int> dicoRelationParameter, string nameParameter, enumPerso myEnumParameter,Sprite spriteParameter, SOCharacter SoCharacterVnParameter) 
    {
        dicoResistance = dicoResistanceParameter;
        dicoRelationPerso = dicoRelationParameter;
        namePerso = nameParameter;
        myEnumPerso = myEnumParameter;
        profil = spriteParameter;
        characterPerso= SoCharacterVnParameter;



    }

}
