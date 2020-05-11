using DG.Tweening;
using System.Collections;
using UnityEngine;

using UnityEngine.UI;


using  AIBOSSCreator;
public class GameManager3BOSSStage : MonoBehaviour
{
    private static GameManager3BOSSStage _INSTANCE;

    public static GameManager3BOSSStage _instance
    {
        get
        {

            return _INSTANCE;
        }
    }


    
    [HideInInspector]
    public bool GameOver = false;
    [HideInInspector]
    public bool GameClear = false;


    public GameObject _player;
    public GameObject _ClrPanel;
    public GameObject _Txt;
    public GameObject _BtnRestart;
    public GameObject _BtnRestartTxt;
    public GameObject _BtnQuit;
    public GameObject _BtnQuitTxt;

    public GameObject _GameOverPanel;
    public GameObject _GameOverTxt;
    public GameObject _WantTotryAgainTxt;

    public GameObject _BtnRestartGameOver;
    public GameObject _BtnQuitGameOver;

    public GameObject _BtnRestartTxtGameOver;
    public GameObject _BtnQuitTxtGameOver;
    public GameObject _PauseBtnGameObject;
    public GameObject _LifeSliderObject;
    bool LifeSliderSetActiveAtFirst = false;
    bool PauseBtnSetActiveAtFirst = false;
    public GameObject _SoldierSetActive;
    public GameObject _SoldierSetActive2;
    public GameObject _BossActiveTime;

    BOSSScript _bossScript;

    bool HasntEnableBeforeBOSS = false;
    bool HasntEnableBeforePlayer = false;




    public GameObject[] PlayerHitTrigger;
    public GameObject[] BOSSHitTrigger;


    public GameObject[] _EnemySoldiers;
    public GameObject[] _EnemyGunSoldiers;






    RectTransform _ClearPanelAnimation;
    RectTransform _TxtAnimation;
   RectTransform _BtnRestartAnimation;
   RectTransform _BtnQuitAnimation;
    RectTransform _TxtAnimationGameOver;
    RectTransform _TxtAnimationWantToTryAgn;

   [SerializeField] PlayerCtrl _playerCtrl;

    [HideInInspector]
    [SerializeField] Animator _txtAnimator;
    [HideInInspector]
    [SerializeField] Animator _BtnRestartAnimator;
    [HideInInspector]
    [SerializeField] Animator _BtnrestartTxtAnimator;
    [HideInInspector]
    [SerializeField] Animator _BtnQuitAnimator;
    [HideInInspector]
    [SerializeField] Animator _BtnQuitTxtAnimator;
    [HideInInspector]
    [SerializeField] Animator _txtAnimatorGameOver;
    [HideInInspector]
    [SerializeField] Animator _txtAnimatorWantToTryAgn;
    [HideInInspector]
    [SerializeField] Animator _BtnRestartAnimatorgGameOver;
    [HideInInspector]
    [SerializeField] Animator _BtnQuitAnimatorGameQuit;
    [HideInInspector]
    [SerializeField] Animator _BtnRestartTxtAnimatorGameOver;
    [HideInInspector]
    [SerializeField] Animator _BtnQuitTxtAnimatorGameOver;
    [HideInInspector]
    [SerializeField] Animator _BtnPauseAnimator;


    public GameObject _StartingPanel;
[HideInInspector] [SerializeField] Animator _StartingPanelAnim;
    AnimatorStateInfo _StartingPanelAnimatorStateInfo;

    public GameObject _StartingTxt;
   [HideInInspector] [SerializeField] Animator _StartingTxtAnim;
    AnimatorStateInfo _StartingTxtAnimatorStateInfo;



    

    bool HasScaleX = false;
    bool HasScaleY = false;
    bool DOMovBoolean = false;
    bool AnimatorHasBeenShown = false;
    bool DoOnce = false;
    bool DoTwice = false;
    bool DoThird = false;
    bool DoFour = false;
    bool HaveJump = false;
    bool HaveDoJump = false;
    bool HaveBtnAppeared = false;
    bool HavePauseDissapear= false;
    bool TurnOnInteracbleGaneClr = false;
    bool TurnOffInteracbleGameOver = false;
    bool InteracbleIEumulator = false;
    bool HavePauseDissapearAnimation = false;
    bool HavePlayerDestroyed = false;
    bool HaveShownClear = false;
    bool HaveShownFail = false;
    private static string BtnAnimationSetActive = "SetActiveToTrue";
    public Slider _LifeSlider;
    public GameObject _audioResourseisGaming;
    public GameObject _audioResourseisGameClr;
    public GameObject _audioResourseisGameOver;
    public int ScreenWidth = 800;
    public int ScreenHeight = 600;




