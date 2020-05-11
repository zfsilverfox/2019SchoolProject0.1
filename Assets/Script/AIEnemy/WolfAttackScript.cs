using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackScript : MonoBehaviour {

    public float DestroyTime;

    Rigidbody _rgbd;

    //  public GameObject _Player;

    public Transform _PlayerTrans;
    public float _Power = 3f;
    void Awake()
    {
        AvoidNullProblem();
    }
    void Start()
    {
        AvoidNullProblem();
        Destroy(this.gameObject,4f);
        if (_PlayerTrans != null)
        {
            Vector3 _Dir = (_PlayerTrans.position - transform.position).normalized;
            this.transform.forward = new Vector3(_Dir.x, 0f, _Dir.z);
            _rgbd.AddForce(this.transform.forward * _Power);
        }
       else Destroy(this.gameObject);
    }


    //This is Mainly used For Avoid The Null Problem
    void AvoidNullProblem()
    {
        if (_PlayerTrans == null)
                _PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();

    }

    // This GameObject Which is Used For Destroy When Enter The Player Hurt Area 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FrontHit"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("BackHit"))
        {
            Destroy(this.gameObject);
        }
         if (other.gameObject.CompareTag("RightHit"))
        {
            Destroy(this.gameObject);
        }
      if (other.gameObject.CompareTag("LeftHit"))
        {
            Destroy(this.gameObject);
        }
    }
}
