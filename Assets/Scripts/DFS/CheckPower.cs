using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPower : DFS
{
    public static CheckPower instance;
    private void Awake()
    {
        instance = this;
    }



    public bool hasPower = false;
    public List<Hexa> listNext = new List<Hexa>();


    protected override void DoSomething(Hexa h)
    {
        
        h.isValidate = true;

        listNext.Add(h);
        if (h.isPower){hasPower = true;}

    }

}
