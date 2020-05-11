using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2ClosingPlayerMoVment : MonoBehaviour {

    public GameObject _Player;

    PlayerCtrl _PlayerCtrl;



    private void Awake()
    {
        AvoidNullProb();
    }


    void AvoidNullProb()
    {
        if (_Player == null) Debug.LogWarning("That's something You need to do");
        if (_PlayerCtrl == null) _PlayerCtrl = _Player.GetComponent<PlayerCtrl>();
    }



    void ClosePlayerMovment()
    {
        if (_PlayerCtrl != null) _PlayerCtrl.enabled = false;
    }



    void OpenPlayerMovment()
    {
        if (_PlayerCtrl != null) _PlayerCtrl.enabled = true;
    }



}
