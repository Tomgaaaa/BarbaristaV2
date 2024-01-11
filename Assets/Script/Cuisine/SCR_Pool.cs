using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Pool : MonoBehaviour
{
    [SerializeField] SCR_PoolItem prefab;
    [SerializeField] int baseQty;

    Queue<SCR_PoolItem> ready;

    private void Awake()
    {
        ready = new Queue<SCR_PoolItem>(baseQty);
        Add(baseQty);
    }

    void Add(int qty = 1)
    {

        for (int i = 0; i < qty; i++)
        {
            SCR_PoolItem item = Instantiate(prefab, transform);
            item.gameObject.SetActive(false);
            item.Init(this);
            ready.Enqueue(item);
        }
    }

    public SCR_PoolItem Instantiate()
    {
        if (ready.Count == 0)
            Add();

        SCR_PoolItem obj = ready.Dequeue();
        obj.transform.parent = null;
        return obj;
    }

    public void Back(SCR_PoolItem obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.parent = transform;
        ready.Enqueue(obj);
    }
}
