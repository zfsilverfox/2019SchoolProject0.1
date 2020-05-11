using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///  /
/// /Second Screen Game Manager 2 
/// </summary>
public class GameManager2 : MonoBehaviour
{
    public GameObject _Player;
    public int ScoreToUnlockTheBoxCollider;
    public int ScoreLimit;
    [HideInInspector]
    public bool GameOver= false;
    [HideInInspector]
    public bool GameClear=false;
    public GameClearObject _ClearObject;
    public PlayerLifeStatement _playerLife;
    public GameObject _GameClearObject;
    public GameObject _UnlockedBox;
    public GameObject _GameOverPanelSetActiveOn;
    public GameObject _PauseBtn;
    public GameObject _LifeSliderGameObject;
    public GameObject _TeachingThePosition;
    public GameObject _TipsDo;
    public GameObject _KillEnemyCountObj;
    public GameObject _ThankYouPlayingAnotherMission;
    public GameObject _LastBlockingBlock;





    public float TeachingCrtTime = 0.0f;
[SerializeField][Range(1,5)]float TeachingMaxTime = 3.0f;




    [HideInInspector]
    [SerializeField] Animator _PauseAnim;
    [HideInInspector]
    [SerializeField] Animator _LifeSliderAnim;
    [SerializeField] Animator _KillEnemyCountDownAnimator;

    [SerializeField] AnimatorStateInfo _KillEnemyCountDownAnimStateInfo;

    bool HasntStartBeforeCountEnemyKill = false;
    bool HasntAnimatorBeforeCountEnemyKillDown = false;







     bool HasntPlayBeforeFungus1= false;
   bool HasntPlayBeforeFungus2 = false;
     bool PauseAnimationGameOver = false;
     bool PauseAnimationGameClear = false;
     bool LifeSliderActiveToFalseGameOver = false;
     bool LifeSliderActiveToFalseGameClear = false;
      bool LifeSliderHasPresentedBfGO = false;
     bool LifeSliderHasPresentedBfGC = false;
    bool isPlayingAnimationPause = false;
    bool HasntPlyBfSdGO = false;
    bool HasntPlyBfSdGC = false;
    public Slider _LifeSlider;
    public Text _EnemyCountTxt;
    public int ScreenWidth= 800;
    public int ScreenHeight = 600;



    public GameObject _EachTenObject;
    [HideInInspector][SerializeField] Animator _EachTenAnim;
    bool HasPlayAnimBeforeTenObject;
    bool HasPlayAnimBeforeTen;


    public GameObject _EachTwentyObject;
    [HideInInspector]
    [SerializeField] Animator _EachTwentyAnimator;
    bool HasPlayAnimaBeforeTwenObject;
    bool HasPlayAnimBeforeTwenty;


    public GameObject _EachThirtyObject;
    [HideInInspector]
    [SerializeField] Animator _EachThirdtyAnimator;
    bool HasPlayAnimBeforeThirdtyObject;
    bool HasPlayeAnimBeforeThirdty;


    public GameObject _EachFourtyObject;
   [HideInInspector][SerializeField] Animator _EachFourtyAnimator;
    bool HasPlayAnimBeforeFourtyObj;
    bool HasPlayAnimBeforeFourty;


    public GameObject _EachFiftyObject;
 [HideInInspector][SerializeField] Animator _EachFiftyAnimator;
    bool HasPlayAnimBeforeFiftyObj;
    bool HasPlayAnimBeforeFifty;
    bool isPlayingPauseAnimationGameClrPart = false;



    public GameObject _NpcChracter;
    bool NpcDisActiveBefore  = false;






    public SpecialMission2 _spMission2;

    [HideInInspector]
    public int KillWolfNum=0;
    bool ExtraMissionHavePlayBefore = false;
    bool ExtraMissionHasDoBefore = false;



   public  float MissionStartTime = 0.0f;
    public float MissionOverTime = 3.0f;


    public AudioSource _adPlaySound;
    public AudioSource _adGameOverSound;


    public GameObject _KillThreeBlock;
    bool HasKillThreeEnemyBlockHasDisActiveBf = false;




    public GameObject _SecondAppreadMission;
    bool HasSecondMissionAppBf;



    public GameObject _WolfAppreadTxt;
    bool HasAppBfWolfTxt = false;

