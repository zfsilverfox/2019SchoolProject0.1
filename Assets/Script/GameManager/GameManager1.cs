using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameManager1 : MonoBehaviour {

    public List<GameObject> EnemyCounter;

    public GameObject[] Enemy;
    public GameObject _Player;
    public GameObject _StartingPanel;
    public GameObject _LifeSilder;
    public GameObject _GameClearPanel;
    public GameObject _GameOverPanel;
    public GameObject _AudioCrtSourses;
    public GameObject _PausePanel;
    public GameObject _pauseBtn;

    public GameObject[] _HitTrigger;





    public GameObject _AudioGameClrSourses;
    public GameObject _AudioGameOverSourses;
    
    [SerializeField] Animator _PauseAnim;



    public Slider _PlayerLifeSlider;

    public bool GameOver= false;
    public bool GameClear= false;

    public int EnemyCountDown;

    public int ScreenWidth = 800;
    public int ScreenHeight = 600;

    bool HavePlayPauseBeforeGameClear = false;
    bool HavePlayPauseBeforeGameOver = false;

   
    public GameObject _YouHaveKillOneEnemyTxt;
  [HideInInspector]  [SerializeField] Animator _AnimFirstKill;
    bool HasKillOneEnmBeforeAnimator = false;
    bool HasKillOneEnmBeforeAnimation = false;


    public GameObject _YouHaveKillTwoEnemyTxt;
    [SerializeField] Animator _AnimSecondKill;
    bool HasKillSecondEnmBeforeAnimator = false;
    bool HasKillSecondEnmBeforeAnimation = false;


    public GameObject _PresentTheFirstMission;
    bool HasTurnOffBfAnimator;
    bool HasTurnOffBfAnimation;
    Animator _HavePresentFirstMissionBfAnim;
    AnimatorStateInfo _PTFMAnimStateInfo;



    public void GameStart()
    {
        _Player.SetActive(true);
       foreach (GameObject i in Enemy)
        {
            i.SetActive(true);
        }
        _StartingPanel.SetActive(false);
        _LifeSilder.SetActive(true);
        _AudioCrtSourses.SetActive(true);
        _pauseBtn.SetActive(true);
        if (_PresentTheFirstMission != null) _PresentTheFirstMission.SetActive(true);


    }
    private void Awake()
    {
        AvoidNullProblem();
        SettingProblem();
    }
    void Start()
    {
        AvoidNullProblem();
        SettingBasicProblem();
       
        BtnCrtProblem();
        HasTurnOffBfAnimator = false;
        HasTurnOffBfAnimation = false;


    }
    void SettingProblem()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }
    }
    void AvoidNullProblem()
    {
        if (_PauseAnim == null) _PauseAnim = _pauseBtn.GetComponent<Animator>();
        if (_AnimFirstKill == null) _AnimFirstKill = _YouHaveKillOneEnemyTxt.GetComponent<Animator>();
        if (_AnimSecondKill == null) _AnimSecondKill = _YouHaveKillTwoEnemyTxt.GetComponent<Animator>();
        if (_HavePresentFirstMissionBfAnim == null) _HavePresentFirstMissionBfAnim = _PresentTheFirstMission.GetComponent<Animator>();
    }
    void BtnCrtProblem()
    {
        if(_pauseBtn!= null)
        {

            _pauseBtn.GetComponent<Button>().interactable = true;

        }
        HavePlayPauseBeforeGameClear = false;
        HavePlayPauseBeforeGameOver = false;
        if (_pauseBtn.activeInHierarchy)
        {
            _PauseAnim.SetBool("PlayPauseBtn",false);
        }
    }

    void SettingBasicProblem()
    {
        GameClear = false;
        GameOver = false;
        HasKillOneEnmBeforeAnimator = false;
        HasKillOneEnmBeforeAnimation = false;
        HasKillSecondEnmBeforeAnimator = false;
        HasKillSecondEnmBeforeAnimation = false;
    }



    void Update()
    {
        if (_PresentTheFirstMission.activeInHierarchy)
        {
            _PTFMAnimStateInfo = _HavePresentFirstMissionBfAnim.GetCurrentAnimatorStateInfo(0);
        }
        _PlayerLifeSlider.value = _Player.GetComponent<PlayerLifeStatement>().Health;
        GameClearFunction();
        GameOverFunction();
        CheckErrorFunction();
        PresentedKillEnemy();

        if (_PresentTheFirstMission.activeInHierarchy)
        {
            if (_PTFMAnimStateInfo.IsName("3DTxtAnimationDissapear") && _PTFMAnimStateInfo.normalizedTime >= 0.98f)
            {
                _PresentTheFirstMission.SetActive(false);
            }

        }



        }
    void GameOverFunction()
    {
        if(_Player.GetComponent<PlayerLifeStatement>().Health <= 0)
        {
            GameOver = true;
        }
        if(GameOver && !GameClear)
        {
           
            _GameOverPanel.SetActive(true);
            if(_pauseBtn != null)
            {
                _pauseBtn.GetComponent<Button>().interactable = false;
            }
            if (!HavePlayPauseBeforeGameOver)
            {
                StartCoroutine("StartingPauseCoroutineGameOver");
            }
            if(_LifeSilder != null)
            {
                _LifeSilder.SetActive(false);
            }
        }
    }
    void GameClearFunction()
    {
    if(EnemyCountDown == 3)
        {
            GameClear = true;
        }
        if (GameClear&&!GameOver)
        {
            if (_Player.activeInHierarchy)
            {
                if(_HitTrigger != null)
                {
                    foreach(GameObject ht in _HitTrigger)
                    {
                        ht.SetActive(false);

                    }
                }
            }
            _Player.SetActive(false);
            _GameClearPanel.SetActive(true);
            if (_pauseBtn != null)
            {
                _pauseBtn.GetComponent<Button>().interactable = false;
            }
            if (_LifeSilder != null)
            {
                _LifeSilder.SetActive(false);
            }
            if (!HavePlayPauseBeforeGameClear)
            {
                StartCoroutine("StartingPlayCoroutineGameClear");
            }
            PlayerPrefs.SetInt("levelReach", 2);
            if (!HasTurnOffBfAnimator)
            {
                _HavePresentFirstMissionBfAnim.SetBool("MissionisOver", true);
                HasTurnOffBfAnimator = true;
            }
     
        }
    }

    void CheckErrorFunction()
    {
        if(GameClear && GameOver)
        {
            Debug.LogError("Something Problem has been presented, Please Do Some Check");
        }
    }
    void PresentedKillEnemy()
    {
        if(EnemyCountDown == 1)
        {
            if(!HasKillOneEnmBeforeAnimator)
            {
                StartCoroutine("FirstKillEnemyAnimation");
            }
        }
        if(EnemyCountDown == 2)
        {
            if(!HasKillSecondEnmBeforeAnimator)
            {
                StartCoroutine("SecondKillEnemyAnimation");
            }
        }
    }
   public  void GoingToNextLevelFunction()
    {
        SceneManager.LoadScene("GameMain 1");
    }
    public void RestartLvl1Function()
    {
        SceneManager.LoadScene("GameMain");
    }
    public void QuitFunction()
    {
        SceneManager.LoadScene("GameStart");
    }
    IEnumerator StartingPauseCoroutineGameOver()
    {
        yield return new WaitForSeconds(0.01f);



        _PauseAnim.SetBool("PlayPauseBtn", true);

        HavePlayPauseBeforeGameOver = true;
    }
    IEnumerator StartingPlayCoroutineGameClear()
    {
        yield return new WaitForSeconds(0.01f);

        _PauseAnim.SetBool("PlayPauseBtn", true);
        HavePlayPauseBeforeGameClear = true;

    }

    IEnumerator FirstKillEnemyAnimation()
    {
        yield return new WaitForSeconds(0.01f);
        if (_YouHaveKillOneEnemyTxt != null)
        {
            _YouHaveKillOneEnemyTxt.SetActive(true);
        }
        else Debug.LogError("You Have Forget to add something");
        yield return new WaitForSeconds(0.5f);
        if(!HasKillOneEnmBeforeAnimation)
        {
            if(_AnimFirstKill != null)
            {
                _AnimFirstKill.SetBool("SetActive", true);
            }
            HasKillOneEnmBeforeAnimation = true;
        }
        yield return new WaitForSeconds(10f);
        if (_YouHaveKillOneEnemyTxt != null)
        {
            _YouHaveKillOneEnemyTxt.SetActive(false);
        }
        else Debug.LogError("You Have Forget to add something");
        HasKillOneEnmBeforeAnimator = true;
        StopCoroutine("FirstKillEnemyAnimation");
    }

    IEnumerator SecondKillEnemyAnimation()
    {
        yield return new WaitForSeconds(0.01f);
        if (_YouHaveKillTwoEnemyTxt != null)
        {
            _YouHaveKillTwoEnemyTxt.SetActive(true);
        }
        else Debug.LogError("That's Something Strange,You need to add somethings");
        yield return new WaitForSeconds(0.5f);
        if (!HasKillSecondEnmBeforeAnimation)
        {
            if (_AnimSecondKill != null)
            {
                _AnimSecondKill.SetBool("SetActive", true);
            }
            else Debug.LogError("That's Something Strange, You need to add Something ");
 



            HasKillSecondEnmBeforeAnimation = true;
        }
        yield return new WaitForSeconds(10f);
        if (_YouHaveKillTwoEnemyTxt != null)
        {
            _YouHaveKillTwoEnemyTxt.SetActive(false);
        }
        else Debug.LogError("That's Something Strange,You need to add somethings");
        HasKillSecondEnmBeforeAnimator = true;
        StopCoroutine("SecondKillEnemyAnimation");
    }
}
