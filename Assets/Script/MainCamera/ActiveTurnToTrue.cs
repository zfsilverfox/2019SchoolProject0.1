using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTurnToTrue : MonoBehaviour {


    public GameObject _SubCameraObject;
    public GameObject[] RedLineThings;

    public GameObject _PauseBtnOBj;
    public GameObject _LifeSliderObj;

    public PlayerCtrl _plyCrtrl;
    public Animator _PlayerAnim;


    public GameObject _MouseClickToGoPanel;


    public GameObject _FirstAppereadTxt;


    public NPCChracterSpecial nPCChracterSpecial;






    //Turn Active as True ,
    // This is the Object which is turn All Things Which is _SubCamera And RedLine
    void TurnActiveAsTrue()
    {
        //Debug.Log("Has Been Called ");

        if (_SubCameraObject != null) _SubCameraObject.SetActive(true);

        if(RedLineThings != null)
        {
            foreach(GameObject rl in RedLineThings)
            {
                rl.SetActive(true);
            }
        }
    }
    // 
    void TurnActiveASFalseMissionArea()
    {
        if (_SubCameraObject != null) _SubCameraObject.SetActive(false);
        if (RedLineThings != null)
        {
            foreach (GameObject rl in RedLineThings)
            {
                rl.SetActive(false);
            }
        }
        if (_PauseBtnOBj != null) _PauseBtnOBj.SetActive(false);
        if (_LifeSliderObj != null) _LifeSliderObj.SetActive(false);
        if (_PlayerAnim != null) _PlayerAnim.SetBool("isMoving", false);
    }
    void TurnActiveASTrueMissionAreaClear()
    {
        if (_SubCameraObject != null) _SubCameraObject.SetActive(true);

        if (RedLineThings != null)
        {
            foreach (GameObject rl in RedLineThings)
            {
                rl.SetActive(true);
            }
        }


        if (_PauseBtnOBj != null) _PauseBtnOBj.SetActive(true);

        if (_LifeSliderObj != null) _LifeSliderObj.SetActive(true);
    }
    void DisActivePlayerMovment()
    {
        if (_plyCrtrl != null) _plyCrtrl.enabled = false;



    }
    void ActivePlayerMovment()
    {
        if (_plyCrtrl != null) _plyCrtrl.enabled = true;
    }
    void ClickMouseAppread()
    {
        if (_MouseClickToGoPanel != null)
            _MouseClickToGoPanel.SetActive(true);
    }
    void ClickMouseDisappeared()
    {
        if (_MouseClickToGoPanel != null)
            _MouseClickToGoPanel.SetActive(false);
    }
    void SetActiveOfTheFirstMission()
    {
        if (_FirstAppereadTxt != null) { _FirstAppereadTxt.SetActive(true); }
        else Debug.LogWarning("That's something You need To added");
    }
    void SetNpcTalkingToTrue()
    {
        if (nPCChracterSpecial != null)
        {
            nPCChracterSpecial.isTalking = true;
        }
        else Debug.Log("That's Something You need To Added ");




    }
    void SetNpcTalkingToFalse()
    {
        if (nPCChracterSpecial != null)
        {
            nPCChracterSpecial.isTalking =false;
        }
        else
            Debug.Log("That's Something You need To Added ");
    }
}
