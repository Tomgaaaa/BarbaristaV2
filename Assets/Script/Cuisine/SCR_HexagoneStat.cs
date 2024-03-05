using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    private bool canUpdateLineUI;

    [SerializeField] private LineRenderer ln = null;
    [SerializeField] private UILineRenderer lineRenderUI = null;



    private void Start()
    {
        //ln = dicoResistanceTrasnform[enumResistance.Thermique].GetComponent<LineRenderer>();
    }

    private void Update()
    {



        if(canUpdateLine)
        {
            UpdateLine();

        }

        if (canUpdateLineUI)
        {
            UpdateLineUI();

        }
    }

    private void ToFalse()
    {
        canUpdateLine = false;
        canUpdateLineUI = false;
    }

    public void UpdateStat(Dictionary<enumResistance,float> statAfficheParameter, bool instantDeplacementParameter, bool inUI = false)
    {

        if (inUI)
        {
            canUpdateLineUI = true;
            Invoke("ToFalse",3);

        }
        else
        {
            canUpdateLine = true;
            Invoke("ToFalse",3);

        }
        


        foreach(KeyValuePair<enumResistance,float> pair in statAfficheParameter)
        {
            if(pair.Key != enumResistance.Thermique)
            {
                float x = Remap(pair.Value, -200, 1500, dicoEmplacementPoint[pair.Key][0].position.x, dicoEmplacementPoint[pair.Key][1].position.x);
                float y = Remap(pair.Value, -200, 1500, dicoEmplacementPoint[pair.Key][0].position.y, dicoEmplacementPoint[pair.Key][1].position.y);
                float z = Remap(pair.Value, -200, 1500, dicoEmplacementPoint[pair.Key][0].position.z, dicoEmplacementPoint[pair.Key][1].position.z);
                Vector3 newPosition = new Vector3(x, y, z);


                if (instantDeplacementParameter)
                {
                    dicoResistanceTrasnform[pair.Key].position = newPosition;
                }
                else
                {
                    dicoResistanceTrasnform[pair.Key].DOMove(newPosition, 3f);

                }



            }
            


        }
        Vector3 newPositionThermique = dicoEmplacementPoint[enumResistance.Thermique][0].position;

        float thermiqueX = Remap(statAfficheParameter[enumResistance.Thermique], -200, 1500, dicoEmplacementPoint[enumResistance.Thermique][0].position.x, dicoEmplacementPoint[enumResistance.Thermique][1].position.x);
        float thermiqueY = Remap(statAfficheParameter[enumResistance.Thermique], -200, 1500, dicoEmplacementPoint[enumResistance.Thermique][0].position.y, dicoEmplacementPoint[enumResistance.Thermique][1].position.y);
        float thermqueZ = Remap(statAfficheParameter[enumResistance.Thermique], -200, 1500, dicoEmplacementPoint[enumResistance.Thermique][0].position.z, dicoEmplacementPoint[enumResistance.Thermique][1].position.z);
        Vector3 newPositionA = new Vector3(thermiqueX, thermiqueY, thermqueZ);


        if (instantDeplacementParameter)
        {
            dicoResistanceTrasnform[enumResistance.Thermique].position = newPositionA;
        }
        else
        {
            dicoResistanceTrasnform[enumResistance.Thermique].DOMove(newPositionA, 3f).OnComplete(Lock);

        }
    }

    


#if UNITY_EDITOR
    [ContextMenu("Update la line")]

    private void UpdateLineEditor()
    {
        UpdateStat(new Dictionary<enumResistance, float> { 
            { enumResistance.Thermique, 750 } ,
            { enumResistance.Hemorragique, 750 } ,
            { enumResistance.Toxique, 750 } ,
            { enumResistance.Cryogenique, 750 } ,
            { enumResistance.Electrique, 750 } ,
            { enumResistance.Lethargique, 750 } 
        },false,true);

        
    }
#endif


    public void UpdateLine()
    {
        ln.SetPosition(0, new Vector3(dicoResistanceTrasnform[enumResistance.Thermique].position.x, dicoResistanceTrasnform[enumResistance.Thermique].position.y, -1));
        ln.SetPosition(1, new Vector3(dicoResistanceTrasnform[enumResistance.Hemorragique].position.x, dicoResistanceTrasnform[enumResistance.Hemorragique].position.y, -1));
        ln.SetPosition(2, new Vector3(dicoResistanceTrasnform[enumResistance.Toxique].position.x, dicoResistanceTrasnform[enumResistance.Toxique].position.y, -1));
        ln.SetPosition(3, new Vector3(dicoResistanceTrasnform[enumResistance.Cryogenique].position.x, dicoResistanceTrasnform[enumResistance.Cryogenique].position.y, -1));
        ln.SetPosition(4, new Vector3(dicoResistanceTrasnform[enumResistance.Electrique].position.x, dicoResistanceTrasnform[enumResistance.Electrique].position.y, -1));
        ln.SetPosition(5, new Vector3(dicoResistanceTrasnform[enumResistance.Lethargique].position.x, dicoResistanceTrasnform[enumResistance.Lethargique].position.y, -1));
        

    }

    public void UpdateLineUI()
    {
        lineRenderUI.points[0] = new Vector2(dicoResistanceTrasnform[enumResistance.Thermique].localPosition.x, dicoResistanceTrasnform[enumResistance.Thermique].localPosition.y);
        lineRenderUI.points[1] = new Vector2(dicoResistanceTrasnform[enumResistance.Hemorragique].localPosition.x, dicoResistanceTrasnform[enumResistance.Hemorragique].localPosition.y);
        lineRenderUI.points[2] = new Vector2(dicoResistanceTrasnform[enumResistance.Toxique].localPosition.x, dicoResistanceTrasnform[enumResistance.Toxique].localPosition.y);
        lineRenderUI.points[3] = new Vector2(dicoResistanceTrasnform[enumResistance.Cryogenique].localPosition.x, dicoResistanceTrasnform[enumResistance.Cryogenique].localPosition.y);
        lineRenderUI.points[4] = new Vector2(dicoResistanceTrasnform[enumResistance.Electrique].localPosition.x, dicoResistanceTrasnform[enumResistance.Electrique].localPosition.y);
        lineRenderUI.points[5] = new Vector2(dicoResistanceTrasnform[enumResistance.Lethargique].localPosition.x, dicoResistanceTrasnform[enumResistance.Lethargique].localPosition.y);
        lineRenderUI.points[6] = new Vector2(dicoResistanceTrasnform[enumResistance.Thermique].localPosition.x, dicoResistanceTrasnform[enumResistance.Thermique].localPosition.y);

        lineRenderUI.SetAllDirty();
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
