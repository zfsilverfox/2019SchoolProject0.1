using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDestroyObject : MonoBehaviour {


    float TimeRandomDestroy;



    // This is Used For The Health Item which is Destroyed By ItSelf 
	void Start () {

        TimeRandomDestroy = Random.Range(3f,5.4f);
        Destroy(this.gameObject, TimeRandomDestroy);
	}
	
	
}
