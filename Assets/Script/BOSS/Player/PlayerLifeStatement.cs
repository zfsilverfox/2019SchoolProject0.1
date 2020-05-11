using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeStatement : MonoBehaviour {

    public  static PlayerLifeStatement _INSTANCE;


    [HideInInspector]
public    PlayerCtrl _PlayerCtrl;


    public int Health= 25;

    // Start Function
    // Mainly use Avoid Null Problem
    void Awake()
    {
        AvoidNullProblem();
    }
    // Start Function
    // Mainly use for Setting Basic Info Or Avoid Null Problem
    void Start()
    {
        AvoidNullProblem();
    }
    // Name : AvoidNullProblem 
    // Method : This Function mainly is use for defend Avoid Null Problem
    void AvoidNullProblem()
    {
        if(_INSTANCE == null)
        {
            _INSTANCE = this;
        }
        _PlayerCtrl = GetComponent<PlayerCtrl>();
    }
    private void Update()
    {
     //   Debug.Log("Health" + Health);
    }
}
