using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearObject : MonoBehaviour {




    public bool GameClear = false;





    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            GameClear = true;
            MessageReceived[] receivers = GameObject.FindObjectsOfType<Fungus.MessageReceived>();
            if(receivers != null)
            {
                foreach (var receiver in receivers)
                {
                    receiver.OnSendFungusMessage("GameClear");
                }
            }

           PlayerPrefs.SetInt("levelReach",3);



        }
    }
}
