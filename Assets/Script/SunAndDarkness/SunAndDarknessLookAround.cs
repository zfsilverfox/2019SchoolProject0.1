using UnityEngine;



// This is The Function Which is Used For Turn Around 
public class SunAndDarknessLookAround : MonoBehaviour {

    [Range(1, 10)]
    public float TimeToChange = 5f;



    public GameObject _LookAtPosition;



   void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, TimeToChange * Time.deltaTime);
        if(_LookAtPosition == null)
        {
            transform.LookAt(Vector3.zero);
        }
        else
        {
            transform.LookAt(_LookAtPosition.transform.position);
        }
   
    }


}
