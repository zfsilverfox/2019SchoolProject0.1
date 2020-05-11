using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BossSoldierScript : MonoBehaviour {

    private Transform _player;
    NavMeshAgent _navMesh;
    Animator _anim;
    AnimatorStateInfo _animStateInfo;
    CapsuleCollider _EnemyCapCol;
    Rigidbody _rgbd;


    public GameObject _GameManager3;
    public GameObject _HealthItem;
    public GameObject _PopUpDamage;
    public GameObject _HurtEffect;
    public GameObject _DeadEffect;

    GameManager3BOSSStage _gameManagerBossStageScript;




    [HideInInspector]
    public bool CanAttackArea;
    [HideInInspector]
    public bool DoAttackTimer;
    [HideInInspector]
    public bool CanAttack;
    [HideInInspector]
public    bool Died = false;
    [HideInInspector]
    public bool HasHurt = false;

    bool HasAlreadyGetHurt = false;
    bool HasPlayDeadAnimation = false;
    bool HaveDestroyObject = false;
    bool HasCreateMeatBefore = false;
    bool HasCreateHurtEffectbefore = false;





    private static string DetectPlayerColliderString = "DetectPlayerCollider";

    public enum BOSSEnemyStates
    {
        BOSS_EnemySoldier_Ilde,
        BOSS_EnemySoldier_Walking,
        BOSS_EnemySoldier_Attack,
        BOSS_EnemySoldier_Hurt,
        BOSS_EnemySoldier_Dead,
    }
    public BOSSEnemyStates _bossEnemyStates;
 public   float AtkTimer= 0.0f;
   public float AtkMax = 3.0f;


    public int Health = 3;

    // This is Avoid The The Enemy Who is Dead but the sword Trigger is still open Problem
    public GameObject swordTrigger;



    public float MeatPosY = 0.0f ;
    
    void Awake()
    {
        AvoidNullProblem();
    }
    void Start()
    {
        if(_player != null)
        {
            _navMesh.SetDestination(_player.position);
        }
        SettingSystem();
    }
    void AvoidNullProblem()
    {
        if (_navMesh == null) _navMesh = GetComponent<NavMeshAgent>();
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_EnemyCapCol == null) _EnemyCapCol = GetComponent<CapsuleCollider>();
        if (_GameManager3 == null) Debug.LogWarning("You must added the Object to this Script");
        if (_gameManagerBossStageScript == null) _gameManagerBossStageScript = _GameManager3.GetComponent<GameManager3BOSSStage>();
        if (_HealthItem == null) Debug.Log("You must added the Object to this Script ");
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
        if (_PopUpDamage == null) Debug.LogWarning("That's Something You need to added ");
        if (_HurtEffect == null) Debug.LogWarning("That's something You need to Added");
        if (_DeadEffect == null) Debug.LogWarning("That's Something You need to Added ");
    }
    void SettingSystem()
    {
        CanAttackArea = false;
        DoAttackTimer = false;
        CanAttack = false;
        Died = false;
        HasHurt = false;
        HasAlreadyGetHurt = false;
        HasPlayDeadAnimation = false;
        HaveDestroyObject = false;
        Health = Random.Range(2,4); 
        _bossEnemyStates = BOSSEnemyStates.BOSS_EnemySoldier_Ilde;
        HasCreateMeatBefore = false;
        AtkMax = Random.Range(3, 6);
        HasCreateHurtEffectbefore = false;
    }

    // Update Function
    //Update is mainly used for update the Attacking 
    // Hurt Or Dead Detection 
    //
     void Update()
    {

        _animStateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        if (!Died)
        {
         if (!HasHurt)
                {
                    AttackMaking();
                }
        }
        HurtOrDeadFunction();
    }

     void FixedUpdate()
    {
        if (!Died)
        {
            MovingFunctionMaking(); 
        }
    }

    // Moving Function is mainly used for setting Moving Function
    void MovingFunctionMaking()
    {
        if (_player != null)
        {
            _navMesh.SetDestination(_player.position);
        }
        if (_navMesh.remainingDistance >= 3.69f)
        {
            _anim.SetBool("Moving", true);
            _anim.ResetTrigger("DoAttk");
            _bossEnemyStates = BOSSEnemyStates.BOSS_EnemySoldier_Walking;
            CanAttackArea = false;
        }
        else if(_navMesh.remainingDistance< 4f)
        {
            _anim.SetBool("Moving",false);
            _bossEnemyStates = BOSSEnemyStates.BOSS_EnemySoldier_Ilde;
            CanAttackArea = true;
        }
    }
    void AttackMaking()
    {
        if (CanAttackArea)
        {
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
                if(_animStateInfo.IsName("Ilde"))
                {
                    AtkTimer += Time.deltaTime;
                    if(AtkTimer > AtkMax)
                    {
                        CanAttack = true;
                        AtkTimer = 0;
                    }
                }
            }
        }
        if (CanAttack)
        {
            CanAttack = false;
            _bossEnemyStates = BOSSEnemyStates.BOSS_EnemySoldier_Attack;
            _anim.SetTrigger("DoAttk");
        }
    }
    void HurtOrDeadFunction()
    {
        if(!Died && HasHurt)
        {
            if (!HasAlreadyGetHurt)
            {
                _anim.SetBool("DeadBool", false);
                _anim.SetTrigger("Hurt");
                HasAlreadyGetHurt = true;
                Health-= 1;

                if (!HasCreateHurtEffectbefore)
                {
                    HasCreateHurtEffectbefore = true;
                    if(_HurtEffect != null)
                    {
                        GameObject _InstanHurtEffect = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                        Destroy(_InstanHurtEffect, 2.2f);
                    }
                    StartCoroutine("CREATEHURTEFFECT");
                }
                if(Health > 0)
                {
                    if(_PopUpDamage != null)
                    {
                        GameObject _mObject = Instantiate(_PopUpDamage, transform.position, Quaternion.identity);
                        _mObject.GetComponent<PopUpDamage>().HIT = "HIT";
                        _mObject.GetComponent<PopUpDamage>()._color = Color.red;
                    }
                }
                StartCoroutine("HasAlreadyGetHurtCoroutine");
            }
            if (Health <= 0)
            {
                Died = true;
                if (_PopUpDamage != null)
                {
                    GameObject _mObject = Instantiate(_PopUpDamage, transform.position, Quaternion.identity);
                    _mObject.GetComponent<PopUpDamage>().HIT = "Dead";
                    _mObject.GetComponent<PopUpDamage>()._color = Color.red;
                }
            }
        }
        else if(Died && HasHurt)
        {
            if (!HasPlayDeadAnimation)
            {
                _anim.SetTrigger("DeadTrigger");
                _anim.applyRootMotion = true;
                HasPlayDeadAnimation = true;
                _EnemyCapCol.isTrigger = true;
                _rgbd.useGravity = false;       
                if(_DeadEffect != null)
                {
                    GameObject _InstanDeadEffect = Instantiate(_DeadEffect, transform.position, Quaternion.identity);
                    Destroy(_InstanDeadEffect, 2.6f);
                }
                StopCoroutine("HasHurtCoroutine");
                StopCoroutine("HasAlreadyGetHurtCoroutine");
            }

            int randFoodZone = Random.Range(1, 10);
            if(randFoodZone >= 1 && randFoodZone <=3)
            {
                Debug.Log("Create Meat ");
                if(!HasCreateMeatBefore )
                {
                    if (_HealthItem != null)
                    {
                    Vector3 meatnewPos = new Vector3(transform.position.x, transform.position.y + MeatPosY, transform.position.z);
                    GameObject _InstanMeat = Instantiate(_HealthItem, meatnewPos, Quaternion.identity);
                    }
                    HasCreateMeatBefore = true;
                }
            }
            else if(randFoodZone >= 4 && randFoodZone <= 10)
            {
                Debug.Log("hasn't  Apperead Something");
            }
        }
        if (Died)
        {
            swordTrigger.SetActive(false);
            if (!HaveDestroyObject)
            {
                Destroy(this.gameObject, 3f);
              
                



                HaveDestroyObject = true; 
            }
        }
    }
     void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerWeapon"))
        {

            if (!HasHurt)
            {
                HasHurt = true;
                Debug.Log("Hurt");
                StartCoroutine("HasHurtCoroutine");
            }
        }
    }
    IEnumerator HasHurtCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        HasHurt= false;
    }
    IEnumerator HasAlreadyGetHurtCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        HasAlreadyGetHurt = false;

    }
    IEnumerator CREATEHURTEFFECT()
    {
        yield return new WaitForSeconds(0.2f);
        HasCreateHurtEffectbefore = false;
    }
}
