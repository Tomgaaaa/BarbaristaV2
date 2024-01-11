using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PoolItem : MonoBehaviour
{
    SCR_Pool pool;

    public virtual void Init(SCR_Pool basePool)
    {
        pool = basePool;
        gameObject.SetActive(true);
    }

    public void Back()
    {
        if (pool != null)
            pool.Back(this);
    }





}
