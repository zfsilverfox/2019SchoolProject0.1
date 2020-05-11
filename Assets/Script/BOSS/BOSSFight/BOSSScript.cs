using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//他の人のソースコードを参考にして、自身で修正を施した
namespace AIBOSSCreator
{
public enum EnemyState
{
        Enemy_WanderState,
        Enemy_IldeState,
        Enemy_FollowPlayerState,
        Enemy_AttackState,
        Enemy_HurtState,
        Enemy_DeadState,
        Enemy_DefendState,
}




public class BOSSScript : StateFulObjectBase<BOSSScript,EnemyState>
    {
       
        Animator _anim;
    public    Rigidbody _rgbd;
        public GameObject _playerObject;
        public GameObject _SwordSkillProblem;
    public    PlayerCtrl _playerCtrl;
        public int Life ;
        public int MaxLife = 30;
        private Transform _player;
        public float Spd = 5f;
      public  float RotationSmooth = 1f;
       public float turrentRotationSmooth = 0.8f;
        public float AttackInterval = 3.5f;
        public float pursuitSqrDistance= 250f;
        public float AttackSqrtDistance = 300f;
        public float margin = 20f;
     public   float changeTargetSqrDistance = 0.6f;
        public bool Hurt = false;
        public bool isDead = false;
        Vector3 _targetPos;
        public GameObject _HurtActiveOn;
        public GameObject _DefendObjectAcrtiveOn;


        public AudioClip _swordHitsound;
        public AudioClip _DefendSound;
        public AudioClip _WalkingSoundClip;


        public GameObject _HitTrigger;
            




        private Camera _camera;


