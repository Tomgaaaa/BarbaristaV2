using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Page_Sociogramme : MonoBehaviour, ISerializationCallbackReceiver
{


    #region Dico
    [System.Serializable] public class dicoRelationPersoImageClass : TemplateDico<enumPerso,Image > { };
    [System.Serializable] public class dicoRelationImageClass : TemplateDico<SO_Personnage, List<dicoRelationPersoImageClass> > { };
    private Dictionary<SO_Personnage, List<dicoRelationPersoImageClass>> dicoDeDicoRelation; 

    [SerializeField] private List<dicoRelationImageClass> listDicoDeDico;




    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        dicoDeDicoRelation = new Dictionary<SO_Personnage, List<dicoRelationPersoImageClass>>();

        foreach (dicoRelationImageClass item in listDicoDeDico)
        {
            if (!dicoDeDicoRelation.ContainsKey(item.key))
            {
                dicoDeDicoRelation.Add(item.key, item.value);
            }
        }



       
    }
    #endregion

    [SerializeField] private List<Image> listImagePersoGauche;
    [SerializeField] private List<Image> listImagePersoDroite;
    [SerializeField] private List<Sprite> listSpritePerso;

    [SerializeField] private List<RectTransform> listPointABJauge;

    private SO_Personnage personnageSelectione;
    private SO_Personnage personnageRelation1 = null;
    private SO_Personnage personnageRelation2 = null;
    private SO_Personnage personnageRelation3 = null;


    
    private void OnEnable()
    {
        ColorLien();
        onClickPerso(0);
    }

    private void ColorLien()
    {

        foreach (KeyValuePair<SO_Personnage,List<dicoRelationPersoImageClass>> dico1 in dicoDeDicoRelation)
        {
            foreach (dicoRelationPersoImageClass personeRelation in dico1.Value)
            {
                personeRelation.value.color = GetColor(dico1.Key, personeRelation.key);
            }
        }

    }

    private Color GetColor(SO_Personnage perso1Parameter,enumPerso perso2Parameter)
    {
        if (perso1Parameter.dicoRelationPerso[perso2Parameter] >= -3 && perso1Parameter.dicoRelationPerso[perso2Parameter] < -1)
        {
            return Color.red;
        }
        else if (perso1Parameter.dicoRelationPerso[perso2Parameter] >= -1 && perso1Parameter.dicoRelationPerso[perso2Parameter] <= 1)
        {
            return Color.grey;
        }else if (perso1Parameter.dicoRelationPerso[perso2Parameter] > 1 && perso1Parameter.dicoRelationPerso[perso2Parameter] <= 3)
        {
            return Color.green;
        }

        return Color.yellow;
    }


    private void MoveCursorJauge(int indexListPoint, SO_Personnage persoRelation)
    {
        /*for (int i = 0; i < 9; i++)
        {
            float xNewPosition = ((listPointABJauge[i + 1].position.x - listPointABJauge[i].position.x) / 6) * 3 ;

            listPointABJauge[i + 2].position = new Vector3(listPointABJauge[i].position.x + xNewPosition, listPointABJauge[i+2].position.y, 0);
            i += 2;
        }*/



            float xNewPosition = ((listPointABJauge[indexListPoint + 1].position.x - listPointABJauge[indexListPoint].position.x) / 6) * (3 + personnageSelectione.dicoRelationPerso[persoRelation.myEnumPerso]);

            listPointABJauge[indexListPoint + 2].position = new Vector3(listPointABJauge[indexListPoint].position.x + xNewPosition, listPointABJauge[indexListPoint + 2].position.y, 0);
            indexListPoint += 2;
        

    }


    public void onClickPerso(int persoCliqueParameter)
    {
        List<Sprite> sprites = listSpritePerso;

        personnageSelectione = SCR_DATA.instanceData.GetListPersos()[persoCliqueParameter];

        List<int> indexListForPerso = new List<int> { 0,1,2,3};

        indexListForPerso.Remove(persoCliqueParameter);
        personnageRelation1 = SCR_DATA.instanceData.GetListPersos()[indexListForPerso[0]];
        personnageRelation2 = SCR_DATA.instanceData.GetListPersos()[indexListForPerso[1]];
        personnageRelation3 = SCR_DATA.instanceData.GetListPersos()[indexListForPerso[2]];
       


        int backupIndex = sprites.IndexOf(personnageSelectione.profil);
        sprites.Remove(personnageSelectione.profil); // wtf ca n'a aucun sens quand je remove dans la list queje creer ca remove de listSpritePerso



        for(int i = 0; i < 3; i++)
        {


            listImagePersoGauche[i].sprite = personnageSelectione.profil;


            listImagePersoDroite[i].sprite = listSpritePerso[i];

            
        }
        sprites.Insert(backupIndex, personnageSelectione.profil);

        MoveCursorJauge(0, personnageRelation1);
        MoveCursorJauge(3, personnageRelation2);
        MoveCursorJauge(6, personnageRelation3);

    }
}
