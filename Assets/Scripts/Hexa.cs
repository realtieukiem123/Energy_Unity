using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexa : MonoBehaviour
{
    float angle = 0;
    float angle2 = 0;
    bool isCLick = false;

    //public float speedRotate
    public bool isValidate = false;
    public bool isPower = false;
    public bool isLight = false;
    public GameObject spriteOn;
    public List<GameObject> listConnect = new List<GameObject>();






    private void Start()
    {
        if (!isPower)
        {
            CheckLight(false);
        
        }
        else
        {
            GameManager.instance.listHexaPower.Add(this);
        }

        GameManager.instance.listAllHexa.Add(this);


    }




    public void OnClickHexa()
    {
        if (isCLick) { return; }
        isCLick = true;
        listConnect.Clear();


        //print(transform.rotation.eulerAngles.z);

        if (isPower)
        {
            angle = transform.rotation.eulerAngles.z;
            angle2 = angle - 30;
            transform.DORotate(new Vector3(0, 0, angle2), 0.1f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(DoneRotatePower);
        }
        else
        {
            //Quaternion rot = transform.rotation;
            angle = transform.rotation.eulerAngles.z;
            angle -= 60;
            if (angle < 0)
            {
                angle += 360;
            }

            transform.DORotate(new Vector3(0, 0, angle), 0.2f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(DoneRoatate);
        }



    }
    void SetClick()
    {
        isCLick = false;
    }
    void DoneRoatate()
    {
        SetClick();
        CheckPower.instance.hasPower = false;
        CheckPower.instance.DFSs(this);
        GameManager.instance.ResetValidate();
        isLight = CheckPower.instance.hasPower;
        CheckLight(isLight);
        HandleAfterRotate.instance.CheckListEnd(this);

        foreach (var e in GameManager.instance.listAllHexa)
        {
            if (e.isPower)
            {
                GameManager.instance.DFS(e);
                GameManager.instance.ResetValidate();
             }
        }
    }
    void DoneRotatePower()
    {
        transform.DORotate(new Vector3(0, 0, angle), 0.1f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(SetClick);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.tag == "Hexa")
        {
            //print("vatrig");
            listConnect.Add(collision.transform.parent.parent.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.tag == "Hexa" && !isCLick)
        {
            //print("outtrig");
            var i = listConnect.IndexOf(collision.transform.parent.parent.gameObject);
            listConnect.RemoveAt(i);
        }
        if (collision.transform.parent.parent.tag == "Hexa" && isCLick && !collision.transform.parent.parent.GetComponent<Hexa>().isPower)
        {
            HandleAfterRotate.instance.listEnd.Add(collision.transform.parent.parent.gameObject);
        }
    }

    public void CheckLight(bool isOn)
    {
        if (isOn)
        {
            spriteOn.SetActive(true);

        }
        else
        {
            spriteOn.SetActive(false);
        }
    }

}
