using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTargetDetect : MonoBehaviour {


    [SerializeField]
    private GameObject _target;

    protected void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "BOSS")
        {
            _target = c.gameObject;
        }


    }

    protected void OnTriggerExit(Collider c)
    {
        if(c.gameObject.tag == "BOSS")
        {
            _target = null;
        }
    }
    public GameObject getTarget()
    {
        return this._target;
    }
}
