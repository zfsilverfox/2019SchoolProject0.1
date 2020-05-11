using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDamage : MonoBehaviour {

    private Vector3 _mTarget;

    private Vector3 _mScreen;




   public  string HIT = "HIT";


    public float ConstWeight = 100f;

    public float ConstHeight = 50f;




    public Color _color = Color.blue;


    private Vector2 _mPoint;

   

    public float FreeTime = 1.5f;






	void Start ()
    {

        _mTarget = transform.position;
        _mScreen = Camera.main.WorldToScreenPoint(_mTarget);


        _mPoint = new Vector2(_mScreen.x, Screen.height - _mScreen.y);


        FreeTime = Random.Range(0.8f, 1.4f);
        StartCoroutine("Free");
	}

	void Update ()
    {
        transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        _mTarget = transform.position;
        _mScreen = Camera.main.WorldToScreenPoint(_mTarget);
        _mPoint = new Vector2(_mScreen.x, Screen.height - _mScreen.y);
    }

     void OnGUI()
    {
        if(_mScreen.z > 0)
        {
            GUI.color = _color;
            GUI.Label(new Rect(_mPoint.x, _mPoint.y, ConstWeight, ConstHeight), HIT);
            
        }
    }



    IEnumerator Free()
    {
        yield return new WaitForSeconds(FreeTime);

        Destroy(this.gameObject); 


    }



}
