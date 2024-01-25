using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HexagoneStat : MonoBehaviour, ISerializationCallbackReceiver
{

    #region dico

    [System.Serializable] public class dicoResistanceTransformClass : TemplateDico<enumResistance, Transform> { };
    private Dictionary<enumResistance, Transform> dicoResistanceTrasnform; // dictionnaire des resistances

    [System.Serializable] public class dicoEmplacementPointClass : TemplateDico<enumResistance, List<Transform>> { };
    private Dictionary<enumResistance, List<Transform>> dicoEmplacementPoint; // dictionnaire des resistances


    

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoResistanceTrasnform = new Dictionary<enumResistance, Transform>();

        foreach (dicoResistanceTransformClass item in listDicoResistanceTransform)
        {
            if (!dicoResistanceTrasnform.ContainsKey(item.key))
            {
                dicoResistanceTrasnform.Add(item.key, item.value);
            }
        }


        dicoEmplacementPoint = new Dictionary<enumResistance, List<Transform>>();

        foreach (dicoEmplacementPointClass item in listDicoEmplacementPoint)
        {
            if (!dicoEmplacementPoint.ContainsKey(item.key))
            {
                dicoEmplacementPoint.Add(item.key, item.value);
            }
        }
    }

    #endregion

    [SerializeField] private List<dicoResistanceTransformClass> listDicoResistanceTransform; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    [SerializeField] private List<dicoEmplacementPointClass> listDicoEmplacementPoint; // permet de visualiser le dico des résistances, en private psk on a pas besoin d'y toucher, c'est juste pour visualiser en éditor

    private bool canUpdateLine;

    private LineRenderer ln;

    public SCR_SO_Ingredient ingredient;

    public bool canUpdate;


    private void Start()
    {
        ln = dicoResistanceTrasnform[enumResistance.Thermique].GetComponent<LineRenderer>();
    }

    private void Update()
    {

        if (canUpdate)
        {
            canUpdate = false;
            UpdateStat(ingredient.dicoResistance);
        }

        if(canUpdateLine)
        {
            UpdateLine();

        }


    }
    public void UpdateStat(Dictionary<enumResistance,float> statAfficheParameter)
    {
        canUpdateLine = true;

        foreach(KeyValuePair<enumResistance,float> pair in statAfficheParameter)
        {
            float x = Remap(pair.Value, -300, 1500, dicoEmplacementPoint[pair.Key][0].position.x, dicoEmplacementPoint[pair.Key][1].position.x);
            float y = Remap(pair.Value, -300, 1500, dicoEmplacementPoint[pair.Key][0].position.y, dicoEmplacementPoint[pair.Key][1].position.y);
            float z = Remap(pair.Value, -300, 1500, dicoEmplacementPoint[pair.Key][0].position.z, dicoEmplacementPoint[pair.Key][1].position.z);
            Vector3 newPosition = new Vector3(x, y, z);
            dicoResistanceTrasnform[pair.Key].DOMove(newPosition,3f);


        }


        Vector3 newPositionThermique = dicoEmplacementPoint[enumResistance.Thermique][0].position;
        dicoResistanceTrasnform[enumResistance.Thermique].DOMove(newPositionThermique,3f).OnComplete(Lock);
    }

    private void UpdateLine()
    {
        ln.SetPosition(0, new Vector3(dicoResistanceTrasnform[enumResistance.Thermique].position.x, dicoResistanceTrasnform[enumResistance.Thermique].position.y, -1));
        ln.SetPosition(1, new Vector3(dicoResistanceTrasnform[enumResistance.Hemorragique].position.x, dicoResistanceTrasnform[enumResistance.Hemorragique].position.y, -1));
        ln.SetPosition(2, new Vector3(dicoResistanceTrasnform[enumResistance.Toxique].position.x, dicoResistanceTrasnform[enumResistance.Toxique].position.y, -1));
        ln.SetPosition(3, new Vector3(dicoResistanceTrasnform[enumResistance.Cryogenique].position.x, dicoResistanceTrasnform[enumResistance.Cryogenique].position.y, -1));
        ln.SetPosition(4, new Vector3(dicoResistanceTrasnform[enumResistance.Electrique].position.x, dicoResistanceTrasnform[enumResistance.Electrique].position.y, -1));
        ln.SetPosition(5, new Vector3(dicoResistanceTrasnform[enumResistance.Lethargique].position.x, dicoResistanceTrasnform[enumResistance.Lethargique].position.y, -1));
        ln.SetPosition(6, new Vector3(dicoResistanceTrasnform[enumResistance.Thermique].position.x, dicoResistanceTrasnform[enumResistance.Thermique].position.y, -1));

    }

    private void Lock()
    {
        canUpdateLine = false;
    }

    public virtual float Remap(float value, float from1, float to1, float from2, float to2) // je le garde psk j'en ai eu besoin pendant un test et que je galere a retrouver le nom remap a chaque fois
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
