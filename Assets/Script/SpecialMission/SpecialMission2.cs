using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is in the Second Stage which is  Used For Extra Mission
public class SpecialMission2 : MonoBehaviour {

    public NPCChracterSpecial _NpcChracterSpecial;

    public GameObject _PauseBtn;
    public GameObject _SubCameraObject;
    public GameObject[] RedLinesObject;
    public GameObject _LifeSliderObject;
    public GameObject _KillEnemyCount;
    public GameObject _Player;
    public GameObject _Wolves;
    public GameObject _NpcChracter;

    public GameObject _MissionPanel;

    public GameObject _NPC3DTxtObj;

    public GameObject _WolfAppreadTxt;


    public GameObject _NpcTaskMission;




    PlayerCtrl _playerCtrl;

  [SerializeField]  Animator _PlayerAnim;


    public bool IsAcceptMission=false  ;




    // This is Mainly use for Checking The Extra Mission
 void Awake()
    {
        AvoidNullProblem();


    }


    void Start()
    {
        AvoidNullProblem();
        IsAcceptMission = false;
    }



    void AvoidNullProblem()
    {
        if (_NpcChracterSpecial == null) Debug.Log("That's Something You need to add");
        if (_playerCtrl == null) _playerCtrl = _Player.GetComponent<PlayerCtrl>();
        if (_PlayerAnim == null) _PlayerAnim = _Player.GetComponentInChildren<Animator>();
    }


    // 
    void SetTalkingActiveToTrue()
    {
        if (_PauseBtn != null) _PauseBtn.SetActive(false);
        else Debug.Log("Pls Added Pause Btn to this object");
        if (_SubCameraObject != null) _SubCameraObject.SetActive(false);
        else Debug.Log("Pls Added Sub Camera Object to this object");
        if (RedLinesObject != null)
        {
            foreach (GameObject rl in RedLinesObject)
            {
                rl.SetActive(false);
            }
        }
        else Debug.Log("That's something You need to add");
        if (_LifeSliderObject != null) _LifeSliderObject.SetActive(false);
        else Debug.Log("Life Slider Object is null,Pls Add to it");
        if (_KillEnemyCount != null) _KillEnemyCount.SetActive(false);
        else Debug.Log("Please Added the kill Enemy Count Object");
        //if (_Player != null) _Player.SetActive(false);
        //  else Debug.Log("Please Add the Player Object");

        if (_playerCtrl != null) _playerCtrl.enabled = false;
        else Debug.Log("That 's Something you need to add");

        if (_PlayerAnim != null) _PlayerAnim.SetBool("isMoving", false);
        else Debug.Log("That's something you need tp add");
        if (_NpcChracterSpecial != null) _NpcChracterSpecial.isTalking = true;
    }
    void SetTalkingActiveToFalse()
    {
        if (_PauseBtn != null) _PauseBtn.SetActive(true);
        else Debug.Log("Pls Added Pause Btn to this object");
        if (_SubCameraObject != null) _SubCameraObject.SetActive(true);
        else Debug.Log("Pls Added Sub Camera Object to this object");
        if (RedLinesObject != null)
        {
            foreach (GameObject rl in RedLinesObject)
            {
                rl.SetActive(true);
            }
        }
        else Debug.Log("That's something You need to add");
        if (_LifeSliderObject != null) _LifeSliderObject.SetActive(true);
        else Debug.Log("Life Slider Object is null,Pls Add to it");
        if (_KillEnemyCount != null) _KillEnemyCount.SetActive(true);
        else Debug.Log("Please Added the kill Enemy Count Object");
        if (_playerCtrl != null) _playerCtrl.enabled = true;
        else Debug.Log("That 's Something you need to add");

        if (_NpcChracterSpecial != null) _NpcChracterSpecial.isTalking =false;
    }

    void MissionPanelSetActive()
    {

        if (_MissionPanel != null) _MissionPanel.SetActive(true);
    }


    void MissionPanelSetActiveAsFalse()
    {
        if (_MissionPanel != null) _MissionPanel.SetActive(false);
    }



    void SetWolfActiveToTrue()
    {

        IsAcceptMission = true;
        if (_Wolves != null)
        {
            _Wolves.SetActive(true);
        }
        else Debug.Log("That's something you need to add");

        //if (_NpcChracter != null)
        //{
        //    _NpcChracter.transform.GetChild(1).GetComponent<SphereCollider>().enabled = false;
        //}
        //else Debug.Log("That's something you need to add ");


        if(_NpcTaskMission != null)
        {
            _NpcTaskMission.SetActive(false);
        }



    }


    void SetNPCActiveAsfalse()
    {
        if (_NpcChracter != null)
        {
            _NpcChracter.SetActive(false);
        }
        else Debug.Log("That's something you need to add ");



    }


    void TurnOffOrTurnOnSomeTxt()
    {
        if (_NPC3DTxtObj != null) _NPC3DTxtObj.SetActive(false);

        if (_WolfAppreadTxt != null) _WolfAppreadTxt.SetActive(true);
    }




}
