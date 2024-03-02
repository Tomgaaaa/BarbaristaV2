using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PoolItem : MonoBehaviour // pool Item doit etre appliqué a tout les objets qui seront dans un pool
{
    [SerializeField] private protected SCR_Pool refPool; // une reference de pool pour pouvoir le renvoyer dans le pool quand on le souhaite

    public virtual void Init(SCR_Pool basePool) // fonction appelé au moment ou le pool "instantiate" l'objet
    {
        refPool = basePool; // on recupere le pool
        gameObject.SetActive(true); // on reactive l'objet qui est deactivé de base

    }

    public void Back()
    {
        if (refPool != null) // verifie si on a bien referencie le pool, c'est pas censé arriver mais ça évite de bloquer le jeu si le pool n'est pas réferencé
            refPool.Back(this);
    }





}
