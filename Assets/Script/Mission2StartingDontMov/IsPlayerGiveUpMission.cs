using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerGiveUpMission : MonoBehaviour {


    public SpecialMission2 _spm2;
    public GameManager2 _gm2;

    public GameObject _Player;
    PlayerCtrl _playerCtrl;
    Animator _PlayerAnim;
    PlayerLifeStatement _PlayerLifeStatemen;
    public GameObject _GiveUPanel;






    bool HasAppreadBeforePanel = false;
    bool HasAppreadBeforePlyCtrl = false;
    bool LfHasMinusBf = false;

    public GameObject _BoxSetActiveAsTrue;

    public GameObject _WolfSetActive;


    public GameObject _WolfAppreadTxtSetActive;

    public GameObject _Npc;





    void Awake()
    {
        AvoidNullProb();
    }

    void Start () {
        AvoidNullProb();
         HasAppreadBeforePanel = false;
      HasAppreadBeforePlyCtrl = false;
        LfHasMinusBf = false;
    }

    void AvoidNullProb()
    {
        if (_spm2 == null) Debug.Log("That's something You need to added ");
        if (_gm2 == null) Debug.Log("That's something you need to added");
        if (_Player == null) Debug.Log("That's something you need to added ");
        if (_GiveUPanel == null) Debug.Log("That 's something you need to added ");
        if (_BoxSetActiveAsTrue == null) Debug.Log("That 's something You need to added ");
        if (_playerCtrl == null) _playerCtrl = _Player.GetComponent<PlayerCtrl>();
        if (_PlayerAnim == null) _PlayerAnim = _Player.GetComponentInChildren<Animator>();
        if (_PlayerLifeStatemen == null) _PlayerLifeStatemen = _Player.GetComponent<PlayerLifeStatement>();
        if (_Npc == null) Debug.Log("That's Something You need to added");
    }
     void OnTriggerEnter(Collider other)
    {
        if (_spm2.IsAcceptMission)
        {

            if(_gm2.KillWolfNum < 4)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (!HasAppreadBeforePanel)
                    {
                        HasAppreadBeforePanel = true;
                        _GiveUPanel.SetActive(true);
                    }

                    if (!HasAppreadBeforePlyCtrl)
                    {
                        HasAppreadBeforePlyCtrl = true;
                        _playerCtrl.enabled = false;
                        _PlayerAnim.SetBool("isMoving", false);
                    }



                   // Debug.Log("Hello World");

                }
            }
        }
    }
    public void GiveUpMission()
    {
        if (_playerCtrl!= null) _playerCtrl.enabled = true;
        if (_WolfSetActive != null) _WolfSetActive.SetActive(false);
        _GiveUPanel.SetActive(false);
        this.gameObject.SetActive(false);
        _spm2.IsAcceptMission = false;
        if (_WolfAppreadTxtSetActive != null) _WolfAppreadTxtSetActive.SetActive(false);
        if (_Npc != null) _Npc.SetActive(false);

        if (!LfHasMinusBf )
        {
            if (_PlayerLifeStatemen != null) _PlayerLifeStatemen.Health -= Random.Range(4, 6);
            LfHasMinusBf = true;

        }
    }
    public void DontGiveUpMission()
    {
        if (_playerCtrl != null) _playerCtrl.enabled = true;
        _GiveUPanel.SetActive(false);
      if (_BoxSetActiveAsTrue != null) _BoxSetActiveAsTrue.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
