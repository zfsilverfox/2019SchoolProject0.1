using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorReceived : MonoBehaviour
{
    public GameObject _HitWeaponTriggerOn;
     void Hit()
    {
        _HitWeaponTriggerOn.SetActive(true);
       
    }
    void OffTrigger()
    {
        _HitWeaponTriggerOn.SetActive(false);
       
    }
}