    public GameObject[] _RedLines;
    public GameObject _SubCameras;
    bool HasDisActiveBfRedLinesGClr = false;
    bool HasDisActiveBfSubCameraGClr = false;
    bool HasDisActiveBfRedLinesGOver = false;
    bool HasDisActiveBfSubCameraGOver = false;
    bool isTimeToActiveRL = false;
    bool isTimeToActiveSubCamera = false;




    public GameObject _PrefectTxt;
    Animator _PrefectTxtAnimator;
    AnimatorStateInfo _AnimStateInfoPrefectTxt;
    PlayerLifeStatement _PlayerLifeStatement;
    bool HasAnimBfPrefectTxtGClr = false;
    bool HasSetActiveBfPrefectTxt = false;
    bool HasSetActiveAfPrefectTxt = false;




    void Awake()
    {
        GameClear = false;
        GameOver = false;
        AvoidNullProblem();
        SettingResolutionProblem();
    }
    void Start()
    {
        AvoidNullProblem();
        BasicSetting();
    }
  void BasicSetting()
    {
        ButtonInteracble(false);
        HasScaleX = false;
        HasScaleY = false;
        DOMovBoolean = false;
        AnimatorHasBeenShown = false;
        HaveShownClear = false;
        HaveShownFail = false;
        DoOnce = false;
        DoTwice = false;
        DoThird = false;
        DoFour = false;
        HaveJump = false;
        HaveDoJump = false;
        HaveBtnAppeared = false;
        _PauseBtnGameObject.GetComponent<Button>().interactable = true;
        HavePauseDissapear = false;
        TurnOnInteracbleGaneClr = false;
        TurnOffInteracbleGameOver = false;
        InteracbleIEumulator = false;
        HavePauseDissapearAnimation = false;
        HavePlayerDestroyed = false;
        _PauseBtnGameObject.SetActive(false);
        _LifeSliderObject.SetActive(false);
        _playerCtrl.enabled = false;
        _bossScript.enabled = false;
        HasDisActiveBfRedLinesGClr = false;
        HasDisActiveBfSubCameraGClr = false;
        HasDisActiveBfRedLinesGOver = false;
         HasDisActiveBfSubCameraGOver = false;
        isTimeToActiveSubCamera = false;
        isTimeToActiveRL = false;
        LifeSliderSetActiveAtFirst = false;
        PauseBtnSetActiveAtFirst = false;
        HasAnimBfPrefectTxtGClr = false;
        HasSetActiveBfPrefectTxt = false;
         HasSetActiveAfPrefectTxt = false;

    }

