using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject UISettingGO;
    public GameObject BGSettingGO;
    public GameObject BlurSettingGO;

    public Sprite[] arraySpriteNumber;
    public Sprite[] arraySpriteBG;
    public Image ImgBG;
    public Image ImgSuggest;
    private void Start()
    {
        ChangeBG();
        CheckSuggest();

        //BLur
        BlurSettingGO.SetActive(true);
        BlurSettingGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        BlurSettingGO.gameObject.GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.Linear);
        BlurSettingGO.transform.DOScale(new Vector3(60f, 60f, 60f), 1f).SetEase(Ease.Linear).OnComplete(() => { BlurSettingGO.SetActive(false); });
    }
    public void ChangeBG()
    {
        
        var i = Random.Range(0, arraySpriteBG.Length);
        ImgBG.sprite = arraySpriteBG[i];
    }
    public void CheckSuggest()
    {
        int numSug = PlayerPrefs.GetInt("numSug");
        switch (numSug)
        {
            case 1:
                ImgSuggest.sprite = arraySpriteNumber[1];
                break; 
            case 2:
                ImgSuggest.sprite = arraySpriteNumber[2];
                break;
            case 3:
                ImgSuggest.sprite = arraySpriteNumber[3];
                break;
            default:
                ImgSuggest.sprite = arraySpriteNumber[0];
                break;
        }
    }

    public void ButtonSetting()
    {
        UISettingGO.SetActive(true);
        BGSettingGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        BGSettingGO.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.Linear);

    }
    public void ButtonCloseSetting()
    {
        BGSettingGO.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.25f).SetEase(Ease.Linear).OnComplete(() => { UISettingGO.SetActive(false); });

    }
    public void ButtonHome()
    {
        SceneManager.LoadScene(0);
    }
    public void ButtonReload()
    {
        /*        BlurSettingGO.SetActive(true);
                BlurSettingGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                BlurSettingGO.transform.DOScale(new Vector3(60f, 60f, 60f), 0.5f).SetEase(Ease.Linear).OnComplete(() => { });*/


        //BlurSettingGO.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
