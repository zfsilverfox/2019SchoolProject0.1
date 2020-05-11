using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraCtrlBOSS : MonoBehaviour {

    public GameObject _Player;

    public float offset;
    public float moveSpeed;
    public float offsetVertical;



    bool LockOn = false;


    [SerializeField]
    LockOnTargetDetect _lockOnTarget;



    [SerializeField] GameObject lockOnTarget;

    void Awake()
    {
        AvoidNullProblem();
    }


    void Start()
    {
        AvoidNullProblem();
    }

    void AvoidNullProblem()
    {
        if (_Player == null)
        {
            _Player = GameObject.FindGameObjectWithTag("Player");
        }
        if(_lockOnTarget == null)
        {
            _lockOnTarget = _Player.transform.GetChild(1).GetComponent<LockOnTargetDetect>();
        }


    }

    void Update()
    {
        if(_Player != null)
        {
            Vector3 originPos = new Vector3(1.615469f, 51f, 42.7577f);
            //   transform.position = new Vector3(1.615469f, 51f, 42.7577f);
            Vector3 newPosition = new Vector3(_Player.transform.position.x + offset, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
            Vector3 newPosVertiOne = new Vector3(transform.position.x, transform.position.y, _Player.transform.position.z + offsetVertical);
            transform.position = Vector3.Lerp(transform.position, newPosVertiOne, moveSpeed * Time.deltaTime);
        }
    }




   





   


}
