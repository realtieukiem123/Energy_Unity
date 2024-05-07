using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAfterRotate : MonoBehaviour
{
    public static HandleAfterRotate instance;
    private void Awake()
    {
        instance = this;
    }


    public List<GameObject> listEnd = new List<GameObject>();

    public void CheckListEnd(Hexa h)
    {
        //print( "index"+ h.listConnect.IndexOf(listEnd[-1]));
/*        foreach (var j in listEnd)
        {
            print(j.transform.parent.name);
        }*/

        for (int i = 0; i < listEnd.Count; i++)
        {
            var index = h.listConnect.IndexOf(listEnd[i]);
            
            if (index > -1)
            {
                print("1");
                listEnd[i] = null;
            }
            else
            {
                print("2");
                CheckPower.instance.hasPower = false;
                CheckPower.instance.listNext.Clear();
                CheckPower.instance.DFSs(listEnd[i].GetComponent<Hexa>());
                GameManager.instance.ResetValidate();

                if (!CheckPower.instance.hasPower)
                {
                    foreach (var l in CheckPower.instance.listNext)
                    {
                        l.isLight = false;
                        l.CheckLight(h.isLight);
                    }

                }
            }
        }

        //listEnd.Clear();

    }

}
