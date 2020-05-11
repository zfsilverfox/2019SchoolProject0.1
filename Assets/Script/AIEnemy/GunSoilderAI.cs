using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoilderAI : MonoBehaviour
{

    public bool IsDead;
  

    public bool CanAttackArea;
    public bool DoAttackTimer;
    public bool CanAttack;

    bool HasPlayDeadAnimateBefore;


    public SphereCollider _DetectPlayerTrigger;

    CapsuleCollider _capsulecollider;
    Rigidbody _rgbd;


    private static string PlayerDetectString = "PlayerDetect";


    public Transform _player;


    Animator _anim;


    public float AtkTimer = 0.0f;
    public float AtkMaxTimer = 3.0f;


    private void Awake()
    {
        AvoidNullProb();
    }


    private void Start()
    {
        AvoidNullProb();
        SettingBasicData();
    }

    void AvoidNullProb()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_capsulecollider == null) _capsulecollider = GetComponent<CapsuleCollider>();
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();


    }


    void SettingBasicData()
    {
        CanAttackArea = false;
        CanAttack = false;
        DoAttackTimer = false;
        IsDead = false;
        HasPlayDeadAnimateBefore = false;
    }
    void Update()
    {
        DoAttackFunction();
        DeadFunction();
        Debug.Log("The Can Attack of the Area" + CanAttackArea);

    }
    void DoAttackFunction()
    {

        if (!IsDead)
        {
            if (CanAttackArea)
            {
                transform.LookAt(_player.position);
                DoAttackTimer = true;
            }
            else if (!CanAttackArea)
            {
                DoAttackTimer = false;
            }


            if (DoAttackTimer)
            {
                if (!CanAttack)
                {
                    AtkTimer += Time.deltaTime;

                    if(AtkTimer >= AtkMaxTimer)
                    {
                        CanAttack = true;
                        AtkTimer = 0.0f;
                    }
                }
            }
            if (CanAttack)
            {
                CanAttack = false;
                _anim.SetTrigger("AtkTime");
            }



        }
        

    }
    void DeadFunction()
    {
        if (IsDead)
        {
            if (!HasPlayDeadAnimateBefore)
            {
                _anim.SetTrigger("Dead");
                _anim.applyRootMotion = true;
                _capsulecollider.isTrigger = true;
                _rgbd.useGravity = false;
                Destroy(this.gameObject, 3f);
                HasPlayDeadAnimateBefore = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&& _DetectPlayerTrigger.CompareTag("PlayerDetect"))
        {
            CanAttackArea = true;
        }

        if (other.gameObject.tag== "PlayerWeapon")
        {
         
            IsDead = true;
        }


    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _DetectPlayerTrigger.CompareTag("PlayerDetect"))
        {
            CanAttackArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _DetectPlayerTrigger.CompareTag("PlayerDetect"))
        {
            CanAttackArea = false;
        }
    }



}
