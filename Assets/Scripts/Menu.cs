using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("LoadFirst") != 1)
        {
            PlayerPrefs.SetInt("LoadFirst", 1);
            LoadFirst();
        }

    }
    void LoadFirst()
    {
        print("load");
        PlayerPrefs.SetInt("numSug", 3);
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Map1");
    }
}
