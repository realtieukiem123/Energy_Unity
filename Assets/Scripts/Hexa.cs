using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public ColorLight colorLight;
    public ColorType colorType;






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
        //GameManager.instance.Vibration();
        Vibrator.Vibrate(1);


    }
    public void RotateSuggest()
    {
        transform.DORotate(new Vector3(0, 0, angleCorrect), speedRotate, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => { StartCoroutine(DoneRoatate()); });
        //SetAngle();
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
        List<Hexa> tempListEnd = new List<Hexa>();
        HandleAfterRotate.instance.listEnd.ForEach(e => { tempListEnd.Add(e.GetComponent<Hexa>()); });
        HandleAfterRotate.instance.CheckListEnd(tempListEnd);
        GameManager.instance.OffWifi();

        GameManager.instance.SetFailWifi();

        GameManager.instance.OnLightToPower(GameManager.instance.listHexaPower);

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
        CheckLight(isLight);

    }
    /*    void DoneRotatePower()
        {
            transform.DORotate(new Vector3(0, 0, angle), 0.1f, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(SetClick);
        }*/

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
        CheckColor();
        if (isOn)
        {
            spriteOn.SetActive(true);

        }
        else
        {
            spriteOn.SetActive(false);
        }
    }
    void CheckColor()
    {
        if (isPower)
        {
            if (isWifi)
            {
                if (colorLight == ColorLight.Red)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[9];
                }
                else if (colorLight == ColorLight.Yellow)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[9];
                }
                else if (colorLight == ColorLight.Orange)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[9];
                }
            }
            else
            {
                if (colorLight == ColorLight.Red)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[0];
                }
                else if (colorLight == ColorLight.Yellow)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[0];
                }
                else if (colorLight == ColorLight.Orange)
                {
                    spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[0];
                }
            }



        }
        else
        {
            switch (colorType)
            {
                case ColorType.one:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[1];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[1];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[1];
                            break;
                    }

                    break;
                case ColorType.two:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[2];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[2];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[2];
                            break;
                    }

                    break;
                case ColorType.three:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[3];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[3];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[3];
                            break;
                    }

                    break;
                case ColorType.four:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[4];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[4];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[4];
                            break;
                    }

                    break;
                case ColorType.five:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[5];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[5];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[5];
                            break;
                    }

                    break;
                case ColorType.six:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[6];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[6];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[6];
                            break;
                    }

                    break;
                case ColorType.seven:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[7];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[7];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[7];
                            break;
                    }

                    break;
                case ColorType.eight:

                    switch (colorLight)
                    {
                        case ColorLight.Red:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[8];
                            break;
                        case ColorLight.Yellow:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[8];
                            break;
                        case ColorLight.Orange:
                            spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[8];
                            break;
                    }

                    break;
                    /* case ColorType.nigh:

                         switch (colorLight)
                         {
                             case ColorLight.Red:
                                 spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[9];
                                 break;
                             case ColorLight.Yellow:
                                 spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[9];
                                 break;
                             case ColorLight.Orange:
                                 spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightOrange[9];
                                 break;
                         }

                         break;*/

            }

            /* if (colorType == ColorType.one)
             {
                 if (colorLight == ColorLight.Red)
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[1];
                 }
                 else
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[1];
                 }

             }
             else if (colorType == ColorType.two)
             {
                 if (colorLight == ColorLight.Red)
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[2];
                 }
                 else
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[2];
                 }
             }
             else if (colorType == ColorType.three)
             {
                 if (colorLight == ColorLight.Red)
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[3];
                 }
                 else
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[3];
                 }
             }
             else if (colorType == ColorType.four)
             {
                 if (colorLight == ColorLight.Red)
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightRed[4];
                 }
                 else
                 {
                     spriteOn.GetComponent<Image>().sprite = GameManager.instance.arraySpriteLightYellow[4];
                 }
             }*/
        }


    }

    public void SetAngle()
    {
        angle = (int)transform.rotation.eulerAngles.z;
    }

    public enum ColorLight
    {
        Red,
        Yellow,
        Orange

    }
    public enum ColorType
    {
        one, two, three, four, five, six, seven, eight, nigh, ten
    }
}


