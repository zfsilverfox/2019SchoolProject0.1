using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;



public class LoadingManager : MonoBehaviour {


    bool PressStart = false;


    public int ScreenWidth = 800;
    public int ScreenHeight = 600;


    private void Awake()
    {
        SettingResolutionProb();
    }


    void Start()
    {
        PressStart = false;
    }
     void Update()
    {
        LoadingChecked();
    }
    void SettingResolutionProb()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }
    }
    void LoadingChecked()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("GameSelect");
        }
 
    }

}
