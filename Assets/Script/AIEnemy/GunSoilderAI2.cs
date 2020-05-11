using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This Is Basically is A GunSoilder EnemY
public class GunSoilderAI2 : MonoBehaviour {



    [HideInInspector]
    public bool IsDead;

    [HideInInspector]
    public bool CanAttackArea;
    [HideInInspector]
    public bool DoAttackTimer;
    [HideInInspector]
    public bool CanAttack;

    bool HasPlayDeadAnimateBefore;


    public SphereCollider _DetectPlayerTrigger;

    CapsuleCollider _capsulecollider;
    Rigidbody _rgbd;


    private static string PlayerDetectString = "PlayerDetect";


    public Transform _player;


    Animator _anim;

    [HideInInspector]
    public float AtkTimer = 0.0f;
[HideInInspector]    public float AtkMaxTimer = 3.0f;



    public GameObject _HealthItem;
    bool HasAppeareadBeforeFood = false;

    public GameObject _PopUpDamage;
    bool _PopUpDamageHasAppreadbf;



   void Awake()
    {
        AvoidNullProb();
    }
    void Start()
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
        if (_HealthItem == null) Debug.Log("That's something You need to add");
    }
    void SettingBasicData()
    {
        CanAttackArea = false;
        CanAttack = false;
        DoAttackTimer = false;
        IsDead = false;
        HasPlayDeadAnimateBefore = false;
        AtkMaxTimer = Random.Range(3.6f ,7.2f);
        HasAppeareadBeforeFood = false;
        _PopUpDamageHasAppreadbf = false;

    }
    void Update()
    {
        DoAttackFunction();
        DeadFunction();
    }
    void DoAttackFunction()
    {
        if (!IsDead)
        {
            if (CanAttackArea)
            {
                //transform.LookAt(_player.position);
                DoAttackTimer = true;
            }
            if (!CanAttackArea)
            {
                AtkTimer = 0.0f;
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

                    if (AtkTimer >= AtkMaxTimer)
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
                int Rand = Random.Range(1, 10);
                if(Rand >=1 && Rand <=2)
                {
                    if (!HasAppeareadBeforeFood)
                    {
                        HasAppeareadBeforeFood = true;
                        if(_HealthItem != null)
                        {
                            Debug.Log("Food Appearead ");
                            GameObject _InstanFood = Instantiate(_HealthItem, transform.position, Quaternion.identity);
                        }
                    }
                }
                else if(Rand >= 3&& Rand <= 10)
                {
                    Debug.Log("Food Don't Appeared");
                }
                if (!_PopUpDamageHasAppreadbf)
                {
                    _PopUpDamageHasAppreadbf = true;
                    if(_PopUpDamage != null)
                    {
                        GameObject _InstanPopUpDamage = Instantiate(_PopUpDamage, transform.position, Quaternion.identity);
                        _InstanPopUpDamage.GetComponent<PopUpDamage>().HIT = "DEAD";
                        _InstanPopUpDamage.GetComponent<PopUpDamage>()._color = Color.red;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _DetectPlayerTrigger.CompareTag("PlayerDetect"))
        {
            CanAttackArea = true;
        }

        if (other.gameObject.tag == "PlayerWeapon")
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
