using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public DFSPower dfsPower;
    public CheckPower checkPower;
    public GameObject WinBlur;
    public bool isStatus = false;

    public Sprite[] arraySpriteLightRed;
    public Sprite[] arraySpriteLightYellow;
    public List<Hexa> listAllHexa = new List<Hexa>();
    public List<Hexa> listHexaPower = new List<Hexa>();
    public List<Hexa> listNotCorrect = new List<Hexa>();
    public List<Hexa> listWifi = new List<Hexa>();

    public bool isRed = false;
    public bool isOnWifi = false;
    private void Start()
    {
        Invoke(nameof(StartCheck), 0.1f) ; 
        //CheckHexa();
    }
    void StartCheck() 
    {
        OnLightToPower(listHexaPower);
    }
    public void OnLightToPower(List<Hexa> list)
    {
        foreach (var e in list) 
        {
            print("power   " + e.name);
            if (e.isPower)
            {
                print(e.colorLight.ToString());

                if (e.colorLight.ToString() == "Red") isRed = true;
                else isRed = false;

                dfsPower.DFSs(e);
                ResetValidate();
            }
        }
    }



    public void ResetValidate()
    {
        foreach (var e in listAllHexa)
        {
            e.isValidate = false;
        }
    }
    int[] GenerateNonRepeatingNumbers(int min, int max, int count)
    {
        int[] result = new int[count];
        bool[] used = new bool[max - min];
        for (int i = 0; i < count; i++)
        {
            int num;
            do
            {
                num = Random.Range(min, max);
            } while (used[num - min]);
            result[i] = num;
            used[num - min] = true;
        }
        return result;
    }
    public void Vibration()
    {
        Handheld.Vibrate();
    }
    public void CheckWifi()
    {
        
        if (isOnWifi)
        {
            listWifi.ForEach(wf => { wf.isPower = true; });
            OnLightToPower(listWifi);
        }
    }
    public void OffWifi()
    {
        listWifi.ForEach(wf => { 
            wf.isPower = false; 
        });
        HandleAfterRotate.instance.CheckListEnd(listWifi);
    }
    public void CheckWin()
    {
        var isWin = true;
        foreach (var e in listAllHexa)
        {
            if (e.isLight == false)
            {
                isWin = false;
                return;
            }
        }

        if (isWin)
        {
            isStatus = true;
            WinBlur.SetActive(true);

            //tween
            WinBlur.transform.DOScale(new Vector3(40, 40, 1), 1f).SetEase(Ease.InCirc).OnComplete(() => { NextScene(); });
        }
    }
    void NextScene()
    {
        print("Win");
    }

    //--------------------------------------------BUTTON---------------------------------------------------
    public void ButtonSuggest()
    {

        listNotCorrect.Clear();

        foreach (var e in listAllHexa)
        {
            if (e.angle != e.angleCorrect && !e.isPower)
            {
                listNotCorrect.Add(e);
            }
        }
        //Check
        if (listNotCorrect.Count < 1)
        {
            print("1");
            return;
        }
        else if (listNotCorrect.Count < 2)
        {
            //1
            //listNotCorrect[0].transform.DORotate(new Vector3(0, 0, listNotCorrect[0].angleCorrect), 0.125f, RotateMode.Fast).SetEase(Ease.Linear);
            //StartCoroutine(SetAngle(0));
            print("2");
            listNotCorrect[0].RotateSuggest();
        }
        else if (listNotCorrect.Count < 3)
        {
            //2
            for (int i = 0; i < 2; i++)
            {
                //listNotCorrect[i].transform.DORotate(new Vector3(0, 0, listNotCorrect[i].angleCorrect), 0.125f, RotateMode.Fast).SetEase(Ease.Linear);
                //StartCoroutine(SetAngle(i));

                listNotCorrect[i].RotateSuggest();
            }
        }
        else if (listNotCorrect.Count < 4)
        {
            //3
            for (int i = 0; i < 3; i++)
            {
                //listNotCorrect[i].transform.DORotate(new Vector3(0, 0, listNotCorrect[i].angleCorrect), 0.125f, RotateMode.Fast).SetEase(Ease.Linear);
                //StartCoroutine(SetAngle(i));
                listNotCorrect[i].RotateSuggest();
            }
        }
        else
        {
            int[] numbers = GenerateNonRepeatingNumbers(0, listNotCorrect.Count - 1, 3);
            for (int i = 0; i < numbers.Length; i++)
            {
                //listNotCorrect[numbers[i]].transform.DORotate(new Vector3(0, 0, listNotCorrect[numbers[i]].angleCorrect), 0.125f, RotateMode.Fast).SetEase(Ease.Linear);
                //listNotCorrect[numbers[i]].SetAngle();
                //StartCoroutine(SetAngle(numbers[i]));

                listNotCorrect[numbers[i]].RotateSuggest();
            }
        }
    }
}


