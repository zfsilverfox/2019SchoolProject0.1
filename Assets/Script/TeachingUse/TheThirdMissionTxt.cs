using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdMissionTxt : MonoBehaviour {

    public GameObject _TheThreetxt;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Has Enter the Three Area ");
            this.gameObject.SetActive(false);
            if (_TheThreetxt != null) _TheThreetxt.SetActive(true);
        }
    }





}
