using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : MonoBehaviour
{
    public void DFSs(Hexa hex)
    {
        var tempctrl = hex.GetComponent<Hexa>();
        DoSomething(tempctrl);
        print(hex.name);
        print("list connect ");
        foreach (var e in tempctrl.listConnect)
        {
            print("c" + e.name);
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
