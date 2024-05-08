using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public DFSPower dfsPower;
    public CheckPower checkPower;
    public List<Hexa> listAllHexa = new List<Hexa>();
    public List<Hexa> listHexaPower = new List<Hexa>();
    //public List<Hexa> listCheck = new List<Hexa>();

    private void Start()
    {
        Invoke("OnLightToPower", 0.1f);
        //CheckHexa();
    }
    public void OnLightToPower()
    {
        foreach (var e in listHexaPower)
        {
            if (e.isPower)
            {
                dfsPower.DFSs(e);
                ResetValidate();
            }
        }
    }

    //public void DFS(Hexa hex)
    //{
    //    Debug.Log(hex.name);
    //    var tempctrl = hex.GetComponent<Hexa>();
    //    DoSomething(tempctrl);
    //    foreach (var e in tempctrl.listConnect)
    //    {
    //        var ctr = e.GetComponent<Hexa>();
    //        if (!ctr.isValidate)
    //        {
    //            DFS(ctr);
    //        }
    //    }
    //}
    //void DoSomething(Hexa h)
    //{
    //    if (h.isValidate) return;
    //    h.isValidate = true;
    //    h.isLight = true;
    //    h.CheckLight(h.isLight);
    //}





    public void ResetValidate()
    {
        foreach (var e in listAllHexa)
        {
            e.isValidate = false;
        }
    }
   

}