    public GameObject _RescueObject;
    bool HasAppreadBeforeRescueObj;

    public GameObject _ReceivedRewardTxt;
    bool HasReceivedRewardBefore = false;


    public GameObject _RequestMission;
    bool HasDoneBeforeRequestMission = false;



    public GameObject[] TwoRedLines;
    public GameObject _SubCameras;
    bool HasTurnOffBfTwoRedLinesGO = false;
    bool HasTurnOffBfSubCameraGO = false;
    bool HasTurnOffBfTwoRedLinesGClr = false;
    bool HasTurnOffBfSubCameraGClr = false;



    bool ThankYouForPyGO;


    public GameObject _LimitBreakPanel;
    bool HasAppreadBeforeLimitBreak = false;
    float LimitBreakPanelCrtTime = 0.0f;
    float LimitBreakPanelMaxTime = 6.5f;


    public GameObject _WhereIsThePosPanel;
    bool HasAppreadBfWhereIsThePanel = false;
    float WhereIsThePanelCrtTime = 0.0f;
    float WhereIsThePanelMaxTime = 3.4f;


    //Awake Function is used for Avoid the Null Problem
    // Or Setting The Platform Problem
   void Awake()
    {
        AvoidNullProb();
        SettingPlatformProb();
    }

    // This Function is used for Setting the Platform Problem
    void SettingPlatformProb()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight,true);
        }


    }
    // This Function is used for Avoid The Null Problem
    // Or Setting The Info Prob 
    void Start()
    {
        AvoidNullProb();
        SettingBasicInformation();
    }
    // This Function is used for Setting The Basic Information
    void SettingBasicInformation()
    {
        ScoreToUnlockTheBoxCollider = 0;
            GameOver = false;
        GameClear = false;
            HasntPlayBeforeFungus1 = false;
        PauseAnimationGameOver = false;
            PauseAnimationGameClear = false;
        LifeSliderActiveToFalseGameOver = false;
            LifeSliderActiveToFalseGameClear = false;
            isPlayingAnimationPause = false;
        LifeSliderHasPresentedBfGO = false;
            LifeSliderHasPresentedBfGC = false;
            HasntPlyBfSdGO = false;
        HasntPlyBfSdGC = false;
            HasntPlayBeforeFungus2 = false;
        HasPlayAnimBeforeTenObject = false;
            HasPlayAnimBeforeTen = false;
        HasPlayAnimaBeforeTwenObject = false;
            HasPlayAnimBeforeTwenty = false;
        HasPlayAnimBeforeThirdtyObject = false ;
            HasPlayeAnimBeforeThirdty = false;
        HasPlayAnimBeforeFourtyObj = false;
            HasPlayAnimBeforeFourty = false;
        HasPlayAnimBeforeFiftyObj = false;
            HasPlayAnimBeforeFifty = false;
        isPlayingPauseAnimationGameClrPart = false;
        HasntStartBeforeCountEnemyKill = false;
        HasntAnimatorBeforeCountEnemyKillDown = false;
        KillWolfNum = 0;
        ExtraMissionHavePlayBefore = false;
        ExtraMissionHasDoBefore = false;
        NpcDisActiveBefore = false;
        HasKillThreeEnemyBlockHasDisActiveBf = false;
        HasSecondMissionAppBf = false;
        HasAppBfWolfTxt = false;
        HasAppreadBeforeRescueObj = false;
        HasReceivedRewardBefore = false;
        HasDoneBeforeRequestMission = false;
        ThankYouForPyGO = false;
        HasTurnOffBfTwoRedLinesGO = false;
        HasTurnOffBfSubCameraGO = false;
        HasAppreadBeforeLimitBreak = false;
        HasAppreadBfWhereIsThePanel = false;
        HasTurnOffBfTwoRedLinesGClr = false;
        HasTurnOffBfSubCameraGClr = false;
    }
    // This Function is used for Avoid Null Problem
    void AvoidNullProb()
    {
        if(_ClearObject == null)
        _ClearObject = _GameClearObject.GetComponent<GameClearObject>();
        if (_playerLife == null)
            _playerLife = _Player.GetComponent<PlayerLifeStatement>();
        if (_PauseBtn == null) Debug.LogWarning("Please Add the Pause Button");
        if (_PauseAnim == null) _PauseAnim = _PauseBtn.GetComponent<Animator>();
        if (_LifeSlider == null) Debug.Log("That is something strange ?");
        if (_LifeSliderAnim == null) _LifeSliderAnim = _LifeSlider.GetComponent<Animator>();
        if (_TeachingThePosition == null) Debug.LogError("Please Added Some Object To this");
        if (_EachTenAnim == null) _EachTenAnim = _EachTenObject.GetComponent<Animator>();
        if (_EachTwentyAnimator == null) _EachTwentyAnimator = _EachTwentyObject.GetComponent<Animator>();
        if (_EachThirdtyAnimator == null) _EachThirdtyAnimator = _EachThirtyObject.GetComponent<Animator>();
        if (_EachFourtyAnimator == null) _EachFourtyAnimator = _EachFourtyObject.GetComponent<Animator>();
        if (_EachFiftyAnimator == null) _EachFiftyAnimator = _EachFiftyObject.GetComponent<Animator>();
        if (_TipsDo == null) Debug.Log("Please Drap the TipDO In This Script");
        if (_KillEnemyCountDownAnimator == null) _KillEnemyCountDownAnimator = _KillEnemyCountObj.GetComponent<Animator>();
        if (_ThankYouPlayingAnotherMission == null) Debug.Log("PleaseAddSomethings");
        if (_spMission2 == null) Debug.Log("That's Something you need to added ");
        if (_NpcChracter == null) Debug.Log("That's A Chracter which is NPC You need to added ");
        if (_WolfAppreadTxt == null) Debug.Log("That's something You need to added");

    }
    // Update Function
    // This Function is used for Checking Unlock Stage Collider,GameClear,GameClear,ScoreEarn 
  void Update()
    {
        UnlockColliderBox();
        UnLockTheThreeBlock();
        GameOverGameClearFunction();
        GameClearFunction();
        LRAndTFGTTP();
        TeachingTime();
        PresentScore();
        KillNumWolfFunction();
        _EnemyCountTxt.text = ScoreToUnlockTheBoxCollider.ToString();
        _LifeSlider.value = _playerLife.Health;



        if(_KillEnemyCountObj.activeInHierarchy)
        _KillEnemyCountDownAnimStateInfo = _KillEnemyCountDownAnimator.GetCurrentAnimatorStateInfo(0);
    }
    // This Function is used 
    void UnlockColliderBox()
    {
        if(ScoreToUnlockTheBoxCollider >= ScoreLimit)
        {
            if (_UnlockedBox != null)
            {
                
                 _UnlockedBox.SetActive(false);
            }   
            else Debug.Log("That's somethins strange ,You need to Do Something ");

            if (_TipsDo != null) _TipsDo.SetActive(false);
            if (!HasAppreadBeforeLimitBreak)
            {
                if (_LimitBreakPanel != null) _LimitBreakPanel.SetActive(true);
                HasAppreadBeforeLimitBreak = true;
            }
        }
        if (_LimitBreakPanel.activeInHierarchy)
        {
            LimitBreakPanelCrtTime += Time.deltaTime;


            if(LimitBreakPanelCrtTime >= LimitBreakPanelMaxTime)
            {
                if(_LimitBreakPanel != null)
                {
                    _LimitBreakPanel.SetActive(false);
                }


                if (!HasAppreadBfWhereIsThePanel)
                {
                    _WhereIsThePosPanel.SetActive(true);



                    HasAppreadBfWhereIsThePanel = true;
                }
            }
        }
        if (_WhereIsThePosPanel.activeInHierarchy)
        {
            WhereIsThePanelCrtTime += Time.deltaTime;

            if(WhereIsThePanelCrtTime >= WhereIsThePanelMaxTime)
            {
                if (_WhereIsThePosPanel.activeInHierarchy)
                {
                    _WhereIsThePosPanel.SetActive(false);
                }
            }
        }
    }
    // This is the Fucntion is meaning that Limit is Reached And
    // Told The Player Fastly Go To The Position
    void LRAndTFGTTP()
    {
        if(ScoreToUnlockTheBoxCollider >= 55)
        {
            if (!HasntPlayBeforeFungus2)
            {

                HasntPlayBeforeFungus2 = true;
                MessageReceived[] receivers = GameObject.FindObjectsOfType<Fungus.MessageReceived>();
                if (receivers != null)
                {
                    foreach (var receiver in receivers)
                    {
                        receiver.OnSendFungusMessage("reachingLimited");
                    }
                }





            }
        }
    }
    // GameOverFunction is used for Setting The Basic Function
    void GameOverGameClearFunction()
    {
        if(_playerLife != null)
        {
            if(_playerLife.Health <= 0)
                {
                    GameOver = true;
                    GameClear = false;

                    if(_Player != null)
                    {
                    Destroy(_Player, 3f);
                    }
                }
        }
        if(GameOver && !GameClear)
        {
            _GameOverPanelSetActiveOn.SetActive(true);
            if(_PauseBtn != null)
            {
                _PauseBtn.GetComponent<Button>().interactable = false;

                if (!PauseAnimationGameOver)
                {
                    StartCoroutine("GameOverIEnumerator");
                }
            }
            if(_LifeSliderGameObject != null)
            {
                if (!LifeSliderHasPresentedBfGO)
                {
                    StartCoroutine("LifeSilderHasToBeenTurnOffGO");
                }
            }
            //   if (_PlayerCountObject != null) _PlayerCountObject.SetActive(false);
            if (!HasntStartBeforeCountEnemyKill)
            {
                StartCoroutine("CountDownEnemyKillSetDisActive");
            }
            if(!ThankYouForPyGO)
            {
                ThankYouForPyGO = true;
                if (_ThankYouPlayingAnotherMission.activeInHierarchy)
                {
                    if (_ThankYouPlayingAnotherMission != null) _ThankYouPlayingAnotherMission.SetActive(false);
                }
            }
            if(_adPlaySound != null)
            {
                if(_adGameOverSound != null)
                {
                    if (_adPlaySound.isPlaying)
                    {
                        _adPlaySound.Stop();
                        _adGameOverSound.Play();
                    }
                }

            }
            if(!HasTurnOffBfTwoRedLinesGO)
            {
                HasTurnOffBfTwoRedLinesGO = true;
                if (TwoRedLines != null)
                {
                    foreach (GameObject twl in TwoRedLines)
                    {
                        twl.SetActive(false);
                    }

                }
                else Debug.Log("That's Something You need to Know");


           }
            if (!HasTurnOffBfSubCameraGO)
            {
                HasTurnOffBfSubCameraGO = true;
                if (_SubCameras != null)
                {
                    _SubCameras.SetActive(false);
                }
                else Debug.Log("That's Something You need to Know ");
            }
        }
        if(GameOver && GameClear)
        {
            Debug.LogWarning("That's Something Strange , You might need to Check It What Happening");
        }
        if(GameClear && !GameOver)
        {
           // Debug.Log("Hello GameClear");
            if (!HasntStartBeforeCountEnemyKill)
            {
                StartCoroutine("CountDownEnemyKillSetDisActive");
            }
            if (_PauseBtn != null)
                {
                _PauseBtn.GetComponent<Button>().interactable = false;

                    if (!PauseAnimationGameClear)
                    {
                        StartCoroutine("GameClearIEnumerator");
                    }
                }

            if(_LifeSliderGameObject != null)
                {
                    if (!LifeSliderHasPresentedBfGC)
                    {
                        StartCoroutine("LifeSilderHasToBeenTurnOffGC");
                    }    
                }
            if (!HasTurnOffBfSubCameraGClr)
            {
                HasTurnOffBfSubCameraGClr = true;
                if (_SubCameras != null)
                {
                    _SubCameras.SetActive(false);
                }
                else Debug.Log("That's Something You need to added ");
            }
            if (!HasTurnOffBfTwoRedLinesGClr)
            {
                HasTurnOffBfTwoRedLinesGClr = true;

                foreach(GameObject rl in TwoRedLines)
                {
                    if (rl != null)
                    {
                        rl.SetActive(false);
                    }
                    else Debug.Log("That's Something You need To Confirm");
                }
            }
        }
    }

    // This is the First Appread which is Going To Tell The Player Where to Go
    void TeachingTime()
    {
        if (ScoreToUnlockTheBoxCollider >= 3)
        {
            _TeachingThePosition.SetActive(true);
        }
        if (_TeachingThePosition.activeInHierarchy)
        {
            TeachingCrtTime += Time.deltaTime;
            if(TeachingCrtTime >= TeachingMaxTime)
            {
                _TeachingThePosition.SetActive(false);
            }
        }
    }

    // This is the First Function When the Player At First Beat the Three Enemy
    void UnLockTheThreeBlock()
    {
        if(ScoreToUnlockTheBoxCollider >= 3)
        {
            if (!HasKillThreeEnemyBlockHasDisActiveBf)
            {
                HasKillThreeEnemyBlockHasDisActiveBf = true;
                if (_KillThreeBlock != null) _KillThreeBlock.SetActive(false);
                else Debug.Log("That's something You need to added");
            }
        }

        if(ScoreToUnlockTheBoxCollider >= 3)
        {
            if (!HasSecondMissionAppBf)
            {
                HasSecondMissionAppBf = true;
                if (_SecondAppreadMission != null) _SecondAppreadMission.SetActive(true);
            }
        }
    }
    // This is the Function is Going To Check When Player is Enter the Game　Clear Area
    void GameClearFunction()
    {
        if (_ClearObject.GameClear)
        {
            GameClear = true;
            GameOver = false;
        }
    }
    // This is the Function Which is Used For Restart The Game
    public void GameRestartBtn()
    {
        SceneManager.LoadScene("GameMain 1");
    }

    // This is the Function use for Quit The Game 
    public void QuitBtn()
    {
        SceneManager.LoadScene("GameStart");
    }
    // This is the Function Which is used For Present The Score
    void PresentScore()
    {
        if(ScoreToUnlockTheBoxCollider >= 10)
        {

            if (!HasPlayAnimBeforeTenObject)
            {
                StartCoroutine("TenObjectIEumurator");
            }
        }
        if(ScoreToUnlockTheBoxCollider >= 20)
        {
            if (!HasPlayAnimaBeforeTwenObject)
            {
                //Debug.Log("has Twenty Combo ");
                StartCoroutine("TwentyObjectIEumurator");
            }
        }
       if(ScoreToUnlockTheBoxCollider >= 30)
        {
            if(!HasPlayAnimBeforeThirdtyObject)
            {
                StartCoroutine("ThirtyObjectIEumurator");
            }


        }
        if (ScoreToUnlockTheBoxCollider >= 40)
        {
            if (!HasPlayAnimBeforeFourtyObj)
            {
                StartCoroutine("FourtyObjectIEumurator");
            }
        }
        if (ScoreToUnlockTheBoxCollider >= 50)
        {
            if(!HasPlayAnimBeforeFiftyObj)
            {
                StartCoroutine("FiftyObjectIEumurator");
            }
        }
    }

    // This is the Extra Mission For The Killing Wolf Mission
    void KillNumWolfFunction()
    {
        if(_spMission2 != null)
        {
            if (_spMission2.IsAcceptMission)
            {
                if(KillWolfNum == 4)
                {
            //       Debug.Log("We add some Active Things");
                    if (!ExtraMissionHavePlayBefore)
                    {

                        if (_ThankYouPlayingAnotherMission != null) _ThankYouPlayingAnotherMission.SetActive(true);


                            ExtraMissionHavePlayBefore = true;
                    }
        }

                if (_ThankYouPlayingAnotherMission.activeInHierarchy)
        {
            MissionStartTime += Time.deltaTime;

            if(MissionStartTime >= MissionOverTime)
            {
                _ThankYouPlayingAnotherMission.SetActive(false);
            }


        }
            }
        }
        if(_spMission2 != null)
        {
            if (_spMission2.IsAcceptMission)
            {
                if (KillWolfNum == 4)
                {
                    if(_LastBlockingBlock != null)
                    {
                        if (_LastBlockingBlock.activeInHierarchy)
                        {
                            _LastBlockingBlock.SetActive(false);
                        }
                    }
                  



                }
            }
        }
        if (_spMission2 != null)
        {
            if (_spMission2.IsAcceptMission)
            {
                if (KillWolfNum == 4)
                {

                    if(!HasAppBfWolfTxt)
                    {
                        if(_WolfAppreadTxt != null)
                        {
                            _WolfAppreadTxt.SetActive(false);
                        }
                        HasAppBfWolfTxt = true;
                    }


                }
            }
        }
        if (_spMission2 != null)
        {

            if (_spMission2.IsAcceptMission)
            {
                if (KillWolfNum == 4)
                {
                    if (!HasAppreadBeforeRescueObj)
                    {
                        HasAppreadBeforeRescueObj = true;

                        if (_RescueObject != null)
                        {
                            _RescueObject.SetActive(true);
                        }
                        if(_RequestMission != null)
                        {
                            _RequestMission.SetActive(false);
                        }

                    }


                }

            }



                }
        if(_spMission2 != null)
        {
            if (_spMission2.IsAcceptMission)
            {
                if (KillWolfNum == 4)
                {
                    if (!HasReceivedRewardBefore)
                    {
                        if(_ReceivedRewardTxt != null)
                        {
                            _ReceivedRewardTxt.SetActive(true);
                        }
                        HasReceivedRewardBefore = true;
                    }

                }

            }





        }





        }


    // This is Used For Making The Animation Of The Pauswe Btn
    IEnumerator GameOverIEnumerator()
    {

        yield return new WaitForSeconds(0.01f);

        if(!isPlayingAnimationPause)
        {
            _PauseAnim.SetBool("AnimClose", true);
            isPlayingAnimationPause = true;
        }


      

        yield return new WaitForSeconds(0.5f);
        _PauseBtn.SetActive(false);


        PauseAnimationGameOver = true;
    }
    // This is Used For Making The Animation Of The Pauswe Btn
    IEnumerator GameClearIEnumerator()
    {
        yield return new WaitForSeconds(0.01f);

        if (!isPlayingPauseAnimationGameClrPart)
        {
            isPlayingPauseAnimationGameClrPart = true;
             _PauseAnim.SetBool("AnimClose", true);
        }
        yield return new WaitForSeconds(0.5f);
        _PauseBtn.SetActive(false);
        PauseAnimationGameClear = true;
    }
    // This is the Function which is Going To Turn Off The Life Silder 
    IEnumerator LifeSilderHasToBeenTurnOffGO()
    {
        yield return new WaitForSeconds(0.01f);
        if(!HasntPlyBfSdGO)
        {
            _LifeSliderAnim.SetBool("Activable", true);
            HasntPlyBfSdGO = true;
        }
        yield return new WaitForSeconds(0.5f);
        _LifeSliderGameObject.SetActive(false);
        LifeSliderHasPresentedBfGO = true;
    }
    // This is the Function which is Going To Turn Off The Life Silder 
    IEnumerator LifeSilderHasToBeenTurnOffGC()
    {
        yield return new WaitForSeconds(0.01f);
        if (!HasntPlyBfSdGC)
        {
            _LifeSliderAnim.SetBool("Activable", true);
            HasntPlyBfSdGC = true;
        }
        yield return new WaitForSeconds(0.05f);
        _LifeSliderGameObject.SetActive(false);
        LifeSliderHasPresentedBfGC = true;
    }
    // This is The Function Which is used for When Player Kill 10 Enemy, To Turn On The Animation
    IEnumerator TenObjectIEumurator()
    {
        yield return new WaitForSeconds(0.01f);
        if(_EachTenObject != null)
        {
            _EachTenObject.SetActive(true);
        }
        else
        {

            Debug.LogError("That is something strange,You might need to Check it up");
        }
        yield return new WaitForSeconds(0.05f);
        if (!HasPlayAnimBeforeTen)
        {
            if(_EachTenAnim != null)
            {
                _EachTenAnim.SetBool("SetActive", true);
            }
            HasPlayAnimBeforeTen = true;
        }
        yield return new WaitForSeconds(10f);
        if(_EachTenObject != null)
        {
            _EachTenObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Please Added Up The Object");
        }
        HasPlayAnimBeforeTenObject = true;
        StopCoroutine("TenObjectIEumurator");
    }
    // This is The Function Which is used for When Player Kill 20 Enemy, To Turn On The Animation
    IEnumerator TwentyObjectIEumurator()
    {
        yield return new WaitForSeconds(0.01f);
        if (_EachTwentyObject != null)
        {
            _EachTwentyObject.SetActive(true);
        }
            else
        {
            Debug.LogWarning("That is something strange , You might need to Check up Some Happening");
        }
        yield return new WaitForSeconds(0.05f);
        if (!HasPlayAnimBeforeTwenty)
        {
            _EachTwentyAnimator.SetBool("SetActiveTrue", true);
            HasPlayAnimBeforeTwenty = true;
        }
        yield return new WaitForSeconds(10f);
        if (_EachTwentyObject != null)
        {
            _EachTwentyObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("That is something strange , You might need to Check up Some Happening");
        }
        HasPlayAnimaBeforeTwenObject = true;
        StopCoroutine("TwentyObjectIEumurator");
    }
    // This is The Function Which is used for When Player Kill 30 Enemy, To Turn On The Animation
    IEnumerator ThirtyObjectIEumurator()
    {
        yield return new WaitForSeconds(0.01f);
        if (_EachThirtyObject != null) _EachThirtyObject.SetActive(true);
        else Debug.LogError("That's Something Strange , You night need to check up Somehing");
        yield return new WaitForSeconds(0.05f);
        if (!HasPlayeAnimBeforeThirdty)
        {
            HasPlayeAnimBeforeThirdty = true;
            _EachThirdtyAnimator.SetBool("SetActive", true);
        }
        yield return new WaitForSeconds(10f);
        if (_EachThirtyObject != null) _EachThirtyObject.SetActive(false);
        else Debug.LogError("That's Something Strange , You night need to check up Somehing");
        HasPlayAnimBeforeThirdtyObject = true;
        StopCoroutine("ThirtyObjectIEumurator");
    }
    // This is The Function Which is used for When Player Kill 40 Enemy, To Turn On The Animation
    IEnumerator FourtyObjectIEumurator()
    {
        yield return new WaitForSeconds(0.01f);

        if (_EachFourtyObject != null) _EachFourtyObject.SetActive(true);
        else Debug.LogError("The Fourty isn't On The Object");
        yield return new WaitForSeconds(0.05f);
        if (!HasPlayAnimBeforeFourty)
        {
            _EachFourtyAnimator.SetBool("SetActive", true);
            HasPlayAnimBeforeFourty = true;
        }
        yield return new WaitForSeconds(10f);
        if (_EachFourtyObject != null) _EachFourtyObject.SetActive(false);
        else Debug.LogError("The Fourty isn't On The Object");
        HasPlayAnimBeforeFourtyObj = true;
        StopCoroutine("FourtyObjectIEumurator");
    }
    // This is The Function Which is used for When Player Kill 50 Enemy, To Turn On The Animation
    IEnumerator FiftyObjectIEumurator()
    {
        yield return new WaitForSeconds(0.01f);
        if (_EachFiftyObject != null) _EachFiftyObject.SetActive(true);
        else Debug.LogError("The fifteen Object isn't Been Added On The Object");
        yield return new WaitForSeconds(0.05f);
        if (!HasPlayAnimBeforeFifty)
        {
            _EachFiftyAnimator.SetBool("SetActive", true);
            HasPlayAnimBeforeFifty = true;
        }
        yield return new WaitForSeconds(10f);
        if (_EachFiftyObject != null) _EachFiftyObject.SetActive(false);
        else Debug.LogError("The fifteen Object isn't Been Added On The Object");
        HasPlayAnimBeforeFiftyObj = true;
        StopCoroutine("FiftyObjectIEumurator");
    }
    // This is the Function used for When Game Is Over/Clear To DisActive The 
    // Txt Which is counting The Number which is Presented On The Game 
    IEnumerator CountDownEnemyKillSetDisActive()
    {
        yield return new WaitForSeconds(0.01f);
        if (!HasntAnimatorBeforeCountEnemyKillDown)
        {
            HasntAnimatorBeforeCountEnemyKillDown = true;
            _KillEnemyCountDownAnimator.SetBool("SetActive",true);
            
        }

        yield return new WaitForSeconds(0.5f);

        if (_KillEnemyCountDownAnimStateInfo.IsName("KillAnimationCount")&& _KillEnemyCountDownAnimStateInfo.normalizedTime >= 0.99f)
        {
            _KillEnemyCountObj.SetActive(false);
        }
        StopCoroutine("CountDownEnemyKillSetDisActive");
            HasntStartBeforeCountEnemyKill = true;
    }
}
