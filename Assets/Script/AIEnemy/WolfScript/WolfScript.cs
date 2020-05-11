using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class WolfScript : MonoBehaviour {

    NavMeshAgent _navMesh;
    Animator _anim;


    public List<Transform> WayPoints;


    bool isDead = false;
    bool CanAttackArea = false;
    bool DoAttack = false;
    bool isAttack = false;
    [HideInInspector]
    public bool FoundThePlayer = false;


    public int wayPointsCount;


    public GameObject _ScannedArea;
    public GameObject _AttackItem;
    public Transform _AtkPos;

    public GameObject _Player;
    PlayerCtrl _PlayerCtrl;

    public GameObject _missionPanel;

    public GameObject _PopUpDamage;
    bool PopUpDamageHPBf;


    public GameObject _DeadEffect;
    bool DeadEffectHasPBf;

    public GameObject _HurtEffect;









    public GameManager2 _gm2;


    public float StartedTime = 0.0f;
    public float EndedTIme = 3.0f;


    public float newPosYForInstanPUEffect;

    public float newPosZForInstanPUEffect;


    public int Life;
    bool isHurt;
    bool isAlreadyGetHurt = false;

    void Awake()
    {
        GetComponentFunction();
    }



    void Start()
    {
        GetComponentFunction();
        BasicSetting();

    }
    void GetComponentFunction()
    {
        if (_navMesh == null) _navMesh = GetComponent<NavMeshAgent>();
        if (_anim == null) _anim =GetComponentInChildren<Animator>();
        if (_Player == null) _Player = GameObject.FindGameObjectWithTag("Player");
        if (_PlayerCtrl == null) _PlayerCtrl = _Player.GetComponent<PlayerCtrl>();

        // else Debug.Log("That's something Strange");
        if (_gm2 == null) Debug.Log("That's something you need to added ");

        if (_DeadEffect == null) Debug.Log("That's something You need to add from Prefab Object");

        if (_HurtEffect == null) Debug.LogWarning("That's something you need to add");


    }
    // This is Doing The Basic Setting Which is The Health,Hurt And Death
    void BasicSetting()
    {
        isDead = false;
       CanAttackArea = false;
        DoAttack = false;
         isAttack = false;
        StartedTime = 0.0f;
        EndedTIme = Random.Range(1.5f,3.6f);
        wayPointsCount = Random.Range(0, WayPoints.Count - 1);
        if (_navMesh != null) _navMesh.SetDestination(WayPoints[wayPointsCount].position);
        PopUpDamageHPBf = false;
        DeadEffectHasPBf = false;
        Life = Random.Range(6,9);
        isHurt = false;
        isAlreadyGetHurt = false;
    }

    // Attack, Death , Hurt Which is done In Update Function
    void Update()
    {
        AttackFunction();
        GetHurtFunction();
        DeadFunction();
    }
    // Moving,Found Player Which is done In Fixed Update Function
 void FixedUpdate()
    {
        MovingFunction();
    }
    void MovingFunction()
    {
        if (!isDead)
        {
            if(!FoundThePlayer)
            {
                _navMesh.stoppingDistance = 0f;

                if (WayPoints != null && wayPointsCount >= 0)
                {

                    _anim.SetBool("Walking", true);
                    if(!FoundThePlayer && _navMesh.remainingDistance <= 1.0f)
                    {
                        _anim.SetBool("Walking", false);
                        wayPointsCount = Random.Range(0, WayPoints.Count - 1);
                        _navMesh.SetDestination(WayPoints[wayPointsCount].position);
                    }
                }
            }
        }
    }
    void AttackFunction()
    {
        if (!isDead)
        {
            if (FoundThePlayer)
            {
                _navMesh.stoppingDistance = 5.0f;
                if(_navMesh != null)
                {
                    if(_Player !=null)
                    _navMesh.SetDestination(_Player.transform.position);      
                }

                if(_navMesh.remainingDistance <= 5f)
                {
                    _anim.SetBool("Walking", false);
                    CanAttackArea = true;
                    //Debug.Log("Has " +CanAttackArea);
                }
                else if (_navMesh.remainingDistance >5f)
                {
                    _anim.SetBool("Walking",true);
                    CanAttackArea = false;
                }


            }
        }

        if (!isDead)
        {
            if (FoundThePlayer)
            {
                if (!DoAttack)
                {

                    if (!_PlayerCtrl.Dead)
                    {
                        if (CanAttackArea)
                        {
                            StartedTime += Time.deltaTime;

                            if(StartedTime >= EndedTIme)
                            {
                                DoAttack = true;
                            }
                        }
                    }

                }
            }
        }
        if (!isDead)
        {
            if (DoAttack)
            {

                Debug.Log("Do Attack ");
                if(_AttackItem != null)
                {
                    GameObject _InstanAtkObject = Instantiate(_AttackItem, _AtkPos.position, Quaternion.identity);
                }
                DoAttack = false;
                StartedTime = 0.0f;
            }
        }
    }
    void GetHurtFunction()
    {
            if (isHurt&& !isDead)
            {
                if (!isAlreadyGetHurt)
                {
                    Life--;
                    isAlreadyGetHurt = true;
                    StartCoroutine("ISALREADTGETHURT");
                    if(Life > 0)
                    {

                        if(_PopUpDamage != null)
                        {
                            GameObject _InstanPopUpDamage = Instantiate(_PopUpDamage, transform.position, Quaternion.identity);
                            _InstanPopUpDamage.GetComponent<PopUpDamage>().HIT = "HIT !!!";
                        }
                  
                   


                    }

                    if(Life > 0)
                    {
                       if(_HurtEffect != null)
                        {
                            GameObject InstanHurtEffect = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                            Destroy(InstanHurtEffect,2f);
                        }
                    }
                }
            }
            if(Life <= 0)
            {
                isDead = true;
            }
    }
    IEnumerator ISALREADTGETHURT()
    {
        yield return new WaitForSeconds(0.3f);
        isAlreadyGetHurt = false;
    }
    IEnumerator IsHurtTurnBackToFalse()
    {
        yield return new WaitForSeconds(0.01f);
        isHurt = false;
    }
        void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && _ScannedArea.CompareTag("EnemyDetect"))
        {
            FoundThePlayer = true;
            Debug.Log("Follow The Player ");
        }

        if (other.gameObject.tag == "PlayerWeapon" && this.gameObject.tag == "Enemy")
        {
            Debug.Log("Is Dead ");


            if (!isHurt)
            {
                isHurt = true;
                StartCoroutine("IsHurtTurnBackToFalse");
            }

           
        }

     

    }
        void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _ScannedArea.CompareTag("EnemyDetect"))
        {
            FoundThePlayer = true;
        }
    }
   void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _ScannedArea.CompareTag("EnemyDetect"))
        {
            FoundThePlayer = false;
        }
    }
    void DeadFunction()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
            if (_gm2 != null)
                _gm2.KillWolfNum ++;
            else Debug.Log("That's something you need to add");
            if (!PopUpDamageHPBf)
            {
                Vector3 _InstanObjectPos = new Vector3(transform.position.x,transform.position.y + newPosYForInstanPUEffect, transform.position.z + newPosZForInstanPUEffect);
                GameObject _mObject = Instantiate(_PopUpDamage, _InstanObjectPos, Quaternion.identity) as GameObject;
                _mObject.GetComponent<PopUpDamage>().HIT = "DEAD";
            }
            if (!DeadEffectHasPBf)
            {
                if (_DeadEffect != null)
                {
                    GameObject _InstanBloodEffect = Instantiate(_DeadEffect, transform.position, Quaternion.identity);
                    float DestroyTiming = Random.Range(1.2f, 4f);
                    Destroy(_InstanBloodEffect, DestroyTiming);
                }
                DeadEffectHasPBf = true;
            }
        }
    }
}
