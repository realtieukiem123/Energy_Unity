using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : MonoBehaviour
{
    public void DFSs(Hexa hex)
    {
        var tempctrl = hex.GetComponent<Hexa>();
        DoSomething(tempctrl);
        foreach (var e in tempctrl.listConnect)
        {
            var ctr = e.GetComponent<Hexa>();
            if (!ctr.isValidate)
            {
                DFSs(ctr);
            }
        }
    }
    protected virtual void DoSomething(Hexa h)
    {

    }

}
