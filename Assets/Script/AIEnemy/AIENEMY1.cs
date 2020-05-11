using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIENEMY1 : MonoBehaviour
{
    Rigidbody _Rgbd;
    Animator _anim;
    AnimatorStateInfo _animStateInfo;



    CapsuleCollider _capCollider;
    NavMeshAgent _navMeshAgent;



    public List<Transform> wayPoint;
    public Transform CrtPos;
    public bool Reverse = true;
    public bool ReachDistance = false;
    public int DesirePos;

    [HideInInspector]
    public bool GetHurt = false;
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool CanAttack = false;
    [HideInInspector]
    public bool DoAttack = false;
    public bool CanAttackArea = false;

    public bool foundPlayer = false;

    bool DeadAnimateHasbeenPlayed = false;

    bool isAlreadyGetHurt = false;

    
    public int Life = 5;
    public float AttackTimer = 0.0f;
    public float stopAttackTimer = 15.0f;
    public int rand;

    public GameObject _Player;

    public GameObject _HealthItem;
    public GameObject _ScannerArea;
    public GameObject _BloodParticulue;
    public GameObject _SwordSlashSkillEffect;
    public GameObject _HitTrigger;
    public GameObject _SwordTrigger;

    public GameObject _HurtEffect;

    public float hurtEffectPosY;



    public GameManager2 _GameManager2;


    public float _HiTriggerPosY;
    public float _HitTriggerPosZ;
    public float _HitTriggerPosX;



    // This Function is mainly used for Getting The Component 
     void Awake()
    {
        GetComponentFunction();
    }
    // Start Function is used for Getting the Component which is not Getting From Awake Function
    // And Setting The basic Information in the Start
   void Start()
    {
        GetComponentFunction();
        SettingBasicInfomartion();
    }

    // Get Component Function is used for Getting the Component
    void GetComponentFunction()
    {
        if(_Rgbd == null)
        {
            _Rgbd = GetComponent<Rigidbody>();
        }
        if (_anim == null)
        {
            _anim = GetComponentInChildren<Animator>();
        }
        if(_capCollider == null)
        {
            _capCollider = GetComponent<CapsuleCollider>();
        }
        if (_navMeshAgent == null)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        if (_BloodParticulue == null) Debug.LogWarning("The Blood Particue hasn't been added up");
        if (_SwordSlashSkillEffect == null) Debug.LogError("Please Add The Sword Skill Effect");
        if (_HitTrigger == null) Debug.Log("That's something you need to add");
        if (_SwordTrigger == null) Debug.LogError("That's something you need to added");
        if (_HurtEffect == null) Debug.LogError("That's something You need to added");
    }


    // This function is used for Setting The Basic Information
    void SettingBasicInfomartion()
    {
        GetHurt = false;
        DeadAnimateHasbeenPlayed = false;
        DesirePos = Random.Range(0, wayPoint.Count - 1);
     
        stopAttackTimer = Random.Range(5, 15);

        if(stopAttackTimer >= 5f && stopAttackTimer <=10)
        {
            Life = Random.Range(5,6);
        }
        else if(stopAttackTimer >= 11 && stopAttackTimer <= 15)
        {
            int randLife = Random.Range(0, 100);
            if(randLife >= 0 && randLife <=89)
            {
                Life = Random.Range(8,9);
            }
            else if(randLife >= 90 && randLife <= 100)
            {
                Life = Random.Range(10, 11);
            }
        }
        foundPlayer = false;
        _anim.applyRootMotion = false;
    }
    // Checking Get Hurt Function 
    // Or Player Enter the Area Function
 void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "PlayerWeapon"&& this.gameObject.tag =="Enemy")
        {   
            if (!GetHurt)
            {
                GetHurt = true;
                _anim.ResetTrigger("OnAttack1");


              StartCoroutine(GetHurtReturnToFalse());
            }
        }
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = true;
            foundPlayer = true;
        }
    }
    // Checking The Player is still Enter the Area of the Enemy
  void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = true;
            foundPlayer = true;
        }
    }
    // Checking The Player is already exit the Function
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = false;
            _navMeshAgent.speed = 0.0f;
            _anim.SetBool("Moving", false);
            foundPlayer = false;
        }
    }
     // Get Hurt Turn Back To False
    IEnumerator GetHurtReturnToFalse()
    {
        yield return new WaitForSeconds(0.03f);
        GetHurt = false;
    }
    // Return Already Get Hurt Function
    IEnumerator ReturnAlreadyGetHurt()
    {
        yield return new WaitForSeconds(0.65f);
        isAlreadyGetHurt = false;
    }
    // Attack Function
    // This Function is mainly used for Attack Function
    void AttackFunction()
    {
        if (!isDead)
        {
            if (CanAttackArea)
            {
                if (!CanAttack)
                {
                    AttackTimer += Time.deltaTime;
                    if (AttackTimer >= stopAttackTimer)
                    {
                        CanAttack = true;
                        AttackTimer = 0.0f;
                    }
                }
                if (CanAttack)
                {

                    if(_Player != null)
                    {
                        transform.LookAt(_Player.transform.position);
                    }
                    else
                    {
                        CanAttackArea = false;
                    }
                        _anim.SetTrigger("OnAttack1");
                        CanAttack = false;
                }
            }
        }
    }

    // Get Hurt Function
    // This function is mainly used for checking The Get Hurt Function
    void GetHurtFunction()
    {
        if(GetHurt && !isDead )
        {
            if (!isAlreadyGetHurt)
            {
                 _anim.SetBool("Dead", false);
                _anim.SetTrigger("GetHurt");
                isAlreadyGetHurt = true;
                StartCoroutine("ReturnAlreadyGetHurt");
                if(_SwordTrigger != null)
                _SwordTrigger.SetActive(false);

                if (_HurtEffect != null)
                {
                    Vector3 _TransPos = new Vector3(transform.position.x , transform.position.y + hurtEffectPosY, transform.position.z );
                    GameObject _InstanHurtObjectEffect =  Instantiate(_HurtEffect, _TransPos, Quaternion.identity);

                    float deadDestroyTime = Random.Range(2, 3.5f);
                    Destroy(_InstanHurtObjectEffect, deadDestroyTime);
                }
                Life --;
                if(Life > 0)
                {
                    if(_HitTrigger != null)
                    {
                        _HitTriggerPosX = Random.Range(-1.2f,1.2f);
                        _HiTriggerPosY = Random.Range(0.2f,0.4f);
                        _HitTriggerPosZ = Random.Range(-0.5f,0.5f);
                    Vector3 newTrans = new Vector3(transform.position.x + _HitTriggerPosX, transform.position.y + _HiTriggerPosY, transform.position.z + _HitTriggerPosZ);
                    GameObject _mObject = Instantiate(_HitTrigger,newTrans,Quaternion.identity) as GameObject;
                    _mObject.GetComponent<PopUpDamage>().HIT = "HIT";
                    }
                }
            }
            if(Life <= 0)
            {
                isDead = true;
                if (_GameManager2 != null) _GameManager2.ScoreToUnlockTheBoxCollider += 1;
                else Debug.Log("That's something you need to added or this is not a second stage");
               rand = Random.Range(0,50);
                if(rand <= 9)
                {
          GameObject _InstanHealthItem =      Instantiate(_HealthItem,new Vector3(transform.position.x,transform.position.y +2.697f,transform.position.z), transform.rotation);
                }
                if(_HitTrigger != null)
                {
                    Vector3 newTrans = new Vector3(transform.position.x, transform.position.y + _HiTriggerPosY, transform.position.z + _HitTriggerPosZ);
                    GameObject _mObject = Instantiate(_HitTrigger, newTrans, Quaternion.identity) as GameObject;
                    _mObject.GetComponent<PopUpDamage>().HIT = "DEAD!";
                    _mObject.GetComponent<PopUpDamage>()._color = Color.red;
                }
            }
        }
        if (GetHurt && isDead)
        {
            if (!DeadAnimateHasbeenPlayed)
            {
                if (_SwordSlashSkillEffect != null)
                    _SwordSlashSkillEffect.SetActive(false);
                else Debug.Log("That's something strange?");
                _anim.SetTrigger("DeadTrigger");
                _anim.applyRootMotion = true;

                if (_SwordTrigger != null) _SwordTrigger.SetActive(false);



                if (_BloodParticulue != null)
                {
                    Vector3 InstanPosition = new Vector3(transform.position.x, transform.position.y + 1.94f, transform.position.z);
                    GameObject _instanThings = Instantiate(_BloodParticulue,InstanPosition,Quaternion.identity);
                    Destroy(_instanThings, 4.1f);
                }
                DeadAnimateHasbeenPlayed = true;
             StopCoroutine(GetHurtReturnToFalse());
           StopCoroutine(ReturnAlreadyGetHurt());
            }
        }
        if (isDead)
        {
            // Invoke("CapsuleColliderOff",1f);
            _capCollider.enabled = false;
            _Rgbd.useGravity = false;
            _navMeshAgent.enabled = false;
            Destroy(this.gameObject,5f);
        }
    }

    // Update Function
    // This Function is used for Check the Function which 
    // is AttackFunction or GetHurtFunction
   void Update()
    {
        GetHurtFunction();
         AttackFunction();

        _animStateInfo = _anim.GetCurrentAnimatorStateInfo(0);



    }
    // Used For Fixed Update The Function which is Moving Function
     void FixedUpdate()
    {
        MovingFunction();
    }
    // Moving Function
    // This Function which is Going to make AI to move 
    void MovingFunction()
    {
        if (!isDead)
        {
            if (!CanAttackArea)
        {
                _navMeshAgent.speed = 3;
                _navMeshAgent.stoppingDistance = 0.0f;
                if (wayPoint.Count > 0 && wayPoint[DesirePos] != null)
                {
                    _navMeshAgent.SetDestination(wayPoint[DesirePos].position);
                    float distance = Vector3.Distance(transform.position, wayPoint[DesirePos].position);
                 _anim.SetBool("Moving", true);
                    ReachDistance = false;
                    if (distance < 1.0 )
                    {
                     _anim.SetBool("Moving", false);
                        DesirePos = Random.Range(0, wayPoint.Count );
                        ReachDistance = true;
                    }
            }
        }
        else if (CanAttackArea)
            {

                if(_animStateInfo.IsName("oh_idle"))
                {
   if(_Player != null)
                {
                    float WithPlayerDistance = Vector3.Distance(this.gameObject.transform.position, _Player.transform.position);
                        _navMeshAgent.stoppingDistance = 4.8f;
              //  _navMeshAgent.speed = 2.5f;
                        _navMeshAgent.SetDestination(_Player.transform.position);

                            if(_navMeshAgent.remainingDistance > 4.8f)
                            {
                                    _anim.SetBool("Moving", true);
                            }
                            else if(_navMeshAgent.remainingDistance <= 4.8f)
                            {
                                    _anim.SetBool("Moving",false);
                            }
                }
                else if(_Player == null)
                {
                    foundPlayer = false;
                }
                }



             
            }
        }
    }
}
