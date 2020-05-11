using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class LevelSelecter : MonoBehaviour {

    public Button[] LevelSelecterBtn;
  void Start()
    {
     //  PlayerPrefs.DeleteAll();
        int levelReach = PlayerPrefs.GetInt("levelReach",1);
        for(int i =0; i < LevelSelecterBtn.Length; i++)
        {
            if(i+1 >levelReach)
            {
                LevelSelecterBtn[i].interactable = false;
            }
        }
    }
}
