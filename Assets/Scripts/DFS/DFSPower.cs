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

        h.colorLight = GameManager.instance.powerColor;

        if (h.isWifi)
        {
            if (h.colorLight == Hexa.ColorLight.Red)
            {
                GameManager.instance.isOnWifiRed = true;
            }
            else if (h.colorLight == Hexa.ColorLight.Yellow)
            {
                GameManager.instance.isOnWifiYellow = true;
            }
            else
            {
                GameManager.instance.isOnWifiOrange = true;
            }

        }

        h.isLight = true;
        h.CheckLight(h.isLight);
    }
}
