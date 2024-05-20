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

    public void CheckListEnd(List<Hexa> list)
    {
       
        for (int i = 0; i < list.Count; i++)
        {
            GameManager.instance.checkPower.hasPower = false;
            GameManager.instance.checkPower.listNext.Clear();
            GameManager.instance.checkPower.DFSs(list[i]);
            GameManager.instance.ResetValidate();

            if (!GameManager.instance.checkPower.hasPower)
            {
                    
                foreach (var l in GameManager.instance.checkPower.listNext)
                {
                    //Debug.Log("Tat den " + l.name);
                    l.isLight = false;
                    l.CheckLight(l.isLight);
                }

            }
        }

        listEnd.Clear();

    }

}
