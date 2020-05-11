using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tommorow I will finished the Script of the Gun
/// </summary>
public class GunScript : MonoBehaviour {

    public float Power = 30f;
    Rigidbody _rgbd;

    private Transform _LookAtPlayer;



    void Awake()
    {
        GetComponentFunction();
        AvoidNullProb();
    }
    void Start () {
        GetComponentFunction();
        AvoidNullProb();
        Destroy(this.gameObject, 7f);
        if(_LookAtPlayer != null)
        {
            Vector3 _Dir = (_LookAtPlayer.position - this.transform.position).normalized;

           
            this.transform.forward = new Vector3(_Dir.x, 0f, _Dir.z);



            _rgbd.AddForce(this.transform.forward * Power);

        }
        else
        {
            Destroy(this.gameObject);
        }

	}
    void GetComponentFunction()
    {
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
    }
  void AvoidNullProb()
    {
        if (_LookAtPlayer == null)
            _LookAtPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        if (_rgbd == null)
            _rgbd = GetComponent<Rigidbody>();

    }

     void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("FrontHit"))
        {
            Destroy(this.gameObject);
        }
      if(other.gameObject.CompareTag("BackHit"))
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
