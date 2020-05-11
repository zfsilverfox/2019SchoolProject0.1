using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheTeachPanel : MonoBehaviour
{


    public GameObject _ClearthePanel;

    public GameObject _PostionTeach;

    public GameObject _designMapSetActive;


public    float TimeStart = 0.0f;
    public float TimeLimit = 2.4f;

    public float TimeStart2 = 0.0f;
    public float TimeLimit2 = 3f;
	void ClearThePanel()
    {
        _ClearthePanel.SetActive(false);
    }
    void OpenThePanel()
    {
        _ClearthePanel.SetActive(true);
    }
    void MovingPositionisOnYouright()
    {

        _PostionTeach.SetActive(true);

     
    }
    void TheDesignOfTheMap()
    {
        if(_designMapSetActive != null)
        {
            _designMapSetActive.SetActive(true);
        }
        else if (_designMapSetActive == null)
        {
            Debug.LogError("This is something Strange , You need to Check Before ");
        }
    }
  void Update()
    {
        if (_PostionTeach.activeInHierarchy)
        {
            TimeStart += Time.deltaTime;

            if(TimeStart >= TimeLimit)
            {
                _PostionTeach.SetActive(false);
            }
        }
        if (_designMapSetActive.activeInHierarchy)
        {
            TimeStart2 += Time.deltaTime;

            if(TimeStart2 > TimeLimit2)
            {
                _designMapSetActive.SetActive(false);
            }
        }
    }
}
