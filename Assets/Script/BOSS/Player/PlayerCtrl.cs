using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {


    [HideInInspector]
    public PlayerLifeStatement _playerLifeState;

    Animator _anim;
    CapsuleCollider _capsuleCollider;
    Rigidbody _rgbd;
AnimatorStateInfo _animstateInfo;

    Vector3 movDir;
  public  float turnSmoothing = 3f;
    public float speed = 7f;
    float spd;
    float vertical;
    float horizontal;
    bool isattack = false;
    [HideInInspector]
    public bool Dead = false;
    [HideInInspector]
    public bool Hurt = false;
    [HideInInspector]
    public bool LockOn = false;
    [HideInInspector]
    public bool CanMov = true;
    public KeyCode SpaceKey;
  


    public GameObject _SwordActiveProblem;


    public GameObject _PopUpDamage;


    public GameObject _TrailSwordEffect;
    public GameObject _PlayerDeadEffect;



    public    enum PLAYERSTATE
    {
        PlayerState_Ilde,
        PlayerState_MovingWalk,
        PlayerState_MovingRun,
        PlayerState_Attack1,
        PlayerState_Attack2,
        PlayerState_Attack3,
        PlayerState_Attack4,
        PlayerState_Attack5,
        PlayerState_Attack6,
    }
public    PLAYERSTATE playerstatement;
    public GameObject _FrontHit;
    public GameObject _BackHit;
    public GameObject _LeftHit;
    public GameObject _RightHit;
    bool HasntPlayBefore= false;
    private static string EnemyWeaponString = "EnemyWeapon";
    private static string StunnedAnimation = "Stunned";
    [HideInInspector]
    public bool isStunnedMode = false;
    bool HasStunnedBefore = false;
    public GameObject _swordPosition;
    public AudioClip _swordHitSound;
    // Btn Hit Count ,is Use For Counting The Btn which is Attack Btn How much time Player is pressed
    [HideInInspector]
    public int BtnHitCount;
    public float Offset;
    // Mainly used For Get The Component Problem
    void Awake()
    {
        GetComponentFunction();
    }
    // Mainly used For Setting The Basic Info
    void Start()
    {
        GetComponentFunction();
        BasicSetting();
    }
    // Basic Setting Of The Infomartion
   void BasicSetting()
    {
        playerstatement = PLAYERSTATE.PlayerState_Ilde;
        BtnHitCount = 0;
        HasntPlayBefore = false;
        isStunnedMode = false;
        CanMov = true;
    }

    // GetComponentFunction
    //　Mainly use for Get Component 
    void GetComponentFunction()
    {
        if (_anim == null)
        {
            _anim = GetComponentInChildren<Animator>();
        }

        if (_capsuleCollider == null)
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        if (_rgbd == null)
        {
            _rgbd = GetComponent<Rigidbody>();
        }
        if(_playerLifeState  == null)
        {

            _playerLifeState = GetComponent<PlayerLifeStatement>();

        }
        if (_PlayerDeadEffect == null) Debug.Log("Please Add The Effect to the Object");
    }


    


    //Name : Update 
    //Method : This function is mainly is used
    // for Attack,Jumping, Get Hurt , Dead Function
    void Update()
    {
        AnimatorStateInfo _animstateInfo = _anim.GetCurrentAnimatorStateInfo(0);
            if (CanMov)
        {
    if (!_animstateInfo.IsName("Ilde") && _animstateInfo.normalizedTime >= 0.9f)
                {
                    _anim.SetInteger("Attack", 0);
                isattack = false;
                BtnHitCount = 0;
                    playerstatement = PLAYERSTATE.PlayerState_Ilde;
                  
                if (_TrailSwordEffect != null)
                {
                    _TrailSwordEffect.SetActive(false);
                }



            }
                if (!Dead)
                {
                    StunnedModeOfThePlayer(); 
                    if (!isStunnedMode)
                    {
                        if (Input.GetKeyDown(SpaceKey))
                        {
                            isattack = true;
                            if (_animstateInfo.IsName("Ilde")&&BtnHitCount ==0)
                            {
                            AudioSource.PlayClipAtPoint(_swordHitSound, _swordPosition.transform.position);
                                _anim.SetInteger("Attack", 1);
                                BtnHitCount = 1;
                                playerstatement = PLAYERSTATE.PlayerState_Attack1;
                            if (_TrailSwordEffect != null)
                            {
                                _TrailSwordEffect.SetActive(true);
                            }
                            else Debug.Log("That's something you need to add");
                        }
                            else if (_animstateInfo.IsName("AtkOne") && _animstateInfo.normalizedTime >= 0.8f)
                            {
                            AudioSource.PlayClipAtPoint(_swordHitSound, _swordPosition.transform.position);
                            _anim.SetInteger("Attack", 2);
                                BtnHitCount = 2;
                                playerstatement = PLAYERSTATE.PlayerState_Attack2;
                                if (_TrailSwordEffect != null)
                                {
                                    _TrailSwordEffect.SetActive(true);
                                }
                            else Debug.Log("That's something you need to add");
                        }
                            else if (_animstateInfo.IsName("AtkTwo") && (_animstateInfo.normalizedTime >= 0.8f))
                            {
                            AudioSource.PlayClipAtPoint(_swordHitSound, _swordPosition.transform.position);
                            _anim.SetInteger("Attack", 3);
                                BtnHitCount = 3;
                                playerstatement = PLAYERSTATE.PlayerState_Attack3;
                            if (_TrailSwordEffect != null)
                            {
                                _TrailSwordEffect.SetActive(true);
                            }
                            else Debug.Log("That's something you need to add");

                        }
                        }
                    }
                    DeadFunction();
                }
        }
        LimitTheHealth();
    }
    // Name :  FixedUpdate
    //　Method : This Function Mainly is used for Movement 
void FixedUpdate()
{ 
   if (CanMov)
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
    if (!Dead)
        {
            if (!isStunnedMode)
            {
                if (!isattack)
                {
                    if ((vertical != 0 || horizontal != 0))
                    {
                    Movement(horizontal, vertical);
         
                    }
                        else if((vertical == 0 || horizontal == 0))
                    {
                    _anim.SetBool("isMoving", false);
                    playerstatement = PLAYERSTATE.PlayerState_Ilde;
                    }
                }
            }   
        }     
    }
}
    //Name  :Movment
    // This Function mainly is use for Get Movement Function
    void Movement(float h, float v)
    {
                if ((h != 0 || v != 0))
                {
                    movDir = new Vector3(h, 0, v);
                    Rotating(h, v);
                    _rgbd.MovePosition(_rgbd.position + speed * movDir * Time.fixedDeltaTime);
                    _anim.SetBool("isMoving", true);
                    playerstatement = PLAYERSTATE.PlayerState_MovingWalk;
                }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            spd += Time.deltaTime;
        }
        else
        {
            spd -= Time.deltaTime;
        }
        spd = Mathf.Clamp(spd, 0f, 1f);
      _anim.SetFloat("Speed", spd);
                if(spd == 1)
                {
                    playerstatement = PLAYERSTATE.PlayerState_MovingRun;
                }
                if (playerstatement == PLAYERSTATE.PlayerState_MovingRun)
                {
                    speed = 9f;
                }
                else if (playerstatement == PLAYERSTATE.PlayerState_MovingWalk)
                {
                    speed = 7f;
                }
    }
    //Function : Rotating 
    // Method : is Use For when Player Moving , The Object which is Player's Object will change Their Rotation
    void Rotating(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(_rgbd.rotation, targetRotation, turnSmoothing * Time.deltaTime);
       _rgbd.MoveRotation(newRotation);
    }
    // Dead Function 
    //　Method : this function mainly use for 
    // Cehck Player Dead Function
    void DeadFunction()
    {
        if (!Dead)
        {
            if(_playerLifeState.Health<=0)
            {
                if (!HasntPlayBefore)
                {
                _anim.SetTrigger("DeadTrigger");
                _RightHit.SetActive(false);
                _LeftHit.SetActive(false);
                _BackHit.SetActive(false);
                _FrontHit.SetActive(false);
                    HasntPlayBefore = true;
                    Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
                    //    Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
                    _swordPosition.SetActive(false);
                    if (_PlayerDeadEffect != null)
                    {
                        GameObject _InstanObject = Instantiate(_PlayerDeadEffect, newPosition, Quaternion.identity);
                        Destroy(_InstanObject, 4f);
                    }
                    else Debug.Log("Please Don't Forget to add something XD");
                    Dead = true;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthItem"))
        {
          
            Destroy(other.gameObject);
            _playerLifeState.Health++;
          //  Debug.Log("EatingHealthItem");
        }


        if (other.gameObject.CompareTag("Enemy"))
        {
            BtnHitCount = 0;
        }


    }

    // This Function mainly is set the limit for the Player Health  
    void LimitTheHealth()
    {
        if (_playerLifeState.Health >= 20)
        {
            _playerLifeState.Health = 20;
        }
    }
    // This Function is use for Do The StunnedMode 
  void StunnedModeOfThePlayer()
    {
        if (isStunnedMode)
        {
            if (_SwordActiveProblem != null)
                _SwordActiveProblem.SetActive(false);
            else Debug.Log("That's something You need to added ");
            if (!HasStunnedBefore)
            {
                HasStunnedBefore = true;
                _anim.SetTrigger("Stunned");
            }
       StartCoroutine("StunnedModeBackToNormal");
        }
    }


    // This is the Function which is Used For Turn Back To Normal Status 
    IEnumerator StunnedModeBackToNormal()
    {
        yield return new WaitForSeconds(1.2f);
        isStunnedMode = false;
        HasStunnedBefore = false;
    }
}
