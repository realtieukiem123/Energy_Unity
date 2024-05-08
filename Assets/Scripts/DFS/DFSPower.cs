using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSPower : DFS
{
    protected override void DoSomething(Hexa h)
    {
        base.DoSomething(h);
        if (h.isValidate) return;
        h.isValidate = true;
        h.isLight = true;
        h.CheckLight(h.isLight);
    }
}
