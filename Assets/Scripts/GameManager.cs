using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject ReplayBlur;
    public bool isStatus = false;
    //Color
    public Sprite[] arraySpriteLightRed;
    public Sprite[] arraySpriteLightOrange;
    public Sprite[] arraySpriteLightYellow;

    public List<Hexa> listAllHexa = new List<Hexa>();
    public List<Hexa> listHexaPower = new List<Hexa>();
    public List<Hexa> listNotCorrect = new List<Hexa>();
    public List<Hexa> listWifi = new List<Hexa>();

    public Hexa.ColorLight powerColor = Hexa.ColorLight.Red;
    //public bool isYellow = false;


    public bool isOnWifiRed = false;
    public bool isOnWifiYellow = false;
    public bool isOnWifiOrange = false;
    private void Start()
    {
        LoadFirst();
    }
    void LoadFirst()
    {
        //fps
        Application.targetFrameRate = 300;
        //Blur
        ReplayBlur.SetActive(true);
        ReplayBlur.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        ReplayBlur.gameObject.GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.Linear);
        ReplayBlur.transform.DOScale(new Vector3(60f, 60f, 60f), 1f).SetEase(Ease.Linear).OnComplete(() => { ReplayBlur.SetActive(false); });
        //check
        Invoke(nameof(StartCheck), 0.1f);
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
                powerColor = e.colorLight;

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
    public void CheckWifi()
    {
        if (isOnWifiRed)
        {
            listWifi.ForEach(wf =>
            {
                if (wf.colorLight == Hexa.ColorLight.Red)
                {
                    wf.isPower = true;
                }
            });
            OnLightToPower(listWifi);
        }
        if (isOnWifiYellow)
        {
            listWifi.ForEach(wf =>
            {
                if (wf.colorLight == Hexa.ColorLight.Yellow)
                {
                    wf.isPower = true;
                }
            });
            OnLightToPower(listWifi);
        }
        if (isOnWifiOrange)
        {
            listWifi.ForEach(wf =>
            {
                if (wf.colorLight == Hexa.ColorLight.Orange)
                {
                    wf.isPower = true;
                }
            });
            OnLightToPower(listWifi);
        }
    }
    public void OffWifi()
    {
        listWifi.ForEach(wf =>
        {
            
            wf.isPower = false;



        });
        HandleAfterRotate.instance.CheckListEnd(listWifi);
    }
    public void SetFailWifi()
    {
        isOnWifiRed = false;
        isOnWifiOrange = false;
        isOnWifiYellow = false;
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

        var indexSceme = SceneManager.GetActiveScene().buildIndex;
        if (indexSceme >= 5)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    //--------------------------------------------BUTTON---------------------------------------------------
    public void ButtonSuggest()
    {

        listNotCorrect.Clear();

        foreach (var e in listAllHexa)
        {
            if ((int)e.transform.rotation.eulerAngles.z != (int)e.angleCorrect)
            {
                print("z" + (int)e.transform.rotation.eulerAngles.z + " angle " + (int)e.angleCorrect);
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