        public GameObject _PopUpDamage;
        public GameObject _BossSword;


        



        
        void Start()
        {
            Initialized();
            Life = MaxLife;
            _camera = Camera.main;
            SettingBasicProb();
        }
        void OnTriggerEnter(Collider other)
        {
            if ( _HurtActiveOn.activeSelf&&_HurtActiveOn.gameObject.CompareTag("BOSSHurtObject") &&other.gameObject.CompareTag("PlayerWeapon") && other.gameObject.activeInHierarchy)
            {
                if(!Hurt)
                {
                    TakeDamage();
                    Hurt = true;
                    ChangeState(EnemyState.Enemy_HurtState);
                    StartCoroutine("HurtTurnBackToFalse");
                }
            }
         if ( _DefendObjectAcrtiveOn.activeSelf&&_DefendObjectAcrtiveOn.gameObject.CompareTag("BOSSDefendObject")&&other.gameObject.CompareTag("PlayerWeapon") && other.gameObject.activeInHierarchy)
            {
                AudioSource.PlayClipAtPoint(_DefendSound, _BossSword.transform.position);
                _playerCtrl.isStunnedMode = true;
                _SwordSkillProblem.SetActive(false);
            }
        }
        IEnumerator HurtTurnBackToFalse()
        {
            yield return new WaitForSeconds(0.78F);
            Hurt = false;
        }
        void Initialized()
        {
            if(_anim == null)
            _anim = GetComponentInChildren<Animator>();
            _player = GameObject.FindWithTag("Player").transform;
            if (_rgbd == null)
                _rgbd = GetComponent<Rigidbody>();
            if (_playerCtrl == null)
                _playerCtrl = _player.GetComponent<PlayerCtrl>();
            stateList.Add(new WanderState(this));
            stateList.Add(new IldeState(this));
            stateList.Add(new FollowPlayer(this));
            stateList.Add(new AttackPlayer(this));
            stateList.Add(new GetHurtStatement(this));
            stateList.Add(new DeadClass(this));
            stateList.Add(new DefendClass(this));
            stateMachine = new StateMachine<BOSSScript>();
            ChangeState(EnemyState.Enemy_WanderState); 

        }
        void SettingBasicProb()
        {
            Hurt = false;
            isDead = false;
        }
        public void TakeDamage()
        {
            Life--;

            GameObject _mObject = (GameObject)Instantiate(_PopUpDamage,new Vector3(transform.position.x,transform.position.y +6.29f,transform.position.z - 1.04f), Quaternion.identity);
            _mObject.GetComponent<PopUpDamage>().HIT = "HIT!";

            if(Life <=0)
            {
                ChangeState(EnemyState.Enemy_DeadState);
                GameManager3BOSSStage._instance.GameClear = true;
                _anim.SetBool("Dead", true);
                    StopAllCoroutines();
            }
        }
        private class WanderState : State<BOSSScript>
        {
            public WanderState(BOSSScript owner): base(owner) { }
            public override void OnEnterState()
            {
               owner. _targetPos = GetRandomPosForEnemyAI();
                owner.Spd = Random.Range(-0.05f, 0.05f);
                owner._HitTrigger.SetActive(false);


            }
            public override void Execute()
            {
                float _rgbdDistanceWithDistancePosition = Vector3.SqrMagnitude(owner._targetPos.normalized - owner.transform.position.normalized);
                owner._anim.SetBool("isWalking", true);
                if (_rgbdDistanceWithDistancePosition <= 0.13f)
                {
                   owner.ChangeState(EnemyState.Enemy_IldeState);
                }
                else if(_rgbdDistanceWithDistancePosition >= 0.25f)
                {
                 //  owner.Spd = -0.05f;
                  owner.ChangeState(EnemyState.Enemy_IldeState);

                }
              //  owner.transform.LookAt(new Vector3(owner._targetPos.x,0f, 0f));
                owner._rgbd.MovePosition(owner._rgbd.position - owner._targetPos * owner.Spd * Time.deltaTime);
                if(owner._player != null)
                {
                   float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);
                                if (_playerDistance <= 200f)
                                {
                                    owner._anim.SetBool("isWalking", false);
                                    owner.Spd = 0.0f;
                                    owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                                }
                }


             
            }
            public override void OnExitState()  {   }
            public Vector3 GetRandomPosForEnemyAI()
            {
                return new Vector3(Random.Range(-29.6f,25.8f), 0f, Random.Range(82.9f,108.4f));
            }
        }
        private class IldeState: State<BOSSScript>
        {
            float TimeToChange = 0.0f;
            float TimeLimit = 3.0f;
            public IldeState(BOSSScript owner): base(owner) { }
            public override void OnEnterState()
            {
                owner.Spd = 0.0f;
                TimeToChange = 0.0f;
                owner._HitTrigger.SetActive(false);
            }
            public override void Execute()
            {
                if(!owner.isDead)
                {
             owner._anim.SetBool("isWalking",false);

                    if (owner._player != null)
                    {
                        float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);
                        if (_playerDistance <= 200f)
                        {
                            Debug.Log("Follow Player ");
                            owner._anim.SetBool("isWalking", false);
                            owner.Spd = 0.0f;
                            owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                        }
                        else if (_playerDistance >= 250f)
                        {
                            TimeToChange += Time.deltaTime;
                            if (TimeToChange >= TimeLimit)
                            {
                                owner.ChangeState(EnemyState.Enemy_WanderState);
                            }
                        }
                    }
                  //  else Debug.Log("That's Something You need to dO");



                }

             
            }
            public override void OnExitState()
            {
              
            }
        }
        private class FollowPlayer : State<BOSSScript>
        {
            public FollowPlayer(BOSSScript owner) : base(owner) { }
            public override void OnEnterState()
            {
                Debug.Log(" Enter the Player Follow Statement ");
                owner.Spd = 1.4f;
                if(owner._anim != null)
                owner._anim.SetBool("isWalking", true);
                if(owner._anim != null)
                owner._anim.ResetTrigger("Attack1Trigger");
                if(owner._anim != null)
                owner._anim.ResetTrigger("Attack2Trigger");
                if(owner._anim != null)
                owner._anim.ResetTrigger("Attack3Trigger");
                if (owner._anim != null)
                    owner._anim.ResetTrigger("Attack4Trigger");
                if (owner._anim != null)
                    owner._anim.ResetTrigger("Attack5Trigger");
                if (owner._anim != null)
                    owner._anim.ResetTrigger("BlockTrigger");
                if(owner._HitTrigger != null)
                owner._HitTrigger.SetActive(false);
            }
            public override void Execute()
            {
                if (!owner.isDead)
                {
                    if (owner._player != null)
                {
                float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position- owner._player.transform.position);
                owner._rgbd.MovePosition(Vector3.Lerp(owner._rgbd.transform.position,owner._player.transform.position,Time.deltaTime* owner.Spd));
                Quaternion lookRot = Quaternion.LookRotation(owner._player.transform.position- owner._rgbd.transform.position);
                owner._rgbd.MoveRotation(lookRot);
                        if (_playerDistance >=69.05f)
                        {
                            owner.ChangeState(EnemyState.Enemy_WanderState);
                        }
                        else if (_playerDistance <= 56f)
                        {
                            owner._anim.SetBool("isWalking", false);
                            int randomStateEnter = Random.Range(0,100);

                            if(owner.Life >= 5)
                            {
                            if(randomStateEnter >=0&&randomStateEnter <= 20)
                                    {
                                        owner.ChangeState(EnemyState.Enemy_DefendState);
                                    }
                                 if(randomStateEnter >=21 && randomStateEnter <= 100)
                                    {
                                        owner.ChangeState(EnemyState.Enemy_AttackState);
                                    }
                            }

                            if(owner.Life < 5)
                            {
                                if(randomStateEnter >= 0 && randomStateEnter <= 40)
                                {
                                    owner.ChangeState(EnemyState.Enemy_DefendState);
                                }
                                if(randomStateEnter >= 41 && randomStateEnter <= 100)
                                {
                                    owner.ChangeState(EnemyState.Enemy_AttackState);
                                }
                            }
                        }
                }
                    else
                    {
                        
                        owner.ChangeState(EnemyState.Enemy_IldeState);
                        
                    }
                }
            }
            public override void OnExitState()
            {
            }
        }
        private  class AttackPlayer : State<BOSSScript>
        {
            private float lastAttackTime;
             int rand;
            public AttackPlayer(BOSSScript owner) : base(owner) { }
            public override void OnEnterState()
            {
                owner.Spd = 0.0f;
                owner._anim.applyRootMotion = false;
                owner.RotationSmooth = 0.0f;
                owner._anim.ResetTrigger("BlockTrigger");
                if (owner.Life <= 10)
                    owner.AttackInterval = Random.Range(1.3f,1.8f);
                else if (owner.Life >= 15)
                    owner.AttackInterval = Random.Range(3.8f,4.2f);
            }
            public override void Execute()
            {
                if (!owner.isDead)
                {
                    if(owner._player != null)
                    {
                    float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);

                            Quaternion lookRot = Quaternion.LookRotation(owner._player.transform.position - owner._rgbd.transform.position);
                            owner._rgbd.MoveRotation(lookRot);
                                if(_playerDistance <= 75f)
                                {

                              
                    

                                    if (Time.time > lastAttackTime + owner.AttackInterval)
                                    {
                                        rand = Random.Range(0,20);
                                        if(rand >=0 && rand <= 3)
                                        {
                                            owner._anim.SetTrigger("Attack1Trigger");
                                            AudioSource.PlayClipAtPoint(owner._swordHitsound, this.owner.gameObject.transform.position);
                                    lastAttackTime = Time.time;
                                }
                                        else if (rand >= 4 && rand <= 6)
                                        {
                                            owner._anim.SetTrigger("Attack2Trigger");
                                            AudioSource.PlayClipAtPoint(owner._swordHitsound, this.owner.gameObject.transform.position);
                                    lastAttackTime = Time.time;
                                }
                                        else if(rand >= 7 && rand <= 9)
                                        {
                                            owner._anim.SetTrigger("Attack3Trigger");
                                    AudioSource.PlayClipAtPoint(owner._swordHitsound, this.owner.gameObject.transform.position);
                                    lastAttackTime = Time.time;
                                }
                                        else if(rand >= 10 && rand <= 15)
                                        {
                                            owner._anim.SetTrigger("Attack4Trigger");
                                    AudioSource.PlayClipAtPoint(owner._swordHitsound, this.owner.gameObject.transform.position);
                                    lastAttackTime = Time.time;
                                }
                                        else if(rand >=16 && rand <= 18)
                                        {
                                            owner._anim.SetTrigger("Attack5Trigger");
                                    AudioSource.PlayClipAtPoint(owner._swordHitsound, this.owner.gameObject.transform.position);
                                    lastAttackTime = Time.time;
                                }
                                        else if(rand >= 19 && rand <= 20)
                                    {
                                        owner.ChangeState(EnemyState.Enemy_DefendState);
                                    lastAttackTime = Time.time;
                                }               
                                    }
                                }
                                else if (_playerDistance >= 80f)
                                {
                                        Debug.Log("Enemy Go Back to Follow Player Statement");
                                owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                                }
                                else if( _playerDistance >= 100f)
                                {
                                owner.ChangeState(EnemyState.Enemy_WanderState);
                                }
                    }
                    else
                    {
                        owner.ChangeState(EnemyState.Enemy_IldeState);
                    }

                }
            }
            public override void OnExitState()
            {
             
            }
        }
        private class GetHurtStatement: State<BOSSScript>
        {
            public GetHurtStatement(BOSSScript owner) : base(owner) { }
            float _TimeStart = 0.0f;
            float _TimeLimit ;
            public override void OnEnterState()
            {
                owner._anim.ResetTrigger("Attack1Trigger");
                owner._anim.ResetTrigger("Attack2Trigger");
                owner._anim.ResetTrigger("Attack3Trigger");
                owner._anim.ResetTrigger("Attack4Trigger");
                owner._anim.ResetTrigger("Attack5Trigger");
                owner._anim.ResetTrigger("BlockTrigger");
                owner._anim.SetTrigger("GetHit");
                owner._anim.SetBool("isWalking", false);
                owner._anim.applyRootMotion = false;
                _TimeStart = 0.0f;


                if(owner.Life >=10)
                _TimeLimit = Random.Range(0.4f, 1.2f);

                if(owner.Life < 10)
                {
                    _TimeLimit = Random.Range(0.2f, 0.9f);
                }

            }
            public override void Execute()
            {
                _TimeStart += Time.deltaTime;
                    if ( _TimeStart >= _TimeLimit)
                {
                    if(owner._player != null)
                    {
                        if (owner._player)
                        {
                                    float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);
                                if(_playerDistance <= 80.0f)
                                    owner.ChangeState(EnemyState.Enemy_DefendState);
                                else if (_playerDistance >= 100.0f)
                                    owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                                else if (_playerDistance >= 135.0f)
                                    owner.ChangeState(EnemyState.Enemy_WanderState);
                        }
                    }
                }
            }
            public override void OnExitState()
            { }
        } 
        private class DeadClass : State<BOSSScript>
        {
            public DeadClass (BOSSScript owner): base(owner) { }
            public override void OnEnterState()
            {
                Debug.Log("Boss Has Enter The Dead State");
                owner._anim.SetTrigger("DeadTrigger");

                //owner._anim.ResetTrigger("BlockTrigger");
                //owner._anim.ResetTrigger("Attack1Trigger");
                //owner._anim.ResetTrigger("Attack2Trigger");
                //owner._anim.ResetTrigger("Attack3Trigger");
                //owner._anim.ResetTrigger("Attack4Trigger");
                //owner._anim.ResetTrigger("Attack5Trigger");

                owner._anim.applyRootMotion = true;
                Destroy(owner.gameObject, 2.5f);
                owner.isDead = true;
            }
            public override void Execute()
            {
               

            }
            public override void OnExitState()
            {
               
            }
        }
        private class DefendClass : State<BOSSScript>
        {
            public DefendClass(BOSSScript owner): base(owner){ }
            float timeCount = 0.0f;
            float TimeLimit ;
           // bool HasPlayBefore = false;
            public override void OnEnterState()
            {
                owner._anim.ResetTrigger("Attack1Trigger");
                owner._anim.ResetTrigger("Attack2Trigger");
                owner._anim.ResetTrigger("Attack3Trigger");
                owner._anim.ResetTrigger("Attack4Trigger");
                owner._anim.ResetTrigger("Attack5Trigger");
                Debug.Log("Enter Defend State");
                owner.  _HurtActiveOn.SetActive(false);
                owner._DefendObjectAcrtiveOn.SetActive(true);
                timeCount = 0.0f;
          //      HasPlayBefore = false;
                if (owner.Life >= 10)
                {
                    TimeLimit = Random.Range(3.5f,6.9f);
                }
               if(owner.Life <=9)
                {
                    TimeLimit = Random.Range(8.5f, 10f);
                }
                owner.Spd = 0.0f;
            }
            public override void Execute()
            {
                if(owner._player != null)
                {
                    float GoingtoFollowPlayer = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);
                 //   Debug.Log("Follow Player " + GoingtoFollowPlayer);

                   if(GoingtoFollowPlayer <= 99F)
                    {
                        if (owner._playerCtrl!= null)
                {
                    if(!owner._playerCtrl.isStunnedMode)
                {
                    timeCount += Time.deltaTime;
              //  owner._anim.SetTrigger("BlockTrigger");
                if (timeCount >= TimeLimit)
                {
                    if (owner._player != null)
                    {
                        float _playerDistance = Vector3.SqrMagnitude(owner._rgbd.transform.position - owner._player.transform.position);
                    if (_playerDistance <= 80.0f)
                        owner.ChangeState(EnemyState.Enemy_AttackState);
                    else if (_playerDistance >= 100.0f)
                        owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                    else if (_playerDistance >= 135.0f)
                        owner.ChangeState(EnemyState.Enemy_WanderState);
                    }
                }
                }
                        else if(owner._playerCtrl.isStunnedMode)
                        {
                            owner.ChangeState(EnemyState.Enemy_AttackState);
                        }
                }
                    }
                   else if(GoingtoFollowPlayer >=100f)
                    {
                        owner.ChangeState(EnemyState.Enemy_FollowPlayerState);
                    }

                   
                }

                if (owner._anim != null)
                {
                    owner._anim.SetTrigger("BlockTrigger");
                }



            }
            public override void OnExitState()
            {
                owner._HurtActiveOn.SetActive(true);
                owner._DefendObjectAcrtiveOn.SetActive(false);
                owner.Spd = 0.5f;
           //     owner._anim.SetBool("BlockBool", false);
                Debug.Log("Has Exit The Defend State");
                if (owner._anim != null)
                {
                    owner._anim.SetBool("BlockBool",false);
                }



            }
        }
    }
}



