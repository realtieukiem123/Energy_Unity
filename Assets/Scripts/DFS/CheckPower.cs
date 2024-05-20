using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPower : DFS
{
    public bool hasPower = false;
    public List<Hexa> listNext = new List<Hexa>();

    protected override void DoSomething(Hexa h)
    {

        h.isValidate = true;
        var isRed = false;
        var isYellow = false;

        listNext.Add(h);

    }

}
