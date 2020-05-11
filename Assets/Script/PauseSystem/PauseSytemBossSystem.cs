using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




// This is the Pause Sys Use For Boss Stage
public class PauseSytemBossSystem : MonoBehaviour {

    public GameObject _pauseBtn;
    public GameObject _pausePanel;
    public GameObject _LifeStanel;


    public GameObject _SubCameraObj;
    public GameObject[] _TwoRedLines ;









   public void PauseSystemOn()
    {

        _pauseBtn.SetActive(false);
        _pausePanel.SetActive(true);
        if(_LifeStanel != null)
        _LifeStanel.SetActive(false);

      //  Debug.Log("has Pressed The Pause System");


        _pauseBtn.GetComponent<UnityEngine.UI.Button>().interactable = false;


        if (_SubCameraObj != null) _SubCameraObj.SetActive(false);

        if(_TwoRedLines != null)
        {
            foreach(GameObject twl in _TwoRedLines)
            {
                twl.SetActive(false);
            }
        }


        if (_pausePanel.activeSelf && !_pauseBtn.activeSelf && !_LifeStanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }


    public void PlaySystem()
    {
        _pauseBtn.SetActive(true);
        _pausePanel.SetActive(false);
        _pauseBtn.GetComponent<UnityEngine.UI.Button>().interactable = true;

        if (_SubCameraObj != null) _SubCameraObj.SetActive(true);

        if (_TwoRedLines != null)
        {
            foreach (GameObject twl in _TwoRedLines)
            {
                twl.SetActive(true);
            }
        }




        if (_pauseBtn.activeSelf && !_pausePanel.activeSelf)
        {
            Time.timeScale = 1;
            _LifeStanel.SetActive(true);
        }
    }
    public void RestartSystem()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GameMain 2");

    }
    public void QuitSystem()
    {
        Debug.Log("Quit System has been pressed ");
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStart");





    }
}
