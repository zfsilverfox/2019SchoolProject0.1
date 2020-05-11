using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimationReceived : MonoBehaviour
{
    public GameObject _SwordTriggerOn;


    void FirstAttackColliderOn()
    {

        _SwordTriggerOn.SetActive(true);
    }

    void FirstColliderColliderOff()
    {
   
        _SwordTriggerOn.SetActive(false);
    }

    void AttackTwoColliderOn()
    {
     
        _SwordTriggerOn.SetActive(true);
    }


    void AttackColliderTwoOff()
    {
      
        _SwordTriggerOn.SetActive(false);
    }


    void AttackThreeColliderOn()
    {
     
        _SwordTriggerOn.SetActive(true);
    }

    void AttackThreeColliderOff()
    {
       
        _SwordTriggerOn.SetActive(false);
    }


}
