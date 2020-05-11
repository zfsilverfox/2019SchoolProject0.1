using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseManagerFirstStage : MonoBehaviour {

    public GameObject _PauseBtn;
    public GameObject _PausePanel;


    public void PauseSystem()
    {
        if(_PauseBtn != null) { _PauseBtn.SetActive(false); }

        if(_PausePanel != null) { _PausePanel.SetActive(true); }

        if(_PausePanel.activeInHierarchy && !_PauseBtn.activeInHierarchy)
        {
            Time.timeScale = 0;
        }



    }


    public void GameStartSys()
    {
        if (_PauseBtn != null) { _PauseBtn.SetActive(true); }

        if (_PausePanel != null) { _PausePanel.SetActive(false); }

        if (!_PausePanel.activeInHierarchy && _PauseBtn.activeInHierarchy)
        {
            Time.timeScale = 1;
        }
    }

    public void RestartSys()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GameMain");


    }



    public void QuitSys()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStart");


    }




}
