using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PoolItem : MonoBehaviour
{
    [SerializeField] private protected SCR_Pool refPool;

    public virtual void Init(SCR_Pool basePool)
    {
        refPool = basePool;
        gameObject.SetActive(true);

    }

    public void Back()
    {
        if (refPool != null)
            refPool.Back(this);
    }





}
