using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexa : MonoBehaviour
{
    public float angle = 0;
    float angle2 = 0;
    bool isCLick = false;

    public int angleCorrect;
    public float speedRotate = 0.125f;
    public bool isValidate = false;
    public bool isPower = false;
    public bool isLight = false;
    public bool isHexa = false;
    public bool isWifi = false;
    public bool isGOLight = false;
    public GameObject spriteOn;
    public List<GameObject> listConnect = new List<GameObject>();






    private void Start()
    {
        if (isWifi) { GameManager.instance.listWifi.Add(this); }


        if (!isPower)
        {
            CheckLight(false);

        }
        else
        {
            SetAngle();
            GameManager.instance.listHexaPower.Add(this);
        }

        GameManager.instance.listAllHexa.Add(this);

        //GameManager.instance.CheckCorrect();

    }




    public void OnClickHexa()
    {
        if (isCLick || GameManager.instance.isStatus) { return; }
        isCLick = true;
        listConnect.Clear();


        //print(transform.rotation.eulerAngles.z);

        /*        if (isPower)
                {
                    SetAngle();
                    angle2 = angle - 30;
                    transform.DORotate(new Vector3(0, 0, angle2), speedRotate, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(DoneRotatePower);
                    //vibaration
                    GameManager.instance.Vibration();
                }
                else
                {

                }*/


        SetAngle();
        if (isHexa)
        {
            angle -= 60;
        }
        else
        {
            angle -= 90;
        }


        if (angle < 0)
        {
            angle += 360;
        }

        transform.DORotate(new Vector3(0, 0, angle), speedRotate, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => { StartCoroutine(DoneRoatate()); });
        //vibaration
        GameManager.instance.Vibration();


    }
    public void RotateSuggest()
    {
        transform.DORotate(new Vector3(0, 0, angleCorrect), speedRotate, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => { StartCoroutine(DoneRoatate()); });
    }

    void SetClick()
    {
        isCLick = false;
    }
    public IEnumerator DoneRoatate()
    {
        yield return new WaitForSeconds(0.02f);
        SetClick();

        // kiem tra chinh minh
        CheckHasConnectToPower();

        HandleAfterRotate.instance.CheckListEnd();
        GameManager.instance.OnLightToPower();

        SetAngle();
        //CheckWifi
        GameManager.instance.CheckWifi();


        //CheckWin
        GameManager.instance.CheckWin();
    }
    public void CheckHasConnectToPower()
    {
        GameManager.instance.checkPower.listNext.Clear();
        GameManager.instance.checkPower.hasPower = false;
        //print("xoay");
        //listConnect.ForEach(x => print(x));
        GameManager.instance.checkPower.DFSs(this);
        GameManager.instance.ResetValidate();
        isLight = GameManager.instance.checkPower.hasPower;
        //print("islight " + isLight);
        CheckLight(isLight);        

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

    public void SetAngle()
    {
        angle = (int)transform.rotation.eulerAngles.z;
    }
}
