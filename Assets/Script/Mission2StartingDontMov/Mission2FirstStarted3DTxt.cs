using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2FirstStarted3DTxt : MonoBehaviour {


	void Start () {
        float DestroyTiming = Random.Range(5f, 7f);
        Destroy(this.gameObject, DestroyTiming);
	}
	
}
