using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour {

    public Animator _anim;

    public GameObject _BtnStart;
    public GameObject _BtnStartTxt;

    bool GameStartBoolean = false;



    AnimatorStateInfo _animStateInfo;


    [SerializeField] Animator _animBtnStart;
    [SerializeField] Animator _animBtntxtStart;


    bool HasntPlayBeforeBtn = false;
    bool HasntPlayBeforeTxt = false;

	public void GameStart()
    {

        GameStartBoolean = true;
      //  SceneManager.LoadScene("LoadingSceneStart");
    }


    public int ScreenWidth = 800;
    public int ScreenHeight = 600;



    void Awake()
    {
        SettingResolutionProblem();
        AvoidNullProblem();
    }
    void SettingResolutionProblem()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }
    }

     void Start()
    {
        SettingBasicProb();
        AvoidNullProblem();
    }

    void SettingBasicProb()
    {
        GameStartBoolean = false;
        _BtnStart.GetComponent<Button>().interactable = true;
        HasntPlayBeforeBtn = false;
         HasntPlayBeforeTxt = false;

    }
    void AvoidNullProblem()
    {
        if (_BtnStart == null) Debug.LogWarning("This is something Strange");
        if (_BtnStartTxt == null) Debug.LogError("Please Added Some Object to This object ");
        if (_animBtnStart == null) _animBtnStart = _BtnStart.GetComponent<Animator>();
        if (_animBtntxtStart == null) _animBtntxtStart = _BtnStartTxt.GetComponent<Animator>();
    }
        void Update()
        {
        _animStateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        _anim.SetBool("GameStart", GameStartBoolean);
            if (GameStartBoolean)
                {
                    _BtnStart.GetComponent<Button>().interactable = false;
                        if (!HasntPlayBeforeBtn)
                        {
                            HasntPlayBeforeBtn = true;
                            _animBtnStart.SetBool("SetAsTrue", true);
                        }
                        if (!HasntPlayBeforeTxt)
                        {
                            HasntPlayBeforeTxt = true;
                            _animBtntxtStart.SetBool("SetAsTrue", true);
                        }
                }
          
            if (_animStateInfo.IsName("Activable")&&_animStateInfo.normalizedTime>0.99f)
        {
            SceneManager.LoadScene("LoadingSceneStart");
        }
        }



    public void GameQuit()
    {
        Application.Quit();
    }
         






}
