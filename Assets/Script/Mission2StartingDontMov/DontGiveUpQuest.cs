using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGiveUpQuest : MonoBehaviour {

    bool HaveDoFungus = false;

     void OnTriggerEnter(Collider other)
    {

        if (this.gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {

            if (!HaveDoFungus)
            {
                MessageReceived[] receivers = GameObject.FindObjectsOfType<Fungus.MessageReceived>();
                if (receivers != null)
                {
                    foreach (var received in receivers)
                    {
                        received.OnSendFungusMessage("CallMission2");
                    }
                }
                    HaveDoFungus = true;




            }
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.activeInHierarchy && other.gameObject.CompareTag("Player"))
        {
            HaveDoFungus = false;
        }
    }



}