    void Update()
    {
        GameOverClrFunction();
        UpdatePlayerLifeProblem();
        SetActivetobeChanged();
        BeginAnimatorProblem();
        GetCurrentPrefectTxtAnimatorFunction();
    }
    void AvoidNullProblem()
    {
        if (_INSTANCE == null) _INSTANCE = this;
        if (_player == null) GameObject.FindGameObjectWithTag("Player");
        if (_playerCtrl == null) _playerCtrl = _player.GetComponent<PlayerCtrl>();
        if (_ClearPanelAnimation == null) _ClearPanelAnimation = _ClrPanel.GetComponent<RectTransform>();
        if (_TxtAnimation == null) _TxtAnimation = _Txt.GetComponent<RectTransform>();
        if (_txtAnimator == null) _txtAnimator = _Txt.GetComponent<Animator>();
        if (_BtnRestartAnimator == null)_BtnRestartAnimator = _BtnRestart.GetComponent<Animator>();
        if (_BtnrestartTxtAnimator == null) _BtnrestartTxtAnimator = _BtnRestart.GetComponent<Animator>();
        if (_BtnQuitAnimator == null) _BtnQuitAnimator = _BtnQuit.GetComponent<Animator>();
        if (_BtnQuitTxtAnimator == null) _BtnQuitTxtAnimator = _BtnQuitTxt.GetComponent<Animator>();
        if (_BtnQuitAnimation == null) _BtnQuitAnimation = _BtnQuit.GetComponent<RectTransform>();
        if (_BtnRestartAnimation == null) _BtnRestartAnimation = _BtnRestart.GetComponent<RectTransform>();
        if (_txtAnimatorGameOver == null) _txtAnimatorGameOver = _GameOverTxt.GetComponent<Animator>();
        if (_TxtAnimationGameOver == null) _TxtAnimationGameOver = _GameOverTxt.GetComponent<RectTransform>();
        if (_txtAnimatorWantToTryAgn == null) _txtAnimatorWantToTryAgn = _WantTotryAgainTxt.GetComponent<Animator>();
        if (_TxtAnimationWantToTryAgn == null) _TxtAnimationWantToTryAgn = _WantTotryAgainTxt.GetComponent<RectTransform>();
        if (_BtnRestartAnimatorgGameOver == null) _BtnRestartAnimatorgGameOver = _BtnRestartGameOver.GetComponent<Animator>();
        if (_BtnQuitAnimatorGameQuit == null) _BtnQuitAnimatorGameQuit = _BtnQuitGameOver.GetComponent<Animator>();
        if (_BtnRestartTxtAnimatorGameOver == null) _BtnRestartTxtAnimatorGameOver = _BtnRestartTxtGameOver.GetComponent<Animator>();
        if (_BtnQuitTxtAnimatorGameOver == null) _BtnQuitTxtAnimatorGameOver = _BtnQuitTxtGameOver.GetComponent<Animator>();
        if (_BtnPauseAnimator == null) _BtnPauseAnimator = _PauseBtnGameObject.GetComponent<Animator>();
        if (_bossScript == null) _bossScript = _BossActiveTime.GetComponent<BOSSScript>();
        if (_StartingPanel == null) Debug.Log("That's something You need to Add Before ");
        if (_StartingPanelAnim == null) _StartingPanelAnim = _StartingPanel.GetComponent<Animator>();
        if (_StartingTxt == null) Debug.Log("That's something You need to Add");
        if (_StartingTxtAnim == null) _StartingTxtAnim = _StartingTxt.GetComponent<Animator>();
        if (_RedLines == null) Debug.Log("That's Something You need To Added Which is the Red Lines Color");
        if (_SubCameras == null) Debug.Log("That's Something You need To Added Which is Sub Camera ");
        if (_PrefectTxtAnimator == null) _PrefectTxtAnimator = _PrefectTxt.GetComponent<Animator>();
        if (_PlayerLifeStatement == null) _PlayerLifeStatement = _player.GetComponent<PlayerLifeStatement>();



    }
    void BeginAnimatorProblem()
    {
        if(_StartingPanel.activeInHierarchy)
        _StartingPanelAnimatorStateInfo = _StartingPanelAnim.GetCurrentAnimatorStateInfo(0);
        if(_StartingPanelAnimatorStateInfo.IsName("BossTrainerFight") && _StartingPanelAnimatorStateInfo.normalizedTime >= 0.99f)
        {
            _StartingPanel.SetActive(false);

            if(!LifeSliderSetActiveAtFirst)
            {
                LifeSliderSetActiveAtFirst = true;
                _LifeSliderObject.SetActive(true);
                
            }
           
            if(!PauseBtnSetActiveAtFirst)
            {
                _PauseBtnGameObject.SetActive(true);
                PauseBtnSetActiveAtFirst = true;
            }
            if (!HasntEnableBeforeBOSS)
            {
                HasntEnableBeforeBOSS = true;
                if (_bossScript != null)
                {
                    _bossScript.enabled = true;
                }
                else Debug.LogWarning("Please Add The Boss Script In the Script");
            }
            if(!HasntEnableBeforePlayer)
            {
                if (_playerCtrl != null)
                {
                    _playerCtrl.enabled = true;
                }
                else Debug.LogWarning("Please Add The PlayerCtrl In The Object ");
                HasntEnableBeforePlayer = true;
            }
            if (!isTimeToActiveRL)
            {
                isTimeToActiveRL = true;
                foreach(GameObject rls in _RedLines)
                {
                    if(rls != null)
                    {
                        rls.SetActive(true);
                    }
                }
            }
            if (!isTimeToActiveSubCamera)
            {
                isTimeToActiveSubCamera = true;
                    if(_SubCameras != null)
                    {
                        _SubCameras.SetActive(true);
                    }
            }
        }
    }
    void SettingResolutionProblem()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }
    }
    void GameOverClrFunction()
    {
        if (PlayerLifeStatement._INSTANCE.Health <= 0)
        {
            GameOver = true;
            GameClear = false;
        }
        if (GameClear&& !GameOver)
        {
            if(_playerCtrl != null)
            {
                _playerCtrl.CanMov = false;
            }
            if (_PauseBtnGameObject != null)
            {
                _PauseBtnGameObject.GetComponent<Button>().interactable = false;
            }
            if (_audioResourseisGaming != null)
            {
                _audioResourseisGaming.SetActive(false);
            }
            if (_audioResourseisGameClr != null)
            {
                _audioResourseisGameClr.SetActive(true);
            }
            foreach(GameObject es in _EnemySoldiers)
            {
                if (es != null)
                {
                    es.SetActive(false);
                }

               
            }
            foreach(GameObject egs in _EnemyGunSoldiers)
            {
                if(egs != null)
                {
                    egs.SetActive(false);
                }
            }
            if(_LifeSliderObject != null)
            {
                _LifeSliderObject.SetActive(false);
            }
            foreach(GameObject b in PlayerHitTrigger)
            {
                if( b != null)
                {
                    b.SetActive(false);
                }
            }
            foreach(GameObject bht in BOSSHitTrigger)
            {
                if(bht != null)
                {
                    bht.SetActive(false);
                }
            }
            if (!HaveShownClear)
            {
                StartCoroutine("ClrActiveSetAsTrue");
            }
            if(!HasDisActiveBfRedLinesGClr)
            {
                HasDisActiveBfRedLinesGClr = true;
                foreach(GameObject SC in _RedLines)
                {
                    if(SC != null)
                    {
                        SC.SetActive(false);
                    }
                }
            }
            if(!HasDisActiveBfSubCameraGClr)
            {
                HasDisActiveBfSubCameraGClr = true;
                if (_SubCameras != null)
                {
                    _SubCameras.SetActive(false);
                }
                else Debug.Log("That's Something You need to Added");
            }

            if(_PlayerLifeStatement.Health == 20)
            {
                StartCoroutine("PrefectCoroutine");
            }
        }
        else if(GameOver && !GameClear)
        {
            if(_audioResourseisGaming != null)
            {
                _audioResourseisGaming.SetActive(false);
            }       
            if(_audioResourseisGameOver != null)
            {
                _audioResourseisGameOver.SetActive(true);
            }
            foreach (GameObject es in _EnemySoldiers)
            {
                if (es != null)
                {
                    es.SetActive(false);
                }
            }
            foreach (GameObject egs in _EnemyGunSoldiers)
            {
                if(egs != null)
                {
                    egs.SetActive(false);
                }
            }
            foreach (GameObject bht in BOSSHitTrigger)
            {

                if (bht != null)
                {
                    bht.SetActive(false);
                }
            }
            if (_PauseBtnGameObject != null)
            {
                _PauseBtnGameObject.GetComponent<Button>().interactable = false;
            }
            else Debug.Log("That's somethingyou need to add");
            if (_LifeSliderObject != null)
            {
                _LifeSliderObject.SetActive(false);
            }
            else Debug.Log("Life Slider has something you need to add");
            if (!HaveShownFail)
            {
                StartCoroutine("GameOverSetAsTrue");
            }
            if (!HasDisActiveBfRedLinesGOver)
            {
                HasDisActiveBfRedLinesGOver = true;
                foreach (GameObject SC in _RedLines)
                {
                    if (SC != null)
                    {
                        SC.SetActive(false);
                    }
                }
            }
            if (!HasDisActiveBfSubCameraGOver)
            {
                HasDisActiveBfSubCameraGOver = true;
                if(_SubCameras != null)
                {
                    _SubCameras.SetActive(false);
                }
            }
        }
        else if (GameClear && GameOver)
        {
            //Debug.Break();
            Debug.LogError("This is strange , You might need to Check Something else happening");
        }
    }
    void GetCurrentPrefectTxtAnimatorFunction()
    {
        if (_PrefectTxt.activeInHierarchy)
        {
            _AnimStateInfoPrefectTxt = _PrefectTxtAnimator.GetCurrentAnimatorStateInfo(0);
        }
    }
    IEnumerator ClrActiveSetAsTrue()
    {
        yield return new WaitForSeconds(0.6f);
        _ClrPanel.SetActive(true);

        if (!HavePauseDissapear)
        {
            StartCoroutine("PauseActiveAsTheFalse");
        }

        yield return new WaitForSeconds(1f);
        if (!HasScaleX)
        {
            HasScaleX = true;
            _ClearPanelAnimation.DOScaleX(1.5f,0.6f);
        }
        yield return new WaitForSeconds(0.5f);
        if(!HasScaleY)
        {
            HasScaleY = true;
            _ClearPanelAnimation.DOScaleY(1.2f, 0.5f);
        }
        yield return new WaitForSeconds(0.9f);
        _Txt.SetActive(true);
        _txtAnimator.SetBool("IfSetActivetrue", true);
        yield return new WaitForSeconds(.5f);
        if(!DOMovBoolean)
        {
            _TxtAnimation.DOMoveX(320, 4f);
            DOMovBoolean = true;
        }
        yield return new WaitForSeconds(5f);
        _BtnRestart.SetActive(true);
        _BtnQuit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        if (!AnimatorHasBeenShown)
        {
            AnimatorHasBeenShown = true;
            _BtnRestartAnimator.SetBool(BtnAnimationSetActive, true);
            _BtnQuitAnimator.SetBool("SetActiveToTrue", true);
        }
        _BtnQuitTxt.SetActive(true);
        _BtnRestartTxt.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        _BtnrestartTxtAnimator.SetBool("SetActiveToTrue", true);
        _BtnQuitTxtAnimator.SetBool("SetActiveToTrue", true);
        yield return new WaitForSeconds(0.1f);
        if (!TurnOnInteracbleGaneClr)
        {
            ButtonInteracble(true);
            TurnOnInteracbleGaneClr = true;
        }
        StopCoroutine("ClrActiveSetAsTrue");
        HaveShownClear = true;
    }
    IEnumerator GameOverSetAsTrue()
    {
        yield return new WaitForSeconds(0.6f);

        if(!HavePlayerDestroyed )
        {
            HavePlayerDestroyed = true;
            Destroy(_player,3f);
        }
        _GameOverPanel.SetActive(true);
        if (!HavePauseDissapear)
        {
            StartCoroutine("PauseActiveAsTheFalse");
        }
        yield return new WaitForSeconds(1.2f);
        _GameOverTxt.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _txtAnimatorGameOver.SetBool("SetActiveAsTrue", true);
        yield return new WaitForSeconds(0.2f);
        if (!DoOnce)
        {
            _TxtAnimationGameOver.DOScaleX(0.76f, 0.3f);
            DoOnce = true;
        }
        yield return new WaitForSeconds(0.2f);
        if (!DoTwice)
        {
            _TxtAnimationGameOver.DOScaleX(1.06f, 0.3f);
            DoTwice = true;
        }
        yield return new WaitForSeconds(0.2f);
        if (!DoThird)
        {
                _TxtAnimationGameOver.DOScaleX(0.72f, 0.3f);
                DoThird = true;
        }
        yield return new WaitForSeconds(0.2f);
        if (!DoFour)
        {
            _TxtAnimationGameOver.DOScaleX(1.02f, 0.3f);
            DoFour = true;
        }
        HaveShownFail = true;
        StopCoroutine("GameOverSetAsTrue");
        if (!HaveJump)
        {
            StartCoroutine("JumpingTxtSetActive");
        }
    }
    IEnumerator JumpingTxtSetActive()
    {
        yield return new WaitForSeconds(1f);
        _WantTotryAgainTxt.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        _txtAnimatorWantToTryAgn.SetBool("SetActiveAsTrue", true);
        yield return new WaitForSeconds(0.5f);
        if (!HaveDoJump)
        {
            HaveDoJump = true;
            _TxtAnimationWantToTryAgn.DOLocalJump(new Vector3(-16,66f, 0), 15, 6, 3F);
        }
        HaveJump = true;
        StopCoroutine("JumpingTxtSetActive");
        if (!HaveBtnAppeared)
        {
            StartCoroutine("BtnTextAppeared");
        }
    }
    // IEumerator : BtnTextAppeared
    // Method : This is use for Text Appeared Method 
    IEnumerator BtnTextAppeared()
    {
        yield return new WaitForSeconds(0.4f);
        _BtnRestartGameOver.SetActive(true);
        _BtnQuitGameOver.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _BtnRestartAnimatorgGameOver.SetBool("SetActiveAsTrue", true);
        _BtnQuitAnimatorGameQuit.SetBool("SetActiveAsTrue", true);
        yield return new WaitForSeconds(0.5f);
        _BtnRestartTxtGameOver.SetActive(true);
        _BtnQuitTxtGameOver.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _BtnRestartTxtAnimatorGameOver.SetBool("SetActiveAsTrue", true);
        _BtnQuitTxtAnimatorGameOver.SetBool("SetActiveAsTrue", true);
        yield return new WaitForSeconds(0.1f);

      
        HaveBtnAppeared = true;
        StopCoroutine("BtnTextAppeared");


        if (!InteracbleIEumulator)
        {
            StartCoroutine("InteracbleTurnOn");
        }
    }

    // IEumerator : InteracbleTurnOn
    // Method : This is use for when
    // Game Over Interacble Turn On Problem
    IEnumerator InteracbleTurnOn()
    {
        yield return new WaitForSeconds(2f);
        if (!TurnOffInteracbleGameOver)
        {
            ButtonInteracble(true);
            TurnOffInteracbleGameOver = true;
        }
        InteracbleIEumulator = true;
    }
    //IEumerator :PauseActiveAsTheFalse
    // This method is use for when Game Over (Or Game Clear)
    // to Present the Animation and DisActive Pause Btn Object
    IEnumerator PauseActiveAsTheFalse()
    {

        yield return new WaitForSeconds(0.4f);
        if(!HavePauseDissapearAnimation)
        {
            HavePauseDissapearAnimation = true;
            _BtnPauseAnimator.SetBool("SetActiveAsTrue", true);
        }
        yield return new WaitForSeconds(1.01f);
        _PauseBtnGameObject.SetActive(false);
        HavePauseDissapear = true;
    }
    // Function : UpdatePlayerLifeProblem 
    // Method : To Present the Life which is player Left
    void UpdatePlayerLifeProblem()
    {
        if(_player != null)
        {
        _LifeSlider.value = _player.GetComponent<PlayerLifeStatement>().Health;
        }
    }
    // Function : ButtonInteracble
    // Method : To turn on or turn off the interactable
    void ButtonInteracble(bool interactable)
    {
        _BtnRestart.GetComponent<Button>().interactable = interactable;
        _BtnQuit.GetComponent<Button>().interactable = interactable;
        _BtnQuitGameOver.GetComponent<Button>().interactable = interactable;
        _BtnRestartGameOver.GetComponent<Button>().interactable = interactable;
    }
    //Set Active to be Changed 
    // This Function is mainly used for set Active BOSS Soldier
    void SetActivetobeChanged()
    {
        if (_bossScript != null)
        {
            if (_bossScript.enabled)
            {
                if (_bossScript != null)
                {
                    if (_bossScript.Life <= 15)
                    {
                        _SoldierSetActive.SetActive(true);
                    }
                }
                else { Debug.LogWarning("That's something You need to add"); }
            }
        }
        if (_bossScript != null)
        {
            if (_bossScript.enabled)
            {
                if (_bossScript != null)
                {
                    if (_bossScript.Life <= 5)
                    {
                        _SoldierSetActive2.SetActive(true);
                    }
                }
                else { Debug.LogWarning("That's something You need to add"); }
            }
        }
    }

    IEnumerator PrefectCoroutine()
    {

        yield return new WaitForSeconds(0.1f);

        if (!HasSetActiveBfPrefectTxt)
        {

            HasSetActiveBfPrefectTxt = true;
            _PrefectTxt.SetActive(true);
        }
        yield return new WaitForSeconds(2.5f);

        if (!HasAnimBfPrefectTxtGClr)
        {
            HasAnimBfPrefectTxtGClr = true;
            _PrefectTxtAnimator.SetBool("SetActiveAsTrue", true);
        }

        yield return new WaitForSeconds(1f);
        if (!HasSetActiveAfPrefectTxt)
        {
            HasSetActiveBfPrefectTxt = true;
            if (_PrefectTxt.activeInHierarchy)
            {
                if (_AnimStateInfoPrefectTxt.IsName("PrefectTxtAnimation") && _AnimStateInfoPrefectTxt.normalizedTime >= 0.99f)
                {
                    _PrefectTxt.SetActive(false);
                }
            }
        }
        yield return new WaitForSeconds(3f);
        StopCoroutine("PrefectCoroutine");
    }
}
