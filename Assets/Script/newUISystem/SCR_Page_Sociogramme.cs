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
    
    private void OnEnable()
    {
        ColorLien();
        onClickPerso(0);
    }

    private void ColorLien()
    {

        foreach (KeyValuePair<SO_Personnage,List<dicoRelationPersoImageClass>> dico1 in dicoDeDicoRelation)
        {
            foreach (dicoRelationPersoImageClass persoenRelation in dico1.Value)
            {
                persoenRelation.value.color = GetColor(dico1.Key, persoenRelation.key);
            }
        }

    }

    private Color GetColor(SO_Personnage perso1Parameter,enumPerso perso2Parameter)
    {
        if (perso1Parameter.dicoRelationPerso[perso2Parameter] >= -3 && perso1Parameter.dicoRelationPerso[perso2Parameter] < -1)
        {
            return Color.red;
        }
        else if (perso1Parameter.dicoRelationPerso[perso2Parameter] >= -1 && perso1Parameter.dicoRelationPerso[perso2Parameter] < 1)
        {
            return Color.grey;
        }else if (perso1Parameter.dicoRelationPerso[perso2Parameter] >= 1 && perso1Parameter.dicoRelationPerso[perso2Parameter] <= 2)
        {
            return Color.green;
        }

        return Color.white;
    }

    public void onClickPerso(int persoCliqueParameter)
    {
        List<Sprite> sprites = listSpritePerso;

        SO_Personnage persoClique = SCR_DATA.instanceData.GetListPersos()[persoCliqueParameter];

        sprites.Remove(persoClique.profil); // wtf ca n'a aucun sens quand je remove dans la list queje creer ca remove de listSpritePerso



        for(int i = 0; i < 3; i++)
        {


            listImagePersoGauche[i].sprite = persoClique.profil;


            listImagePersoDroite[i].sprite = listSpritePerso[i];

            
        }
        sprites.Add(persoClique.profil);


    }
}
