using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//他の人のソースコードを参考にして、自身で修正を施した
public class MainCameraCtrl : MonoBehaviour {

    public GameObject _Player;

    public float offset;
    public float moveSpeed;
    public float offsetVertical;


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
        if(_Player == null)
        {
            _Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

  void Update()
    {
        if(_Player != null)
        {
        Vector3 newPosition = new Vector3(_Player.transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
        Vector3 newPosVertiOne = new Vector3(transform.position.x, transform.position.y, _Player.transform.position.z + offsetVertical);
        transform.position = Vector3.Lerp(transform.position, newPosVertiOne, moveSpeed * Time.deltaTime);
        }



       
    }




}
