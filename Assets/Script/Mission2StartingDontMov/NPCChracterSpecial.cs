using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

public class NPCChracterSpecial : MonoBehaviour {


    [HideInInspector]
 public   Animator _anim;


    public SphereCollider _SphCollider;
    // NPCRescueTrigger

    public  SphereCollider    _SphereColliderResucue;



    [HideInInspector]
    public  bool isTalking = false;
    bool HaveHealthBefore = false;
    bool HaveTalkingBefore = false;
    bool HavingThankBefore = false;


    public GameObject _RewardTxt;
    bool RewardTxtHAsAppreadBf = false;

    void Awake()
    {
        AvokeNullProblem();
    }


    void Start()
    {
        AvokeNullProblem();
        isTalking = false;

        HaveTalkingBefore = false;
    }

    void AvokeNullProblem()
    {
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_SphCollider == null) Debug.Log("That's something You need to Add");
    }


    void Update()
    {
        TalkingAnimationPresented();
    }



    void TalkingAnimationPresented()
    {

        if (_anim != null) _anim.SetBool("isTalking", isTalking);
        else Debug.Log("That 's SOMETHING STRANGE");

    }




    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && _SphCollider.gameObject.CompareTag("NPCTrigger")  && _SphCollider.gameObject.activeInHierarchy)
        {
            transform.LookAt(other.gameObject.transform.position,Vector3.up);
            if(!HaveTalkingBefore)
            {
                MessageReceived[] received = FindObjectsOfType<MessageReceived>();
                if(received != null)
                {
                    foreach(var receivers in received)
                    {

                        receivers.OnSendFungusMessage("SpecialMission");
                    }


                }
                HaveTalkingBefore = true;
            }
        }
   
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _SphCollider.gameObject.CompareTag("NPCTrigger") && this.gameObject.activeInHierarchy)
        {
            HaveTalkingBefore = false;
        }
    }











}
