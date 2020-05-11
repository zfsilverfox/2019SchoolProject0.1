using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIENEMY : MonoBehaviour
{
    Rigidbody _Rgbd;
    Animator _anim;
    CapsuleCollider _capCollider;
    NavMeshAgent _navMeshAgent;
public    GameManager1 _gameManager1;

    public GameObject _BeingHurtObject;

    public GameObject _HurtEffect;



    public List<Transform> wayPoint;
    public Transform CrtPos;
    public bool Reverse = true;
    public bool ReachDistance = false;
    public int DesirePos;

    public bool GetHurt= false;
    public bool isDead = false;

    public bool CanAttack = false;
    public bool DoAttack = false;
    public bool CanAttackArea = false;

    bool DeadAnimateHasbeenPlayed = false;
    bool isAlreadyGetHurt = false;
    public int Life = 5;
    public float AttackTimer=0.0f;
    public float stopAttackTimer = 15.0f;
    public GameObject _Player;
    public GameObject _ScannerArea;
    public GameObject _swordPos;
    public GameObject _PopUpDamage;
    public GameObject _SwordEffectObject;



    [Range(0,1.0f)]
    public float ColorChangeRd =0.5f;
    [Range(0,1.0f)]
    public float ColorChangeBl = 0.5f;
    [Range(0, 1.0f)]
    public float ColorChangeGr = 0.5f;
    [Range(0, 1.0f)]
    public float ColorChangeA = 0.5f;


    public AudioClip _swordEffect;
    public AudioClip _DeadClip;


    public float DeadEffectYRealPos = 0.0f;



    // Awake
    // This function is mainly used for Get the Component 
     void Awake()
    {
        GetComponentFunction();
    }

    // Start
    // This Function is mainly used for GetComponent or Setting Basic Problem
   void Start()
    {
        GetComponentFunction();
        SettingProblem();
    }

    // Setting Problem 
    // The setting problem which is setting the problem
    void SettingProblem()
    {
            GetHurt = false;
            DeadAnimateHasbeenPlayed = false;
        _capCollider.isTrigger = false;
        _anim.applyRootMotion = false;
        DesirePos = Random.Range(0, wayPoint.Count - 1);

          //  Life = Random.Range(3,5);
            stopAttackTimer = Random.Range(5, 13);

        if(stopAttackTimer >= 5 && stopAttackTimer < 9)
        {
            Life = Random.Range(8, 10);
        }
        else if(stopAttackTimer >= 9 && stopAttackTimer <= 13)
        {
            Life = Random.Range(11,13);
        }



    }

    // GetComponent Function
    //This Function is mainly used for Get Component 
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
        if(_gameManager1 == null)
        {
            _gameManager1 = GameObject.FindGameObjectWithTag("GameManager1").GetComponent<GameManager1>();
        }
        if (_PopUpDamage == null) Debug.LogError("Please Added the Pop UP Damage Effect");
        if (_SwordEffectObject == null) Debug.Log("Please Add The Effect Object to the  Script");

    }
    // OnTriggerEnter 
    // Get Hurt Or Played Enter the Area of the  Scanned Area 
 void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "PlayerWeapon"&& this.gameObject.tag =="Enemy")
        {
            
             //   Debug.Log("Enemy Was Get Hurted ");
            _BeingHurtObject.SetActive(false);
            if (!GetHurt)
            {
                GetHurt = true;
                StartCoroutine(GetHurtReturnToFalse());
            }
        }
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = true;
        }


    }
    // On Trigger Stay 
    // This Function is mainly used for when Player stayed in the Area of the
    // Scanned Area
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = true;
        }

    }
    // OnTriggerExit 
    // This function is mainly used for when Player Exit the Area of the 
    // Scanned Area
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _ScannerArea.gameObject.CompareTag("EnemyDetect"))
        {
            CanAttackArea = false;
        }
    }
    // Get Hurt Return to False
    // This Function is mainly used for When Get Hurt turn back to false
    IEnumerator GetHurtReturnToFalse()
    {
        yield return new WaitForSeconds(0.25f);
        GetHurt = false;
    }
    // Return AI ready Get Hurt 
    // This Function is mainly used for return the AI Area
    IEnumerator ReturnAlreadyGetHurt()
    {
        yield return new WaitForSeconds(0.8f);
        isAlreadyGetHurt = false;
    }
    // This Attack Function is used for Attack The Function 
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
                        _anim.SetTrigger("OnAttack1");
                        AudioSource.PlayClipAtPoint(_swordEffect, _swordPos.transform.position);
                        CanAttack = false;
                }
            }
        }
    }
    // Get Hurt 
    // Check if Enemy Health is more than 0 
    // if more than 0 ,will become Hurt State
    // else become Dead State
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
                if(Life > 0 )
                {
                    if (!isDead)
                    {
                        GameObject _mObject = (GameObject)Instantiate(_PopUpDamage, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z - 1.04f), Quaternion.identity);
                                        _mObject.GetComponent<PopUpDamage>().HIT = "HIT!";
                                        _mObject.GetComponent<PopUpDamage>()._color = new Color(ColorChangeRd, ColorChangeBl, ColorChangeGr, ColorChangeA);
                    }
                }
           
                Life --;
            }
            if(Life <= 0)
            {
                isDead = true;
                _gameManager1.EnemyCountDown += 1;
            }
        }
        if (GetHurt && isDead)
        {
            if (!DeadAnimateHasbeenPlayed)
            {
                _BeingHurtObject.SetActive(false);
                _anim.SetTrigger("DeadTrigger");
                _anim.applyRootMotion = true;
                if (_SwordEffectObject != null)
                {
                    _SwordEffectObject.SetActive(false);
                }
                else Debug.Log("That's something Strange");
                AudioSource.PlayClipAtPoint(_DeadClip, this.transform.position);
                GameObject _mObject = (GameObject)Instantiate(_PopUpDamage, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z - 1.04f), Quaternion.identity);
                _mObject.GetComponent<PopUpDamage>().HIT = "DEAD!";
                _mObject.GetComponent<PopUpDamage>()._color = Color.magenta;
                if (_HurtEffect != null)
                {
                    Vector3 PosForDeadEffectPos = new Vector3(transform.position.x, transform.position.y + DeadEffectYRealPos, transform.position.z);
                    GameObject _INSTANDeadEffect = Instantiate(_HurtEffect,PosForDeadEffectPos, Quaternion.identity);
                    Destroy(_INSTANDeadEffect, 4f);

                }
                _capCollider.isTrigger = true;
                DeadAnimateHasbeenPlayed = true;
                StopCoroutine(GetHurtReturnToFalse());
                StopCoroutine(ReturnAlreadyGetHurt());
            }
        }
        if (isDead)
        {
           
            _navMeshAgent.enabled = false;
          Destroy(this.gameObject,5f);
            
        }
    }
    // Mainly used for Update the Attack State 
    // Or Get Hurt State
   void Update()
    {
        GetHurtFunction();
         AttackFunction();
    }

    // Mainly used for Fixed Update the Moving State
     void FixedUpdate()
    {
        MovingFunction();
    }

    // Mainly used for Moving the State which is Moving
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
                    float WithPlayerDistance = Vector3.Distance(this.gameObject.transform.position, _Player.transform.position);
                    if(WithPlayerDistance <=3.08f)
                {
                        _anim.SetBool("Moving", false);
                        _navMeshAgent.speed = 0.0f;
                    _navMeshAgent.stoppingDistance = 0.2f;
                }
                      else if (WithPlayerDistance >= 3.26f)
                {
                        _anim.SetBool("Moving", true);
                        _navMeshAgent.speed = 1.5f;
                    _navMeshAgent.stoppingDistance = 0.2f;
                }
                    _navMeshAgent.SetDestination(_Player.transform.position);
            }
        }
    }
}
