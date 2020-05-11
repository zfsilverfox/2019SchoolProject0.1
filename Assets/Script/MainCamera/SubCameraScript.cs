using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraScript : MonoBehaviour {

    public GameObject _Player;

   public float Offset;


    public float OffSetX;


    public float Spd = 0.6f;

    private void Awake()
    {
        AvoidNullProblem();
    }
    private void Start()
    {
        AvoidNullProblem();
    }

    void AvoidNullProblem()
    {
        if (_Player == null) _Player = GameObject.FindGameObjectWithTag("Player");
    }
     void Update()
    {
        UpdateCameraProb();
    }
    void UpdateCameraProb()
    {
        if(_Player != null)
        {
            Vector3 newPosition = new Vector3(_Player.transform.position.x + OffSetX, transform.position.y,_Player.transform.position.z + Offset);
            transform.position = Vector3.Slerp(transform.position, newPosition, Spd * Time.deltaTime);
        }
       
    }
}
