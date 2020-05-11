using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManagerLastScreen : MonoBehaviour {


    public int ScreenWidth = 800;
    public int ScreenHeight = 600;

    private void Awake()
    {
        SettingResolutionProb();
    }


    void Start () {
        StartCoroutine("LoadingCoroutine");
	}


    void SettingResolutionProb()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }
    }
	
	
    IEnumerator LoadingCoroutine()
    {

        yield return new WaitForSeconds(6f);
        AsyncOperation async = SceneManager.LoadSceneAsync("GameStart");
        while (!async.isDone)
        {
            yield return null;
        }
    }



}
