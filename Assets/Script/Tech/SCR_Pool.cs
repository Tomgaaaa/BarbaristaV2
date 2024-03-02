using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Pool : MonoBehaviour // de bon souvenirs non ?
{
    [SerializeField] SCR_PoolItem prefab; // prefab qui sera spawn au tout début 
    [SerializeField] int baseQty; // quantité a instancier de base

    Queue<SCR_PoolItem> ready; // globalement c'est comme une list ou un array mais avec le systeme de FIFO (First In First Out)

    private void Awake()
    {
        ready = new Queue<SCR_PoolItem>(baseQty); // on définit la longeur de la queue
        Add(baseQty); // puis on instancie le nombre renseigné de pool item
    }

    void Add(int qty = 1) // fonction qui instancie des pool items 
    {

        for (int i = 0; i < qty; i++) // pour realiser l'action autant de foix que la quantite necessaire
        {
            SCR_PoolItem item = Instantiate(prefab, transform); // instancie le prefab renseigne et définit le pool comme parent
            item.Init(this); // initialise le pool item = renseigné le pool
            item.gameObject.SetActive(false); // désactive l'objet car pas besoin de le laisser activer
            ready.Enqueue(item); // rajoute l'objet dans la queue 
        }
    }

    public SCR_PoolItem Instantiate() // fonction qui permet d'appeler un pool item et de l'"instancier" 
    {
        if (ready.Count == 0) // si la queue est vide il faut instancier un nouveau pool item
        {
            Add(); // ajoute un nouveau pool item
            Debug.Log("Pool vide");
        }

        SCR_PoolItem obj = ready.Dequeue(); // retire de la queue l'objet
        obj.transform.parent = null; // reset le parent du pool item
        return obj; // retourne l'objet créer pour pouvoir le manipuler
    }

    public void Back(SCR_PoolItem obj) // fonction qui permet de renvoyer dans la pool un pool item 
    {
        obj.gameObject.SetActive(false); // désactive l'objet car pas besoin qu'il soit actif
        obj.transform.parent = transform; // définit le pool comme parent
        ready.Enqueue(obj); // rajoute l'objet dans la queue
    }
}
