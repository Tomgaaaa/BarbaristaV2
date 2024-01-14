using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PoolItem : MonoBehaviour // pool Item doit etre appliqu� a tout les objets qui seront dans un pool
{
    [SerializeField] private protected SCR_Pool refPool; // une reference de pool pour pouvoir le renvoyer dans le pool quand on le souhaite

    public virtual void Init(SCR_Pool basePool) // fonction appel� au moment ou le pool "instantiate" l'objet
    {
        refPool = basePool; // on recupere le pool
        gameObject.SetActive(true); // on reactive l'objet qui est deactiv� de base

    }

    public void Back()
    {
        if (refPool != null) // verifie si on a bien referencie le pool, c'est pas cens� arriver mais �a �vite de bloquer le jeu si le pool n'est pas r�ferenc�
            refPool.Back(this);
    }





}
