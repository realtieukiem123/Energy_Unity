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
       /* if (h.isPower)
        {
            hasPower = true;
            if (h.colorLight.ToString() == "Red" && !isYellow)
            {
                isRed = true;
                print("red");
            }
            if (h.colorLight.ToString() == "Yellow" && !isRed)
            {
                isYellow = true;
                print("yellow");
            }
        }*/

        /*if (hasPower)
        {
            foreach (Hexa a in listNext)
            {
                if (!a.isPower)
                {
                    print("power" + isYellow);
                    print("power" + isRed);
                    if (isRed)
                    {
                        a.colorLight = Hexa.ColorLight.Red;
                        print("onred");
                    }
                    if (isYellow)
                    {
                        a.colorLight = Hexa.ColorLight.Yellow;
                        print("onyellow");
                    }

                }
            }
        }*/

    }

}
