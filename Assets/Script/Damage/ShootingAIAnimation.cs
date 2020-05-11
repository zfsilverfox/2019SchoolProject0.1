using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAIAnimation : MonoBehaviour {

    public Transform _Gunposition;

    public GameObject _InstanGun;


     void Start()
    {
        if (_Gunposition == null) Debug.LogWarning("Please Added the position");
        if (_InstanGun == null) Debug.LogWarning("Please added the OBJECT ");
    }


    void Shoot()
    {
        Instantiate(_InstanGun, _Gunposition.position, Quaternion.identity);
    }


}
