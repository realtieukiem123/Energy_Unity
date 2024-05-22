using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject UISettingGO;
    public GameObject BGSettingGO;
    public GameObject BlurSettingGO;
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
