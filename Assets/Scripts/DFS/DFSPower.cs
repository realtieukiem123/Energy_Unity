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


        if (GameManager.instance.isRed)
        {
            h.colorLight = Hexa.ColorLight.Red;
            print("onred");
        }
        else
        {
            h.colorLight = Hexa.ColorLight.Yellow;
            print("onyellow");
        }

        h.isLight = true;
        h.CheckLight(h.isLight);
    }
}
