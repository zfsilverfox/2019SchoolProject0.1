using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSoldierAnimatorReceived : MonoBehaviour {

    public GameObject _SwordTrigger;


    void OpenCollider()
    {
        Debug.Log("Open Collider ");
        _SwordTrigger.SetActive(true);
    }

    void CloseCollider()
    {
        Debug.Log("Close Collider");
        _SwordTrigger.SetActive(false);
    }




}
