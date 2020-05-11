using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIENEMYAnimationReceived : MonoBehaviour {
    public GameObject _HitTrigger;
  //  public GameObject _SwordEffect;


 void OpenDamageColliders()
    {
        _HitTrigger.SetActive(true);
       // _SwordEffect.SetActive(true);
    }

    void CloseDamageColliders()
    {
        _HitTrigger.SetActive(false);
       // _SwordEffect.SetActive(false);
    }

}
