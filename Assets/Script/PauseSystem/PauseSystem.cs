using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {


    public GameObject _pausePanel;
    public GameObject _pauseBtn;
    public AudioClip _pauseSound;
    public AudioClip _playSound;
    public AudioSource _adSound1;
    public AudioSource _adSound2;

    public GameObject _SubCamera;
    public GameObject[] _TwoRedLines;


    public GameObject _LifeSlider;
    public GameObject _CountingNumberTxt;



    private void Start()
    {
        AvoidNullProb();
        _adSound1.clip = _playSound;
        _adSound2.clip = _pauseSound;
    }
    void AvoidNullProb()
    {
        if (_adSound1 == null)
            _adSound1 = GameObject.FindGameObjectWithTag("AudioResourses").GetComponent<AudioSource>();

     
    }
    public void PauseCalled()
    {
        _pausePanel.SetActive(true);
        _pauseBtn.SetActive(false);
        if (_adSound1 != null && _adSound2 != null)
        {
            if (_adSound1.isPlaying)
            {
                _adSound1.Stop();
                _adSound2.Play();
            }
        }
        if (_pausePanel.activeSelf&& !_pauseBtn.activeSelf)
        {
            Time.timeScale = 0;
        }

        _pauseBtn.GetComponent<UnityEngine.UI.Button>().interactable = false;
        if (_SubCamera != null) _SubCamera.SetActive(false);
        if(_TwoRedLines != null)
        {
            foreach(GameObject twl in _TwoRedLines)
            {
                twl.SetActive(false);
            }
        }

        if (_LifeSlider != null) _LifeSlider.SetActive(false);
        if (_CountingNumberTxt != null) _CountingNumberTxt.SetActive(false);


    }
    public void PlayCalled()
    {
        _pausePanel.SetActive(false);
        _pauseBtn.SetActive(true);
        _pauseBtn.GetComponent<UnityEngine.UI.Button>().interactable = true;
        if (_adSound1 != null && _adSound2 != null)
        {
            if (_adSound2.isPlaying)
            {
                _adSound2.Stop();
                _adSound1.Play();
            }
        }
        if (_pausePanel != null && _pauseBtn != null)
        {
            if (!_pausePanel.activeSelf && _pauseBtn.activeSelf)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }
        }

        if (_SubCamera != null) _SubCamera.SetActive(true);
        if (_TwoRedLines != null)
        {
            foreach (GameObject twl in _TwoRedLines)
            {
                twl.SetActive(true);
            }
        }

        if (_LifeSlider != null) _LifeSlider.SetActive(true);
        if (_CountingNumberTxt != null) _CountingNumberTxt.SetActive(true);

    }
    public void RestartCalled()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameMain 1");
    }
    public void QuitCalled()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStart");
    }
}
