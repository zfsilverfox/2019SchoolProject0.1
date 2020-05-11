using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGetDetect : MonoBehaviour
{

    public PlayerLifeStatement _PlayerLifeCtrl;
    public PlayerCtrl _PlayerCtrl;


    public GameObject _FrontHit;
    public GameObject _BackHit;
    public GameObject _RightHit;
    public GameObject _LeftHit;
    

    private static string EnemyWeaponString = "EnemyWeapon";

    private static string BOSSWeapon = "BOSSWeapon";


    public GameObject _HurtEffect;







    public Animator _PlayerAnim;

    public bool GetHit = false;
    private bool GetHitBoss = false;
  void Awake()
    {
        GetComponentFunction();
    }
    void Start()
    {
        GetComponentFunction();
        GetHit = false;
    }
    // GetComponentFunction 
    // Mainly used for Get the Component
    void GetComponentFunction()
    {
        if(_PlayerAnim == null)
        {
            _PlayerAnim = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<Animator>();
        }
        if(_PlayerLifeCtrl == null)
        {
            _PlayerLifeCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeStatement>();
        }
        if (_PlayerCtrl == null)
        {
            _PlayerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(EnemyWeaponString) && this.gameObject.tag == "FrontHit")
        {
            _BackHit.SetActive(false);
            _LeftHit.SetActive(false);
            _RightHit.SetActive(false);

            if (_HurtEffect != null)
            {
                GameObject _HurtEffectInstan = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                Destroy(_HurtEffectInstan, 10f);
            }
            else Debug.LogWarning("That's somethin  you might need to add ");
            _PlayerLifeCtrl.Health--;
            _PlayerAnim.SetTrigger("GetHitForward");
            _PlayerAnim.SetBool("Dead", false);
            _PlayerCtrl.BtnHitCount = 0;
            StartCoroutine("FrontHitReturn");

        }
        else if (other.gameObject.CompareTag(EnemyWeaponString) && this.gameObject.tag == "BackHit")
        {
            //  Debug.Log("Back Hit" + _PlayerLifeCtrl.Health);

            if (_HurtEffect != null)
            {
                GameObject _HurtEffectInstan = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                Destroy(_HurtEffectInstan, 10f);
            }
            else Debug.LogWarning("That's somethin  you might need to add ");
            _FrontHit.SetActive(false);
            _LeftHit.SetActive(false);
            _RightHit.SetActive(false);
            _PlayerAnim.SetTrigger("GetHitBack");
            _PlayerAnim.SetBool("Dead", false);
            _PlayerLifeCtrl.Health--;
            _PlayerCtrl.BtnHitCount = 0;
            StartCoroutine("BackHitReturn");
        }
        else if(other.gameObject.CompareTag(EnemyWeaponString) &&this.gameObject.tag == "RightHit")
        {
            // Debug.Log("Right Hit" + _PlayerLifeCtrl.Health);

            if (_HurtEffect != null)
            {
                GameObject _HurtEffectInstan = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                Destroy(_HurtEffectInstan, 10f);
            }
            else Debug.LogWarning("That's somethin  you might need to add ");
            _BackHit.SetActive(false);
            _LeftHit.SetActive(false);
          _FrontHit.SetActive(false);
            _PlayerAnim.SetTrigger("GetHitRight");
            _PlayerAnim.SetBool("Dead", false);
            _PlayerLifeCtrl.Health--;
            _PlayerCtrl.BtnHitCount = 0;
            StartCoroutine("RightHitReturn");
        }
        else if (other.gameObject.CompareTag(EnemyWeaponString) &&this.gameObject.tag == "LeftHit")
        {
           
            if (_HurtEffect != null)
            {
                GameObject _HurtEffectInstan = Instantiate(_HurtEffect, transform.position, Quaternion.identity);
                Destroy(_HurtEffectInstan, 10f);
            }
            else Debug.LogWarning("That's somethin  you might need to add ");
            _BackHit.SetActive(false);
           _RightHit.SetActive(false);
            _FrontHit.SetActive(false);
            _PlayerAnim.SetTrigger("GetHitLeft");
            _PlayerAnim.SetBool("Dead", false);
            _PlayerLifeCtrl.Health--;
            _PlayerCtrl.BtnHitCount = 0;
            StartCoroutine("LeftHitReturn");
        }



        if (other.gameObject.CompareTag(BOSSWeapon)&& this.gameObject.tag == "FrontHit")
        {
            if (!GetHitBoss)
            {
            _BackHit.SetActive(false);
            _LeftHit.SetActive(false);
            _RightHit.SetActive(false);
            _PlayerLifeCtrl.Health--;
            _PlayerAnim.SetTrigger("GetHitForward");

            _PlayerAnim.SetBool("Dead", false);
            _PlayerCtrl.BtnHitCount = 0;
            GetHitBoss = true;
                StartCoroutine("FrontHitTargetBOSS");
            StartCoroutine("BOSSHitReturn");
            }
        }
            else if (other.gameObject.CompareTag(BOSSWeapon) && this.gameObject.tag == "BackHit")
        {

            if(!GetHitBoss)
            {
                GetHitBoss = true;
                _FrontHit.SetActive(false);
                _LeftHit.SetActive(false);
                _RightHit.SetActive(false);
                _PlayerAnim.SetTrigger("GetHitBack");
                _PlayerAnim.SetBool("Dead", false);
                _PlayerLifeCtrl.Health--;
                _PlayerCtrl.BtnHitCount = 0;
                StartCoroutine("BOSSHitReturn");
                StartCoroutine("BackHitTargetBOSS");
            }
        }
            else if (other.gameObject.CompareTag(BOSSWeapon) && this.gameObject.tag == "RightHit")
        {
            if (!GetHitBoss)
            {
             GetHitBoss = true;
            _BackHit.SetActive(false);
            _LeftHit.SetActive(false);
            _FrontHit.SetActive(false);
            _PlayerAnim.SetTrigger("GetHitRight");
            _PlayerAnim.SetBool("Dead", false);
            _PlayerLifeCtrl.Health--;
            _PlayerCtrl.BtnHitCount = 0;
            StartCoroutine("BOSSHitReturn");
                StartCoroutine("RightHitTargetBOSS");
            } 
        }
            else if(other.gameObject.CompareTag(BOSSWeapon) && this.gameObject.tag == "LeftHit")
        {
            if (!GetHitBoss)
            {
                GetHitBoss = true;
                _BackHit.SetActive(false);
                _RightHit.SetActive(false);
                _FrontHit.SetActive(false);
                _PlayerAnim.SetTrigger("GetHitLeft");
                _PlayerAnim.SetBool("Dead", false);
                _PlayerLifeCtrl.Health--;
                _PlayerCtrl.BtnHitCount = 0;
                StartCoroutine("BOSSHitReturn");
                StartCoroutine("LeftHitTargetBOSS");
            }
        }
    }
    IEnumerator FrontHitReturn()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _LeftHit.SetActive(true);
        _RightHit.SetActive(true);
        PlayerLifeStatement._INSTANCE._PlayerCtrl.Hurt = false;
    }
    IEnumerator BackHitReturn()
    {
        yield return new WaitForSeconds(0.7f);
        _FrontHit.SetActive(true);
        _LeftHit.SetActive(true);
        _RightHit.SetActive(true);
        PlayerLifeStatement._INSTANCE._PlayerCtrl.Hurt = false;
    }
    IEnumerator RightHitReturn()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _LeftHit.SetActive(true);
        _FrontHit.SetActive(true);
        PlayerLifeStatement._INSTANCE._PlayerCtrl.Hurt = false;
    }
    IEnumerator LeftHitReturn()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _RightHit.SetActive(true);
        _FrontHit.SetActive(true);
        PlayerLifeStatement._INSTANCE._PlayerCtrl.Hurt = false;
    }
    IEnumerator BOSSHitReturn()
    {
        yield return new WaitForSeconds(1.5f);
        GetHitBoss = false;
    }

    IEnumerator FrontHitTargetBOSS()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _LeftHit.SetActive(true);
        _RightHit.SetActive(true);
    }

    IEnumerator BackHitTargetBOSS()
    {
        yield return new WaitForSeconds(0.7f);
        _FrontHit.SetActive(true);
        _LeftHit.SetActive(true);
        _RightHit.SetActive(true);
    }
     
    
    IEnumerator RightHitTargetBOSS()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _LeftHit.SetActive(true);
        _FrontHit.SetActive(true);
    }

    IEnumerator LeftHitTargetBOSS()
    {
        yield return new WaitForSeconds(0.7f);
        _BackHit.SetActive(true);
        _RightHit.SetActive(true);
        _FrontHit.SetActive(true);
    }
}
