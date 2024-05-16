using ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject n;
    public Transform t;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            n = null;
            n = Pooler.SpawnFromPool(PoolerEnum.Hexa, t);
        }

      
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pooler.AddToPool("Hexa",n);
        }
    }
}
